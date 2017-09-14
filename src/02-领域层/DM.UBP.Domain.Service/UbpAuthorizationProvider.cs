using Abp.Authorization;
using Abp.Configuration.Startup;
using Abp.Localization;
using Abp.MultiTenancy;
using DM.UBP.Authorization;
using DM.UBP.Domain.Service.ReportManager;

namespace DM.UBP.Domain.Service
{
    /// <summary>
    /// Application's authorization provider.
    /// Defines permissions for the application.
    /// See <see cref="AppPermissions"/> for all permission names.
    /// </summary>
    public class UbpAuthorizationProvider : AuthorizationProvider
    {
        private readonly bool _isMultiTenancyEnabled;

        public UbpAuthorizationProvider(bool isMultiTenancyEnabled)
        {
            _isMultiTenancyEnabled = isMultiTenancyEnabled;
        }

        public UbpAuthorizationProvider(IMultiTenancyConfig multiTenancyConfig)
        {
            _isMultiTenancyEnabled = multiTenancyConfig.IsEnabled;
        }

        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            var pages = context.GetPermissionOrNull(AppPermissions.Pages);

            if (pages != null)
            {
                var reportManager = pages.CreateChildPermission(AppPermissions_ReportManager.Pages_ReportManager, L("ReportManager"));

                var reportManagerCategories = reportManager.CreateChildPermission(AppPermissions_ReportManager.Pages_ReportManager_Categories, L("ReportManagerCategories"));
                reportManagerCategories.CreateChildPermission(AppPermissions_ReportManager.Pages_ReportManager_Categories_Create, L("CreatingNewCategory"));
                reportManagerCategories.CreateChildPermission(AppPermissions_ReportManager.Pages_ReportManager_Categories_Edit, L("EditingCategory"));
                reportManagerCategories.CreateChildPermission(AppPermissions_ReportManager.Pages_ReportManager_Categories_Delete, L("DeletingCategory"));
            }
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, UBPConsts.LocalizationSourceName);
        }
    }
}
