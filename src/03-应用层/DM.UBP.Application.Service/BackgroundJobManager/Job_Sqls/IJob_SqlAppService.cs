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
using DM.UBP.Dto;
using DM.UBP.Application.Dto.BackgroundJobManager.Job_Sqls;

namespace DM.UBP.Application.Service.BackgroundJobManager.Job_Sqls
{
    /// <summary>
    /// SQL任务的Application.Service.Interface
    /// <summary>
    public interface IJob_SqlAppService : IApplicationService
    {
        Task<PagedResultDto<Job_SqlOutputDto>> GetJob_Sql();

        Task<PagedResultDto<Job_SqlOutputDto>> GetJob_Sql(PagedAndSortedInputDto input);

        Task<Job_SqlOutputDto> GetJob_SqlById(long id);

        Task<bool> CreateJob_Sql(Job_SqlInputDto input);

        Task<bool> UpdateJob_Sql(Job_SqlInputDto input);

        Task DeleteJob_Sql(EntityDto input);

        Task<List<ComboboxItemDto>> GetJobSqlToItem(long selectValue);
    }
}
