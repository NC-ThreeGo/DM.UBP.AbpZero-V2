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

namespace DM.UBP.Domain.Service.BackgroundJobManager.Job_Sqls
{
    /// <summary>
    /// SQL任务的Domain.Service.Interface
    /// <summary>
    public interface IJob_SqlManager : IDomainService
    {
        Task<List<Job_Sql>> GetAllJob_SqlAsync();

        Task<Job_Sql> GetJob_SqlByIdAsync(long id);

        Task<bool> CreateJob_SqlAsync(Job_Sql job_sql);

        Task<bool> UpdateJob_SqlAsync(Job_Sql job_sql);

        Task DeleteJob_SqlAsync(Job_Sql job_sql);

    }
}
