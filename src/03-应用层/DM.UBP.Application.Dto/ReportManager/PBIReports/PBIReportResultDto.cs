using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.UBP.Application.Dto.ReportManager.PBIReports
{
    public class PBIReportResultDto
    {
        public string Context { get; set; }

        public List<PBIReportOutputDto> Value { get; set; }
    }
}
