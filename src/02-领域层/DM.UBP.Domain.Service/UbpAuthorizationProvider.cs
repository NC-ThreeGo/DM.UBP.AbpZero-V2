using Abp.Authorization;
using Abp.Configuration.Startup;
using Abp.Localization;
using Abp.MultiTenancy;
using DM.UBP.Authorization;
using DM.UBP.Domain.Service.ReportManage;

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
                var reportManage = pages.CreateChildPermission(AppPermissions_ReportManage.Pages_ReportManage, L("ReportManage"));

                var reportDesigner = reportManage.CreateChildPermission(AppPermissions_ReportManage.Pages_ReportManage_Designer, L("ReportManageDesigner"));
                reportDesigner.CreateChildPermission(AppPermissions_ReportManage.Pages_ReportManage_Designer_Create, L("CreatingNewReport"));
                reportDesigner.CreateChildPermission(AppPermissions_ReportManage.Pages_ReportManage_Designer_Edit, L("EditingReport"));
                reportDesigner.CreateChildPermission(AppPermissions_ReportManage.Pages_ReportManage_Designer_Delete, L("DeletingReport"));
            }
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, UBPConsts.LocalizationSourceName);
        }
    }
}
