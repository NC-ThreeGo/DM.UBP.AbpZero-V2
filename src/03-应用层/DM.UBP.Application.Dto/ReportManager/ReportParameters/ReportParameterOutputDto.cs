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

namespace DM.UBP.Application.Dto.ReportManager.ReportParameters
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
        public string Labelname { get; set; }

        [StringLength(StringMaxLengthConst.MaxStringLength100)]
        [Required]
        public string Parametername { get; set; }

        [Required]
        public int Paramtertype { get; set; }

        [Required]
        public int Uitype { get; set; }

        [StringLength(StringMaxLengthConst.MaxStringLength2000)]
        public string Dynamicsql { get; set; }

        [Required]
        public int Sortid { get; set; }

        public bool IsEditMode
        {
            get { return Id > 0; }
        }
    }
}
