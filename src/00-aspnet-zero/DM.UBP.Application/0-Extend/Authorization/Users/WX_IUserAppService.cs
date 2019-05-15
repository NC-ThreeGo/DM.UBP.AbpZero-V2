using DM.UBP.Authorization.Users.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.UBP.Authorization.Users
{
    public interface WX_IUserAppService : IUserAppService
    {
        /// <summary>
        /// 获取所有用户
        /// </summary>
        /// <returns></returns>
        Task<List<WX_UserListDto>> GetAllUsers();

        /// <summary>
        /// 创建更新用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<long> CreateOrUpdateUser(WX_CreateOrUpdateUserInput input);
    }
}
