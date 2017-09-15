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
using DM.UBP.Application.Dto.ReportManager.Categories;
using DM.UBP.Dto;

namespace DM.UBP.Application.Service.ReportManager.Categories
{
    /// <summary>
    /// 报表分类的Application.Service.Interface
    /// <summary>
    public interface IReportCategoryAppService : IApplicationService
    {
        Task<PagedResultDto<ReportCategoryOutputDto>> GetCategories();

        Task<PagedResultDto<ReportCategoryOutputDto>> GetCategories(PagedAndSortedInputDto input);

        Task<ReportCategoryOutputDto> GetCategoryById(long id);

        Task<bool> CreateCategory(ReportCategoryInputDto input);

        Task<bool> UpdateCategory(ReportCategoryInputDto input);

        Task DeleteCategory(EntityDto input);

    }
}
