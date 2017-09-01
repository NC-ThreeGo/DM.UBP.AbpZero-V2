using Abp.Events.Bus;
using Abp.Events.Bus.Entities;
using Abp.MultiTenancy;
using Abp.Zero.EntityFramework;
using DM.UBP.Domain.SeedAction.SeedData.Host;
using DM.UBP.Domain.SeedAction.SeedData.Tenants;
using DM.UBP.EF;
using DM.UBP.EF.Migrations;
using EntityFramework.DynamicFilters;

namespace DM.UBP.Domain.SeedAction
{
    public class CreateDatabaseSeedAction : ISeedAction, IMultiTenantSeed
    {
        public AbpTenantBase Tenant { get; set; }

        #region Implementation of ISeedAction

        /// <summary>
        /// 获取 操作排序，数值越小越先执行
        /// </summary>
        public int Order { get { return 1; } }

        /// <summary>
        /// 定义种子数据初始化过程
        /// </summary>
        /// <param name="context">数据上下文</param>
        public void Action(UbpDbContext context)
        {
            context.DisableAllFilters();

            context.EntityChangeEventHelper = NullEntityChangeEventHelper.Instance;
            context.EventBus = NullEventBus.Instance;

            if (Tenant == null)
            {
                //Host seed
                new InitialHostDbBuilder(context).Create();

                //Default tenant seed (in host database).
                new DefaultTenantBuilder(context).Create();
                new TenantRoleAndUserBuilder(context, 1).Create();
            }
            else
            {
                //You can add seed for tenant databases using Tenant property...
            }

            context.SaveChanges();
        }

        #endregion
    }
}