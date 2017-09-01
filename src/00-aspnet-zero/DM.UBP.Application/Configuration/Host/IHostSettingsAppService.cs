using System.Threading.Tasks;
using Abp.Application.Services;
using DM.UBP.Configuration.Host.Dto;

namespace DM.UBP.Configuration.Host
{
    public interface IHostSettingsAppService : IApplicationService
    {
        Task<HostSettingsEditDto> GetAllSettings();

        Task UpdateAllSettings(HostSettingsEditDto input);

        Task SendTestEmail(SendTestEmailInput input);
    }
}
