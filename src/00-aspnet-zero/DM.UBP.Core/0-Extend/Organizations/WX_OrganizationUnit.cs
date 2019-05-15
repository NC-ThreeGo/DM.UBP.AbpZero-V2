using Abp.Organizations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.UBP.Organizations
{
    [Table("AbpOrganizationUnits")]
    public class WX_OrganizationUnit : OrganizationUnit
    {
        public string WeiXinDepId { get; set; }

        public string WeiXinParentId { get; set; }
    }
}
