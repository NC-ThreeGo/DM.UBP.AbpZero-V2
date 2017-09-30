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

namespace DM.UBP.Application.Dto.ReportManager.ReportDataSources
{
    /// <summary>
    /// 报表数据源的InputDto
    /// <summary>
    [AutoMapTo(typeof(ReportDataSource))]
    public class ReportDataSourceInputDto : EntityDto<long>
    {
        [Required]
        public long Template_Id { get; set; }

        [StringLength(StringMaxLengthConst.MaxStringLength100)]
        [Required]
        public string Connkeyname { get; set; }

        [StringLength(StringMaxLengthConst.MaxStringLength100)]
        [Required]
        public string Tablename { get; set; }

        [Required]
        public int Commandtype { get; set; }

        [StringLength(StringMaxLengthConst.MaxStringLength2000)]
        [Required]
        public string Commandtext { get; set; }

    }
}
