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

namespace DM.UBP.Application.Dto.WeiXinManager.WeiXinConfigs
{
    /// <summary>
    /// 的InputDto
    /// <summary>
    [AutoMapTo(typeof(WeiXinConfig))]
    public class WeiXinConfigInputDto : EntityDto<long>
    {
        [Display(Name = "企业ID")]
        [StringLength(StringMaxLengthConst.MaxStringLength50)]
        [Required]
        public string CorpId { get; set; }

        [Display(Name = "企业名称")]
        [StringLength(StringMaxLengthConst.MaxStringLength50)]
        [Required]
        public string CorpName { get; set; }

        [Display(Name = "通讯录密钥")]
        [StringLength(StringMaxLengthConst.MaxStringLength50)]
        [Required]
        public string TXL_Secret { get; set; }

    }
}
