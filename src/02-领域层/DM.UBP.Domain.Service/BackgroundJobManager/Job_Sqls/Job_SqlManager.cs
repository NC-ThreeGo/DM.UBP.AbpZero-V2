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

namespace DM.UBP.Domain.Service.BackgroundJobManager.Job_Sqls
{
    /// <summary>
    /// SQL任务的Domain.Service
    /// <summary>
    public class Job_SqlManager : DomainService, IJob_SqlManager
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        private readonly IRepository<Job_Sql, long> _job_sqlRepository;

        public Job_SqlManager(
           IUnitOfWorkManager unitOfWorkManager,
           IRepository<Job_Sql, long> job_sqlRepository
           )
        {
            _unitOfWorkManager = unitOfWorkManager;
            _job_sqlRepository = job_sqlRepository;
        }

        public async Task<List<Job_Sql>> GetAllJob_SqlAsync()
        {
            var job_sql = _job_sqlRepository.GetAll().OrderBy(p => p.Id);
            return await job_sql.ToListAsync();
        }

        public async Task<Job_Sql> GetJob_SqlByIdAsync(long id)
        {
            return await _job_sqlRepository.GetAsync(id);
        }

        public async Task<bool> CreateJob_SqlAsync(Job_Sql job_sql)
        {
            var entity = await _job_sqlRepository.InsertAsync(job_sql);
            return entity != null;
        }

        public async Task<bool> UpdateJob_SqlAsync(Job_Sql job_sql)
        {
            var entity = await _job_sqlRepository.UpdateAsync(job_sql);
            return entity != null;
        }

        public async Task DeleteJob_SqlAsync(Job_Sql job_sql)
        {
            await _job_sqlRepository.DeleteAsync(job_sql);
        }

    }
}
