using System.Threading.Tasks;
using Abp.Application.Services;
using DM.UBP.Configuration.Tenants.Dto;

namespace DM.UBP.Configuration.Tenants
{
    public interface ITenantSettingsAppService : IApplicationService
    {
        Task<TenantSettingsEditDto> GetAllSettings();

        Task UpdateAllSettings(TenantSettingsEditDto input);

        Task ClearLogo();

        Task ClearCustomCss();
    }
}
