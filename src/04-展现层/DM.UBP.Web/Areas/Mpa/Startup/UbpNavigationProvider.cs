using Abp.Application.Navigation;
using Abp.Localization;
using DM.UBP.Authorization;
using DM.UBP.Domain.Service.ReportManager;
using DM.UBP.Web.Navigation;

namespace DM.UBP.Web.Areas.Mpa.Startup
{
    public class UbpNavigationProvider : NavigationProvider
    {
        public override void SetNavigation(INavigationProviderContext context)
        {
            var menu = context.Manager.Menus[MpaNavigationProvider.MenuName];

            if (menu != null)
            {
                menu.AddItem(new MenuItemDefinition(
                   UbpMenu.ReportManager,
                   L("ReportManager"),
                   icon: "icon-globe",
                   requiredPermissionName: AppPermissions_ReportManager.Pages_ReportManager
                   ).AddItem(new MenuItemDefinition(
                        UbpMenu.ReportManagerCategories,
                        L("ReportManagerCategories"),
                        url: "ReportManager/Category",
                        icon: "icon-layers",
                        requiredPermissionName: AppPermissions_ReportManager.Pages_ReportManager_Categories
                        )
                   )
                   .AddItem(new MenuItemDefinition(
                        UbpMenu.ReportManagerDesigner,
                        L("ReportManagerDesigner"),
                        url: "ReportManager/Designer",
                        icon: "icon-layers",
                        requiredPermissionName: AppPermissions_ReportManager.Pages_ReportManager_Designer
                        )
                   )
               );
            }
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, UBPConsts.LocalizationSourceName);
        }
    }
}