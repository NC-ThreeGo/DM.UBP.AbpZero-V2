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
    /// 触发器的实体类
    /// <summary>
    public class Trigger : FullAuditedEntity<long>, IMayHaveTenant
    {
        /// <summary>
        /// 触发器名称
        /// <summary>
        [Display(Name = "触发器名称")]
        [StringLength(StringMaxLengthConst.MaxStringLength50)]
        public string TriggerName { get; set; }

        /// <summary>
        /// Cron表达式
        /// <summary>
        [Display(Name = "Cron表达式")]
        [StringLength(StringMaxLengthConst.MaxStringLength50)]
        public string CronStr { get; set; }

        public int? TenantId { get; set; }

        /// <summary>
        /// 说明
        /// <summary>
        [Display(Name = "说明")]
        [StringLength(StringMaxLengthConst.MaxStringLength1000)]
        public string Description { get; set; }

    }
}
