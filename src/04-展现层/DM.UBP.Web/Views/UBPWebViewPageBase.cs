using Abp.Dependency;
using Abp.Runtime.Session;
using Abp.Web.Mvc.Views;

namespace DM.UBP.Web.Views
{
    public abstract class UBPWebViewPageBase : UBPWebViewPageBase<dynamic>
    {
       
    }

    public abstract class UBPWebViewPageBase<TModel> : AbpWebViewPage<TModel>
    {
        public IAbpSession AbpSession { get; private set; }
        
        protected UBPWebViewPageBase()
        {
            AbpSession = IocManager.Instance.Resolve<IAbpSession>();
            LocalizationSourceName = UBPConsts.LocalizationSourceName;
        }
    }
}