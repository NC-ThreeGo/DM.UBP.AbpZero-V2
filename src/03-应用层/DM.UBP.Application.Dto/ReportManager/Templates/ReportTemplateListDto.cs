using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.UBP.Application.Dto.ReportManager.Templates
{
    public class ReportTemplateListDto: EntityDto<long>
    {
        public string CategoryName { get; set; }
        public string TemplateName { get; set; }
        public string Description { get; set; }
    }
}
