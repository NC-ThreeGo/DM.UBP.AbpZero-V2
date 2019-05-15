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
        /// 获取所有部门信息
        /// </summary>
        /// <returns></returns>
        Task<List<WX_OrganizationUnitDto>> GetAllOrganizationUnits();

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
