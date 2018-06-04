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
    /// 调度的实体类
    /// <summary>
    public class Scheduler : FullAuditedEntity<long>, IMayHaveTenant
    {
        /// <summary>
        /// 调度名称
        /// <summary>
        [Display(Name = "调度名称")]
        [StringLength(StringMaxLengthConst.MaxStringLength50)]
        public string SchedulerName { get; set; }

        /// <summary>
        /// 工作组ID
        /// <summary>
        [Display(Name = "工作组ID")]
        public long JobGroup_Id { get; set; }

        /// <summary>
        /// 工作ID
        /// <summary>
        [Display(Name = "工作ID")]
        public long Job_Id { get; set; }

        /// <summary>
        /// 触发器ID
        /// <summary>
        [Display(Name = "触发器ID")]
        public long Trigger_Id { get; set; }

        /// <summary>
        /// 状态0停用，1启用
        /// <summary>
        [Display(Name = "状态0停用，1启用")]
        public bool Status { get; set; }

        /// <summary>
        /// 最后执行时间
        /// <summary>
        [Display(Name = "最后执行时间")]
        public DateTime? LastExtTime { get; set; }

        public int? TenantId { get; set; }

        /// <summary>
        /// 说明
        /// <summary>
        [Display(Name = "说明")]
        [StringLength(StringMaxLengthConst.MaxStringLength1000)]
        public string Description { get; set; }

    }
}
