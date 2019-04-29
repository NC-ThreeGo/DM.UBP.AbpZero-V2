using Abp.Application.Navigation;
using Abp.Domain.Uow;
using Abp.Localization;
using DM.UBP.Domain.Service.BackgroundJobManager;
using DM.UBP.Domain.Service.ReportManager;
using DM.UBP.Domain.Service.ReportManager.Categories;
using DM.UBP.Domain.Service.WeiXinManager;
using DM.UBP.Domain.Service.WeiXinManager.WeiXinApps;
using DM.UBP.Web.Navigation;

namespace DM.UBP.Web.Areas.Mpa.Startup
{
    public class UbpNavigationProvider : NavigationProvider
    {
        private readonly IReportCategoryManager _reportCategoryManager;
        private readonly IWeiXinAppManager _weiXinAppManager;
        public UbpNavigationProvider(IReportCategoryManager reportCategoryManager,
            IWeiXinAppManager weiXinAppManager)
        {
            _reportCategoryManager = reportCategoryManager;
            _weiXinAppManager = weiXinAppManager;
        }

        [UnitOfWork]
        public override void SetNavigation(INavigationProviderContext context)
        {
            var menu = context.Manager.Menus[MpaNavigationProvider.MenuName];

            if (menu != null)
            {
                #region 报表导航
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

                reports.AddItem(new MenuItemDefinition(
                        UbpMenu.ReportsPBIReports,
                        L("PBIReports"),
                        url: "ReportManager/Previewer/PBIReportList",
                        icon: "icon-doc",
                        requiredPermissionName: AppPermissions_ReportManager.Pages_ReportManager_Reports_PBIReports
                        ));

                menu.AddItem(reports);
                #endregion

                #region 报表管理导航
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
               );
                #endregion

                #region 后台任务管理导航
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
                        UbpMenu.BackgroundJobManagerJob_Sql,
                        L("Job_SqlManager"),
                        url: "BackgroundJobManager/Job_Sql",
                        icon: "icon-bag",
                        requiredPermissionName: AppPermissions_BackgroundJobManager.Pages_BackgroundJobManager_Job_Sql
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
                    ).AddItem(new MenuItemDefinition(
                        UbpMenu.BackgroundJobManagerLoggers,
                        L("LoggerManager"),
                        url: "BackgroundJobManager/Logger",
                        icon: "icon-note",
                        requiredPermissionName: AppPermissions_BackgroundJobManager.Pages_BackgroundJobManager_Loggers
                        )
                    )
                );
                #endregion

                #region 企业微信管理导航
                //icon-share 
                menu.AddItem(new MenuItemDefinition(
                   UbpMenu.WeiXinManager,
                   L("WeiXinManager"),
                   icon: "icon-bubble",
                   requiredPermissionName: AppPermissions_WeiXinManager.Pages_WeiXinManager
                   ).AddItem(new MenuItemDefinition(
                        UbpMenu.WeiXinManager_WeiXinConfigs,
                        L("WeiXinManager_Config"),
                        url: "WeiXinManager/WeiXinConfig",
                        icon: "icon-wrench",
                        requiredPermissionName: AppPermissions_WeiXinManager.Pages_WeiXinManager_WeiXinConfigs
                        )
                   ).AddItem(new MenuItemDefinition(
                        UbpMenu.WeiXinAppManager,
                        L("WeiXinManager_App"),
                        url: "WeiXinManager/WeiXinApp",
                        icon: "icon-social-dropbox",
                        requiredPermissionName: AppPermissions_WeiXinManager.Pages_WeiXinManager_WeiXinApps
                        )
                   )
               );

                #endregion
            }
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, UBPConsts.LocalizationSourceName);
        }
    }
}