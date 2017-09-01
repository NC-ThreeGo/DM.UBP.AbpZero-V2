using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.UBP.Application.Dto.BaseManage.Permission
{
    /// <summary>
    /// 操作权限的DTO
    /// </summary>
    public class OperatePermDto : EntityDto<int>
    {
        /// <summary>
        /// 操作码
        /// </summary>
        public string KeyCode { get; set; }
        /// <summary>
        /// 是否有权限
        /// </summary>
        public bool IsValid { get; set; }
    }
}
