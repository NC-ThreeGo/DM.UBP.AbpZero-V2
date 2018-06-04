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
using DM.UBP.Domain.Service.BackgroundJobManager.Job_RPTEmails;
using DM.UBP.Domain.Service.BackgroundJobManager;
using DM.UBP.Application.Dto.BackgroundJobManager.Job_RPTEmails;
using System.Linq;
using System.Linq.Dynamic;
using DM.UBP.Dto;
using DM.UBP.Domain.Service.ReportManager.Templates;
using DM.UBP.Domain.Service.BackgroundJobManager.JobGroups;

namespace DM.UBP.Application.Service.BackgroundJobManager.Job_RPTEmails
{
    /// <summary>
    /// 工作的Application.Service
    /// <summary>
    [AbpAuthorize(AppPermissions_BackgroundJobManager.Pages_BackgroundJobManager_Job_RPTEmails)]
    public class Job_RPTEmailAppService : IJob_RPTEmailAppService
    {
        private readonly IJob_RPTEmailManager _Job_RPTEmailManager;
        private readonly IReportTemplateManager _ReportTemplateManager;
        private readonly IJobGroupManager _JobGroupManager;


        public Job_RPTEmailAppService(
           IJob_RPTEmailManager job_rptemailmanager,
           IReportTemplateManager reporttemplatemanager,
           IJobGroupManager jobgroupmanager
           )
        {
            _Job_RPTEmailManager = job_rptemailmanager;
            _ReportTemplateManager = reporttemplatemanager;
            _JobGroupManager = jobgroupmanager;
        }

        public async Task<PagedResultDto<Job_RPTEmailOutputDto>> GetJob_RPTEmail()
        {
            var entities = await _Job_RPTEmailManager.GetAllJob_RPTEmailAsync();
            var listDto = entities.MapTo<List<Job_RPTEmailOutputDto>>();

            return new PagedResultDto<Job_RPTEmailOutputDto>(
            listDto.Count,
            listDto
            );
        }
        public async Task<PagedResultDto<Job_RPTEmailOutputDto>> GetJob_RPTEmail(PagedAndSortedInputDto input)
        {
            var jobEntities = await _Job_RPTEmailManager.GetAllJob_RPTEmailAsync();

            var templateEntities = await _ReportTemplateManager.GetAllReportTemplatesAsync();

            var jobGroupEntities = await _JobGroupManager.GetAllJobGroupsAsync();

            var entities = jobEntities
                .Join(jobGroupEntities, t => t.BGJM_JobGroup_Id, c => c.Id, (t, c) => new { t, c })
                .Join(templateEntities, t => t.t.Template_Id, c => c.Id, (t, c) => new Job_RPTEmailOutputDto
                {
                    Id = t.t.Id,
                    Job_RPTEmailName = t.t.Job_RPTEmailName,
                    BGJM_JobGroup_Id = t.t.BGJM_JobGroup_Id,
                    Emails = t.t.Emails,
                    Template_Id = t.t.Template_Id,
                    Parameters = t.t.Parameters,
                    Description = t.t.Description,
                    JobGroupName = t.c.JobGroupName,
                    TemplateName = c.TemplateName
                });

            if (string.IsNullOrEmpty(input.Sorting))
                input.Sorting = "Id";
            var orderEntities = await Task.FromResult(entities.OrderBy(input.Sorting));
            var pageEntities = await Task.FromResult(entities.Skip(input.SkipCount).Take(input.MaxResultCount));
            var listDto = pageEntities.MapTo<List<Job_RPTEmailOutputDto>>();

            return new PagedResultDto<Job_RPTEmailOutputDto>(
            entities.Count(),
            listDto
            );
        }
        public async Task<Job_RPTEmailOutputDto> GetJob_RPTEmailById(long id)
        {
            var entity = await _Job_RPTEmailManager.GetJob_RPTEmailByIdAsync(id);
            return entity.MapTo<Job_RPTEmailOutputDto>();
        }
        [AbpAuthorize(AppPermissions_BackgroundJobManager.Pages_BackgroundJobManager_Job_RPTEmails_Create)]
        public async Task<bool> CreateJob_RPTEmail(Job_RPTEmailInputDto input)
        {
            var entity = input.MapTo<Job_RPTEmail>();
            return await _Job_RPTEmailManager.CreateJob_RPTEmailAsync(entity);
        }
        [AbpAuthorize(AppPermissions_BackgroundJobManager.Pages_BackgroundJobManager_Job_RPTEmails_Edit)]
        public async Task<bool> UpdateJob_RPTEmail(Job_RPTEmailInputDto input)
        {
            var entity = await _Job_RPTEmailManager.GetJob_RPTEmailByIdAsync(input.Id);
            input.MapTo(entity);
            return await _Job_RPTEmailManager.UpdateJob_RPTEmailAsync(entity);
        }
        [AbpAuthorize(AppPermissions_BackgroundJobManager.Pages_BackgroundJobManager_Job_RPTEmails_Delete)]
        public async Task DeleteJob_RPTEmail(EntityDto input)
        {
            var entity = await _Job_RPTEmailManager.GetJob_RPTEmailByIdAsync(input.Id);
            await _Job_RPTEmailManager.DeleteJob_RPTEmailAsync(entity);
        }

        public async Task<List<ComboboxItemDto>> GetJobRPTEmailsToItem(long selectValue)
        {
            var entities = await _Job_RPTEmailManager.GetAllJob_RPTEmailAsync();
            var items = entities.Select(c => new ComboboxItemDto(c.Id.ToString(), c.Job_RPTEmailName) { IsSelected = c.Id == selectValue }).ToList();
            return items;
        }
    }
}
