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
using DM.UBP.Domain.Entity.ReportManager;
using DM.UBP.Domain.Service.ReportManager.Templates;
using DM.UBP.Domain.Service.ReportManager;
using DM.UBP.Application.Dto.ReportManager.Templates;
using System.Linq;
using System.Linq.Dynamic;
using DM.UBP.Dto;
using System.Text;
using DM.UBP.Domain.Service.ReportManager.Categories;
using System;
using System.IO;
using AutoMapper;
using DM.UBP.Application.Dto.ReportManager.DataSources;
using DM.UBP.Domain.Service.ReportManager.DataSources;
using System.Xml;

namespace DM.UBP.Application.Service.ReportManager.Templates
{
    /// <summary>
    /// 报表模板的Application.Service
    /// <summary>
    [AbpAuthorize(AppPermissions_ReportManager.Pages_ReportManager_Templates)]
    public class ReportTemplateAppService : IReportTemplateAppService
    {
        private readonly IReportTemplateManager _ReportTemplateManager;
        private readonly IReportCategoryManager _ReportCategoryManager;

        public ReportTemplateAppService(
           IReportTemplateManager reporttemplatemanager,
            IReportCategoryManager reportcategorymanager
           )
        {
            _ReportTemplateManager = reporttemplatemanager;
            _ReportCategoryManager = reportcategorymanager;
        }

        public async Task<PagedResultDto<ReportTemplateOutputDto>> GetReportTemplates()
        {
            var entities = await _ReportTemplateManager.GetAllReportTemplatesAsync();
            var listDto = entities.MapTo<List<ReportTemplateOutputDto>>();

            return new PagedResultDto<ReportTemplateOutputDto>(
            listDto.Count,
            listDto
            );
        }
        public async Task<PagedResultDto<ReportTemplateOutputDto>> GetReportTemplates(PagedAndSortedInputDto input)
        {
            var templateEntities = await _ReportTemplateManager.GetAllReportTemplatesAsync();
            var categoryEntities = await _ReportCategoryManager.GetAllCategoriesAsync();

            var entities = templateEntities.Join(categoryEntities, t => t.Category_Id, c => c.Id, (t, c) => new
            ReportTemplateOutputDto
            {
                CategoryName = c.CategoryName,
                Id = t.Id,
                TemplateName = t.TemplateName,
                Description = t.Description
            });


            if (string.IsNullOrEmpty(input.Sorting))
                input.Sorting = "Id";
            var orderEntities = await Task.FromResult(entities.OrderBy(input.Sorting));
            var pageEntities = await Task.FromResult(orderEntities.Skip(input.SkipCount).Take(input.MaxResultCount));

            //var listDto = pageEntities.MapTo<List<ReportTemplateListDto>>();

            return new PagedResultDto<ReportTemplateOutputDto>(
            entities.Count(),
            pageEntities.ToList()
            );
        }
        public async Task<ReportTemplateOutputDto> GetReportTemplateById(long id)
        {
            var entity = await _ReportTemplateManager.GetReportTemplateByIdAsync(id);
            return entity.MapTo<ReportTemplateOutputDto>();
        }
        [AbpAuthorize(AppPermissions_ReportManager.Pages_ReportManager_Templates_Create)]
        public async Task<bool> CreateReportTemplate(ReportTemplateInputDto input)
        {
            var entity = input.MapTo<ReportTemplate>();
            entity.FileName = CreateFrxFileName();
            entity.FilePath = WriteFrxXml(entity.FileName);
            return await _ReportTemplateManager.CreateReportTemplateAsync(entity);
        }

        private string WriteFrxXml(string fileName)
        {
            string dirPath = System.AppDomain.CurrentDomain.BaseDirectory + "TemplateFiles\\";
            if (!Directory.Exists(dirPath))
                Directory.CreateDirectory(dirPath);

            string filePath = dirPath + fileName;

            XmlDocument xmlReport = new XmlDocument();
            xmlReport.Load(dirPath + "\\BlankReport.frx");
            xmlReport.Save(filePath);


            //string newTime = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            //StringBuilder xmlSB = new StringBuilder();
            //xmlSB.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            //xmlSB.AppendFormat("<Report ScriptLanguage=\"CSharp\" ReportInfo.Created=\"{0}\" ReportInfo.Modified=\"{0}\" ReportInfo.CreatorVersion=\"2017.1.16.0\">", newTime);
            //xmlSB.Append("<Dictionary/>");
            //xmlSB.Append("<ReportPage Name=\"Page1\">");
            //xmlSB.Append("<ReportTitleBand Name=\"ReportTitle1\" Width=\"718.2\" Height=\"37.8\"/>");
            //xmlSB.Append("<PageHeaderBand Name=\"PageHeader1\" Top=\"41.8\" Width=\"718.2\" Height=\"28.35\"/>");
            //xmlSB.Append("<DataBand Name=\"Data1\" Top=\"74.15\" Width=\"718.2\" Height=\"75.6\"/>");
            //xmlSB.Append("<PageFooterBand Name=\"PageFooter1\" Top=\"153.75\" Width=\"718.2\" Height=\"18.9\"/>");
            //xmlSB.Append("</ReportPage>");
            //xmlSB.Append("</Report>");

            //using (StreamWriter writer_CS = new StreamWriter(filePath, false, System.Text.Encoding.GetEncoding("UTF-8")))
            //{
            //    writer_CS.Write(xmlSB.ToString());
            //    writer_CS.Close();
            //}

            return filePath;
        }

        private string CreateFrxFileName()
        {
            return Guid.NewGuid().ToString() + ".frx";
        }

        [AbpAuthorize(AppPermissions_ReportManager.Pages_ReportManager_Templates_Edit)]
        public async Task<bool> UpdateReportTemplate(ReportTemplateInputDto input)
        {
            var entity = await _ReportTemplateManager.GetReportTemplateByIdAsync(input.Id);
            input.MapTo(entity);
            return await _ReportTemplateManager.UpdateReportTemplateAsync(entity);
        }
        [AbpAuthorize(AppPermissions_ReportManager.Pages_ReportManager_Templates_Delete)]
        public async Task DeleteReportTemplate(EntityDto input)
        {
            var entity = await _ReportTemplateManager.GetReportTemplateByIdAsync(input.Id);
            await _ReportTemplateManager.DeleteReportTemplateAsync(entity);
        }

        
    }
}
