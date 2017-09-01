using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DM.Common.Extensions;
using DM.UBP.Common.Config;
using DM.UBP.EF.Properties;

namespace DM.UBP.EF
{
    /// <summary>
    /// 数据库初始化器，从程序集中反射实体映射类并加载到相应上下文类中，进行上下文类型的初始化
    /// </summary>
    public class DatabaseInitializer : IDatabaseInitializer
    {
        /// <summary>
        /// 获取或设置 实体映射程序集查找器
        ///     必须在配置文件中指定MapperAssembly，暂时不启用“生成默认配置文件”
        /// </summary>
        //public IEntityMapperAssemblyFinder MapperAssemblyFinder { get; set; }

        /// <summary>
        /// 开始初始化数据上下文
        /// </summary>
        /// <param name="config">数据库配置信息</param>
        public virtual void Initialize(DbContextInitializerConfig config)
        {
            DbContextInitializerBase initializer = CreateInitializer(config);
            DbContextManager.Instance.RegisterInitializer(initializer);
        }

        private static DbContextInitializerBase CreateInitializer(DbContextInitializerConfig config)
        {
            Type initializerType = config.InitializerType;
            DbContextInitializerBase initializer = Activator.CreateInstance(initializerType) as DbContextInitializerBase;
            if (initializer == null)
            {
                throw new InvalidOperationException(Resources.DatabaseInitializer_TypeNotDatabaseInitializer.FormatWith(initializerType));
            }
            foreach (Assembly mapperAssembly in config.EntityMapperAssemblies)
            {
                if (initializer.MapperAssemblies.Contains(mapperAssembly))
                {
                    continue;
                }
                initializer.MapperAssemblies.Add(mapperAssembly);
            }
            return initializer;
        }
    }
}
