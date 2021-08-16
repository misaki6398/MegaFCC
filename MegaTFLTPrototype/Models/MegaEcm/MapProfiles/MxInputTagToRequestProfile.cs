using AutoMapper;
using MegaTFLT.Models.Edq.Models;
using MegaTFLT.Models.MegaEcm.Models;

namespace MegaTFLT.Models.MegaEcm.MapProfiles
{
    public class MxInputTagToRequestProfile : Profile
    {
        public MxInputTagToRequestProfile()
        {
            CreateMap<string, string>().ConvertUsing(s => s ?? string.Empty);
            CreateMap<MxInputTagModel, NameAddressRequestModel>()
            .ForMember(c => c.Input, x => x.MapFrom(y => y.Input))
            .ForMember(c => c.TagName, x => x.MapFrom(y => y.TagName));

            CreateMap<MxInputTagModel, CountryCityRequestModel>()
            .ForMember(c => c.Input, x => x.MapFrom(y => y.Input))
            .ForMember(c => c.TagName, x => x.MapFrom(y => y.TagName));

            CreateMap<MxInputTagModel, GoodsRequestModel>()
            .ForMember(c => c.Input, x => x.MapFrom(y => y.Input))
            .ForMember(c => c.TagName, x => x.MapFrom(y => y.TagName));
            
            CreateMap<MxInputTagModel, NarrativeRequestModel>()
            .ForMember(c => c.Input, x => x.MapFrom(y => y.Input))
            .ForMember(c => c.TagName, x => x.MapFrom(y => y.TagName));

            
            CreateMap<MxInputTagModel, PortRequestModel>()
            .ForMember(c => c.Input, x => x.MapFrom(y => y.Input))
            .ForMember(c => c.TagName, x => x.MapFrom(y => y.TagName));

            CreateMap<MxInputTagModel, BicRequestModel>()
            .ForMember(c => c.Input, x => x.MapFrom(y => y.Input))
            .ForMember(c => c.TagName, x => x.MapFrom(y => y.TagName));
        }
    }
}