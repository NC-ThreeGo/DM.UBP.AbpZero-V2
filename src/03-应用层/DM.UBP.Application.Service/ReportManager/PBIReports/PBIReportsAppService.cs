using Abp.Application.Services.Dto;
using Abp.Authorization;
using DM.Common.Security;
using DM.UBP.Application.Dto.ReportManager.PBIReports;
using DM.UBP.Domain.Service.ReportManager;
using DM.UBP.Dto;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DM.UBP.Application.Service.ReportManager.PBIReports
{
    [AbpAuthorize(AppPermissions_ReportManager.Pages_ReportManager_Reports_PBIReports)]
    public class PBIReportsAppService : IPBIReportsAppService
    {
        public PagedResultDto<PBIReportOutputDto> GetPBIReportList(PagedAndSortedInputDto input)
        {
            string pbiApi = ConfigurationManager.AppSettings["PBIReportsApi"];
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(pbiApi);
            request.ContentType = "application/json";

            string adminUserName = ConfigurationManager.AppSettings["AdminUserName"];
            string adminPassWord = ConfigurationManager.AppSettings["AdminPassWord"];
            request.Credentials = new NetworkCredential(adminUserName, adminPassWord);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream receiveStream = response.GetResponseStream();
            StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
            var result = readStream.ReadToEnd();
            response.Close();
            readStream.Close();

            var entities = JsonConvert.DeserializeObject<PBIReportResultDto>(result);

            return new PagedResultDto<PBIReportOutputDto>(
            entities.Value.Count(),
            entities.Value
            );
        }

        public string GetPowerBIUrl(string userName, string pbiName)
        {
            string url = ConfigurationManager.AppSettings["PBIReportsUrl"];

            JwtTokenUtils jwtTokenUtils = new JwtTokenUtils();
            string token = jwtTokenUtils.GenerateJwtToken(userName, "", "pbirs");

            return url + pbiName + "?rs:embed=true&token=" + token;
        }
    }
}
