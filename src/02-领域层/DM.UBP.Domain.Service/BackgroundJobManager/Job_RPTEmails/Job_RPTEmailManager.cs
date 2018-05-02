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

namespace DM.UBP.Domain.Service.BackgroundJobManager.Job_RPTEmails
{
    /// <summary>
    /// 工作的Domain.Service
    /// <summary>
    public class Job_RPTEmailManager : DomainService, IJob_RPTEmailManager
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        private readonly IRepository<Job_RPTEmail, long> _job_rptemailRepository;

        public Job_RPTEmailManager(
           IUnitOfWorkManager unitOfWorkManager,
           IRepository<Job_RPTEmail, long> job_rptemailRepository
           )
        {
            _unitOfWorkManager = unitOfWorkManager;
            _job_rptemailRepository = job_rptemailRepository;
        }

        public async Task<List<Job_RPTEmail>> GetAllJob_RPTEmailAsync()
        {
            var job_rptemail = _job_rptemailRepository.GetAll().OrderBy(p => p.Id);
            return await job_rptemail.ToListAsync();
        }

        public async Task<Job_RPTEmail> GetJob_RPTEmailByIdAsync(long id)
        {
            return await _job_rptemailRepository.GetAsync(id);
        }

        public async Task<bool> CreateJob_RPTEmailAsync(Job_RPTEmail job_rptemail)
        {
            var entity = await _job_rptemailRepository.InsertAsync(job_rptemail);
            return entity != null;
        }

        public async Task<bool> UpdateJob_RPTEmailAsync(Job_RPTEmail job_rptemail)
        {
            var entity = await _job_rptemailRepository.UpdateAsync(job_rptemail);
            return entity != null;
        }

        public async Task DeleteJob_RPTEmailAsync(Job_RPTEmail job_rptemail)
        {
            await _job_rptemailRepository.DeleteAsync(job_rptemail);
        }

    }
}
