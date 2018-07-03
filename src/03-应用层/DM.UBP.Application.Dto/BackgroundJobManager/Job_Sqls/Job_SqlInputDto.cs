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

namespace DM.UBP.Application.Dto.BackgroundJobManager.Job_Sqls
{
    /// <summary>
    /// SQL任务的InputDto
    /// <summary>
    [AutoMapTo(typeof(Job_Sql))]
    public class Job_SqlInputDto : EntityDto<long>
    {
        [Display(Name = "任务名称")]
        [StringLength(StringMaxLengthConst.MaxStringLength50)]
        [Required]
        public string Job_SqlName { get; set; }

        [Display(Name = "任务组ID")]
        [Required]
        public long BGJM_JobGroup_Id { get; set; }

        /// <summary>
        /// SQL链接字符串
        /// <summary>
        [Display(Name = "SQL链接字符串")]
        [Required]
        public string ConnkeyName { get; set; }

        [Display(Name = "SQL类型")]
        [Required]
        public int CommandType { get; set; }

        [Display(Name = "SQL脚本")]
        [StringLength(StringMaxLengthConst.MaxStringLength2000)]
        [Required]
        public string CommandText { get; set; }

        [Display(Name = "SQL参数")]
        [StringLength(StringMaxLengthConst.MaxStringLength1000)]
        public string Paramters { get; set; }

        [Display(Name = "表述")]
        [StringLength(StringMaxLengthConst.MaxStringLength1000)]
        public string Description { get; set; }

    }
}
