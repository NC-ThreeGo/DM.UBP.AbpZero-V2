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
        public string Labelname { get; set; }

        [StringLength(StringMaxLengthConst.MaxStringLength100)]
        public string Parametername { get; set; }

        public int Paramtertype { get; set; }

        public int Uitype { get; set; }

        [StringLength(StringMaxLengthConst.MaxStringLength2000)]
        public string Dynamicsql { get; set; }

        public int Sortid { get; set; }

        public int? TenantId { get; set; }

    }
}
