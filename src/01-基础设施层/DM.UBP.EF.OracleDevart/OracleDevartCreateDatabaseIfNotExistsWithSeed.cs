using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DM.UBP.EF.Migrations;
using DM.UBP.Domain.SeedAction;

namespace DM.UBP.EF.OracleDevart
{
    public class OracleDevartCreateDatabaseIfNotExistsWithSeed : CreateDatabaseIfNotExistsWithSeedBase<UbpDbContext>
    {
        public OracleDevartCreateDatabaseIfNotExistsWithSeed()
        {
            SeedActions.Add(new CreateDatabaseSeedAction());
        }
    }
}
