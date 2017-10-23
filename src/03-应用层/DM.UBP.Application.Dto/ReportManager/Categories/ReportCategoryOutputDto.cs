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
    /// 报表分类的OutputDto
    /// <summary>
    [AutoMapFrom(typeof(ReportCategory))]
    public class ReportCategoryOutputDto : FullAuditedEntityDto<long>
    {

        [StringLength(StringMaxLengthConst.MaxStringLength100)]
        [Required]
        public string CategoryName { get; set; }

        public long ParentId { get; set; }

        [StringLength(StringMaxLengthConst.MaxStringLength1000)]
        public string Code { get; set; }

        public int? TenantId { get; set; }

        /// <summary>
        /// 是否处于修改状态，如果Id有值则表示修改否则表示新增。
        /// </summary>
        public bool IsEditMode
        {
            get { return Id > 0; }
        }

    }
}
