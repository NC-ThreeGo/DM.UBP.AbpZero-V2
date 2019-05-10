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
using System.Data;

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

        /// <summary>
        /// 获取微信里面的部门信息，用于下载的时候显示用
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        DataTable GetWeiXinDepartmentInfo(WeiXinConfigOutputDto input);

        /// <summary>
        /// 获取UBP种所有部门，用户上传的时候显示用
        /// </summary>
        /// <returns></returns>
        DataTable GetOrganizationUnitInfo();

        /// <summary>
        /// 确定下载通讯录
        /// </summary>
        /// <param name="input"></param>
        /// <param name="txlIds"></param>
        /// <returns></returns>
        Task<bool> DownTXL(WeiXinConfigDownTXL input);

        /// <summary>
        /// 确定上传通讯录
        /// </summary>
        /// <param name="input"></param>
        /// <param name="txlIds"></param>
        /// <returns></returns>
        Task<bool> UploadTXL(WeiXinConfigDownTXL input);

        Task<bool> SendMsg(WeiXinConfigSendMsgDto input);
    }
}
