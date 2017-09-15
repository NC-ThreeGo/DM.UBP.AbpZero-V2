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

namespace DM.UBP.Application.Dto.ReportManager.Categories
{
    /// <summary>
    /// 报表分类的InputDto
    /// <summary>
    [AutoMapTo(typeof(ReportCategory))]
    public class ReportCategoryInputDto : EntityDto<long>
    {
        [StringLength(StringMaxLengthConst.MaxStringLength100)]
        [Required]
        public string Categoryname { get; set; }

        [Required]
        public long Parentid { get; set; }

        [StringLength(StringMaxLengthConst.MaxStringLength1000)]
        [Required]
        public string Code { get; set; }
    }
}
