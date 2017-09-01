using System.ComponentModel.DataAnnotations;
using Abp.Auditing;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using DM.UBP.Domain.Entity.BaseManage.Permission;

namespace DM.UBP.Application.Dto.BaseManage.Permission.Modules
{
    [AutoMapFrom(typeof(Module))]
    public class CreateModuleInput
    {
        //[NotNullExpression]
        [Display(Name = "ID")]
        public long Id { get; set; }

        [Required]
        [Display(Name = "上级ID")]
        public int ParentId { get; set; }

        [Display(Name = "模块编码")]
        public string ModuleCode { get; set; }

        [Display(Name = "模块名称")]
        public string ModuleName { get; set; }

        [Display(Name = "Url")]
        public string Url { get; set; }

        [Display(Name = "图标")]
        public string Icon { get; set; }

        [Display(Name = "排序号")]
        public int Sort { get; set; }

        [Display(Name = "说明")]
        public string Remark { get; set; }

        [Display(Name = "状态")]
        public bool EnabledMark { get; set; }

        [Display(Name = "是否最后一项")]
        public bool IsLast { get; set; }
    }
}