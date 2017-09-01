using Abp.Localization;
using DM.UBP.EF;

namespace DM.UBP.Domain.EntityConfiguration.OracleDevart.AbpZero.Localization
{
    public class ApplicationLanguageTextConfiguration : EntityConfigurationBase<ApplicationLanguageText, long>
    {
        public ApplicationLanguageTextConfiguration()
        {
            Property(j => j.Value)
                .HasMaxLength(4000);
        }
    }
}
