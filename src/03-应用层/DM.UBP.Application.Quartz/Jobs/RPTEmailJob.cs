using Abp.Configuration;
using Abp.Dependency;
using Abp.Domain.Uow;
using Abp.Net.Mail;
using Abp.Quartz.Quartz;
using Abp.Runtime.Security;
using Devart.Data.Oracle;
using DM.Common.Extensions;
using DM.UBP.Common.DbHelper;
using DM.UBP.Configuration.Host.Dto;
using DM.UBP.Domain.Service.ReportManager.DataSources;
using DM.UBP.Domain.Service.ReportManager.Parameters;
using DM.UBP.Domain.Service.ReportManager.Templates;
using FastReport.Web;
using Quartz;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace DM.UBP.Application.Quartz.Jobs
{
    public class RPTEmailJob : JobBase, ITransientDependency
    {
        private readonly IReportDataSourceManager _ReportDataSourceManager;
        private readonly IReportTemplateManager _ReportTemplateManager;
        private readonly IReportParameterManager _ReportParameterManager;

        public RPTEmailJob(IReportDataSourceManager reportdatasourcemanager,
           IReportTemplateManager reporttemplatemanager,
           IReportParameterManager reportParameterManager)
        {
            _ReportTemplateManager = reporttemplatemanager;
            _ReportDataSourceManager = reportdatasourcemanager;
            _ReportParameterManager = reportParameterManager;
        }

        [UnitOfWork]
        public override void Execute(IJobExecutionContext context)
        {
            JobDataMap jobDM = context.JobDetail.JobDataMap;
            long rptId = Convert.ToInt64(jobDM["rptId"]);
            string emails = jobDM["emails"].ToString();
            string parameters = jobDM["parameters"].ToString();
            string job_RPTEmailName = jobDM["job_RPTEmailName"].ToString();

            var pathList = ExportReport(rptId, parameters, job_RPTEmailName);
            SendEmail(DateTime.Now.ToString("yyyy-MM-dd") + job_RPTEmailName, "", emails, pathList);

        }

        private List<string> ExportReport(long rptId, string parameters,string job_RPTEmailName)
        {
            var tempalte = _ReportTemplateManager.GetReportTemplateByIdAsync(rptId).Result;

            WebReport _webReport = new WebReport();
            _webReport.Report.Load(System.AppDomain.CurrentDomain.BaseDirectory + tempalte.FilePath);
            var dicParameter = parameters.ToObject<Dictionary<string, string>>();

            //var listDs = _DataSourceAppService.GetDataSource(jobId, dicParameter).Result;
            var listDs = GetDataSource(rptId, dicParameter).Result;

            foreach (var ds in listDs)
            {
                _webReport.Report.RegisterData(ds, ds.DataSetName);
            }

            _webReport.Width = Unit.Percentage(100);
            _webReport.Height = Unit.Percentage(100);
            #region 导出
            //_webReport.Export();
            _webReport.Report.Prepare();
            string fileName = "ExportReport\\" + DateTime.Now.ToString("yyyyMMddHHmmss") + job_RPTEmailName;


            string csvFile = System.AppDomain.CurrentDomain.BaseDirectory + fileName + ".csv";
            using (FastReport.Export.Csv.CSVExport csv = new FastReport.Export.Csv.CSVExport())
            {
                csv.Export(_webReport.Report, csvFile);
            }
            string pdfFile = System.AppDomain.CurrentDomain.BaseDirectory + fileName + ".pdf";
            using (FastReport.Export.Pdf.PDFExport pdf = new FastReport.Export.Pdf.PDFExport())
            {
                pdf.Export(_webReport.Report, pdfFile);
            }

            List<string> pathList = new List<string>() { pdfFile, csvFile };
            return pathList;


            ///FTP上传
            //FastReport.Cloud.StorageClient.Ftp.FtpStorageClient ftp = new FastReport.Cloud.StorageClient.Ftp.FtpStorageClient();
            //ftp.Server = "10.50.239.55";
            //ftp.Username = "administrator";
            //ftp.Password = "123@abc";
            //ftp.SaveReport(_webReport.Report, pdf);
            //ftp.SaveReport(_webReport.Report, csv);
            #endregion
        }

        private void SendEmail(string subject, string content, string emails, List<string> fileList)
        {
            var emailSetting = GetEmailSettingsAsync().Result;

            //smtp客户端
            SmtpClient smtp = new SmtpClient(emailSetting.SmtpHost);

            //发件人邮箱身份验证凭证。 参数分别为 发件邮箱登录名和密码  
            smtp.Credentials = new NetworkCredential(emailSetting.SmtpUserName, emailSetting.SmtpPassword);

            //创建邮件
            MailMessage mail = new MailMessage();

            //主题编码  
            mail.SubjectEncoding = Encoding.GetEncoding("GB2312");

            //正文编码  
            mail.BodyEncoding = Encoding.GetEncoding("GB2312");

            //邮件优先级
            mail.Priority = MailPriority.Normal;

            //以HTML格式发送邮件,为false则发送纯文本邮箱
            mail.IsBodyHtml = false;

            //发件人邮箱  
            mail.From = new MailAddress(emailSetting.DefaultFromAddress);

            foreach (string eamil in emails.Split(';'))
            {
                if (string.IsNullOrEmpty(eamil))
                    continue;
                mail.To.Add(eamil);
            }

            //邮件主题和内容
            mail.Subject = subject;
            mail.Body = content;

            //定义附件,参数为附件文件名,包含路径,推荐使用绝对路径  
            foreach (string path in fileList)
            {
                if (string.IsNullOrEmpty(path))
                    continue;
                System.Net.Mail.Attachment objFile = new System.Net.Mail.Attachment(path);
                //objFile.Name = skey;
                mail.Attachments.Add(objFile);
            }

            //发送邮件
            smtp.Send(mail);
        }

        private async Task<EmailSettingsEditDto> GetEmailSettingsAsync()
        {
            var smtpPassword = await SettingManager.GetSettingValueAsync(EmailSettingNames.Smtp.Password);

            return new EmailSettingsEditDto
            {
                DefaultFromAddress = await SettingManager.GetSettingValueAsync(EmailSettingNames.DefaultFromAddress),
                DefaultFromDisplayName = await SettingManager.GetSettingValueAsync(EmailSettingNames.DefaultFromDisplayName),
                SmtpHost = await SettingManager.GetSettingValueAsync(EmailSettingNames.Smtp.Host),
                SmtpPort = await SettingManager.GetSettingValueAsync<int>(EmailSettingNames.Smtp.Port),
                SmtpUserName = await SettingManager.GetSettingValueAsync(EmailSettingNames.Smtp.UserName),
                SmtpPassword = SimpleStringCipher.Instance.Decrypt(smtpPassword),
                SmtpDomain = await SettingManager.GetSettingValueAsync(EmailSettingNames.Smtp.Domain),
                SmtpEnableSsl = await SettingManager.GetSettingValueAsync<bool>(EmailSettingNames.Smtp.EnableSsl),
                SmtpUseDefaultCredentials = await SettingManager.GetSettingValueAsync<bool>(EmailSettingNames.Smtp.UseDefaultCredentials)
            };
        }

        public async Task<List<DataSet>> GetDataSource(long template_Id, Dictionary<string, string> dicParameters)
        {
            var entityParameters = await _ReportParameterManager.GetAllReportParametersAsync();
            var parameters = entityParameters.Where(d => d.Template_Id == template_Id).OrderBy(d => d.Id);

            var entityDataSources = await _ReportDataSourceManager.GetAllReportDataSourcesAsync();
            var dataSources = entityDataSources.Where(d => d.Template_Id == template_Id).OrderBy(d => d.Id);

            List<DataSet> listDs = new List<DataSet>();
            foreach (var dataSource in dataSources)
            {
                string sql = dataSource.CommandText;
                string conn = ConfigurationManager.ConnectionStrings[dataSource.ConnkeyName].ConnectionString;

                //List<string> resultP = GetParamsBySql(sql);
                List<string> resultP = GetParamsByStr(dataSource.DataParams);

                OracleParameter[] paras = new OracleParameter[resultP.Count];
                for (int i = 0; i < resultP.Count; i++)
                {
                    var dataParam = parameters.Where(p => p.ParameterName.ToUpper() == resultP[i].ToUpper());
                    if (dataParam.Count() == 0)
                        continue;

                    string val = string.Empty;

                    if (dicParameters.Keys.Contains(resultP[i]))
                        val = dicParameters[resultP[i]];

                    #region 判断类型
                    switch (dataParam.First().ParamterType)
                    {
                        case 1:
                            paras[i] = new OracleParameter
                            {
                                ParameterName = resultP[i],
                                Value = val
                            };
                            break;
                        case 2:
                            paras[i] = new OracleParameter
                            {
                                ParameterName = resultP[i],
                                Value = Convert.ToInt32(val)
                            };
                            break;
                        case 3:
                            paras[i] = new OracleParameter
                            {
                                ParameterName = resultP[i],
                                Value = Convert.ToDecimal(val)
                            };
                            break;
                        case 4:
                            paras[i] = new OracleParameter
                            {
                                ParameterName = resultP[i],
                                Value = val == "#sysDate#" ? DateTime.Now : Convert.ToDateTime(val)
                            };
                            break;
                        case 5:
                            paras[i] = new OracleParameter
                            {
                                ParameterName = resultP[i],
                                Value = Convert.ToBoolean(val)
                            };
                            break;
                        case 6:
                            paras[i] = new OracleParameter
                            {
                                ParameterName = resultP[i],
                                Value = new Guid(val)
                            };
                            break;
                        default:
                            paras[i] = new OracleParameter
                            {
                                ParameterName = resultP[i],
                                Value = val
                            };
                            break;
                    }
                    #endregion

                }
                var dataSet = OracleDbHelper.ExecuteDataset(conn,
                    sql,
                    dataSource.CommandType == 1 ? System.Data.CommandType.Text : System.Data.CommandType.StoredProcedure,
                    paras);
                dataSet.Tables[0].TableName = "dt_" + dataSource.TableName;
                dataSet.DataSetName = "ds_" + dataSource.TableName;
                listDs.Add(dataSet);
            }
            return await Task.FromResult(listDs);
        }

        private List<string> GetParamsByStr(string str)
        {
            List<string> resilt = new List<string>();

            if (string.IsNullOrWhiteSpace(str))
                return resilt;

            string[] strArr = str.Split(',');

            foreach (var item in strArr)
            {
                if (string.IsNullOrWhiteSpace(item))
                    continue;
                resilt.Add(item);
            }
            return resilt;
        }
    }
}
