using Abp.Configuration.Startup;
using Abp.Dependency;
using Abp.EntityFramework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DM.Common.Extensions;
using DM.UBP.EF.Migrations;
using DM.UBP.EF.Properties;

namespace DM.UBP.EF
{
    /// <summary>
    /// 数据上下文初始化基类
    /// </summary>
    public abstract class DbContextInitializerBase<TDbContext>
        : DbContextInitializerBase
        where TDbContext : AbpDbContext, new()
    {
        /// <summary>
        /// 初始化一个<see cref="DbContextInitializerBase"/>类型的新实例
        /// </summary>
        protected DbContextInitializerBase()
        {
            DatabaseInitializer = new CreateDatabaseIfNotExists<TDbContext>();

            if (EnabledAutoMigrate)
            {
                DatabaseInitializer = new MigrateDatabaseToLatestVersion<TDbContext, AutoMigrationsConfiguration<TDbContext>>();
            }
        }

        /// <summary>
        /// 获取或设置 设置数据库初始化。
        ///     一般有两种情况：创建初始化：CreateDatabaseIfNotExists
        ///                     自动迁移：MigrateDatabaseToLatestVersion
        /// </summary>
        public IDatabaseInitializer<TDbContext> DatabaseInitializer { get; set; }

        /// <summary>
        /// 获取或设置 数据迁移策略，默认为自动迁移
        /// </summary>
        public IDatabaseInitializer<TDbContext> MigrateInitializer { get; set; }

        /// <summary>
        /// 重写以筛选出当前上下文的实体映射信息
        ///     由于不像OSharp框架那样支持多个DbContext，所以暂时不用。
        /// </summary>
        protected override IEnumerable<IEntityMapper> EntityMappersFilter(IEnumerable<IEntityMapper> entityMappers)
        {
            //Type contextType = typeof(TDbContext);
            //Expression<Func<IEntityMapper, bool>> predicate = m => m.DbContextType == contextType;
            //if (contextType == typeof(DefaultDbContext))
            //{
            //    predicate = predicate.Or(m => m.DbContextType == null);
            //}
            //return entityMappers.Where(predicate.Compile());

            return entityMappers;
        }

        /// <summary>
        /// 数据库初始化实现，设置数据库初始化策略，并进行EntityFramework的预热
        /// </summary>
        protected override void ContextInitialize()
        {
            #region 由于AbpDbContext启用了RegisterToChanges事件，所以不能使用context.Database.Exists()语句判断数据库是否存在。
            /*
            TDbContext context = new TDbContext();
            IDatabaseInitializer<TDbContext> initializer;
            if (!context.Database.Exists())
            {
                initializer = CreateDatabaseInitializer;
            }
            else
            {
                initializer = MigrateInitializer;
            }
            Database.SetInitializer(initializer);
            */
            #endregion

            //如果开发模式是先建表、再通过代码生成器生成代码（即不启用CodeFirst），则必须使用Database.SetInitializer<TDbContext>(null)
            //      这样就可以先在数据库中建好表，再通过代码生成器生成相关代码。
            if (!EnabledCodeFirst)
            {
                Database.SetInitializer<TDbContext>(null);
            }
            else
            {
                Database.SetInitializer(DatabaseInitializer);
            }


            #region EF预热，为集合中的单个容器映射创建（范围、生成的视图）字典，提高EF的性能
            TDbContext context = new TDbContext();
            //TDbContext context = (TDbContext)Activator.CreateInstance(TDbContext.GetType, ""); ;
            ObjectContext objectContext = ((IObjectContextAdapter)context).ObjectContext;
            StorageMappingItemCollection mappingItemCollection = (StorageMappingItemCollection)objectContext.ObjectStateManager
                .MetadataWorkspace.GetItemCollection(DataSpace.CSSpace);
            mappingItemCollection.GenerateViews(new List<EdmSchemaError>());
            context.Dispose();
            #endregion
        }
    }

    
    /// <summary>
    /// 数据库初始化基类
    /// </summary>
    public abstract class DbContextInitializerBase
    {
        /// <summary>
        /// 初始化一个<see cref="DbContextInitializerBase"/>类型的新实例
        /// </summary>
        protected DbContextInitializerBase()
        {
            MapperAssemblies = new List<Assembly>();
            EntityMappers = new ReadOnlyDictionary<Type, IEntityMapper>(new Dictionary<Type, IEntityMapper>());

            AbpConfiguration = IocManager.Instance.Resolve<IAbpStartupConfiguration>();

            //读取配置文件中的EnabledCodeFirst
            if (ConfigurationManager.AppSettings["EnabledCodeFirst"] != null)
            {
                EnabledCodeFirst = Boolean.Parse(ConfigurationManager.AppSettings["EnabledCodeFirst"]);
            }

            //读取配置文件中的EnabledAutoMigrate
            if (ConfigurationManager.AppSettings["EnabledAutoMigrate"] != null)
            {
                EnabledAutoMigrate = Boolean.Parse(ConfigurationManager.AppSettings["EnabledAutoMigrate"]);
            }
        }


        /// <summary>
        /// 获取或设置 实体映射程序集
        /// </summary>
        public ICollection<Assembly> MapperAssemblies { get; private set; }

        /// <summary>
        /// 获取 当前上下文的实体映射信息集合
        /// </summary>
        public IReadOnlyDictionary<Type, IEntityMapper> EntityMappers { get; private set; }

        /// <summary>
        /// 是否启用CodeFirst自动生成数据库表。
        ///     如果不启用，则开发模式是先建表、再通过代码生成器生成代码。
        ///     如果启用，则通过Database.SetInitializer自动创建数据库对象（可以通过EnabledAutoMigrate设置是否启用自动迁移）。
        /// </summary>
        protected bool EnabledCodeFirst { get; set; }

        /// <summary>
        /// 是否启用自动迁移，如果启用则使用MigrateDatabaseToLatestVersion，如果不启用则使用CreateDatabaseIfNotExists
        /// </summary>
        public bool EnabledAutoMigrate { get; set; }

        /// <summary>
        /// 执行数据上下文初始化
        /// </summary>
        public void Initialize()
        {
            EntityMappersInitialize();
            ContextInitialize();
        }

        /// <summary>
        /// 初始化实体映射类型
        /// </summary>
        private void EntityMappersInitialize()
        {
            if (MapperAssemblies.Count == 0)
            {
                throw new InvalidOperationException(Resources.DBInitializerBase_MapperAssembliesIsEmpty.FormatWith(this.GetType().FullName));
            }
            Type baseType = typeof(IEntityMapper);
            Type[] mapperTypes = MapperAssemblies.SelectMany(assembly => assembly.GetTypes())
                .Where(type => baseType.IsAssignableFrom(type) && type != baseType && !type.IsAbstract).ToArray();
            IEnumerable<IEntityMapper> entityMappers = mapperTypes.Select(type => Activator.CreateInstance(type) as IEntityMapper).ToList();
            entityMappers = EntityMappersFilter(entityMappers);
            IDictionary<Type, IEntityMapper> dict = new Dictionary<Type, IEntityMapper>();
            foreach (IEntityMapper mapper in entityMappers)
            {
                Type baseMapperType = mapper.GetType().BaseType;
                if (baseMapperType == null)
                {
                    continue;
                }
                Type entityType = baseMapperType.GetGenericArguments().FirstOrDefault();
                if (entityType == null || dict.ContainsKey(entityType))
                {
                    continue;
                }
                dict[entityType] = mapper;
            }
            EntityMappers = new ReadOnlyDictionary<Type, IEntityMapper>(dict);
        }

        /// <summary>
        /// 重写以筛选出当前上下文的实体映射信息
        /// </summary>
        protected abstract IEnumerable<IEntityMapper> EntityMappersFilter(IEnumerable<IEntityMapper> entityMappers);

        /// <summary>
        /// 数据库初始化实现，设置数据库初始化策略，并进行EntityFramework的预热
        /// </summary>
        protected abstract void ContextInitialize();

        /// <summary>
        /// 通过AbpStartupConfiguration对Abp框架进行一些设置。
        ///     不同的数据库实现时，有可能需要对AbpStartupConfiguration进行一些设置，所以增加这个属性一遍实现类调用。
        ///     具体来说OracleDevart，不支持默认的ReadUncommitted事物级别，需要修改成ReadCommitted。
        /// </summary>
        /// <param name="abpConfiguration"></param>
        public IAbpStartupConfiguration AbpConfiguration { get; set; }
    }
}
