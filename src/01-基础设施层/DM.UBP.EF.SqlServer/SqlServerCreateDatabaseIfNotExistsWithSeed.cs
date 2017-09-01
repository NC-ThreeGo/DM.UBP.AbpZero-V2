using DM.UBP.Domain.SeedAction;
using DM.UBP.EF.Migrations;

namespace DM.UBP.EF.SqlServer
{
    public class SqlServerCreateDatabaseIfNotExistsWithSeed : CreateDatabaseIfNotExistsWithSeedBase<UbpDbContext>
    {
        public SqlServerCreateDatabaseIfNotExistsWithSeed()
        {
            SeedActions.Add(new CreateDatabaseSeedAction());
        }
    }
}
