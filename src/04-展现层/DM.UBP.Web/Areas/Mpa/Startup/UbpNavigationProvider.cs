using Abp.Application.Navigation;
using Abp.Domain.Uow;
using Abp.Localization;
using DM.UBP.Authorization;
using DM.UBP.Domain.Service.BackgroundJobManager;
using DM.UBP.Domain.Service.ReportManager;
using DM.UBP.Domain.Service.ReportManager.Categories;
using DM.UBP.Web.Navigation;

namespace DM.UBP.Web.Areas.Mpa.Startup
{
    public class UbpNavigationProvider : NavigationProvider
    {
        private readonly IReportCategoryManager _reportCategoryManager;
        public UbpNavigationProvider(IReportCategoryManager reportCategoryManager)
        {
            _reportCategoryManager = reportCategoryManager;
        }

        [UnitOfWork]
        public override void SetNavigation(INavigationProviderContext context)
        {
            var menu = context.Manager.Menus[MpaNavigationProvider.MenuName];

            if (menu != null)
            {
                var reports = new MenuItemDefinition(
                   UbpMenu.Reports,
                   L("Reports"),
                   icon: "icon-docs",
                   requiredPermissionName: AppPermissions_ReportManager.Pages_Reports
                   );

                var categories = _reportCategoryManager.GetAllCategoriesAsync().Result;

                categories.ForEach(category =>
                {
                    reports.AddItem(new MenuItemDefinition(
                        UbpMenu.Reports + "." + category.CategoryName,
                        L(category.CategoryName),
                        url: "ReportManager/Previewer/ReportList?categoryId=" + category.Id,
                        icon: "icon-doc",
                        requiredPermissionName: "Pages.ReportManager.Reports." + category.CategoryName
                        ));
                });

                menu.AddItem(reports);

                menu.AddItem(new MenuItemDefinition(
                   UbpMenu.ReportManager,
                   L("ReportManager"),
                   icon: "icon-graph",
                   requiredPermissionName: AppPermissions_ReportManager.Pages_ReportManager
                   ).AddItem(new MenuItemDefinition(
                        UbpMenu.ReportManagerCategories,
                        L("ReportManagerCategories"),
                        url: "ReportManager/Category",
                        icon: "icon-grid",
                        requiredPermissionName: AppPermissions_ReportManager.Pages_ReportManager_Categories
                        )
                   ).AddItem(new MenuItemDefinition(
                        UbpMenu.ReportManagerTemplates,
                        L("ReportManagerTemplates"),
                        url: "ReportManager/Template",
                        icon: "icon-grid",
                        requiredPermissionName: AppPermissions_ReportManager.Pages_ReportManager_Templates
                        )
                   )
               //.AddItem(new MenuItemDefinition(
               //     UbpMenu.ReportManagerDesigner,
               //     L("ReportManagerDesigner"),
               //     url: "ReportManager/Designer",
               //     icon: "icon-layers",
               //     requiredPermissionName: AppPermissions_ReportManager.Pages_ReportManager_Designer
               //     )
               //)
               );

                menu.AddItem(new MenuItemDefinition(
                   UbpMenu.BackgroundJobManager,
                   L("BackgroundJobManager"),
                   icon: "icon-calendar",
                   requiredPermissionName: AppPermissions_BackgroundJobManager.Pages_BackgroundJobManager
                   ).AddItem(new MenuItemDefinition(
                        UbpMenu.BackgroundJobManagerJobGroups,
                        L("JobGroupManager"),
                        url: "BackgroundJobManager/JobGroup",
                        icon: "icon-film",
                        requiredPermissionName: AppPermissions_BackgroundJobManager.Pages_BackgroundJobManager_JobGroups
                        )
                   ).AddItem(new MenuItemDefinition(
                        UbpMenu.BackgroundJobManagerJob_RPTEmails,
                        L("Job_RPTEmailManager"),
                        url: "BackgroundJobManager/Job_RPTEmail",
                        icon: "icon-directions",
                        requiredPermissionName: AppPermissions_BackgroundJobManager.Pages_BackgroundJobManager_Job_RPTEmails
                        )
                    ).AddItem(new MenuItemDefinition(
                        UbpMenu.BackgroundJobManagerSchedulers,
                        L("ScheduleManager"),
                        url: "BackgroundJobManager/Scheduler",
                        icon: "icon-bell",
                        requiredPermissionName: AppPermissions_BackgroundJobManager.Pages_BackgroundJobManager_Schedulers
                        )
                    ).AddItem(new MenuItemDefinition(
                        UbpMenu.BackgroundJobManagerTriggers,
                        L("TriggerManager"),
                        url: "BackgroundJobManager/Trigger",
                        icon: "icon-clock",
                        requiredPermissionName: AppPermissions_BackgroundJobManager.Pages_BackgroundJobManager_Triggers
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