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
using DM.UBP.Domain.Entity.ReportManager;

namespace DM.UBP.Application.Dto.ReportManager.Templates
{
    /// <summary>
    /// 报表模板的InputDto
    /// <summary>
    [AutoMapTo(typeof(ReportTemplate))]
    public class ReportTemplateInputDto : EntityDto<long>
    {
        [StringLength(StringMaxLengthConst.MaxStringLength100)]
        [Required]
        public string TemplateName { get; set; }

        [StringLength(StringMaxLengthConst.MaxStringLength100)]
        [Required]
        public string FileName { get; set; }

        [StringLength(StringMaxLengthConst.MaxStringLength250)]
        [Required]
        public string FilePath { get; set; }

        [StringLength(StringMaxLengthConst.MaxStringLength2000)]
        public string Description { get; set; }

    }
}
