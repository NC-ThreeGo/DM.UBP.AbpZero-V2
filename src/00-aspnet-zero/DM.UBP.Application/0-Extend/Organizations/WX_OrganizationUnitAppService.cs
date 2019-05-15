using Abp.Application.Services.Dto;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Organizations;
using DM.UBP.Organizations.Dto;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.UBP.Organizations
{
    public class WX_OrganizationUnitAppService : OrganizationUnitAppService, WX_IOrganizationUnitAppService
    {
        private readonly IRepository<WX_OrganizationUnit, long> _weixinOrganizationUnitRepository;
        private readonly WX_OrganizationUnitManager _weixinOrganizationUnitManager;

        private readonly OrganizationUnitManager _organizationUnitManager;
        private readonly IRepository<OrganizationUnit, long> _organizationUnitRepository;
        private readonly IRepository<UserOrganizationUnit, long> _userOrganizationUnitRepository;

        public WX_OrganizationUnitAppService(
            OrganizationUnitManager organizationUnitManager,
            IRepository<OrganizationUnit, long> organizationUnitRepository,
            IRepository<UserOrganizationUnit, long> userOrganizationUnitRepository,
            IRepository<WX_OrganizationUnit, long> weixinOrganizationUnitRepository,
            WX_OrganizationUnitManager weixinOrganizationUnitManager) : 
            base(organizationUnitManager, organizationUnitRepository, userOrganizationUnitRepository)
        {
            _organizationUnitManager = organizationUnitManager;
            _organizationUnitRepository = organizationUnitRepository;
            _userOrganizationUnitRepository = userOrganizationUnitRepository;

            _weixinOrganizationUnitRepository = weixinOrganizationUnitRepository;
            _weixinOrganizationUnitManager = weixinOrganizationUnitManager;
        }

        /// <summary>
        /// 获取所有部门信息
        /// </summary>
        /// <returns></returns>
        public async Task<List<WX_OrganizationUnitDto>> GetAllOrganizationUnits()
        {
            var query =
                from ou in _weixinOrganizationUnitRepository.GetAll()
                join uou in _userOrganizationUnitRepository.GetAll() on ou.Id equals uou.OrganizationUnitId into g
                select new { ou, memberCount = g.Count() };

            var items = await query.ToListAsync();

            return new List<WX_OrganizationUnitDto>(
                items.Select(item =>
                {
                    var dto = item.ou.MapTo<WX_OrganizationUnitDto>();
                    dto.MemberCount = item.memberCount;
                    return dto;
                }).ToList());
        }

        /// <summary>
        /// 创建OU新部门
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<WX_OrganizationUnitDto> CreateOrganizationUnit(WX_CreateOrganizationUnitInput input)
        {
            //var organizationUnit = new EntityOrganizationUnitWeiXin(AbpSession.TenantId, input.DisplayName, input.ParentId);

            var wxOrganizationUnit = new WX_OrganizationUnit();
            wxOrganizationUnit.TenantId = AbpSession.TenantId;
            wxOrganizationUnit.DisplayName = input.DisplayName;
            wxOrganizationUnit.ParentId = input.ParentId;
            wxOrganizationUnit.WeiXinDepId = input.WeiXinDepId;
            wxOrganizationUnit.WeiXinParentId = input.WeiXinParentId;
            wxOrganizationUnit.Code = await _weixinOrganizationUnitManager.GetNextChildCodeAsync(wxOrganizationUnit.ParentId);

            await _weixinOrganizationUnitManager.ValidateOrganizationUnitAsync(wxOrganizationUnit);
            await _weixinOrganizationUnitRepository.InsertAsync(wxOrganizationUnit);
            //await _organizationUnitManager.CreateAsync(organizationUnit);
            await CurrentUnitOfWork.SaveChangesAsync();

            return wxOrganizationUnit.MapTo<WX_OrganizationUnitDto>();
        }

        public async Task<WX_OrganizationUnitDto> UpdateOrganizationUnit(WX_UpdateOrganizationUnitInput input)
        {
            var wxOrganizationUnit = await _weixinOrganizationUnitRepository.GetAsync(input.Id);

            wxOrganizationUnit.DisplayName = input.DisplayName;
            wxOrganizationUnit.ParentId = input.ParentId;
            wxOrganizationUnit.WeiXinDepId = input.WeiXinDepId;
            wxOrganizationUnit.WeiXinParentId = input.WeiXinParentId;
            wxOrganizationUnit.Code = await _weixinOrganizationUnitManager.GetNextChildCodeAsync(wxOrganizationUnit.ParentId);

            await _weixinOrganizationUnitManager.ValidateOrganizationUnitAsync(wxOrganizationUnit);
            await _weixinOrganizationUnitRepository.UpdateAsync(wxOrganizationUnit);
            await CurrentUnitOfWork.SaveChangesAsync();

            return wxOrganizationUnit.MapTo<WX_OrganizationUnitDto>();
        }

        /// <summary>
        /// 获取部门下所有用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<List<OrganizationUnitUserListDto>> GetOrganizationUnitAllUsers(long id)
        {
            var query = from uou in _userOrganizationUnitRepository.GetAll()
                        join ou in _organizationUnitRepository.GetAll() on uou.OrganizationUnitId equals ou.Id
                        join user in UserManager.Users on uou.UserId equals user.Id
                        where uou.OrganizationUnitId == id
                        select new { uou, user };

            var users = await query.ToListAsync();

            return new List<OrganizationUnitUserListDto>(
                users.Select(u =>
                {
                    var dto = u.user.MapTo<OrganizationUnitUserListDto>();
                    dto.AddedTime = u.uou.CreationTime;
                    return dto;
                }).ToList());
        }
    }
}
