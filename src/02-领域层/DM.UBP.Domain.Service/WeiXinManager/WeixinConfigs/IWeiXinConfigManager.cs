//------------------------------------------------------------
// All Rights Reserved , Copyright (C)  
// 版本：1.0
/// <author>
///		<name></name>
///		<date>0001/1/1 0:00:00</date>
/// </author>
//------------------------------------------------------------

using System.Threading.Tasks;
using System.Collections.Generic;
using Abp.Domain.Services;
using DM.UBP.Domain.Entity.WeiXinManager;

namespace DM.UBP.Domain.Service.WeiXinManager.WeiXinConfigs
{
    /// <summary>
    /// 的Domain.Service.Interface
    /// <summary>
    public interface IWeiXinConfigManager : IDomainService
    {
        Task<List<WeiXinConfig>> GetAllWeiXinConfigsAsync();

        Task<WeiXinConfig> GetWeiXinConfigByIdAsync(long id);

        Task<bool> CreateWeiXinConfigAsync(WeiXinConfig weixinconfig);

        Task<bool> UpdateWeiXinConfigAsync(WeiXinConfig weixinconfig);

        Task DeleteWeiXinConfigAsync(WeiXinConfig weixinconfig);

    }
}
