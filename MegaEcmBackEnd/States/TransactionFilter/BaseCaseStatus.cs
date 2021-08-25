using System.Threading.Tasks;
using MegaEcmBackEnd.Enums;

namespace MegaEcmBackEnd.States.TransactionFilter
{
    public abstract class BaseCaseStatus
    {
        protected CaseContext _context;
        public abstract Task Running(CaseStatus nextStatus);
    }
}