using Abp.Authorization;
using Abp.Configuration.Startup;
using Abp.Domain.Uow;
using Abp.Localization;
using DM.UBP.Authorization;
using DM.UBP.Domain.Service.BackgroundJobManager;
using DM.UBP.Domain.Service.ReportManager;
using DM.UBP.Domain.Service.ReportManager.Categories;
using DM.UBP.Domain.Service.ReportManager.Templates;
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
        private readonly IReportTemplateManager _reportTemplateManager;

        public UbpAuthorizationProvider(bool isMultiTenancyEnabled,
            IReportCategoryManager reportCategoryManager,
            IReportTemplateManager reportTemplateManager)
        {
            _isMultiTenancyEnabled = isMultiTenancyEnabled;
            _reportCategoryManager = reportCategoryManager;
            _reportTemplateManager = reportTemplateManager;
        }

        public UbpAuthorizationProvider(IMultiTenancyConfig multiTenancyConfig, 
            IReportCategoryManager reportCategoryManager,
            IReportTemplateManager reportTemplateManager)
        {
            _isMultiTenancyEnabled = multiTenancyConfig.IsEnabled;
            _reportCategoryManager = reportCategoryManager;
            _reportTemplateManager = reportTemplateManager;
        }

        [UnitOfWork]
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            var pages = context.GetPermissionOrNull(AppPermissions.Pages);

            if (pages != null)
            {
                var reports = pages.CreateChildPermission(AppPermissions_ReportManager.Pages_Reports, L("Reports"));

                var reportlCategories = _reportCategoryManager.GetAllCategoriesAsync().Result;
                reportlCategories.ForEach(category =>
                {
                    var report = reports.CreateChildPermission("Pages.ReportManager.Reports." + category.CategoryName, L(category.CategoryName));

                    var templates = _reportTemplateManager.GetAllReportTemplatesAsync().Result.Where(t => t.Category_Id == category.Id);

                    templates.OrderBy(t => t.Id).ToList().ForEach(template => {
                        report.CreateChildPermission("Pages.ReportManager.Reports." + category.CategoryName + "." + template.TemplateName, L(template.TemplateName));
                    });
                });

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

                var reportManagerDataSources = reportManager.CreateChildPermission(AppPermissions_ReportManager.Pages_ReportManager_DataSources, L("ReportManagerDataSources"));
                reportManagerDataSources.CreateChildPermission(AppPermissions_ReportManager.Pages_ReportManager_DataSources_Create, L("CreatingNewDataSource"));
                reportManagerDataSources.CreateChildPermission(AppPermissions_ReportManager.Pages_ReportManager_DataSources_Edit, L("EditingDataSource"));
                reportManagerDataSources.CreateChildPermission(AppPermissions_ReportManager.Pages_ReportManager_DataSources_Delete, L("DeletingDataSource"));

                var reportManagerParameters = reportManager.CreateChildPermission(AppPermissions_ReportManager.Pages_ReportManager_Parameters, L("ReportManagerParameters"));
                reportManagerParameters.CreateChildPermission(AppPermissions_ReportManager.Pages_ReportManager_Parameters_Create, L("CreatingNewParameter"));
                reportManagerParameters.CreateChildPermission(AppPermissions_ReportManager.Pages_ReportManager_Parameters_Edit, L("EditingParameter"));
                reportManagerParameters.CreateChildPermission(AppPermissions_ReportManager.Pages_ReportManager_Parameters_Delete, L("DeletingParameter"));


                var backgroundJobManager = pages.CreateChildPermission(AppPermissions_BackgroundJobManager.Pages_BackgroundJobManager, L("BackgroundJobManager"));

                var jobGroupManager = backgroundJobManager.CreateChildPermission(AppPermissions_BackgroundJobManager.Pages_BackgroundJobManager_JobGroups, L("JobGroupManager"));
                jobGroupManager.CreateChildPermission(AppPermissions_BackgroundJobManager.Pages_BackgroundJobManager_JobGroups_Create, L("CreatingNewJobGroup"));
                jobGroupManager.CreateChildPermission(AppPermissions_BackgroundJobManager.Pages_BackgroundJobManager_JobGroups_Edit, L("EditingJobGroup"));
                jobGroupManager.CreateChildPermission(AppPermissions_BackgroundJobManager.Pages_BackgroundJobManager_JobGroups_Delete, L("DeletingJobGroup"));

                var job_RPTEmailManager = backgroundJobManager.CreateChildPermission(AppPermissions_BackgroundJobManager.Pages_BackgroundJobManager_Job_RPTEmails, L("Job_RPTEmailManager"));
                job_RPTEmailManager.CreateChildPermission(AppPermissions_BackgroundJobManager.Pages_BackgroundJobManager_Job_RPTEmails_Create, L("CreatingNewJob_RPTEmail"));
                job_RPTEmailManager.CreateChildPermission(AppPermissions_BackgroundJobManager.Pages_BackgroundJobManager_Job_RPTEmails_Edit, L("EditingJob_RPTEmail"));
                job_RPTEmailManager.CreateChildPermission(AppPermissions_BackgroundJobManager.Pages_BackgroundJobManager_Job_RPTEmails_Delete, L("DeletingJob_RPTEmail"));

                var scheduleManager = backgroundJobManager.CreateChildPermission(AppPermissions_BackgroundJobManager.Pages_BackgroundJobManager_Schedulers, L("ScheduleManager"));
                scheduleManager.CreateChildPermission(AppPermissions_BackgroundJobManager.Pages_BackgroundJobManager_Schedulers_Create, L("CreatingNewScheduler"));
                scheduleManager.CreateChildPermission(AppPermissions_BackgroundJobManager.Pages_BackgroundJobManager_Schedulers_Edit, L("EditingScheduler"));
                scheduleManager.CreateChildPermission(AppPermissions_BackgroundJobManager.Pages_BackgroundJobManager_Schedulers_Delete, L("DeletingScheduler"));

                var triggerManager = backgroundJobManager.CreateChildPermission(AppPermissions_BackgroundJobManager.Pages_BackgroundJobManager_Triggers, L("TriggerManager"));
                triggerManager.CreateChildPermission(AppPermissions_BackgroundJobManager.Pages_BackgroundJobManager_Triggers_Create, L("CreatingNewTrigger"));
                triggerManager.CreateChildPermission(AppPermissions_BackgroundJobManager.Pages_BackgroundJobManager_Triggers_Edit, L("EditingTrigger"));
                triggerManager.CreateChildPermission(AppPermissions_BackgroundJobManager.Pages_BackgroundJobManager_Triggers_Delete, L("DeletingTrigger"));
            }
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, UBPConsts.LocalizationSourceName);
        }
    }
}
