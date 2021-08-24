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
            CreateMap<ScreeningInputTagModel, NameAddressRequestModel>()
            .ForMember(c => c.Input, x => x.MapFrom(y => y.Input))
            .ForMember(c => c.TagName, x => x.MapFrom(y => y.TagName));

            CreateMap<ScreeningInputTagModel, CountryCityRequestModel>()
            .ForMember(c => c.Input, x => x.MapFrom(y => y.Input))
            .ForMember(c => c.TagName, x => x.MapFrom(y => y.TagName));

            CreateMap<ScreeningInputTagModel, GoodsRequestModel>()
            .ForMember(c => c.Input, x => x.MapFrom(y => y.Input))
            .ForMember(c => c.TagName, x => x.MapFrom(y => y.TagName));
            
            CreateMap<ScreeningInputTagModel, NarrativeRequestModel>()
            .ForMember(c => c.Input, x => x.MapFrom(y => y.Input))
            .ForMember(c => c.TagName, x => x.MapFrom(y => y.TagName));

            
            CreateMap<ScreeningInputTagModel, PortRequestModel>()
            .ForMember(c => c.Input, x => x.MapFrom(y => y.Input))
            .ForMember(c => c.TagName, x => x.MapFrom(y => y.TagName));

            CreateMap<ScreeningInputTagModel, BicRequestModel>()
            .ForMember(c => c.Input, x => x.MapFrom(y => y.Input))
            .ForMember(c => c.TagName, x => x.MapFrom(y => y.TagName));
        }
    }
}