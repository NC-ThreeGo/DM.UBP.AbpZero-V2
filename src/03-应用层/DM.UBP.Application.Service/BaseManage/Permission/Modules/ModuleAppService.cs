using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using DM.UBP.Application.Dto.BaseManage.Permission.Modules;
using DM.UBP.Domain.Core.BaseManage.Permission;
using DM.UBP.Domain.Core.BaseManage.Permission.Modules;
using DM.UBP.Domain.Entity.BaseManage.Permission;
using Abp.Auditing;
using DM.UBP.Core.LinqHelper;

namespace DM.UBP.Application.Service.BaseManage.Permission.Modules
{
    /* THIS IS JUST A SAMPLE. */
    //TODO:修改成读取数据库的角色列表
    //[AbpAuthorize(PermissionNames.Pages_Users)]
    [DisableAuditing]
    public class ModuleAppService : UbpAppServiceBase, IModuleAppService
    {
        //private readonly IRepository<Module, int> _moduleRepository;
        private readonly ModuleManager _moduleManager;

        public ModuleAppService(ModuleManager moduleManager)
        {
            //_moduleRepository = moduleRepository;
            _moduleManager = moduleManager;
        }

        #region 模块
        public async Task<List<ModuleListDto>> GetModules(int parentId)
        {
            var modules = await _moduleManager.GetModules(parentId);
            return new List<ModuleListDto>(
              modules.MapTo<List<ModuleListDto>>()
              );
        }

        public CreateModuleInput GetModuleById(int id)
        {
            var module = _moduleManager.GetModuleById(id);
            return module.MapTo<CreateModuleInput>();
        }

        public async Task<bool> CreateModule(CreateModuleInput input)
        {
            Module newModule = new Module()
            {
                TenantId = AbpSession.TenantId,
                ModuleCode = input.ModuleCode,
                ModuleName = input.ModuleName,
                ParentId = input.ParentId,
                Url = input.Url,
                Icon = input.Icon,
                Sort = input.Sort,
                Remark = input.Remark,
                EnabledMark = input.EnabledMark,
                IsLast = input.IsLast
            };
            return await _moduleManager.CreateModuleAsync(newModule);
        }

        public async Task<bool> UpdateModule(CreateModuleInput input)
        {
            var module = _moduleManager.GetModuleById(input.Id);
            module.ModuleCode = input.ModuleCode;
            module.ModuleName = input.ModuleName;
            module.ParentId = input.ParentId;
            module.Url = input.Url;
            module.Icon = input.Icon;
            module.Sort = input.Sort;
            module.Remark = input.Remark;
            module.EnabledMark = input.EnabledMark;
            module.IsLast = input.IsLast;

            return await _moduleManager.UpdateModuleAsync(module);
        }
        #endregion

        #region 模块的操作码
        public List<ModuleOperateDto> GetModuleOperates(ref GridPager pager, int moduleId)
        {
            var moduleOperates = _moduleManager.GetModuleOperates(ref pager, moduleId);
            return new List<ModuleOperateDto>(
              moduleOperates.MapTo<List<ModuleOperateDto>>()
              );
        }

        public ModuleOperateDto GetModuleOperateById(int id)
        {
            var moduleOperate = _moduleManager.GetModuleOperateById(id);
            return moduleOperate.MapTo<ModuleOperateDto>();
        }

        public async Task<bool> CreateModuleOperate(ModuleOperateDto input)
        {
            ModuleOperate newModuleOperate = new ModuleOperate()
            {
                OperateCode = input.OperateCode,
                OperateName = input.OperateName,
                ModuleId = input.ModuleId,
                IsValid = input.IsValid,
                Url = input.Url,
                Icon = input.Icon,
                Sort = input.Sort,
            };
            return await _moduleManager.CreateModuleOperateAsync(newModuleOperate);
        }

        public async Task<bool> UpdateModuleOperate(ModuleOperateDto input)
        {
            var moduleOpterate = _moduleManager.GetModuleOperateById(input.Id).Result;
            moduleOpterate.OperateCode = input.OperateCode;
            moduleOpterate.OperateName = input.OperateName;
            moduleOpterate.ModuleId = input.ModuleId;
            moduleOpterate.IsValid = input.IsValid;
            moduleOpterate.Url = input.Url;
            moduleOpterate.Icon = input.Icon;
            moduleOpterate.Sort = input.Sort;

            return await _moduleManager.UpdateModuleOperateAsync(moduleOpterate);
        }
        #endregion

        #region 模块的数据列过滤器
        public List<ModuleColumnFilterDto> GetColumnFilters(ref GridPager pager, int moduleId)
        {
            var columnFilters = _moduleManager.GetColumnFilters(ref pager, moduleId);
            return new List<ModuleColumnFilterDto>(
              columnFilters.MapTo<List<ModuleColumnFilterDto>>()
              );
        }

        public ModuleColumnFilterDto GetColumnFilterById(int id)
        {
            var columnFilter = _moduleManager.GetColumnFilterById(id);
            return columnFilter.MapTo<ModuleColumnFilterDto>();
        }

        public async Task<bool> CreateColumnFilter(ModuleColumnFilterDto input)
        {
            ModuleColumnFilter newColumnFilter = new ModuleColumnFilter()
            {
                ColumnCode = input.ColumnCode,
                ColumnName = input.ColumnName,
                ModuleId = input.ModuleId,
                IsValid = input.IsValid,
                Sort = input.Sort,
            };
            return await _moduleManager.CreateColumnFilterAsync(newColumnFilter);
        }

        public async Task<bool> UpdateColumnFilter(ModuleColumnFilterDto input)
        {
            var newColumnFilter = _moduleManager.GetColumnFilterById(input.Id).Result;
            newColumnFilter.ColumnCode = input.ColumnCode;
            newColumnFilter.ColumnName = input.ColumnName;
            newColumnFilter.ModuleId = input.ModuleId;
            newColumnFilter.IsValid = input.IsValid;
            newColumnFilter.Sort = input.Sort;

            return await _moduleManager.UpdateColumnFilterAsync(newColumnFilter);
        }
        #endregion
    }
}