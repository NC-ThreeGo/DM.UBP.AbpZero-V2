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

namespace DM.UBP.Domain.Entity.BackgroundJobManager
{
    /// <summary>
    /// 工作组的实体类
    /// <summary>
    public class JobGroup : FullAuditedEntity<long>, IMayHaveTenant
    {
        /// <summary>
        /// 组名称
        /// <summary>
        [Display(Name = "组名称")]
        [StringLength(StringMaxLengthConst.MaxStringLength10)]
        public string JobGroupName { get; set; }

        /// <summary>
        /// 程序集名称
        /// <summary>
        [Display(Name = "程序集名称")]
        [StringLength(StringMaxLengthConst.MaxStringLength50)]
        public string AssemblyName { get; set; }

        /// <summary>
        /// 类名称
        /// <summary>
        [Display(Name = "类名称")]
        [StringLength(StringMaxLengthConst.MaxStringLength50)]
        public string ClassName { get; set; }

        /// <summary>
        /// 说明
        /// <summary>
        [Display(Name = "说明")]
        [StringLength(StringMaxLengthConst.MaxStringLength1000)]
        public string Description { get; set; }

        public int? TenantId { get; set; }

        /// <summary>
        /// 组类型表
        /// <summary>
        [Display(Name = "组类型表")]
        [StringLength(StringMaxLengthConst.MaxStringLength50)]
        public string TypeTable { get; set; }

    }
}
