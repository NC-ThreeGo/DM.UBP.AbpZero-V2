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

namespace DM.UBP.Domain.Entity.WeiXinManager
{
    /// <summary>
    /// 企业微信配置类
    /// <summary>
    public class WeiXinConfig : FullAuditedEntity<long>, IMayHaveTenant
    {
        /// <summary>
        /// 企业ID
        /// <summary>
        [Display(Name = "企业ID")]
        [StringLength(StringMaxLengthConst.MaxStringLength50)]
        public string CorpId { get; set; }

        /// <summary>
        /// 企业名称
        /// <summary>
        [Display(Name = "企业名称")]
        [StringLength(StringMaxLengthConst.MaxStringLength50)]
        public string CorpName { get; set; }

        /// <summary>
        /// 通讯录密钥
        /// <summary>
        [Display(Name = "通讯录密钥")]
        [StringLength(StringMaxLengthConst.MaxStringLength50)]
        public string TXL_Secret { get; set; }

        public int? TenantId { get; set; }

    }
}
