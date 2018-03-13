//------------------------------------------------------------
// All Rights Reserved , Copyright (C)  
// 版本：1.0
/// <author>
///		<name></name>
///		<date>0001/1/1 0:00:00</date>
/// </author>
//------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using Abp.Application.Services.Dto;
using DM.UBP.Domain.Entity;
using DM.UBP.Domain.Entity.TriggerManager;
using System;

namespace DM.UBP.Application.Dto.TriggerManager.TriggerMonths
{
    /// <summary>
    /// 按月计算的触发器的OutputDto
    /// <summary>
    [AutoMapFrom(typeof(TriggerMonth))]
    public class TriggerMonthOutputDto : FullAuditedEntityDto
    {
        [Display(Name = "任务类型1表示报表")]
        [Required]
        public int TaskType { get; set; }

        [Display(Name = "间隔月数")]
        public int? Span { get; set; }

        [StringLength(StringMaxLengthConst.MaxStringLength100)]
        [Display(Name = "一年中包含的月份1-2 逗号隔开")]
        public string Include { get; set; }

        [StringLength(StringMaxLengthConst.MaxStringLength100)]
        [Display(Name = "一个月中包含的星期1-5 逗号隔开")]
        public string Weeks { get; set; }

        [StringLength(StringMaxLengthConst.MaxStringLength100)]
        [Display(Name = "一个星期包含的天数1-7 逗号隔开")]
        public string Week_Days { get; set; }

        [StringLength(StringMaxLengthConst.MaxStringLength100)]
        [Display(Name = "一个月中包含的天数1-31 逗号隔开")]
        public string Month_Days { get; set; }

        [Display(Name = "触发小时")]
        [Required]
        public int Hour { get; set; }

        [Display(Name = "触发分钟")]
        [Required]
        public int Min { get; set; }

        [Display(Name = "最后一次执行时间")]
        public DateTime? LastExtTime { get; set; }

        [Display(Name = "状态")]
        public bool? Status { get; set; }

        public bool IsEditMode
        {
            get { return Id > 0; }
        }
    }
}
