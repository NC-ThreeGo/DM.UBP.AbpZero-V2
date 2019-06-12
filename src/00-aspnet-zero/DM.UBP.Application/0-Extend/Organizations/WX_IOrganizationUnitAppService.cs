using DM.UBP.Organizations.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.UBP.Organizations
{
    public interface WX_IOrganizationUnitAppService : IOrganizationUnitAppService
    {
        /// <summary>
        /// 获取所有部门信息，用于上传显示用
        /// </summary>
        /// <returns></returns>
        List<WX_OrganizationUnitDto> GetAllOrganizationUnitsByCorpId(string corpId);

        /// <summary>
        /// 按照OU名称和父级ID 获OU信息，防止重复增加
        /// </summary>
        /// <param name="WeiXinDepId">微信ID</param>
        /// <returns></returns>
        Task<WX_OrganizationUnitDto> GetOrganizationUnitsByName(string DisplayName,long? parentId);

        /// <summary>
        /// 获OU信息
        /// </summary>
        /// <param name="WeiXinDepId">微信ID</param>
        /// <returns></returns>
        Task<WX_OrganizationUnitDto> GetOrganizationUnitsByWXID(string WeiXinCorpId, string WeiXinDepId);

        /// <summary>
        /// 获OU信息
        /// </summary>
        /// <param name="WeiXinParentId">微信父级ID</param>
        /// <returns></returns>
        Task<WX_OrganizationUnitDto> GetOrganizationUnitsByWXPID(string WeiXinCorpId, string WeiXinParentId);

        /// <summary>
        /// 创建OU新部门
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<WX_OrganizationUnitDto> CreateOrganizationUnit(WX_CreateOrganizationUnitInput input);

        /// <summary>
        /// 更新OU部门
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        Task<WX_OrganizationUnitDto> UpdateOrganizationUnit(WX_UpdateOrganizationUnitInput input);

        /// <summary>
        /// 获取部门下所有用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<List<OrganizationUnitUserListDto>> GetOrganizationUnitAllUsers(long id);
    }
}
