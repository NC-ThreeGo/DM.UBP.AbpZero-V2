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
using DM.UBP.Application.Dto.BackgroundJobManager.Schedulers;
using DM.UBP.Dto;

namespace DM.UBP.Application.Service.BackgroundJobManager.Schedulers
{
    /// <summary>
    /// 工作的Application.Service.Interface
    /// <summary>
    public interface ISchedulerAppService : IApplicationService
    {
        Task<PagedResultDto<SchedulerOutputDto>> GetSchedulers();

        Task<PagedResultDto<SchedulerOutputDto>> GetSchedulers(PagedAndSortedInputDto input);

        Task<SchedulerOutputDto> GetSchedulerById(long id);

        Task<bool> CreateScheduler(SchedulerInputDto input);

        Task<bool> UpdateScheduler(SchedulerInputDto input);

        Task DeleteScheduler(EntityDto input);

    }
}
