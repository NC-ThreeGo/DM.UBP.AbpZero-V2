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
    /// 工作的实体类
    /// <summary>
    public class Job_RPTEmail : FullAuditedEntity<long>, IMayHaveTenant
    {
        /// <summary>
        /// 工作名称
        /// <summary>
        [Display(Name = "工作名称")]
        [StringLength(StringMaxLengthConst.MaxStringLength10)]
        public string Job_RPTEmailName { get; set; }

        /// <summary>
        /// 工作组ID
        /// <summary>
        [Display(Name = "工作组ID")]
        public long BGJM_JobGroup_Id { get; set; }

        [StringLength(StringMaxLengthConst.MaxStringLength1000)]
        public string Emails { get; set; }

        /// <summary>
        /// 报表模板ID
        /// <summary>
        [Display(Name = "报表模板ID")]
        public long Template_Id { get; set; }

        /// <summary>
        /// 报表参数
        /// <summary>
        [Display(Name = "报表参数")]
        [StringLength(StringMaxLengthConst.MaxStringLength1000)]
        public string Parameters { get; set; }

        /// <summary>
        /// 说明
        /// <summary>
        [Display(Name = "说明")]
        [StringLength(StringMaxLengthConst.MaxStringLength1000)]
        public string Description { get; set; }

        public int? TenantId { get; set; }

    }
}
