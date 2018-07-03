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
    /// SQL任务的实体类
    /// <summary>
    public class Job_Sql : FullAuditedEntity<long>, IMayHaveTenant
    {
        /// <summary>
        /// 任务名称
        /// <summary>
        [Display(Name = "任务名称")]
        [StringLength(StringMaxLengthConst.MaxStringLength50)]
        public string Job_SqlName { get; set; }

        /// <summary>
        /// 任务组ID
        /// <summary>
        [Display(Name = "任务组ID")]
        public long BGJM_JobGroup_Id { get; set; }

        /// <summary>
        /// SQL链接字符串
        /// <summary>
        [Display(Name = "SQL链接字符串")]
        [StringLength(StringMaxLengthConst.MaxStringLength100)]
        public string ConnkeyName { get; set; }

        /// <summary>
        /// SQL类型
        /// <summary>
        [Display(Name = "SQL类型")]
        public int CommandType { get; set; }

        /// <summary>
        /// SQL脚本
        /// <summary>
        [Display(Name = "SQL脚本")]
        [StringLength(StringMaxLengthConst.MaxStringLength2000)]
        public string CommandText { get; set; }

        /// <summary>
        /// SQL参数
        /// <summary>
        [Display(Name = "SQL参数")]
        [StringLength(StringMaxLengthConst.MaxStringLength1000)]
        public string Paramters { get; set; }

        /// <summary>
        /// 表述
        /// <summary>
        [Display(Name = "表述")]
        [StringLength(StringMaxLengthConst.MaxStringLength1000)]
        public string Description { get; set; }

        public int? TenantId { get; set; }

    }
}
