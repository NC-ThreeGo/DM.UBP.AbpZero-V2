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
using DM.UBP.Domain.Service.WeiXinManager.WeiXinConfigs;
using DM.UBP.Domain.Service.WeiXinManager;
using DM.UBP.Application.Dto.WeiXinManager.WeiXinConfigs;
using System.Linq;
using System.Linq.Dynamic;
using DM.UBP.Dto;
using Abp.Runtime.Caching;
using Newtonsoft.Json.Linq;
using DM.UBP.Organizations;

namespace DM.UBP.Application.Service.WeiXinManager.WeiXinConfigs
{
    /// <summary>
    /// 的Application.Service
    /// <summary>
    [AbpAuthorize(AppPermissions_WeiXinManager.Pages_WeiXinManager_WeiXinConfigs)]
    public class WeiXinConfigAppService : IWeiXinConfigAppService
    {
        private readonly IWeiXinConfigManager _WeiXinConfigManager;
        private readonly IOrganizationUnitAppService _organizationUnitAppService;
        private readonly ICacheManager _cacheManager;

        public WeiXinConfigAppService(
           IWeiXinConfigManager weixinconfigmanager,
           IOrganizationUnitAppService organizationUnitAppService,
           ICacheManager cacheManager
           
           )
        {
            _WeiXinConfigManager = weixinconfigmanager;
            _organizationUnitAppService = organizationUnitAppService;
            _cacheManager = cacheManager;
        }

        public async Task<PagedResultDto<WeiXinConfigOutputDto>> GetWeiXinConfigs()
        {
            var entities = await _WeiXinConfigManager.GetAllWeiXinConfigsAsync();
            var listDto = entities.MapTo<List<WeiXinConfigOutputDto>>();

            return new PagedResultDto<WeiXinConfigOutputDto>(
            listDto.Count,
            listDto
            );
        }
        public async Task<PagedResultDto<WeiXinConfigOutputDto>> GetWeiXinConfigs(PagedAndSortedInputDto input)
        {
            var entities = await _WeiXinConfigManager.GetAllWeiXinConfigsAsync();
            if (string.IsNullOrEmpty(input.Sorting))
                input.Sorting = "Id";
            var orderEntities = await Task.FromResult(entities.OrderBy(input.Sorting));
            var pageEntities = await Task.FromResult(orderEntities.Skip(input.SkipCount).Take(input.MaxResultCount));
            var listDto = pageEntities.MapTo<List<WeiXinConfigOutputDto>>();

            return new PagedResultDto<WeiXinConfigOutputDto>(
            entities.Count,
            listDto
            );
        }
        public async Task<WeiXinConfigOutputDto> GetWeiXinConfigById(long id)
        {
            var entity = await _WeiXinConfigManager.GetWeiXinConfigByIdAsync(id);
            return entity.MapTo<WeiXinConfigOutputDto>();
        }
        [AbpAuthorize(AppPermissions_WeiXinManager.Pages_WeiXinManager_WeiXinConfigs_Create)]
        public async Task<bool> CreateWeiXinConfig(WeiXinConfigInputDto input)
        {
            var entity = input.MapTo<WeiXinConfig>();
            return await _WeiXinConfigManager.CreateWeiXinConfigAsync(entity);
        }
        [AbpAuthorize(AppPermissions_WeiXinManager.Pages_WeiXinManager_WeiXinConfigs_Edit)]
        public async Task<bool> UpdateWeiXinConfig(WeiXinConfigInputDto input)
        {
            var entity = await _WeiXinConfigManager.GetWeiXinConfigByIdAsync(input.Id);
            input.MapTo(entity);
            return await _WeiXinConfigManager.UpdateWeiXinConfigAsync(entity);
        }
        [AbpAuthorize(AppPermissions_WeiXinManager.Pages_WeiXinManager_WeiXinConfigs_Delete)]
        public async Task DeleteWeiXinConfig(EntityDto input)
        {
            var entity = await _WeiXinConfigManager.GetWeiXinConfigByIdAsync(input.Id);
            await _WeiXinConfigManager.DeleteWeiXinConfigAsync(entity);
        }

        /// <summary>
        /// 同步通讯录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(AppPermissions_WeiXinManager.Pages_WeiXinManager_WeiXinConfigs_Edit)]
        public async Task<bool> SynchroTXL(WeiXinConfigInputDto input)
        {
            var entity = await _WeiXinConfigManager.GetWeiXinConfigByIdAsync(input.Id);
            
            WeiXinApi api = new WeiXinApi(_cacheManager.GetCache("WeiXinApi"), entity.CorpId, entity.TXL_Secret);

            //获取部门列表
            JObject joDepInfo = api.GetDepartment();

            SynchroDepartment(joDepInfo["department"]);
            return true;
        }

        /// <summary>
        /// 下载企业微信中的部门到本系统
        /// </summary>
        /// <param name="joDep"></param>
        private async void SynchroDepartment(JToken joDep)
        {
            for (int i = 0; i < joDep.Count(); i++)
            {
                var name = joDep[i]["name"].ToString();
                var parentid = joDep[i]["parentid"].ToString();

                var orgList = await _organizationUnitAppService.GetOrganizationUnits();
                if (orgList.Items.Count(item => item.DisplayName == name) == 0)
                {//没有就创建
                    joDep.Select(d => d["parentid"].ToString() == parentid);
                    string pName = joDep.FirstOrDefault()["name"].ToString();

                    CreateOrganizationUnit(name, pName);
                }
            }
        }

        /// <summary>
        /// 创建部门
        /// </summary>
        /// <param name="oName"></param>
        /// <param name="pName"></param>
        private async void CreateOrganizationUnit(string oName, string pName)
        {
            var orgList = await _organizationUnitAppService.GetOrganizationUnits();
            if (orgList.Items.Count(item => item.DisplayName == pName) == 0)
            {
                var input = new Organizations.Dto.CreateOrganizationUnitInput();
                input.DisplayName = oName;
                await _organizationUnitAppService.CreateOrganizationUnit(input);
            }
            else
            {
                var input = new Organizations.Dto.CreateOrganizationUnitInput();
                input.DisplayName = oName;
                input.ParentId = orgList.Items.First(item => item.DisplayName == pName).Id;
                await _organizationUnitAppService.CreateOrganizationUnit(input);
            }
        }
    }
}
