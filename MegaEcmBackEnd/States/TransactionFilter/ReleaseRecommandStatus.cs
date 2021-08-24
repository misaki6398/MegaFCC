using System;
using MegaEcmBackEnd.Enums;

namespace MegaEcmBackEnd.States.TransactionFilter
{
    public class ReleaseRecommandStatus : BaseCaseStatus
    {
        public ReleaseRecommandStatus(CaseContext context)
        {
            _context = context;
        }

        public override void Running(CaseStatus nextStatus)
        {
            switch (nextStatus)
            {
                case CaseStatus.Release:
                    break;
                case CaseStatus.Reject:
                    break;
                default:
                    throw new ArgumentException("Next status error");
            }
        }
    }
}