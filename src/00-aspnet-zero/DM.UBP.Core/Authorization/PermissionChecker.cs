using Abp.Authorization;
using DM.UBP.Authorization.Roles;
using DM.UBP.Authorization.Users;
using DM.UBP.MultiTenancy;

namespace DM.UBP.Authorization
{
    /// <summary>
    /// Implements <see cref="PermissionChecker"/>.
    /// </summary>
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {

        }
    }
}
