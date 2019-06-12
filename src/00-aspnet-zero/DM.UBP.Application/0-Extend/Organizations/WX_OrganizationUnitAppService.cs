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
        public List<WX_OrganizationUnitDto> GetAllOrganizationUnitsByCorpId(string corpId)
        {
            var result = new List<WX_OrganizationUnitDto>();
            var root = getRootByCorpId(corpId);
            if (root == null)
                return result;

            result = getOUByParentId(root.Id);
            return result;

            //var query =
            //    from ou in _weixinOrganizationUnitRepository.GetAll()
            //    join uou in _userOrganizationUnitRepository.GetAll() on ou.Id equals uou.OrganizationUnitId into g
            //    select new { ou, memberCount = g.Count() };

            //var items = await query.ToListAsync();

            //return new List<WX_OrganizationUnitDto>(
            //    items.Select(item =>
            //    {
            //        var dto = item.ou.MapTo<WX_OrganizationUnitDto>();
            //        dto.MemberCount = item.memberCount;
            //        return dto;
            //    }).ToList());
        }

        private List<WX_OrganizationUnitDto> getOUByParentId(long parentId)
        {
            var result = new List<WX_OrganizationUnitDto>();

            var own =
                from ou in _weixinOrganizationUnitRepository.GetAll().Where(o => o.Id == parentId)
                join uou in _userOrganizationUnitRepository.GetAll() on ou.Id equals uou.OrganizationUnitId into g
                select new { ou, memberCount = g.Count() };

            var ownModel = own.First().ou.MapTo<WX_OrganizationUnitDto>();
            ownModel.MemberCount = own.First().memberCount;
            result.Add(ownModel);

            var query =
                from ou in _weixinOrganizationUnitRepository.GetAll().Where(o=> o.ParentId == parentId)
                join uou in _userOrganizationUnitRepository.GetAll() on ou.Id equals uou.OrganizationUnitId into g
                select new { ou, memberCount = g.Count() };

            foreach (var item in query)
            {
                var model = new WX_OrganizationUnitDto();
                item.ou.MapTo(model);
                model.MemberCount = item.memberCount;

                result.AddRange(getOUByParentId(model.Id));
            }

            return result;
        }

        /// <summary>
        /// 获取一个企业微信的根 节点
        /// </summary>
        /// <param name="corpId"></param>
        /// <returns></returns>
        private WX_OrganizationUnit getRootByCorpId(string corpId)
        {
            var allOu = _weixinOrganizationUnitRepository.GetAll();
            return allOu.SingleOrDefault(o => o.WeiXinCorpId == corpId && o.WeiXinParentId == "0");
        }

        /// <summary>
        /// 按照OU名称和父级ID 获OU信息
        /// </summary>
        /// <param name="WeiXinDepId"></param>
        /// <returns></returns>
        public async Task<WX_OrganizationUnitDto> GetOrganizationUnitsByName(string DisplayName, long? parentId)
        {
            var entity = await _weixinOrganizationUnitRepository.FirstOrDefaultAsync(o => o.DisplayName == DisplayName && o.ParentId == parentId);
            return entity?.MapTo<WX_OrganizationUnitDto>();
        }

        /// <summary>
        /// 按照微信ID 获OU信息
        /// </summary>
        /// <param name="WeiXinDepId"></param>
        /// <returns></returns>
        public async Task<WX_OrganizationUnitDto> GetOrganizationUnitsByWXID(string WeiXinCorpId, string WeiXinDepId)
        {
            var entity = await _weixinOrganizationUnitRepository.FirstOrDefaultAsync(o => o.WeiXinDepId == WeiXinDepId && o.WeiXinCorpId == WeiXinCorpId);
            return entity?.MapTo<WX_OrganizationUnitDto>();
        }
        /// <summary>
        /// 按照微信父级ID 找出ou信息
        /// </summary>
        /// <param name="WeiXinParentId"></param>
        /// <returns></returns>
        public async Task<WX_OrganizationUnitDto> GetOrganizationUnitsByWXPID(string WeiXinCorpId, string WeiXinParentId)
        {
            var entity = await _weixinOrganizationUnitRepository.FirstOrDefaultAsync(o => o.WeiXinDepId == WeiXinParentId && o.WeiXinCorpId == WeiXinCorpId);
            return entity?.MapTo<WX_OrganizationUnitDto>();
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
            wxOrganizationUnit.WeiXinCorpId = input.WeiXinCorpId;
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
            wxOrganizationUnit.WeiXinCorpId = input.WeiXinCorpId;

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
