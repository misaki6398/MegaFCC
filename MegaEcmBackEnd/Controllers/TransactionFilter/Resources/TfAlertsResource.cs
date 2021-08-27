using System;
using CommonMegaAp11.Utilitys;
using MegaEcmBackEnd.Enums;

namespace MegaEcmBackEnd.Controllers.TransactionFilter.Resources
{
    public class TfAlertsResource
    {
        private byte[] alertIdRaw { get; set; }        
        public Guid AlertId
        {
            get { return GuidUtility.FromRaw16(alertIdRaw); }
        }
        public string MatchedListRecordId { get; set; }
        public string MatchType { get; set; }
        public string MatchedName { get; set; }
        public string MatchedListSubKey { get; set; }
        public string Rule { get; set; }
        public string ListSubTypeId { get; set; }
        public string TagName { get; set; }
        public AlertStatus AlertStatusCode { get; set; }
        // public bool ValidFlag { get; set; }
        // public string CreateUser { get; set; }
        // public DateTime CreateDatetime { get; set; }
        // public string UpdateUser { get; set; }
        // public DateTime? UpdateDatetime { get; set; } = null;
    }
}