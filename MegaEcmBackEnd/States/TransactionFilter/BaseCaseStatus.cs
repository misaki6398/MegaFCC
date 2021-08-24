using MegaEcmBackEnd.Enums;

namespace MegaEcmBackEnd.States.TransactionFilter
{
    public abstract class BaseCaseStatus
    {
        protected CaseContext _context;
        public abstract void Running(CaseStatus nextStatus);
    }
}