using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Organizations;
using Abp.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.UBP.Organizations
{
    public class WX_OrganizationUnitManager : OrganizationUnitManager
    {
        public WX_OrganizationUnitManager(IRepository<OrganizationUnit, long> organizationUnitRepository) 
            : base(organizationUnitRepository)
        {

        }

        public async Task ValidateOrganizationUnitAsync(WX_OrganizationUnit organizationUnit)
        {
            var siblings = (await FindChildrenAsync(organizationUnit.ParentId))
                .Where(ou => ou.Id != organizationUnit.Id)
                .ToList();

            if (siblings.Any(ou => ou.DisplayName == organizationUnit.DisplayName))
            {
                throw new UserFriendlyException(L("OrganizationUnitDuplicateDisplayNameWarning", organizationUnit.DisplayName));
            }
        }
    }
}
