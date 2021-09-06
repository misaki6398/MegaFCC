using System;
using MegaEcmBackEnd.Enums;

namespace MegaEcmBackEnd.Controllers.TransactionFilter.Resources
{
    public class TfCaseAuditResource
    {
        public string UserComment { get; set; }
        public string CaseStatus { get; set; }
        public string CreateUser { get; set; }
        public DateTime CreateDateTime { get; set; }
        
    }
}