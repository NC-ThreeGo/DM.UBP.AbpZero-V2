using Abp.WebApi.Controllers;

namespace DM.UBP.WebApi
{
    public abstract class UBPApiControllerBase : AbpApiController
    {
        protected UBPApiControllerBase()
        {
            LocalizationSourceName = UBPConsts.LocalizationSourceName;
        }
    }
}