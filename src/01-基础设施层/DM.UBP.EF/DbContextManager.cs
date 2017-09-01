using System;
using System.Collections.Generic;

namespace DM.UBP.EF
{
    /// <summary>
    /// 数据上下文管理器
    /// </summary>
    public sealed class DbContextManager
    {
        private static readonly Lazy<DbContextManager> InstanceLazy = new Lazy<DbContextManager>(() => new DbContextManager());

        /// <summary>
        /// 上下文类型-上下文初始化类型
        /// </summary>
        private DbContextInitializerBase _contextInitializer;

        /// <summary>
        /// 获取 上下文管理器的唯一实例
        /// </summary>
        public static DbContextManager Instance { get { return InstanceLazy.Value; } }

        /// <summary>
        /// 注册上下文初始化器
        /// </summary>
        /// <param name="contextType">上下文类型</param>
        /// <param name="initializer">上下文初始化器</param>
        public void RegisterInitializer(DbContextInitializerBase initializer)
        {
            _contextInitializer = initializer;
            initializer.Initialize();
        }

        /// <summary>
        /// 获取 实体映射集合
        /// </summary>
        /// <returns>实体集合</returns>
        /// <exception cref="InvalidOperationException">如果<c>dbContextType</c>不存在则抛出<c>InvalidOperationException</c>异常</exception>
        public IEnumerable<IEntityMapper> GetEntityMappers()
        {
            if (_contextInitializer != null)
            {
                return _contextInitializer.EntityMappers.Values;
            }
            return new List<IEntityMapper>();
        }
    }
}
