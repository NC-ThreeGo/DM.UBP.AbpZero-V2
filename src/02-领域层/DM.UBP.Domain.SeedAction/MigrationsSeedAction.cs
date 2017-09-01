using Abp.MultiTenancy;
using Abp.Zero.EntityFramework;
using DM.UBP.EF;
using DM.UBP.EF.Migrations;
using System.Data.Entity;

namespace DM.UBP.Domain.SeedAction
{
    public class MigrationsSeedAction : ISeedAction, IMultiTenantSeed
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
            //if (context.Set<Role>().Count(p => p.Name == "系统管理员") == 0)
            //    context.Set<Role>().AddOrUpdate(new Role() { Name = "系统管理员", Remark = "系统管理员角色，拥有系统最高权限", IsAdmin = true, IsSystem = true, CreatedTime = DateTime.Now });

            //if (context.Set<User>().Count(p => p.UserName == "admin") == 0)
            //    context.Set<User>().AddOrUpdate(new User()
            //    {
            //        UserName = "admin",
            //        NickName = "系统管理员",
            //        Email = "admin@threego.com",
            //        EmailConfirmed = false,
            //        PasswordHash = "AFJuxDmNkeA5Rg+djBwaDDhJFCEPC5fts9HtkV2zsu5q9L9OfPQ3sLmbIKJpGNlPCQ==",
            //        PhoneNumberConfirmed = false,
            //        TwoFactorEnabled = false,
            //        LockoutEnabled = true
            //    });


            //Host seed
            //new InitialHostDbBuilder(context).Create();

            //Default tenant seed (in host database).
            //new DefaultTenantCreator(context).Create();
            //new TenantRoleAndUserBuilder(context, 1).Create();
        }

        #endregion
    }
}
