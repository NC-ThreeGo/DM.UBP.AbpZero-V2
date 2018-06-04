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
using DM.UBP.Application.Dto.BackgroundJobManager.JobGroups;
using DM.UBP.Dto;

namespace DM.UBP.Application.Service.BackgroundJobManager.JobGroups
{
    /// <summary>
    /// 工作组的Application.Service.Interface
    /// <summary>
    public interface IJobGroupAppService : IApplicationService
    {
        Task<PagedResultDto<JobGroupOutputDto>> GetJobGroups();

        Task<PagedResultDto<JobGroupOutputDto>> GetJobGroups(PagedAndSortedInputDto input);

        Task<JobGroupOutputDto> GetJobGroupById(long id);

        Task<bool> CreateJobGroup(JobGroupInputDto input);

        Task<bool> UpdateJobGroup(JobGroupInputDto input);

        Task DeleteJobGroup(EntityDto input);


        Task<List<ComboboxItemDto>> GetJobGroupsToItem(long selectValue);

        Task<List<ComboboxItemDto>> GetJobsToItem(long selectValue);
    }
}
