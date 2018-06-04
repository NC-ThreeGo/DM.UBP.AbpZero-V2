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
using DM.UBP.Application.Dto.BackgroundJobManager.Job_RPTEmails;
using DM.UBP.Dto;

namespace DM.UBP.Application.Service.BackgroundJobManager.Job_RPTEmails
{
    /// <summary>
    /// 工作的Application.Service.Interface
    /// <summary>
    public interface IJob_RPTEmailAppService : IApplicationService
    {
        Task<PagedResultDto<Job_RPTEmailOutputDto>> GetJob_RPTEmail();

        Task<PagedResultDto<Job_RPTEmailOutputDto>> GetJob_RPTEmail(PagedAndSortedInputDto input);

        Task<Job_RPTEmailOutputDto> GetJob_RPTEmailById(long id);

        Task<bool> CreateJob_RPTEmail(Job_RPTEmailInputDto input);

        Task<bool> UpdateJob_RPTEmail(Job_RPTEmailInputDto input);

        Task DeleteJob_RPTEmail(EntityDto input);

        Task<List<ComboboxItemDto>> GetJobRPTEmailsToItem(long selectValue);
    }
}
