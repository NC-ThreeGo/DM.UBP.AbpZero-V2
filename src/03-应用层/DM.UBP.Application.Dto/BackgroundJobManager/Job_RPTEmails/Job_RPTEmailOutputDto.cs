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

namespace DM.UBP.Application.Dto.BackgroundJobManager.Job_RPTEmails
{
    /// <summary>
    /// 工作的OutputDto
    /// <summary>
    [AutoMapFrom(typeof(Job_RPTEmail))]
    public class Job_RPTEmailOutputDto : FullAuditedEntityDto<long>
    {
        [Display(Name = "工作名称")]
        [StringLength(StringMaxLengthConst.MaxStringLength10)]
        [Required]
        public string Job_RPTEmailName { get; set; }

        [Display(Name = "工作组ID")]
        [Required]
        public long BGJM_JobGroup_Id { get; set; }

        [StringLength(StringMaxLengthConst.MaxStringLength1000)]
        [Required]
        public string Emails { get; set; }

        [Display(Name = "报表模板ID")]
        [Required]
        public long Template_Id { get; set; }

        [Display(Name = "报表参数")]
        [StringLength(StringMaxLengthConst.MaxStringLength1000)]
        public string Parameters { get; set; }

        [Display(Name = "说明")]
        [StringLength(StringMaxLengthConst.MaxStringLength1000)]
        public string Description { get; set; }

        public bool IsEditMode
        {
            get { return Id > 0; }
        }
    }
}
