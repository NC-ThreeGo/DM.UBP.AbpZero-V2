using System.Collections.Generic;
using Abp.Application.Services.Dto;
using DM.UBP.Configuration.Tenants.Dto;

namespace DM.UBP.Web.Areas.Mpa.Models.Settings
{
    public class SettingsViewModel
    {
        public TenantSettingsEditDto Settings { get; set; }
        
        public List<ComboboxItemDto> TimezoneItems { get; set; }
    }
}