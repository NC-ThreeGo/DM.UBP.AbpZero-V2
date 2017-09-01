using System.Collections.Generic;
using Abp.Application.Services.Dto;
using DM.UBP.Editions.Dto;

namespace DM.UBP.MultiTenancy.Dto
{
    public class GetTenantFeaturesForEditOutput
    {
        public List<NameValueDto> FeatureValues { get; set; }

        public List<FlatFeatureDto> Features { get; set; }
    }
}