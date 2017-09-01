using Abp.AutoMapper;
using DM.UBP.Authorization.Users;
using DM.UBP.Authorization.Users.Dto;
using DM.UBP.Web.Areas.Mpa.Models.Common;

namespace DM.UBP.Web.Areas.Mpa.Models.Users
{
    [AutoMapFrom(typeof(GetUserPermissionsForEditOutput))]
    public class UserPermissionsEditViewModel : GetUserPermissionsForEditOutput, IPermissionsEditViewModel
    {
        public User User { get; private set; }

        public UserPermissionsEditViewModel(GetUserPermissionsForEditOutput output, User user)
        {
            User = user;
            output.MapTo(this);
        }
    }
}