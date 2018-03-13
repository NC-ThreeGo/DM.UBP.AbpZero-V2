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

namespace DM.UBP.Domain.Entity.TriggerManager
{
    /// <summary>
    /// 按天计算的触发器的实体类
    /// <summary>
    public class TriggerDay : FullAuditedEntity, IMayHaveTenant
    {
        /// <summary>
        /// 任务类型1表示报表
        /// <summary>
        [Display(Name = "任务类型1表示报表")]
        public int TaskType { get; set; }

        /// <summary>
        /// 间隔天数
        /// <summary>
        [Display(Name = "间隔天数")]
        public int? Span { get; set; }

        /// <summary>
        /// 触发小时
        /// <summary>
        [Display(Name = "触发小时")]
        public int Hour { get; set; }

        /// <summary>
        /// 触发分钟
        /// <summary>
        [Display(Name = "触发分钟")]
        public int Min { get; set; }

        /// <summary>
        /// 最后一次执行时间
        /// <summary>
        [Display(Name = "最后一次执行时间")]
        public DateTime? LastExtTime { get; set; }

        /// <summary>
        /// 状态
        /// <summary>
        [Display(Name = "状态")]
        public bool? Status { get; set; }

        public int? TenantId { get; set; }

    }
}
