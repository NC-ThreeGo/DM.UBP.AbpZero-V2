using DM.UBP.Organizations.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.UBP.Organizations.Dto
{
    public class WX_UpdateOrganizationUnitInput: UpdateOrganizationUnitInput
    {
        public string WeiXinDepId { get; set; }

        public string WeiXinParentId { get; set; }
    }
}
