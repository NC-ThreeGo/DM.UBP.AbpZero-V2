using DM.UBP.Authorization.Users;
using DM.UBP.EF;

namespace DM.UBP.Domain.EntityConfiguration.OracleDevart.AbpZero.Authorization
{
    public class UserConfiguration : EntityConfigurationBase<User, long>
    {
        public UserConfiguration()
        {
            Property(p => p.ShouldChangePasswordOnNextLogin).HasColumnName("ShouldChangePwdOnNextLogin");
        }
    }
}
