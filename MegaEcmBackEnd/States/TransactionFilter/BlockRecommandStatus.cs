using System;
using MegaEcmBackEnd.Enums;

namespace MegaEcmBackEnd.States.TransactionFilter
{
    public class BlockRecommandStatus : BaseCaseStatus
    {
        public BlockRecommandStatus(CaseContext context)
        {
            _context = context;
        }

        public override void Running(CaseStatus nextStatus)
        {
            switch (nextStatus)
            {
                case CaseStatus.Block:
                    break;
                case CaseStatus.Reject:
                    break;
                default:
                    throw new ArgumentException("Next status error");
            }
        }
    }
}