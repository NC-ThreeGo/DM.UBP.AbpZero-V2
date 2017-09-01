using System.Collections.Generic;
using Abp.Application.Services.Dto;
using DM.UBP.Editions.Dto;

namespace DM.UBP.Web.Areas.Mpa.Models.Common
{
    public interface IFeatureEditViewModel
    {
        List<NameValueDto> FeatureValues { get; set; }

        List<FlatFeatureDto> Features { get; set; }
    }
}