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
using Abp.Domain.Services;
using DM.UBP.Domain.Entity.BackgroundJobManager;

namespace DM.UBP.Domain.Service.BackgroundJobManager.JobGroups
{
    /// <summary>
    /// 工作组的Domain.Service.Interface
    /// <summary>
    public interface IJobGroupManager : IDomainService
    {
        Task<List<JobGroup>> GetAllJobGroupsAsync();

        Task<JobGroup> GetJobGroupByIdAsync(long id);

        Task<bool> CreateJobGroupAsync(JobGroup jobgroup);

        Task<bool> UpdateJobGroupAsync(JobGroup jobgroup);

        Task DeleteJobGroupAsync(JobGroup jobgroup);

    }
}
