using System;
using System.Threading.Tasks;
using MegaEcmBackEnd.Enums;

namespace MegaEcmBackEnd.States.TransactionFilter
{
    public class AssignedStatus : BaseCaseStatus
    {
        public AssignedStatus(CaseContext context)
        {
            _context = context;
        }

        public override Task Running(CaseStatus nextStatus)
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
            return Task.CompletedTask;
        }
    }
}