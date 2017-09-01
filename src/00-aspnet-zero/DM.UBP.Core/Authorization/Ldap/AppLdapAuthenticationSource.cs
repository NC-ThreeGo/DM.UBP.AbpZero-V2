using Abp.Zero.Ldap.Authentication;
using Abp.Zero.Ldap.Configuration;
using DM.UBP.Authorization.Users;
using DM.UBP.MultiTenancy;

namespace DM.UBP.Authorization.Ldap
{
    public class AppLdapAuthenticationSource : LdapAuthenticationSource<Tenant, User>
    {
        public AppLdapAuthenticationSource(ILdapSettings settings, IAbpZeroLdapModuleConfig ldapModuleConfig)
            : base(settings, ldapModuleConfig)
        {
        }
    }
}
