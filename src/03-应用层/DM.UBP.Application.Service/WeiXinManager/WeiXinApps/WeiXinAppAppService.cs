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
using Abp.Auditing;
using Abp.AutoMapper;
using Abp.Authorization;
using Abp.Application.Services.Dto;
using DM.UBP.Domain.Entity.WeiXinManager;
using DM.UBP.Domain.Service.WeiXinManager.WeiXinApps;
using DM.UBP.Domain.Service.WeiXinManager;
using DM.UBP.Application.Dto.WeiXinManager.WeiXinApps;
using System.Linq;
using System.Linq.Dynamic;
using DM.UBP.Dto;

namespace DM.UBP.Application.Service.WeiXinManager.WeiXinApps
{
    /// <summary>
    /// 的Application.Service
    /// <summary>
    [AbpAuthorize(AppPermissions_WeiXinManager.Pages_WeiXinManager_WeiXinApps)]
    public class WeiXinAppAppService : IWeiXinAppAppService
    {
        private readonly IWeiXinAppManager _WeiXinAppManager;
        public WeiXinAppAppService(
           IWeiXinAppManager weixinappmanager
           )
        {
            _WeiXinAppManager = weixinappmanager;
        }

        public async Task<PagedResultDto<WeiXinAppOutputDto>> GetWeiXinApps()
        {
            var entities = await _WeiXinAppManager.GetAllWeiXinAppsAsync();
            var listDto = entities.MapTo<List<WeiXinAppOutputDto>>();

            return new PagedResultDto<WeiXinAppOutputDto>(
            listDto.Count,
            listDto
            );
        }
        public async Task<PagedResultDto<WeiXinAppOutputDto>> GetWeiXinApps(PagedAndSortedInputDto input)
        {
            var entities = await _WeiXinAppManager.GetAllWeiXinAppsAsync();
            if (string.IsNullOrEmpty(input.Sorting))
                input.Sorting = "Id";
            var orderEntities = await Task.FromResult(entities.OrderBy(input.Sorting));
            var pageEntities = await Task.FromResult(orderEntities.Skip(input.SkipCount).Take(input.MaxResultCount));
            var listDto = pageEntities.MapTo<List<WeiXinAppOutputDto>>();

            return new PagedResultDto<WeiXinAppOutputDto>(
            entities.Count,
            listDto
            );
        }
        public async Task<WeiXinAppOutputDto> GetWeiXinAppById(long id)
        {
            var entity = await _WeiXinAppManager.GetWeiXinAppByIdAsync(id);
            return entity.MapTo<WeiXinAppOutputDto>();
        }
        [AbpAuthorize(AppPermissions_WeiXinManager.Pages_WeiXinManager_WeiXinApps_Create)]
        public async Task<bool> CreateWeiXinApp(WeiXinAppInputDto input)
        {
            var entity = input.MapTo<WeiXinApp>();
            return await _WeiXinAppManager.CreateWeiXinAppAsync(entity);
        }
        [AbpAuthorize(AppPermissions_WeiXinManager.Pages_WeiXinManager_WeiXinApps_Edit)]
        public async Task<bool> UpdateWeiXinApp(WeiXinAppInputDto input)
        {
            var entity = await _WeiXinAppManager.GetWeiXinAppByIdAsync(input.Id);
            input.MapTo(entity);
            return await _WeiXinAppManager.UpdateWeiXinAppAsync(entity);
        }
        [AbpAuthorize(AppPermissions_WeiXinManager.Pages_WeiXinManager_WeiXinApps_Delete)]
        public async Task DeleteWeiXinApp(EntityDto input)
        {
            var entity = await _WeiXinAppManager.GetWeiXinAppByIdAsync(input.Id);
            await _WeiXinAppManager.DeleteWeiXinAppAsync(entity);
        }
    }
}
