using AutoMapper;
using MegaTFLT.Models.Edq.Models;
using MegaTFLT.Models.MegaEcm.Models;

namespace MegaTFLT.Models.MegaEcm.MapProfiles
{
    public class EdqResponseToAlertsProfile : Profile
    {
        public EdqResponseToAlertsProfile()
        {
            CreateMap<string, string>().ConvertUsing(s => s ?? string.Empty);
            CreateMap<NameAddressResponseModel, TfAlertsModel>();
            CreateMap<CountryCityResponseModel, TfAlertsModel>()
            .ForMember(c => c.Score, x => x.MapFrom(y => y.MatchScore))
            .ForMember(c => c.MatchedListRecordId, x => x.MapFrom(y => y.RecordId))
            .ForMember(c => c.MatchType, x => x.MapFrom(y => y.MatchedListType))
            .ForMember(c => c.Rule, x => x.MapFrom(y => y.MatchRule))
            .ForMember(c => c.Input, x => x.MapFrom(y => y.InputString));

            CreateMap<GoodsResponseModel, TfAlertsModel>()
            .ForMember(c=>c.Id, x => x.Ignore())
            .ForMember(c => c.Score, x => x.MapFrom(y => y.RiskScore))
            .ForMember(c => c.MatchedListRecordId, x => x.MapFrom(y => y.ID))
            .ForMember(c => c.MatchType, x => x.MapFrom(y => y.MatchedListType));
            
            CreateMap<NarrativeResponseModel, TfAlertsModel>()            
            .ForMember(c => c.MatchedListRecordId, x => x.MapFrom(y => y.IdentifierValue))
            .ForMember(c => c.DetailsOfPort, x => x.MapFrom(y => y.PortDetails))
            .ForMember(c => c.DetailsOfGoods, x => x.MapFrom(y => y.GoodsDetails))
            .ForMember(c => c.DetailsOfBIC, x => x.MapFrom(y => y.BICCodeName));
            
            CreateMap<PortResponseModel, TfAlertsModel>()
            .ForMember(c=>c.Id, x => x.Ignore())
            .ForMember(c => c.Score, x => x.MapFrom(y => y.RiskScore))
            .ForMember(c => c.MatchedListRecordId, x => x.MapFrom(y => y.ID))
            .ForMember(c => c.MatchType, x => x.MapFrom(y => y.MatchedListType));

            CreateMap<BicResponseModel, TfAlertsModel>()
            .ForMember(c=>c.Id, x => x.Ignore())
            .ForMember(c => c.Score, x => x.MapFrom(y => y.MatchScore))
            .ForMember(c => c.MatchedListRecordId, x => x.MapFrom(y => y.ID))
            .ForMember(c => c.MatchType, x => x.MapFrom(y => y.MatchedListType))
            .ForMember(c => c.MatchedListSubKey, x => x.MapFrom(y => y.MatchedSubList));

            
        }
    }
}