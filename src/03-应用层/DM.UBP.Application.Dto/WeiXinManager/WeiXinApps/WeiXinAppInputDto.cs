//------------------------------------------------------------
// All Rights Reserved , Copyright (C)  
// 版本：1.0
/// <author>
///		<name></name>
///		<date>0001/1/1 0:00:00</date>
/// </author>
//------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using Abp.Application.Services.Dto;
using DM.UBP.Domain.Entity;
using System;
using DM.UBP.Domain.Entity.WeiXinManager;

namespace DM.UBP.Application.Dto.WeiXinManager.WeiXinApps
{
    /// <summary>
    /// 的InputDto
    /// <summary>
    [AutoMapTo(typeof(WeiXinApp))]
    public class WeiXinAppInputDto : EntityDto<long>
    {
        /// <summary>
        /// 企业ID
        /// <summary>
        [Display(Name = "企业ID")]
        [StringLength(StringMaxLengthConst.MaxStringLength50)]
        public string CorpId { get; set; }

        [Display(Name = "企业应用名称，长度不超过32个utf8字符")]
        [StringLength(StringMaxLengthConst.MaxStringLength50)]
        [Required]
        public string AppName { get; set; }

        [Display(Name = "企业应用详情，长度为4至120个utf8字符")]
        [StringLength(StringMaxLengthConst.MaxStringLength1000)]
        [Required]
        public string Descriotion { get; set; }

        [Display(Name = "应用主页url。url必须以http或者https开头（为了提高安全性，建议使用https）。")]
        [StringLength(StringMaxLengthConst.MaxStringLength250)]
        [Required]
        public string Home_Url { get; set; }

        [Display(Name = "企业应用可信域名。注意：域名需通过所有权校验，否则jssdk功能将受限，此时返回错误码85005")]
        [StringLength(StringMaxLengthConst.MaxStringLength250)]
        [Required]
        public string Redorect_Domain { get; set; }

        [Display(Name = "企业应用的id")]
        [StringLength(StringMaxLengthConst.MaxStringLength50)]
        [Required]
        public string AgentId { get; set; }

        [Display(Name = "企业应用的密钥")]
        [StringLength(StringMaxLengthConst.MaxStringLength50)]
        [Required]
        public string CorpSecret { get; set; }

        [Display(Name = "企业应用是否打开地理位置上报 0：不上报；1：进入会话上报；")]
        [Required]
        public bool Report_location_Flag { get; set; }

        [Display(Name = "企业应用是否打开地理位置上报 0：不上报；1：进入会话上报；")]
        [Required]
        public bool IsReportEnter { get; set; }

    }
}
