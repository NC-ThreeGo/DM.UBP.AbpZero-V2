using Abp.Organizations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.UBP.Organizations
{
    public class WX_OrganizationUnit : OrganizationUnit
    {
        public virtual string WeiXinDepId { get; set; }

        public virtual string WeiXinParentId { get; set; }

        public virtual string WeiXinCorpId { get; set; }
    }
}
