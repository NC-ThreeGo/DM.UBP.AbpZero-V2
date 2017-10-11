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
    /// 报表参数的实体类
    /// <summary>
    public class ReportParameter : FullAuditedEntity<long>, IMayHaveTenant
    {
        public long Template_Id { get; set; }

        [StringLength(StringMaxLengthConst.MaxStringLength100)]
        public string LabelName { get; set; }

        [StringLength(StringMaxLengthConst.MaxStringLength100)]
        public string ParameterName { get; set; }

        public int ParamterType { get; set; }

        public int UiType { get; set; }

        [StringLength(StringMaxLengthConst.MaxStringLength2000)]
        public string DynamicSql { get; set; }

        public int SortId { get; set; }

        public int? TenantId { get; set; }

    }
}
