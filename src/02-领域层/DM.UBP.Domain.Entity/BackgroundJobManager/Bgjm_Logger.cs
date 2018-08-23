//------------------------------------------------------------
// All Rights Reserved , Copyright (C)  
// 版本：1.0
/// <author>
///		<name></name>
///		<date>0001/1/1 0:00:00</date>
/// </author>
//------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.MultiTenancy;

namespace DM.UBP.Domain.Entity.BackgroundJobManager
{
    /// <summary>
    /// 后台任务系统日志的实体类
    /// <summary>
    public class Logger : FullAuditedEntity<long>, IMayHaveTenant
    {
        /// <summary>
        /// 任务类型
        /// <summary>
        [Display(Name = "任务类型")]
        [StringLength(StringMaxLengthConst.MaxStringLength50)]
        public string JobType { get; set; }

        /// <summary>
        /// 任务ID
        /// <summary>
        [Display(Name = "任务ID")]
        public long? Job_Id { get; set; }

        /// <summary>
        /// 任务名称
        /// <summary>
        [Display(Name = "任务名称")]
        [StringLength(StringMaxLengthConst.MaxStringLength50)]
        public string JobName { get; set; }

        /// <summary>
        /// 执行开始时间
        /// <summary>
        [Display(Name = "执行开始时间")]
        public DateTime? ExecStartTime { get; set; }

        /// <summary>
        /// 执行结束时间
        /// <summary>
        [Display(Name = "执行结束时间")]
        public DateTime? ExecEndTime { get; set; }

        /// <summary>
        /// 是否异常
        /// <summary>
        [Display(Name = "是否异常")]
        public bool? IsException { get; set; }

        /// <summary>
        /// 日志内容
        /// <summary>
        [Display(Name = "日志内容")]
        [StringLength(StringMaxLengthConst.MaxStringLength4000)]
        public string Note { get; set; }

        public int? TenantId { get; set; }

    }
}
