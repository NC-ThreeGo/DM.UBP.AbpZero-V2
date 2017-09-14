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
using DM.UBP.Domain.Service.ReportManager.Categories;
using DM.UBP.Domain.Service.ReportManager;
using DM.UBP.Application.Dto.ReportManager.Categories;
using System.Linq;
using DM.UBP.Dto;
using System.Linq.Dynamic;

namespace DM.UBP.Application.Service.ReportManager.Categories
{
    /// <summary>
    /// 报表分类的Application.Service
    /// <summary>
    [AbpAuthorize(AppPermissions_ReportManager.Pages_ReportManager)]
    public class CategoryAppService : ICategoryAppService
    {
        private readonly ICategoryManager _CategoryManager;
        public CategoryAppService(
           ICategoryManager categorymanager
           )
        {
            _CategoryManager = categorymanager;
        }

        public async Task<PagedResultDto<CategoryOutputDto>> GetCategories()
        {
            var entities = await _CategoryManager.GetAllCategoriesAsync();
            var listDto = entities.MapTo<List<CategoryOutputDto>>();

            return new PagedResultDto<CategoryOutputDto>(
            listDto.Count,
            listDto
            );

        }
        public async Task<PagedResultDto<CategoryOutputDto>> GetCategories(PagedAndSortedInputDto input)
        {
            var entities = await _CategoryManager.GetAllCategoriesAsync();

            if (string.IsNullOrEmpty(input.Sorting))
                input.Sorting = "Id";

            var orderEntities = await Task.FromResult(entities.OrderBy(input.Sorting));

            var pageEntities = await Task.FromResult(orderEntities.Skip(input.SkipCount).Take(input.MaxResultCount));

            var listDto = pageEntities.MapTo<List<CategoryOutputDto>>();

            return new PagedResultDto<CategoryOutputDto>(
            entities.Count,
            listDto
            );
        }
        public async Task<CategoryOutputDto> GetCategoryById(long id)
        {
            var entity = await _CategoryManager.GetCategoryByIdAsync(id);
            return entity.MapTo<CategoryOutputDto>();
        }
        [AbpAuthorize(AppPermissions_ReportManager.Pages_ReportManager_Categories_Create)]
        public async Task<bool> CreateCategory(CategoryInputDto input)
        {
            var entity = input.MapTo<Category>();
            return await _CategoryManager.CreateCategoryAsync(entity);
        }
        [AbpAuthorize(AppPermissions_ReportManager.Pages_ReportManager_Categories_Edit)]
        public async Task<bool> UpdateCategory(CategoryInputDto input)
        {
            var entity = await _CategoryManager.GetCategoryByIdAsync(input.Id);
            input.MapTo(entity);
            return await _CategoryManager.UpdateCategoryAsync(entity);
        }
        [AbpAuthorize(AppPermissions_ReportManager.Pages_ReportManager_Categories_Delete)]
        public async Task DeleteCategory(EntityDto input)
        {
            var entity = await _CategoryManager.GetCategoryByIdAsync(input.Id);
            await _CategoryManager.DeleteCategoryAsync(entity);
        }
    }
}
