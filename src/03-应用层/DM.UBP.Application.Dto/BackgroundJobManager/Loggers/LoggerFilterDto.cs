using DM.UBP.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.UBP.Application.Dto.BackgroundJobManager.Loggers
{
    public class LoggerFilterDto: PagedAndSortedInputDto
    {
        public string Filter { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool? IsException { get; set; }

        public string JobType { get; set; }
    }
}
