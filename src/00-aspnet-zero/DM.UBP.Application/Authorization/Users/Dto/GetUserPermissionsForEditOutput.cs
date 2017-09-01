using System.Collections.Generic;
using Abp.Application.Services.Dto;
using DM.UBP.Authorization.Permissions.Dto;

namespace DM.UBP.Authorization.Users.Dto
{
    public class GetUserPermissionsForEditOutput
    {
        public List<FlatPermissionDto> Permissions { get; set; }

        public List<string> GrantedPermissionNames { get; set; }
    }
}