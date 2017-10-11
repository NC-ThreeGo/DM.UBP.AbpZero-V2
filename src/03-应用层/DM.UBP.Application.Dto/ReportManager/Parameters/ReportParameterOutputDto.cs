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

namespace DM.UBP.Application.Dto.ReportManager.Parameters
{
    /// <summary>
    /// 报表参数的OutputDto
    /// <summary>
    [AutoMapFrom(typeof(ReportParameter))]
    public class ReportParameterOutputDto : FullAuditedEntityDto<long>
    {
        [Required]
        public long Template_Id { get; set; }

        [StringLength(StringMaxLengthConst.MaxStringLength100)]
        [Required]
        public string LabelName { get; set; }

        [StringLength(StringMaxLengthConst.MaxStringLength100)]
        [Required]
        public string ParameterName { get; set; }

        [Required]
        public int ParamterType { get; set; }

        [Required]
        public int UiType { get; set; }

        [StringLength(StringMaxLengthConst.MaxStringLength2000)]
        public string DynamicSql { get; set; }

        [Required]
        public int SortId { get; set; }

        public int? TenantId { get; set; }

        public bool IsEditMode
        {
            get { return Id > 0; }
        }
    }
}
