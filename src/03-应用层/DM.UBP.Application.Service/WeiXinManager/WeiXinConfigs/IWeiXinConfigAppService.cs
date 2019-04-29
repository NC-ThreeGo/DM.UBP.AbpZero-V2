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
using DM.UBP.Application.Dto.WeiXinManager.WeiXinConfigs;
using DM.UBP.Dto;

namespace DM.UBP.Application.Service.WeiXinManager.WeiXinConfigs
{
    /// <summary>
    /// 的Application.Service.Interface
    /// <summary>
    public interface IWeiXinConfigAppService : IApplicationService
    {
        Task<PagedResultDto<WeiXinConfigOutputDto>> GetWeiXinConfigs();

        Task<PagedResultDto<WeiXinConfigOutputDto>> GetWeiXinConfigs(PagedAndSortedInputDto input);

        Task<WeiXinConfigOutputDto> GetWeiXinConfigById(long id);

        Task<bool> CreateWeiXinConfig(WeiXinConfigInputDto input);

        Task<bool> UpdateWeiXinConfig(WeiXinConfigInputDto input);

        Task DeleteWeiXinConfig(EntityDto input);

        Task<bool> SynchroTXL(WeiXinConfigInputDto input);
    }
}
