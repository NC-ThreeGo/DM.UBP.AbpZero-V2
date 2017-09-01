using System.Web.Mvc;
using Abp.Application.Navigation;
using Abp.Configuration;
using Abp.Configuration.Startup;
using Abp.Domain.Uow;
using Abp.Localization;
using Abp.MultiTenancy;
using Abp.Runtime.Session;
using Abp.Threading;
using DM.UBP.Configuration;
using DM.UBP.MultiTenancy;
using DM.UBP.Web.Models.Layout;
using DM.UBP.Web.MultiTenancy;
using DM.UBP.Web.Navigation;
using DM.UBP.Web.Session;

namespace DM.UBP.Web.Controllers
{
    /// <summary>
    /// Layout for 'front end' pages.
    /// </summary>
    public class LayoutController : UBPControllerBase
    {
        private readonly IPerRequestSessionCache _sessionCache;
        private readonly IUserNavigationManager _userNavigationManager;
        private readonly IMultiTenancyConfig _multiTenancyConfig;
        private readonly ILanguageManager _languageManager;
        private readonly ITenancyNameFinder _tenancyNameFinder;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly TenantManager _tenantManager;

        public LayoutController(
            IPerRequestSessionCache sessionCache,
            IUserNavigationManager userNavigationManager,
            IMultiTenancyConfig multiTenancyConfig,
            ILanguageManager languageManager,
            ITenancyNameFinder tenancyNameFinder,
            IUnitOfWorkManager unitOfWorkManager,
            TenantManager tenantManager)
        {
            _sessionCache = sessionCache;
            _userNavigationManager = userNavigationManager;
            _multiTenancyConfig = multiTenancyConfig;
            _languageManager = languageManager;
            _tenancyNameFinder = tenancyNameFinder;
            _unitOfWorkManager = unitOfWorkManager;
            _tenantManager = tenantManager;
        }

        [ChildActionOnly]
        public PartialViewResult Header(string currentPageName = "")
        {
            var headerModel = new HeaderViewModel();

            if (AbpSession.UserId.HasValue)
            {
                headerModel.LoginInformations = AsyncHelper.RunSync(() => _sessionCache.GetCurrentLoginInformationsAsync());
            }

            headerModel.Languages = _languageManager.GetLanguages();
            headerModel.CurrentLanguage = _languageManager.CurrentLanguage;

            headerModel.Menu = AsyncHelper.RunSync(() => _userNavigationManager.GetMenuAsync(FrontEndNavigationProvider.MenuName, AbpSession.ToUserIdentifier()));
            headerModel.CurrentPageName = currentPageName;

            headerModel.IsMultiTenancyEnabled = _multiTenancyConfig.IsEnabled;
            headerModel.TenantRegistrationEnabled = SettingManager.GetSettingValue<bool>(AppSettings.TenantManagement.AllowSelfRegistration);

            return PartialView("~/Views/Layout/_Header.cshtml", headerModel);
        }

        [ChildActionOnly]
        public PartialViewResult TenantCustomCss()
        {
            var tenancyName = _tenancyNameFinder.GetCurrentTenancyNameOrNull();
            if (string.IsNullOrEmpty(tenancyName))
            {
                if (AbpSession.UserId.HasValue)
                {
                    var sessionInfo = AsyncHelper.RunSync(() => _sessionCache.GetCurrentLoginInformationsAsync());
                    return PartialView("_TenantCustomCss", new TenantCustomCssViewModel
                    {
                        CustomCssId = sessionInfo.Tenant?.CustomCssId
                    });
                }

                return PartialView("_TenantCustomCss", new TenantCustomCssViewModel
                {
                    CustomCssId = null
                });
            }

            using (_unitOfWorkManager.Begin())
            {
                using (_unitOfWorkManager.Current.SetTenantId(null))
                {
                    var tenant = _tenantManager.FindByTenancyName(tenancyName);
                    return PartialView("_TenantCustomCss", new TenantCustomCssViewModel
                    {
                        CustomCssId = tenant?.CustomCssId
                    });
                }
            }
        }

        [ChildActionOnly]
        public PartialViewResult AppLogo(string appPath, int width = 168, int height = 33)
        {
            ViewBag.TenantCustomLogoWidth = width;
            ViewBag.TenantCustomLogoHeight = height;

            var tenancyName = _tenancyNameFinder.GetCurrentTenancyNameOrNull();
            if (string.IsNullOrEmpty(tenancyName))
            {
                if (AbpSession.TenantId.HasValue)
                {
                    using (_unitOfWorkManager.Begin())
                    {
                        using (_unitOfWorkManager.Current.SetTenantId(null))
                        {
                            var tenant = _tenantManager.FindById(AbpSession.GetTenantId());
                            SetLogoUrlForTenant(appPath, tenant);
                        }
                    }
                }
                else
                {
                    ViewBag.TenantCustomLogoUrl = appPath + "Common/Images/app-logo-on-light.png";
                }
            }
            else
            {
                using (_unitOfWorkManager.Begin())
                {
                    using (_unitOfWorkManager.Current.SetTenantId(null))
                    {
                        var tenant = _tenantManager.FindByTenancyName(tenancyName);
                        SetLogoUrlForTenant(appPath, tenant);
                    }
                }
            }

            return PartialView("~/Views/Account/_AppLogo.cshtml");
        }

        private void SetLogoUrlForTenant(string appPath, Tenant tenant)
        {
            ViewBag.TenantCustomLogoUrl = tenant != null && tenant.LogoId.HasValue
                ? appPath + "TenantCustomization/GetLogo"
                : appPath + "Common/Images/app-logo-on-light.png";
        }
    }
}