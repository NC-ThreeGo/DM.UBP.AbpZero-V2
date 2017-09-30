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
    /// 报表数据源的实体类
    /// <summary>
    public class ReportDataSource : FullAuditedEntity<long>, IMayHaveTenant
    {
        public long Template_Id { get; set; }

        [StringLength(StringMaxLengthConst.MaxStringLength100)]
        public string Connkeyname { get; set; }

        [StringLength(StringMaxLengthConst.MaxStringLength100)]
        public string Tablename { get; set; }

        public int Commandtype { get; set; }

        [StringLength(StringMaxLengthConst.MaxStringLength2000)]
        public string Commandtext { get; set; }

        public int? TenantId { get; set; }

    }
}
