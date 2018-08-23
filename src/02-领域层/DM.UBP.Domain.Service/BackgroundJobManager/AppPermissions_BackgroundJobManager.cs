using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.UBP.Domain.Service.BackgroundJobManager
{
    /// <summary>
    /// ReportManager的Permission定义
    /// <summary>
    public class AppPermissions_BackgroundJobManager
    {
        public const string Pages_BackgroundJobManager = "Pages.BackgroundJobManager";

        public const string Pages_BackgroundJobManager_JobGroups = "Pages.BackgroundJobManager.JobGroups";
        public const string Pages_BackgroundJobManager_JobGroups_Create = "Pages.BackgroundJobManager.JobGroups.Create";
        public const string Pages_BackgroundJobManager_JobGroups_Edit = "Pages.BackgroundJobManager.JobGroups.Edit";
        public const string Pages_BackgroundJobManager_JobGroups_Delete = "Pages.BackgroundJobManager.JobGroups.Delete";

        public const string Pages_BackgroundJobManager_Job_RPTEmails = "Pages.BackgroundJobManager.Job_RPTEmails";
        public const string Pages_BackgroundJobManager_Job_RPTEmails_Create = "Pages.BackgroundJobManager.Job_RPTEmails.Create";
        public const string Pages_BackgroundJobManager_Job_RPTEmails_Edit = "Pages.BackgroundJobManager.Job_RPTEmails.Edit";
        public const string Pages_BackgroundJobManager_Job_RPTEmails_Delete = "Pages.BackgroundJobManager.Job_RPTEmails.Delete";

        public const string Pages_BackgroundJobManager_Triggers = "Pages.BackgroundJobManager.Triggers";
        public const string Pages_BackgroundJobManager_Triggers_Create = "Pages.BackgroundJobManager.Triggers.Create";
        public const string Pages_BackgroundJobManager_Triggers_Edit = "Pages.BackgroundJobManager.Triggers.Edit";
        public const string Pages_BackgroundJobManager_Triggers_Delete = "Pages.BackgroundJobManager.Triggers.Delete";

        public const string Pages_BackgroundJobManager_Schedulers = "Pages.BackgroundJobManager.Schedulers";
        public const string Pages_BackgroundJobManager_Schedulers_Create = "Pages.BackgroundJobManager.Schedulers.Create";
        public const string Pages_BackgroundJobManager_Schedulers_Edit = "Pages.BackgroundJobManager.Schedulers.Edit";
        public const string Pages_BackgroundJobManager_Schedulers_Delete = "Pages.BackgroundJobManager.Schedulers.Delete";

        public const string Pages_BackgroundJobManager_Job_Sql = "Pages.BackgroundJobManager.Job_Sql";
        public const string Pages_BackgroundJobManager_Job_Sql_Create = "Pages.BackgroundJobManager.Job_Sql.Create";
        public const string Pages_BackgroundJobManager_Job_Sql_Edit = "Pages.BackgroundJobManager.Job_Sql.Edit";
        public const string Pages_BackgroundJobManager_Job_Sql_Delete = "Pages.BackgroundJobManager.Job_Sql.Delete";

        public const string Pages_BackgroundJobManager_Loggers = "Pages.BackgroundJobManager.Loggers";
        //日志不需要创建修改删除权限
        //public const string Pages_BackgroundJobManager_Loggers_Create = "Pages.BackgroundJobManager.Loggers.Create";
        //public const string Pages_BackgroundJobManager_Loggers_Edit = "Pages.BackgroundJobManager.Loggers.Edit";
        //public const string Pages_BackgroundJobManager_Loggers_Delete = "Pages.BackgroundJobManager.Loggers.Delete";
        //@@Insert Position




    }
}
