using Abp.Configuration.Startup;
using Abp.Dependency;
using DM.UBP.Common.Config;

namespace DM.UBP.EF
{
    /// <summary>
    /// 定义数据库初始化器，从程序集中反射实体映射类并加载到相应上下文类中，进行上下文类型的初始化
    /// </summary>
    public interface IDatabaseInitializer : ISingletonDependency
    {
        /// <summary>
        /// 开始初始化数据库
        /// </summary>
        /// <param name="config">数据库上下文初始化配置信息</param>
        void Initialize(DbContextInitializerConfig config);
    }
}
