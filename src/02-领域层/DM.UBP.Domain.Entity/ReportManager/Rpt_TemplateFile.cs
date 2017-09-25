//------------------------------------------------------------
// All Rights Reserved , Copyright (C)  
// 版本：1.0
/// <author>
///		<name></name>
///		<date>0001/1/1 0:00:00</date>
/// </author>
//------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.MultiTenancy;

namespace DM.UBP.Domain.Entity.ReportManager
{
    /// <summary>
    /// 报表模板的实体类
    /// <summary>
    public class ReportTemplate : FullAuditedEntity<long>, IMayHaveTenant
    {
        [StringLength(StringMaxLengthConst.MaxStringLength100)]
        public string TemplateName { get; set; }

        [StringLength(StringMaxLengthConst.MaxStringLength100)]
        public string FileName { get; set; }

        [StringLength(StringMaxLengthConst.MaxStringLength250)]
        public string FilePath { get; set; }

        [StringLength(StringMaxLengthConst.MaxStringLength2000)]
        public string Description { get; set; }

        public int? TenantId { get; set; }

    }
}
