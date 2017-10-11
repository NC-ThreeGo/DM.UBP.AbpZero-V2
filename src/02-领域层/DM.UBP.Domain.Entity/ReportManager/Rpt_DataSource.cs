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
        public string ConnkeyName { get; set; }

        [StringLength(StringMaxLengthConst.MaxStringLength100)]
        public string TableName { get; set; }

        public int CommandType { get; set; }

        [StringLength(StringMaxLengthConst.MaxStringLength2000)]
        public string CommandText { get; set; }

        public int? TenantId { get; set; }

    }
}
