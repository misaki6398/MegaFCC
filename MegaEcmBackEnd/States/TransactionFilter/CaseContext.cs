using System;
using System.Threading.Tasks;
using AutoMapper;
using MegaEcmBackEnd.Controllers.TransactionFilter.Resources;
using MegaEcmBackEnd.Enums;
using MegaEcmBackEnd.Models.MegaEcm.MapProfiles;
using MegaEcmBackEnd.Models.MegaEcm.Repositorys;

namespace MegaEcmBackEnd.States.TransactionFilter
{
    public class CaseContext
    {
        internal BaseCaseStatus CurrnetProceess { get; set; }
        internal CaseStatus CurrnetStatus { get; set; }
        internal readonly MegaEcmUnitOfWork _megaEcmUnitOfWork;
        internal readonly StateResource _resource;
        internal readonly IMapper _mapper;
        public CaseContext(CaseStatus status, StateResource resource, MegaEcmUnitOfWork megaEcmUnitOfWork)
        {
            _resource = resource;
            _megaEcmUnitOfWork = megaEcmUnitOfWork;
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ResourceToModelProfile());
            });

            _mapper = config.CreateMapper();

            switch (status)
            {
                case CaseStatus.NewCase:
                    CurrnetProceess = new NewCaseStatus(this);
                    break;
                case CaseStatus.Assigned:
                    CurrnetProceess = new AssignedStatus(this);
                    break;
                case CaseStatus.ReleaseRecommand:
                    CurrnetProceess = new ReleaseRecommandStatus(this);
                    break;
                case CaseStatus.BlockRecommand:
                    CurrnetProceess = new BlockRecommandStatus(this);
                    break;
                case CaseStatus.Release:
                    break;
                case CaseStatus.Block:
                    break;
                default:
                    throw new ArgumentException("Status error");
            }
        }

        public async Task RunProcess(CaseStatus nextStatus)
        {
            await CurrnetProceess.Running(nextStatus);
        }
    }
}