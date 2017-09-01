using Abp.MultiTenancy;
using Abp.Zero.EntityFramework;
using DM.UBP.EF.Migrations;
using System.Data.Entity.Migrations;

namespace DM.UBP.EF.AbpZeroDbMigrator
{
    public class OracleDevartMigrationsConfiguration : DbMigrationsConfiguration<UbpDbContext>
    {
        private const string ProviderName = "Devart.Data.Oracle";

        public OracleDevartMigrationsConfiguration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = typeof(UbpDbContext).FullName;

            SetSqlGenerator(ProviderName, new Devart.Data.Oracle.Entity.Migrations.OracleEntityMigrationSqlGenerator());

            InintDevartOracle();
        }

        private void InintDevartOracle()
        {
            Devart.Data.Oracle.OracleMonitor monitor = new Devart.Data.Oracle.OracleMonitor() { IsActive = true };
            Devart.Data.Oracle.Entity.Configuration.OracleEntityProviderConfig devartConfig = Devart.Data.Oracle.Entity.Configuration.OracleEntityProviderConfig.Instance;
            devartConfig.Workarounds.IgnoreSchemaName = true;
            devartConfig.Workarounds.DisableQuoting = true;
            devartConfig.CodeFirstOptions.UseDateTimeAsDate = true;
            devartConfig.CodeFirstOptions.UseNonLobStrings = true;
            devartConfig.CodeFirstOptions.UseNonUnicodeStrings = true;
            devartConfig.CodeFirstOptions.TruncateLongDefaultNames = true;
            //devartConfig.DatabaseScript.Column.MaxStringSize = Devart.Data.Oracle.Entity.Configuration.OracleMaxStringSize.Standard;
        }
    }
}
