using Abp.Dependency;
using Abp.Domain.Uow;
using Abp.MultiTenancy;
using Abp.Zero.EntityFramework;
using DM.UBP.EF;

namespace DM.UBP.EF.OracleDevart
{
    /// <summary>
    /// AbpZero中对多租户的数据迁移
    /// </summary>
    //TODO：需要考虑如何支持多数据库类型，一种方法是通过配置文件指定要启用的具体实现类，然后在DM.UBP.EF中手工注册到IOC中（IAbpZeroDbMigrator）；
    public class AbpZeroDbMigratorForOracleDevart : AbpZeroDbMigrator<UbpDbContext, OracleDevartMigrationsConfiguration>
    {
        public AbpZeroDbMigratorForOracleDevart(
            IUnitOfWorkManager unitOfWorkManager,
            IDbPerTenantConnectionStringResolver connectionStringResolver,
            IIocResolver iocResolver) :
            base(
                unitOfWorkManager,
                connectionStringResolver,
                iocResolver)
        {

        }
    }
}
