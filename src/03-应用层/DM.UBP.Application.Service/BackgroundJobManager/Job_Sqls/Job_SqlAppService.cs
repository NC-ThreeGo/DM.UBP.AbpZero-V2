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
using Abp.Auditing;
using Abp.AutoMapper;
using Abp.Authorization;
using Abp.Application.Services.Dto;
using DM.UBP.Domain.Entity.BackgroundJobManager;
using DM.UBP.Domain.Service.BackgroundJobManager;
using System.Linq;
using System.Linq.Dynamic;
using DM.UBP.Dto;
using DM.UBP.Application.Dto.BackgroundJobManager.Job_Sqls;
using DM.UBP.Domain.Service.BackgroundJobManager.Job_Sqls;

namespace DM.UBP.Application.Service.BackgroundJobManager.Job_Sqls
{
    /// <summary>
    /// SQL任务的Application.Service
    /// <summary>
    [AbpAuthorize(AppPermissions_BackgroundJobManager.Pages_BackgroundJobManager_Job_Sql)]
    public class Job_SqlAppService : IJob_SqlAppService
    {
        private readonly IJob_SqlManager _Job_SqlManager;
        public Job_SqlAppService(
           IJob_SqlManager job_sqlmanager
           )
        {
            _Job_SqlManager = job_sqlmanager;
        }

        public async Task<PagedResultDto<Job_SqlOutputDto>> GetJob_Sql()
        {
            var entities = await _Job_SqlManager.GetAllJob_SqlAsync();
            var listDto = entities.MapTo<List<Job_SqlOutputDto>>();

            return new PagedResultDto<Job_SqlOutputDto>(
            listDto.Count,
            listDto
            );
        }
        public async Task<PagedResultDto<Job_SqlOutputDto>> GetJob_Sql(PagedAndSortedInputDto input)
        {
            var entities = await _Job_SqlManager.GetAllJob_SqlAsync();
            if (string.IsNullOrEmpty(input.Sorting))
                input.Sorting = "Id";
            var orderEntities = await Task.FromResult(entities.OrderBy(input.Sorting));
            var pageEntities = await Task.FromResult(orderEntities.Skip(input.SkipCount).Take(input.MaxResultCount));
            var listDto = pageEntities.MapTo<List<Job_SqlOutputDto>>();

            return new PagedResultDto<Job_SqlOutputDto>(
            entities.Count,
            listDto
            );
        }
        public async Task<Job_SqlOutputDto> GetJob_SqlById(long id)
        {
            var entity = await _Job_SqlManager.GetJob_SqlByIdAsync(id);
            return entity.MapTo<Job_SqlOutputDto>();
        }
        [AbpAuthorize(AppPermissions_BackgroundJobManager.Pages_BackgroundJobManager_Job_Sql_Create)]
        public async Task<bool> CreateJob_Sql(Job_SqlInputDto input)
        {
            var entity = input.MapTo<Job_Sql>();
            return await _Job_SqlManager.CreateJob_SqlAsync(entity);
        }
        [AbpAuthorize(AppPermissions_BackgroundJobManager.Pages_BackgroundJobManager_Job_Sql_Edit)]
        public async Task<bool> UpdateJob_Sql(Job_SqlInputDto input)
        {
            var entity = await _Job_SqlManager.GetJob_SqlByIdAsync(input.Id);
            input.MapTo(entity);
            return await _Job_SqlManager.UpdateJob_SqlAsync(entity);
        }
        [AbpAuthorize(AppPermissions_BackgroundJobManager.Pages_BackgroundJobManager_Job_Sql_Delete)]
        public async Task DeleteJob_Sql(EntityDto input)
        {
            var entity = await _Job_SqlManager.GetJob_SqlByIdAsync(input.Id);
            await _Job_SqlManager.DeleteJob_SqlAsync(entity);
        }

        public async Task<List<ComboboxItemDto>> GetJobSqlToItem(long selectValue)
        {
            var entities = await _Job_SqlManager.GetAllJob_SqlAsync();
            var items = entities.Select(c => new ComboboxItemDto(c.Id.ToString(), c.Job_SqlName) { IsSelected = c.Id == selectValue }).ToList();
            return items;
        }
    }
}
