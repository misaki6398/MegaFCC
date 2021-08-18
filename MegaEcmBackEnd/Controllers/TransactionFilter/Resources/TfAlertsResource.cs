using System;

namespace MegaEcmBackEnd.Controllers.TransactionFilter.Resources
{
    public class TfAlertsResource
    {
        public string MatchedListRecordId { get; set; }
        public string MatchType { get; set; }
        public string MatchedName { get; set; }
        public string MatchedListSubKey { get; set; }
        public string Rule { get; set; }
        public string ListSubTypeId { get; set; }
        
        
        // public string TagName { get; set; }
        // public bool ValidFlag { get; set; }
        // public string CreateUser { get; set; }
        // public DateTime CreateDatetime { get; set; }
        // public string UpdateUser { get; set; }
        // public DateTime? UpdateDatetime { get; set; } = null;
    }
}