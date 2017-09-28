using Abp.Authorization;
using Abp.Configuration.Startup;
using Abp.Localization;
using DM.UBP.Authorization;
using DM.UBP.Domain.Service.ReportManager;
using DM.UBP.Domain.Service.ReportManager.Categories;
using System.Linq;

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
        private readonly IReportCategoryManager _reportCategoryManager;

        public UbpAuthorizationProvider(bool isMultiTenancyEnabled, IReportCategoryManager reportCategoryManager)
        {
            _isMultiTenancyEnabled = isMultiTenancyEnabled;
            _reportCategoryManager = reportCategoryManager;
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
                //var reports = pages.CreateChildPermission(AppPermissions_ReportManager.Pages_Reports, L("Reports"));

                //var reportlCategories = _reportCategoryManager.GetAllCategoriesAsync().Result;
                //reportlCategories.ForEach(category =>
                //{
                //    reports.CreateChildPermission(category.CategoryName, L(category.CategoryName));
                //});

                var reportManager = pages.CreateChildPermission(AppPermissions_ReportManager.Pages_ReportManager, L("ReportManager"));

                var reportManagerCategories = reportManager.CreateChildPermission(AppPermissions_ReportManager.Pages_ReportManager_Categories, L("ReportManagerCategories"));
                reportManagerCategories.CreateChildPermission(AppPermissions_ReportManager.Pages_ReportManager_Categories_Create, L("CreatingNewCategory"));
                reportManagerCategories.CreateChildPermission(AppPermissions_ReportManager.Pages_ReportManager_Categories_Edit, L("EditingCategory"));
                reportManagerCategories.CreateChildPermission(AppPermissions_ReportManager.Pages_ReportManager_Categories_Delete, L("DeletingCategory"));

                var reportManagerDesigner = reportManager.CreateChildPermission(AppPermissions_ReportManager.Pages_ReportManager_Designer, L("ReportManagerDesigner"));

                var reportManagerTemplateFiles = reportManager.CreateChildPermission(AppPermissions_ReportManager.Pages_ReportManager_Templates, L("ReportManagerTemplateFiles"));
                reportManagerTemplateFiles.CreateChildPermission(AppPermissions_ReportManager.Pages_ReportManager_Templates_Create, L("CreatingNewTemplate"));
                reportManagerTemplateFiles.CreateChildPermission(AppPermissions_ReportManager.Pages_ReportManager_Templates_Edit, L("EditingTemplate"));
                reportManagerTemplateFiles.CreateChildPermission(AppPermissions_ReportManager.Pages_ReportManager_Templates_Delete, L("DeletingTemplate"));
    
            }
        }

        public void SetPermissionsForReports(IPermissionDefinitionContext context)
        {
            var pages = context.GetPermissionOrNull(AppPermissions.Pages);
            if (pages != null)
            {
                var reports = pages.CreateChildPermission(AppPermissions_ReportManager.Pages_Reports, L("Reports"));

                var reportlCategories = _reportCategoryManager.GetAllCategoriesAsync().Result;
                reportlCategories.ForEach(category =>
                {
                    reports.CreateChildPermission(category.CategoryName, L(category.CategoryName));
                });
            }
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, UBPConsts.LocalizationSourceName);
        }
    }
}
