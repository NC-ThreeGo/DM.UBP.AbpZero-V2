using DM.UBP.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.UBP.Application.Dto.ReportManager.Templates
{
    public class ReportTemplatesFilterDto : PagedAndSortedInputDto
    {
        public string Filter { get; set; }
        public long? Category { get; set; }
    }
}
