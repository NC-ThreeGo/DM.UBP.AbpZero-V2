using Abp.AutoMapper;
using Abp.Organizations;

namespace DM.UBP.Web.Areas.Mpa.Models.OrganizationUnits
{
    [AutoMapFrom(typeof(OrganizationUnit))]
    public class EditOrganizationUnitModalViewModel
    {
        public long? Id { get; set; }

        public string DisplayName { get; set; }
    }
}