using System;
using MegaEcmBackEnd.Enums;

namespace MegaEcmBackEnd.States.TransactionFilter
{
    public class NewCaseStatus : BaseCaseStatus
    {
        public NewCaseStatus(CaseContext context)
        {
            _context = context;
        }

        public override void Running(CaseStatus nextStatus)
        {
            switch (nextStatus)
            {
                case CaseStatus.ReleaseRecommand:
                    break;
                case CaseStatus.BlockRecommand:
                    break;
                default:
                    throw new ArgumentException("Next status error");
            }
        }
    }
}