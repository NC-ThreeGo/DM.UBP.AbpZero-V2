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

namespace DM.UBP.Application.Dto.TriggerManager.TriggerWeeks
{
    /// <summary>
    /// 按周计算的触发器的OutputDto
    /// <summary>
    [AutoMapFrom(typeof(TriggerWeek))]
    public class TriggerWeekOutputDto : FullAuditedEntityDto
    {
        [Display(Name = "任务类型1表示报表")]
        [Required]
        public int TaskType { get; set; }

        [Display(Name = "间隔周数")]
        public int? Span { get; set; }

        [StringLength(StringMaxLengthConst.MaxStringLength100)]
        [Display(Name = "包含1-5周 逗号隔开")]
        public string Include { get; set; }

        [StringLength(StringMaxLengthConst.MaxStringLength100)]
        [Display(Name = "包含天数 星期1-7 逗号隔开")]
        public string Days { get; set; }

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
