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
using DM.UBP.Domain.Entity.ReportManager;

namespace DM.UBP.Domain.Service.ReportManager.Templates
{
    /// <summary>
    /// 报表模板的Domain.Service
    /// <summary>
    public class ReportTemplateManager : DomainService, IReportTemplateManager
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        private readonly IRepository<ReportTemplate, long> _reporttemplateRepository;

        public ReportTemplateManager(
           IUnitOfWorkManager unitOfWorkManager,
           IRepository<ReportTemplate, long> reporttemplateRepository
           )
        {
            _unitOfWorkManager = unitOfWorkManager;
            _reporttemplateRepository = reporttemplateRepository;
        }

        public async Task<List<ReportTemplate>> GetAllReportTemplatesAsync()
        {
            var reporttemplates = _reporttemplateRepository.GetAll().OrderBy(p => p.Id);
            return await reporttemplates.ToListAsync();
        }

        public async Task<ReportTemplate> GetReportTemplateByIdAsync(long id)
        {
            return await _reporttemplateRepository.GetAsync(id);
        }

        public async Task<bool> CreateReportTemplateAsync(ReportTemplate reporttemplate)
        {
            var entity = await _reporttemplateRepository.InsertAsync(reporttemplate);
            return entity != null;
        }

        public async Task<bool> UpdateReportTemplateAsync(ReportTemplate reporttemplate)
        {
            var entity = await _reporttemplateRepository.UpdateAsync(reporttemplate);
            return entity != null;
        }

        public async Task DeleteReportTemplateAsync(ReportTemplate reporttemplate)
        {
            await _reporttemplateRepository.DeleteAsync(reporttemplate);
        }

    }
}
