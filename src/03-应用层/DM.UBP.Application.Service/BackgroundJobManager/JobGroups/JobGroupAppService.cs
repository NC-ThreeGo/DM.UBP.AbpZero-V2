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
using DM.UBP.Domain.Service.BackgroundJobManager.JobGroups;
using DM.UBP.Domain.Service.BackgroundJobManager;
using DM.UBP.Application.Dto.BackgroundJobManager.JobGroups;
using System.Linq;
using System.Linq.Dynamic;
using DM.UBP.Dto;
using DM.UBP.Application.Service.BackgroundJobManager.Job_RPTEmails;
using DM.UBP.Application.Service.BackgroundJobManager.Job_Sqls;

namespace DM.UBP.Application.Service.BackgroundJobManager.JobGroups
{
    /// <summary>
    /// 工作组的Application.Service
    /// <summary>
    [AbpAuthorize(AppPermissions_BackgroundJobManager.Pages_BackgroundJobManager_JobGroups)]
    public class JobGroupAppService : IJobGroupAppService
    {
        private readonly IJobGroupManager _JobGroupManager;
        private readonly IJob_RPTEmailAppService _Job_RPTEmailAppManager;
        private readonly IJob_SqlAppService _Job_SqlAppService;

        public JobGroupAppService(
           IJobGroupManager jobgroupmanager,
           IJob_RPTEmailAppService job_rptemailmanager,
           IJob_SqlAppService job_sqlappservice
           )
        {
            _Job_RPTEmailAppManager = job_rptemailmanager;
            _JobGroupManager = jobgroupmanager;
            _Job_SqlAppService = job_sqlappservice;
        }

        public async Task<PagedResultDto<JobGroupOutputDto>> GetJobGroups()
        {
            var entities = await _JobGroupManager.GetAllJobGroupsAsync();
            var listDto = entities.MapTo<List<JobGroupOutputDto>>();

            return new PagedResultDto<JobGroupOutputDto>(
            listDto.Count,
            listDto
            );
        }
        public async Task<PagedResultDto<JobGroupOutputDto>> GetJobGroups(PagedAndSortedInputDto input)
        {
            var entities = await _JobGroupManager.GetAllJobGroupsAsync();
            if (string.IsNullOrEmpty(input.Sorting))
                input.Sorting = "Id";
            var orderEntities = await Task.FromResult(entities.OrderBy(input.Sorting));
            var pageEntities = await Task.FromResult(orderEntities.Skip(input.SkipCount).Take(input.MaxResultCount));
            var listDto = pageEntities.MapTo<List<JobGroupOutputDto>>();

            return new PagedResultDto<JobGroupOutputDto>(
            entities.Count,
            listDto
            );
        }
        public async Task<JobGroupOutputDto> GetJobGroupById(long id)
        {
            var entity = await _JobGroupManager.GetJobGroupByIdAsync(id);
            return entity.MapTo<JobGroupOutputDto>();
        }
        [AbpAuthorize(AppPermissions_BackgroundJobManager.Pages_BackgroundJobManager_JobGroups_Create)]
        public async Task<bool> CreateJobGroup(JobGroupInputDto input)
        {
            var entity = input.MapTo<JobGroup>();
            return await _JobGroupManager.CreateJobGroupAsync(entity);
        }
        [AbpAuthorize(AppPermissions_BackgroundJobManager.Pages_BackgroundJobManager_JobGroups_Edit)]
        public async Task<bool> UpdateJobGroup(JobGroupInputDto input)
        {
            var entity = await _JobGroupManager.GetJobGroupByIdAsync(input.Id);
            input.MapTo(entity);
            return await _JobGroupManager.UpdateJobGroupAsync(entity);
        }
        [AbpAuthorize(AppPermissions_BackgroundJobManager.Pages_BackgroundJobManager_JobGroups_Delete)]
        public async Task DeleteJobGroup(EntityDto input)
        {
            var entity = await _JobGroupManager.GetJobGroupByIdAsync(input.Id);
            await _JobGroupManager.DeleteJobGroupAsync(entity);
        }


        public async Task<List<ComboboxItemDto>> GetJobGroupsToItem(long selectValue)
        {
            var entities = await _JobGroupManager.GetAllJobGroupsAsync();
            var items = entities.Select(c => new ComboboxItemDto(c.Id.ToString(), c.JobGroupName) { IsSelected = c.Id == selectValue }).ToList();
            return items;
        }

        public async Task<List<ComboboxItemDto>> GetJobsToItem(long selectValue)
        {
            var entity = await _JobGroupManager.GetJobGroupByIdAsync(selectValue);
            if (entity.TypeTable == "BGJM_JOB_RPTEMAIL")
            {
                return await _Job_RPTEmailAppManager.GetJobRPTEmailsToItem(0);
            }
            if (entity.TypeTable == "BGJM_JOB_SQL")
            {
                return await _Job_SqlAppService.GetJobSqlToItem(0);
            }

            return new List<ComboboxItemDto>();

        }
        
    }
}
