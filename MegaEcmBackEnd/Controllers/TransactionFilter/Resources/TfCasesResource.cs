using System;
using CommonMegaAp11.Utilitys;
using MegaEcmBackEnd.Enums;

namespace MegaEcmBackEnd.Controllers.TransactionFilter.Resources
{
    public class TfCasesResource
    {
        public string MessageType { get; set; }
        public string SwallowId { get; set; }
        public string BusinessMessageIdentifier { get; set; }
        public string Currency { get; set; }
        public float Amount { get; set; }
        public string BranchNo { get; set; }
        public string CaseStatus { get; set; }
        public CaseStatus CaseStatusCode { get; set; }
        public string Assignee { get; set; }
        public string CreateDateTime { get; set; }

        private byte[] caseIdRaw { get; set; }        
        public Guid CaseId
        {
            get { return GuidUtility.FromRaw16(caseIdRaw); }
        }
        
        
    }
}