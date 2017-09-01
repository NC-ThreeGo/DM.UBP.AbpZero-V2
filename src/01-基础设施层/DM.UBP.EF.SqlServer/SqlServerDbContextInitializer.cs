using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.UBP.EF.SqlServer
{
    public class SqlServerDbContextInitializer : DbContextInitializerBase<UbpDbContext>
    {
        public SqlServerDbContextInitializer()
        {
            DatabaseInitializer = new SqlServerCreateDatabaseIfNotExistsWithSeed();
            if (EnabledAutoMigrate)
            {
                DatabaseInitializer = new MigrateDatabaseToLatestVersion<UbpDbContext, SqlServerMigrationsConfiguration>();
            }
        }
    }
}
