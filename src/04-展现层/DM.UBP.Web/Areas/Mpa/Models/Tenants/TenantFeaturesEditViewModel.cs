using Abp.AutoMapper;
using DM.UBP.MultiTenancy;
using DM.UBP.MultiTenancy.Dto;
using DM.UBP.Web.Areas.Mpa.Models.Common;

namespace DM.UBP.Web.Areas.Mpa.Models.Tenants
{
    [AutoMapFrom(typeof (GetTenantFeaturesForEditOutput))]
    public class TenantFeaturesEditViewModel : GetTenantFeaturesForEditOutput, IFeatureEditViewModel
    {
        public Tenant Tenant { get; set; }

        public TenantFeaturesEditViewModel(Tenant tenant, GetTenantFeaturesForEditOutput output)
        {
            Tenant = tenant;
            output.MapTo(this);
        }
    }
}