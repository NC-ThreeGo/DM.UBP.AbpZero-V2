using Abp.Application.Services;
using DM.UBP.Tenants.Dashboard.Dto;

namespace DM.UBP.Tenants.Dashboard
{
    public interface ITenantDashboardAppService : IApplicationService
    {
        GetMemberActivityOutput GetMemberActivity();
    }
}
