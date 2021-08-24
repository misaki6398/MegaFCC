using System;
using MegaEcmBackEnd.Enums;

namespace MegaEcmBackEnd.States.TransactionFilter
{
    public class CaseContext
    {
        internal BaseCaseStatus CurrnetProceess { get; set; }
        internal CaseStatus CurrnetStatus { get; set; }
        public CaseContext(CaseStatus status)
        {
            switch (status)
            {
                case CaseStatus.NewCase:
                    CurrnetProceess = new NewCaseStatus(this);
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

        public void RunProcess(CaseStatus nextStatus)
        {
            CurrnetProceess.Running(nextStatus);
        }
    }
}