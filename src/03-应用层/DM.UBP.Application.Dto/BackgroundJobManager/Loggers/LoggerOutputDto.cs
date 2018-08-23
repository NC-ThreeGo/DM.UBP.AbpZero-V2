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

namespace DM.UBP.Application.Dto.BackgroundJobManager.Loggers
{
    /// <summary>
    /// 后台任务系统日志的OutputDto
    /// <summary>
    [AutoMapFrom(typeof(Logger))]
    public class LoggerOutputDto : FullAuditedEntityDto<long>
    {
        [Display(Name = "任务类型")]
        [StringLength(StringMaxLengthConst.MaxStringLength50)]
        [Required]
        public string JobType { get; set; }

        [Display(Name = "任务ID")]
        [Required]
        public long? Job_Id { get; set; }

        [Display(Name = "任务名称")]
        [StringLength(StringMaxLengthConst.MaxStringLength50)]
        [Required]
        public string JobName { get; set; }

        [Display(Name = "执行开始时间")]
        [Required]
        public DateTime? ExecStartTime { get; set; }

        [Display(Name = "执行结束时间")]
        [Required]
        public DateTime? ExecEndTime { get; set; }

        [Display(Name = "是否异常")]
        [Required]
        public bool? IsException { get; set; }

        [Display(Name = "日志内容")]
        [StringLength(StringMaxLengthConst.MaxStringLength4000)]
        [Required]
        public string Note { get; set; }

        public bool IsEditMode
        {
            get { return Id > 0; }
        }
    }
}
