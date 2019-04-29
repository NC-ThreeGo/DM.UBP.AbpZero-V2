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
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using DM.UBP.Application.Dto.WeiXinManager.WeiXinApps;
using DM.UBP.Dto;

namespace DM.UBP.Application.Service.WeiXinManager.WeiXinApps
{
    /// <summary>
    /// 的Application.Service.Interface
    /// <summary>
    public interface IWeiXinAppAppService : IApplicationService
    {
        Task<PagedResultDto<WeiXinAppOutputDto>> GetWeiXinApps();

        Task<PagedResultDto<WeiXinAppOutputDto>> GetWeiXinApps(PagedAndSortedInputDto input);

        Task<WeiXinAppOutputDto> GetWeiXinAppById(long id);

        Task<bool> CreateWeiXinApp(WeiXinAppInputDto input);

        Task<bool> UpdateWeiXinApp(WeiXinAppInputDto input);

        Task DeleteWeiXinApp(EntityDto input);

    }
}
