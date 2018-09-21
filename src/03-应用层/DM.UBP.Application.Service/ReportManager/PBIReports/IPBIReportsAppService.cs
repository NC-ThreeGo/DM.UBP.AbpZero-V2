using Abp.Application.Services;
using Abp.Application.Services.Dto;
using DM.UBP.Application.Dto.ReportManager.PBIReports;
using DM.UBP.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.UBP.Application.Service.ReportManager.PBIReports
{
    public interface IPBIReportsAppService: IApplicationService
    {
        PagedResultDto<PBIReportOutputDto> GetPBIReportList(PagedAndSortedInputDto input);

        string GetPowerBIUrl(string userName, string pbiName);
    }
}
