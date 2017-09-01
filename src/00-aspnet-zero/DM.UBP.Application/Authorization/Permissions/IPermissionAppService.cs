using Abp.Application.Services;
using Abp.Application.Services.Dto;
using DM.UBP.Authorization.Permissions.Dto;

namespace DM.UBP.Authorization.Permissions
{
    public interface IPermissionAppService : IApplicationService
    {
        ListResultDto<FlatPermissionWithLevelDto> GetAllPermissions();
    }
}
