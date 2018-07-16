using Abp.Dependency;
using Abp.Domain.Uow;
using Abp.Quartz.Quartz;
using Devart.Data.Oracle;
using DM.Common.Extensions;
using DM.UBP.Application.Dto.ReportManager;
using DM.UBP.Common.DbHelper;
using DM.UBP.Domain.Service.BackgroundJobManager.Job_Sqls;
using Quartz;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DM.UBP.Application.Quartz.Jobs
{
    public class SQLJob : JobBase, ITransientDependency
    {
        private readonly IJob_SqlManager _Job_SqlManager;
        public SQLJob(IJob_SqlManager job_sqlmanager)
        {
            _Job_SqlManager = job_sqlmanager;
        }

        [UnitOfWork]
        public override void Execute(IJobExecutionContext context)
        {
            JobDataMap jobDM = context.JobDetail.JobDataMap;

            string connkeyName = jobDM["connkeyName"] == null ? string.Empty : jobDM["connkeyName"].ToString();
            string commandType = jobDM["commandType"] == null ? string.Empty : jobDM["commandType"].ToString();
            string commandText = jobDM["commandText"] == null ? string.Empty : jobDM["commandText"].ToString();
            string paramters = jobDM["paramters"] == null ? string.Empty : jobDM["paramters"].ToString();

            //Thread.Sleep(999);

            var dicParameter = paramters.ToObject<Dictionary<string, string>>();

            OracleParameter[] paras = new OracleParameter[dicParameter == null ? 0 : dicParameter.Count()];

            if (dicParameter != null)
            {
                int i = 0;
                foreach (var p in dicParameter)
                {
                    string pName = p.Key;
                    string pValue = p.Value.Split(',')[0];
                    int pType = p.Value.Split(',')[1] == null ? 0 : Convert.ToInt32((p.Value.Split(',')[1]));

                    #region 判断类型
                    switch (pType)
                    {
                        case (int)ReportDefine.ParamterTypes.字符型:
                            paras[i] = new OracleParameter
                            {
                                ParameterName = pName,
                                Value = pValue
                            };
                            break;
                        case (int)ReportDefine.ParamterTypes.整型:
                            paras[i] = new OracleParameter
                            {
                                ParameterName = pName,
                                Value = Convert.ToInt32(pValue)
                            };
                            break;
                        case (int)ReportDefine.ParamterTypes.浮点型:
                            paras[i] = new OracleParameter
                            {
                                ParameterName = pName,
                                Value = Convert.ToDecimal(pValue)
                            };
                            break;
                        case (int)ReportDefine.ParamterTypes.日期型:
                            paras[i] = new OracleParameter
                            {
                                ParameterName = pName,
                                Value = pValue == "#sysDate#" ? DateTime.Now : Convert.ToDateTime(pValue)
                            };
                            break;
                        case (int)ReportDefine.ParamterTypes.布尔型:
                            paras[i] = new OracleParameter
                            {
                                ParameterName = pName,
                                Value = Convert.ToBoolean(pValue)
                            };
                            break;
                        case (int)ReportDefine.ParamterTypes.Guid型:
                            paras[i] = new OracleParameter
                            {
                                ParameterName = pName,
                                Value = new Guid(pValue)
                            };
                            break;
                        default:
                            paras[i] = new OracleParameter
                            {
                                ParameterName = pName,
                                Value = pValue
                            };
                            break;
                    }
                    #endregion

                    i++;
                }
            }
            string conn = ConfigurationManager.ConnectionStrings[connkeyName].ConnectionString;
            OracleDbHelper.ExecuteNonQuery(
                conn,
                commandText,
                commandType == "1" ? System.Data.CommandType.Text : System.Data.CommandType.StoredProcedure,
                paras);
        }
    }
}
