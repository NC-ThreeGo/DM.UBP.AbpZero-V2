using System.Data.Common;
using System.Data.Entity;
using Abp.Zero.EntityFramework;
using DM.UBP.Authorization.Roles;
using DM.UBP.Authorization.Users;
using DM.UBP.Chat;
using DM.UBP.Friendships;
using DM.UBP.MultiTenancy;
using DM.UBP.Storage;

namespace DM.UBP.EntityFramework
{
    /* Constructors of this DbContext is important and each one has it's own use case.
     * - Default constructor is used by EF tooling on design time.
     * - constructor(nameOrConnectionString) is used by ABP on runtime.
     * - constructor(existingConnection) is used by unit tests.
     * - constructor(existingConnection,contextOwnsConnection) can be used by ABP if DbContextEfTransactionStrategy is used.
     * See http://www.aspnetboilerplate.com/Pages/Documents/EntityFramework-Integration for more.
     */

    public class UBPDbContext : AbpZeroDbContext<Tenant, Role, User>
    {
        /* Define an IDbSet for each entity of the application */

        public virtual IDbSet<BinaryObject> BinaryObjects { get; set; }

        public virtual IDbSet<Friendship> Friendships { get; set; }

        public virtual IDbSet<ChatMessage> ChatMessages { get; set; }

        public UBPDbContext()
            : base("Default")
        {
            
        }

        public UBPDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        public UBPDbContext(DbConnection existingConnection)
           : base(existingConnection, false)
        {

        }

        public UBPDbContext(DbConnection existingConnection, bool contextOwnsConnection)
            : base(existingConnection, contextOwnsConnection)
        {

        }
    }
}
