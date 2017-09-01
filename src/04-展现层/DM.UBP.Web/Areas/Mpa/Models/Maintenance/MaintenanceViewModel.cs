using System.Collections.Generic;
using DM.UBP.Caching.Dto;

namespace DM.UBP.Web.Areas.Mpa.Models.Maintenance
{
    public class MaintenanceViewModel
    {
        public IReadOnlyList<CacheDto> Caches { get; set; }
    }
}