using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using DM.UBP.Domain.Entity.BaseManage.Permission;

namespace DM.UBP.Application.Dto.BaseManage.Permission.Modules
{
    [AutoMapFrom(typeof(Module))]
    public class ModuleListDto : FullAuditedEntityDto
    {
        public long? ParentId { get; set; }

        public string ModuleCode { get; set; }

        public string ModuleName { get; set; }

        public string Icon { get; set; }

        public string Url { get; set; }

        public string Target { set; get; }

        public bool IsLast { get; set; }

        public int? Sort { get; set; }

        public bool EnabledMark { get; set; }

        public string Remark { get; set; }

        /// <summary>
        /// 是否有子模块，如果有则="closed"，否则="open"
        /// </summary>
        public string state { get; set; }
    }
}