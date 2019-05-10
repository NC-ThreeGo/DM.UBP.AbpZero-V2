using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using DM.UBP.Domain.Entity;
using DM.UBP.Domain.Entity.WeiXinManager;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.UBP.Application.Dto.WeiXinManager.WeiXinConfigs
{
    /// <summary>
    /// 下载通讯录的dto
    /// <summary>
    public class WeiXinConfigDownTXL : EntityDto<long>
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

        [Display(Name = "需要同步的微信对应部门id")]
        [Required]
        public List<string> DepIds { get; set; }
    }
}
