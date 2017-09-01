using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System.ComponentModel.DataAnnotations;
using DM.UBP.Domain.Entity;
using DM.UBP.Domain.Entity.BaseManage.Permission;

namespace DM.UBP.Application.Dto.BaseManage.Permission.Modules
{
    [AutoMapFrom(typeof(ModuleColumnFilter))]
    public class ModuleColumnFilterDto : FullAuditedEntityDto
    {
        [Display(Name = "字段编码")]
        [StringLength(StringMaxLengthConst.MaxStringLength50)]
        public string ColumnCode { get; set; }

        [Display(Name = "字段名称")]
        [StringLength(StringMaxLengthConst.MaxStringLength50)]
        public string ColumnName { get; set; }

        [Display(Name = "所属模块")]
        public long ModuleId { get; set; }

        [Display(Name = "是否验证")]
        public bool IsValid { get; set; }

        [Display(Name = "排序号")]
        public int Sort { set; get; }
    }
}
