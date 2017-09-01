using Abp.Domain.Services;

namespace DM.UBP
{
    public abstract class UBPDomainServiceBase : DomainService
    {
        /* Add your common members for all your domain services. */

        protected UBPDomainServiceBase()
        {
            LocalizationSourceName = UBPConsts.LocalizationSourceName;
        }
    }
}
