using System.Collections.Generic;
using DM.UBP.Authorization.Permissions.Dto;

namespace DM.UBP.Web.Areas.Mpa.Models.Common
{
    public interface IPermissionsEditViewModel
    {
        List<FlatPermissionDto> Permissions { get; set; }

        List<string> GrantedPermissionNames { get; set; }
    }
}