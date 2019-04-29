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
    /// 企业微信应用设置类
    /// <summary>
    public class WeiXinApp : FullAuditedEntity<long>, IMayHaveTenant
    {
        /// <summary>
        /// 企业ID
        /// <summary>
        [Display(Name = "企业ID")]
        [StringLength(StringMaxLengthConst.MaxStringLength50)]
        public string CorpId { get; set; }

        /// <summary>
        /// 企业应用名称，长度不超过32个utf8字符
        /// <summary>
        [Display(Name = "企业应用名称，长度不超过32个utf8字符")]
        [StringLength(StringMaxLengthConst.MaxStringLength50)]
        public string AppName { get; set; }

        /// <summary>
        /// 企业应用详情，长度为4至120个utf8字符
        /// <summary>
        [Display(Name = "企业应用详情，长度为4至120个utf8字符")]
        [StringLength(StringMaxLengthConst.MaxStringLength1000)]
        public string Descriotion { get; set; }

        /// <summary>
        /// 应用主页url。url必须以http或者https开头（为了提高安全性，建议使用https）。
        /// <summary>
        [Display(Name = "应用主页url。url必须以http或者https开头（为了提高安全性，建议使用https）。")]
        [StringLength(StringMaxLengthConst.MaxStringLength250)]
        public string Home_Url { get; set; }

        /// <summary>
        /// 企业应用可信域名。注意：域名需通过所有权校验，否则jssdk功能将受限，此时返回错误码85005
        /// <summary>
        [Display(Name = "企业应用可信域名。注意：域名需通过所有权校验，否则jssdk功能将受限，此时返回错误码85005")]
        [StringLength(StringMaxLengthConst.MaxStringLength250)]
        public string Redorect_Domain { get; set; }

        /// <summary>
        /// 企业应用的id
        /// <summary>
        [Display(Name = "企业应用的id")]
        [StringLength(StringMaxLengthConst.MaxStringLength50)]
        public string AgentId { get; set; }

        /// <summary>
        /// 企业应用的密钥
        /// <summary>
        [Display(Name = "企业应用的密钥")]
        [StringLength(StringMaxLengthConst.MaxStringLength50)]
        public string CorpSecret { get; set; }

        /// <summary>
        /// 企业应用是否打开地理位置上报 0：不上报；1：进入会话上报；
        /// <summary>
        [Display(Name = "企业应用是否打开地理位置上报 0：不上报；1：进入会话上报；")]
        public bool Report_location_Flag { get; set; }

        /// <summary>
        /// 企业应用是否上报用户进入应用事件。0：不接收；1：接收。
        /// <summary>
        [Display(Name = "企业应用是否上报用户进入应用事件。0：不接收；1：接收。")]
        public bool IsReportEnter { get; set; }

        public int? TenantId { get; set; }

    }
}
