using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using DM.Common.Extensions;
using DM.UBP.Common.Properties;

namespace DM.UBP.Common.Config
{
    /// <summary>
    /// 数据上下文初始化配置
    /// </summary>
    public class DbContextInitializerConfig
    {
        public DbContextInitializerConfig()
        {
            EntityMapperAssemblies = new List<Assembly>();
        }

        /// <summary>
        /// 初始化一个<see cref="DbContextInitializerConfig"/>类型的新实例
        /// </summary>
        internal DbContextInitializerConfig(DbContextInitializerElement element)
        {
            Type type = Type.GetType(element.InitializerTypeName);
            if (type == null)
            {
                throw new InvalidOperationException(Resources.DbContextInitializerConfig_InitializerNotExists.FormatWith(element.InitializerTypeName));
            }
            InitializerType = type;

            ConnectionStringName = element.ConnectionStringName;

            string binPath = AppDomain.CurrentDomain.RelativeSearchPath ?? AppDomain.CurrentDomain.BaseDirectory;
            string[] mapperFiles = element.EntityMapperFiles.Split(',')
                .Select(fileName => fileName.EndsWith(".dll") ? fileName : fileName + ".dll")
                .Select(fileName => Path.Combine(binPath, fileName)).ToArray();
            EntityMapperAssemblies = mapperFiles.Select(Assembly.LoadFrom).ToList();
        }

        /// <summary>
        /// 获取或设置 数据上下文初始化类型
        /// </summary>
        public Type InitializerType { get; set; }

        /// <summary>
        /// 获取或设置 数据库连接串名称
        /// </summary>
        public string ConnectionStringName { get; set; }

        /// <summary>
        /// 获取或设置 实体映射类型所在程序集集合
        /// </summary>
        public ICollection<Assembly> EntityMapperAssemblies { get; set; }
    }
}
