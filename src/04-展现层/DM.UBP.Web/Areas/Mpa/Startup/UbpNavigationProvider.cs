using Abp.Application.Navigation;
using Abp.Localization;
using DM.UBP.Authorization;
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
                   UbpMenu.ReportManage,
                   L("ReportManage"),
                   url: "Mpa/ReportManage",
                   icon: "icon-globe",
                   requiredPermissionName: AppPermissions_ReportManage.Pages_ReportManage
                   )
               );
            }
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, UbpDomainServiceConsts.LocalizationSourceName);
        }
    }
}