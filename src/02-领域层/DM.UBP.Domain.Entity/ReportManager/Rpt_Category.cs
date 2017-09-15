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
    /// 报表分类的实体类
    /// <summary>
    public class ReportCategory : FullAuditedEntity<long>, IMayHaveTenant
    {
        [StringLength(StringMaxLengthConst.MaxStringLength100)]
        public string CategoryName { get; set; }

        public long ParentId { get; set; }

        [StringLength(StringMaxLengthConst.MaxStringLength1000)]
        public string Code { get; set; }

        public int? TenantId { get; set; }

    }
}
