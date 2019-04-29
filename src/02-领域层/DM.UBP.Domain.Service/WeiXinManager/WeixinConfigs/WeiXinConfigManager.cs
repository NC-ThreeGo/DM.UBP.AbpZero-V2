//------------------------------------------------------------
// All Rights Reserved , Copyright (C)  
// 版本：1.0
/// <author>
///		<name></name>
///		<date>0001/1/1 0:00:00</date>
/// </author>
//------------------------------------------------------------

using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Collections.Generic;
using Abp.Domain.Uow;
using Abp.Domain.Services;
using Abp.Domain.Repositories;
using DM.UBP.Domain.Entity.WeiXinManager;

namespace DM.UBP.Domain.Service.WeiXinManager.WeiXinConfigs
{
    /// <summary>
    /// 的Domain.Service
    /// <summary>
    public class WeiXinConfigManager : DomainService, IWeiXinConfigManager
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        private readonly IRepository<WeiXinConfig, long> _weixinconfigRepository;

        public WeiXinConfigManager(
           IUnitOfWorkManager unitOfWorkManager,
           IRepository<WeiXinConfig, long> weixinconfigRepository
           )
        {
            _unitOfWorkManager = unitOfWorkManager;
            _weixinconfigRepository = weixinconfigRepository;
        }

        public async Task<List<WeiXinConfig>> GetAllWeiXinConfigsAsync()
        {
            var weixinconfigs = _weixinconfigRepository.GetAll().OrderBy(p => p.Id);
            return await weixinconfigs.ToListAsync();
        }

        public async Task<WeiXinConfig> GetWeiXinConfigByIdAsync(long id)
        {
            return await _weixinconfigRepository.GetAsync(id);
        }

        public async Task<bool> CreateWeiXinConfigAsync(WeiXinConfig weixinconfig)
        {
            var entity = await _weixinconfigRepository.InsertAsync(weixinconfig);
            return entity != null;
        }

        public async Task<bool> UpdateWeiXinConfigAsync(WeiXinConfig weixinconfig)
        {
            var entity = await _weixinconfigRepository.UpdateAsync(weixinconfig);
            return entity != null;
        }

        public async Task DeleteWeiXinConfigAsync(WeiXinConfig weixinconfig)
        {
            await _weixinconfigRepository.DeleteAsync(weixinconfig);
        }

    }
}
