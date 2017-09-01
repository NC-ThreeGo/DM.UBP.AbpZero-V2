using System.Threading.Tasks;
using Abp.Configuration;

namespace DM.UBP.Timing
{
    public interface ITimeZoneService
    {
        Task<string> GetDefaultTimezoneAsync(SettingScopes scope, int? tenantId);
    }
}
