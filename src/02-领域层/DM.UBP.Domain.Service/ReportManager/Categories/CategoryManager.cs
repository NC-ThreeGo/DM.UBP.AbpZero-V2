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

namespace DM.UBP.Domain.Service.ReportManager.Categories
{
    /// <summary>
    /// 报表分类的Domain.Service
    /// <summary>
    public class CategoryManager : DomainService, ICategoryManager
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        private readonly IRepository<ReportCategory, long> _categoryRepository;

        public CategoryManager(
           IUnitOfWorkManager unitOfWorkManager,
           IRepository<ReportCategory, long> categoryRepository
           )
        {
            _unitOfWorkManager = unitOfWorkManager;
            _categoryRepository = categoryRepository;
        }

        public async Task<List<ReportCategory>> GetAllCategoriesAsync()
        {
            var categories = _categoryRepository.GetAll().OrderBy(p => p.Id);

            return await categories.ToListAsync();
        }

        public async Task<ReportCategory> GetCategoryByIdAsync(long id)
        {
            return await _categoryRepository.GetAsync(id);
        }

        public async Task<bool> CreateCategoryAsync(ReportCategory category)
        {
            var entity = await _categoryRepository.InsertAsync(category);
            return entity != null;
        }

        public async Task<bool> UpdateCategoryAsync(ReportCategory category)
        {
            var entity = await _categoryRepository.UpdateAsync(category);
            return entity != null;
        }

        public async Task DeleteCategoryAsync(ReportCategory category)
        {
            await _categoryRepository.DeleteAsync(category);
        }

    }
}
