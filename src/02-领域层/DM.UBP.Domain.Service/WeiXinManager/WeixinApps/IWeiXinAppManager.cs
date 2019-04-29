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

namespace DM.UBP.Domain.Service.WeiXinManager.WeiXinApps
{
    /// <summary>
    /// 的Domain.Service.Interface
    /// <summary>
    public interface IWeiXinAppManager : IDomainService
    {
        Task<List<WeiXinApp>> GetAllWeiXinAppsAsync();

        Task<WeiXinApp> GetWeiXinAppByIdAsync(long id);

        Task<bool> CreateWeiXinAppAsync(WeiXinApp weixinapp);

        Task<bool> UpdateWeiXinAppAsync(WeiXinApp weixinapp);

        Task DeleteWeiXinAppAsync(WeiXinApp weixinapp);

    }
}
