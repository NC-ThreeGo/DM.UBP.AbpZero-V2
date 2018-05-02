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

namespace DM.UBP.Domain.Service.BackgroundJobManager.Job_RPTEmails
{
    /// <summary>
    /// 工作的Domain.Service.Interface
    /// <summary>
    public interface IJob_RPTEmailManager : IDomainService
    {
        Task<List<Job_RPTEmail>> GetAllJob_RPTEmailAsync();

        Task<Job_RPTEmail> GetJob_RPTEmailByIdAsync(long id);

        Task<bool> CreateJob_RPTEmailAsync(Job_RPTEmail job_rptemail);

        Task<bool> UpdateJob_RPTEmailAsync(Job_RPTEmail job_rptemail);

        Task DeleteJob_RPTEmailAsync(Job_RPTEmail job_rptemail);

    }
}
