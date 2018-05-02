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
using System;
using DM.UBP.Domain.Entity.BackgroundJobManager;

namespace DM.UBP.Application.Dto.BackgroundJobManager.Triggers
{
    /// <summary>
    /// 工作的InputDto
    /// <summary>
    [AutoMapTo(typeof(Trigger))]
    public class TriggerInputDto : EntityDto<long>
    {
        [Display(Name = "触发器名称")]
        [StringLength(StringMaxLengthConst.MaxStringLength50)]
        [Required]
        public string TriggerName { get; set; }

        [Display(Name = "Cron表达式")]
        [StringLength(StringMaxLengthConst.MaxStringLength50)]
        [Required]
        public string CronStr { get; set; }

        [Display(Name = "说明")]
        [StringLength(StringMaxLengthConst.MaxStringLength1000)]
        public string Description { get; set; }

    }
}
