using System.Threading.Tasks;
using Abp.Application.Services;
using DM.UBP.Application.Dto.BaseManage.Permission.Roles;

namespace DM.UBP.Application.Service.BaseManage.Permission.Roles
{
    public interface IRoleAppService : IApplicationService
    {
        Task UpdateRolePermissions(UpdateRolePermissionsInput input);
    }
}
