using System;
using CommonMegaAp11.Utilitys;

namespace MegaEcmBackEnd.Controllers.TransactionFilter.Resources
{
    public class TfCasesResource
    {
        public string MessageDefinitionIdentifier { get; set; }
        public string SwallowId { get; set; }
        public string BusinessMessageIdentifier { get; set; }
        public string Currency { get; set; }
        public float Amount { get; set; }
        public string BranchNo { get; set; }
        public string CaseStatus { get; set; }
        public string CreateDateTime { get; set; }

        private byte[] caseIdRaw { get; set; }        
        public Guid CaseId
        {
            get { return GuidUtility.FromRaw16(caseIdRaw); }
        }
        
        
    }
}