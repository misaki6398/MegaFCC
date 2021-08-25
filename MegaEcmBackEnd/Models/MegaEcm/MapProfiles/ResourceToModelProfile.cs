using AutoMapper;
using CommonMegaAp11.Utilitys;
using MegaEcmBackEnd.Controllers.TransactionFilter.Resources;
using MegaEcmBackEnd.Models.MegaEcm.Models;

namespace MegaEcmBackEnd.Models.MegaEcm.MapProfiles
{
    public class ResourceToModelProfile : Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<StateResource, TfCaseAuditsModel>()
            .ForMember(c => c.CaseId, x => x.MapFrom(y => GuidUtility.ToRaw16(y.CaseId)));
        }
    }
}