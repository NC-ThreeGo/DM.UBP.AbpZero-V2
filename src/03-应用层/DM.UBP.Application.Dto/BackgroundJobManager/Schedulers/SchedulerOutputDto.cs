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

namespace DM.UBP.Application.Dto.BackgroundJobManager.Schedulers
{
    /// <summary>
    /// 工作的OutputDto
    /// <summary>
    [AutoMapFrom(typeof(Scheduler))]
    public class SchedulerOutputDto : FullAuditedEntityDto<long>
    {
        [Display(Name = "调度名称")]
        [StringLength(StringMaxLengthConst.MaxStringLength50)]
        [Required]
        public string SchedulerName { get; set; }

        [Display(Name = "工作组ID")]
        [Required]
        public long JobGroup_Id { get; set; }

        [Display(Name = "工作ID")]
        [Required]
        public long Job_Id { get; set; }

        [Display(Name = "触发器ID")]
        [Required]
        public long Trigger_Id { get; set; }

        [Display(Name = "状态0停用，1启用")]
        [Required]
        public bool Status { get; set; }

        [Display(Name = "最后执行时间")]
        public DateTime? LastExtTime { get; set; }

        [Display(Name = "说明")]
        [StringLength(StringMaxLengthConst.MaxStringLength1000)]
        public string Description { get; set; }

        public bool IsEditMode
        {
            get { return Id > 0; }
        }
    }
}
