using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.UBP.Domain.Entity.ReportManage
{
    [Table("RptCategorys")]
    public class RptCategorys: Entity<long>
    {
        /// <summary>
        /// 分类名称
        /// </summary>
        public string CategoryName { get; set; }

        public int ParentId { get; set; }

        public string Code { get; set; }

        public int TenantId { get; set; }
    }
}
