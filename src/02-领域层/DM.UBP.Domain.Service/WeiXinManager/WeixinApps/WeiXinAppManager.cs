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

namespace DM.UBP.Domain.Service.WeiXinManager.WeiXinApps
{
    /// <summary>
    /// 的Domain.Service
    /// <summary>
    public class WeiXinAppManager : DomainService, IWeiXinAppManager
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        private readonly IRepository<WeiXinApp, long> _weixinappRepository;

        public WeiXinAppManager(
           IUnitOfWorkManager unitOfWorkManager,
           IRepository<WeiXinApp, long> weixinappRepository
           )
        {
            _unitOfWorkManager = unitOfWorkManager;
            _weixinappRepository = weixinappRepository;
        }

        public async Task<List<WeiXinApp>> GetAllWeiXinAppsAsync()
        {
            var weixinapps = _weixinappRepository.GetAll().OrderBy(p => p.Id);
            return await weixinapps.ToListAsync();
        }

        public async Task<WeiXinApp> GetWeiXinAppByIdAsync(long id)
        {
            return await _weixinappRepository.GetAsync(id);
        }

        public async Task<bool> CreateWeiXinAppAsync(WeiXinApp weixinapp)
        {
            var entity = await _weixinappRepository.InsertAsync(weixinapp);
            return entity != null;
        }

        public async Task<bool> UpdateWeiXinAppAsync(WeiXinApp weixinapp)
        {
            var entity = await _weixinappRepository.UpdateAsync(weixinapp);
            return entity != null;
        }

        public async Task DeleteWeiXinAppAsync(WeiXinApp weixinapp)
        {
            await _weixinappRepository.DeleteAsync(weixinapp);
        }

    }
}
