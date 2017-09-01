using Abp.Notifications;
using System;
using DM.UBP.EF;

namespace DM.UBP.Domain.EntityConfiguration.OracleDevart.AbpZero.Notification
{
    public class TenantNotificationConfiguration : EntityConfigurationBase<TenantNotificationInfo, Guid>
    {
        public TenantNotificationConfiguration()
        {
            Property(j => j.EntityTypeAssemblyQualifiedName)
                .HasColumnName("EntityTypeAssemblyQN");
            Property(j => j.Data)
                .HasMaxLength(4000);
        }
    }
}
