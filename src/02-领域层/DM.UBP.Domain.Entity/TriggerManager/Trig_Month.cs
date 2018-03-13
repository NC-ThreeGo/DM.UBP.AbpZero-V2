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
    /// 按月计算的触发器的实体类
    /// <summary>
    public class TriggerMonth : FullAuditedEntity, IMayHaveTenant
    {
        /// <summary>
        /// 任务类型1表示报表
        /// <summary>
        [Display(Name = "任务类型1表示报表")]
        public int TaskType { get; set; }

        /// <summary>
        /// 间隔月数
        /// <summary>
        [Display(Name = "间隔月数")]
        public int? Span { get; set; }

        /// <summary>
        /// 一年中包含的月份1-2 逗号隔开
        /// <summary>
        [Display(Name = "一年中包含的月份1-2 逗号隔开")]
        [StringLength(StringMaxLengthConst.MaxStringLength100)]
        public string Include { get; set; }

        /// <summary>
        /// 一个月中包含的星期1-5 逗号隔开
        /// <summary>
        [Display(Name = "一个月中包含的星期1-5 逗号隔开")]
        [StringLength(StringMaxLengthConst.MaxStringLength100)]
        public string Weeks { get; set; }

        /// <summary>
        /// 一个星期包含的天数1-7 逗号隔开
        /// <summary>
        [Display(Name = "一个星期包含的天数1-7 逗号隔开")]
        [StringLength(StringMaxLengthConst.MaxStringLength100)]
        public string Week_Days { get; set; }

        /// <summary>
        /// 一个月中包含的天数1-31 逗号隔开
        /// <summary>
        [Display(Name = "一个月中包含的天数1-31 逗号隔开")]
        [StringLength(StringMaxLengthConst.MaxStringLength100)]
        public string Month_Days { get; set; }

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
