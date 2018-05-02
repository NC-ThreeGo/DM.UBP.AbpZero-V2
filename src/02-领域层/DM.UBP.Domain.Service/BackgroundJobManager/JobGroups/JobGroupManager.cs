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
using DM.UBP.Domain.Entity.BackgroundJobManager;

namespace DM.UBP.Domain.Service.BackgroundJobManager.JobGroups
{
    /// <summary>
    /// 工作组的Domain.Service
    /// <summary>
    public class JobGroupManager : DomainService, IJobGroupManager
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        private readonly IRepository<JobGroup, long> _jobgroupRepository;

        public JobGroupManager(
           IUnitOfWorkManager unitOfWorkManager,
           IRepository<JobGroup, long> jobgroupRepository
           )
        {
            _unitOfWorkManager = unitOfWorkManager;
            _jobgroupRepository = jobgroupRepository;
        }

        public async Task<List<JobGroup>> GetAllJobGroupsAsync()
        {
            var jobgroups = _jobgroupRepository.GetAll().OrderBy(p => p.Id);
            return await jobgroups.ToListAsync();
        }

        public async Task<JobGroup> GetJobGroupByIdAsync(long id)
        {
            return await _jobgroupRepository.GetAsync(id);
        }

        public async Task<bool> CreateJobGroupAsync(JobGroup jobgroup)
        {
            var entity = await _jobgroupRepository.InsertAsync(jobgroup);
            return entity != null;
        }

        public async Task<bool> UpdateJobGroupAsync(JobGroup jobgroup)
        {
            var entity = await _jobgroupRepository.UpdateAsync(jobgroup);
            return entity != null;
        }

        public async Task DeleteJobGroupAsync(JobGroup jobgroup)
        {
            await _jobgroupRepository.DeleteAsync(jobgroup);
        }

    }
}
