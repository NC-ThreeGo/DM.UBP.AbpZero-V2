using Abp.Domain.Entities;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;

namespace DM.UBP.EF
{
    public abstract class UbpRepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<UbpDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected UbpRepositoryBase(IDbContextProvider<UbpDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //add common methods for all repositories
    }

    public abstract class UBPRepositoryBase<TEntity> : UbpRepositoryBase<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        protected UBPRepositoryBase(IDbContextProvider<UbpDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //do not add any method here, add to the class above (since this inherits it)
    }
}
