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

namespace DM.UBP.Application.Dto.BackgroundJobManager.JobGroups
{
    /// <summary>
    /// 工作组的InputDto
    /// <summary>
    [AutoMapTo(typeof(JobGroup))]
    public class JobGroupInputDto : EntityDto<long>
    {
        [Display(Name = "组名称")]
        [StringLength(StringMaxLengthConst.MaxStringLength50)]
        [Required]
        public string JobGroupName { get; set; }

        [Display(Name = "程序集名称")]
        [StringLength(StringMaxLengthConst.MaxStringLength50)]
        [Required]
        public string AssemblyName { get; set; }

        [Display(Name = "类名称")]
        [StringLength(StringMaxLengthConst.MaxStringLength50)]
        [Required]
        public string ClassName { get; set; }

        [Display(Name = "说明")]
        [StringLength(StringMaxLengthConst.MaxStringLength1000)]
        public string Description { get; set; }

        [Display(Name = "组类型表")]
        [StringLength(StringMaxLengthConst.MaxStringLength50)]
        [Required]
        public string TypeTable { get; set; }

    }
}
