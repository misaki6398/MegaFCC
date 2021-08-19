using System;
using CommonMegaAp11.Utilitys;

namespace MegaTFLT.Models.MegaEcm.Models
{
    public class TfAlertListDetailModel
    {
        public TfAlertListDetailModel(byte[] alertId, string listSubTypeId)
        {
            this.AlertId = alertId; 
            this.ListSubTypeId = listSubTypeId;
        }
        //public byte[] id { get; set; } = GuidUtility.ToRaw16(Guid.NewGuid());
        public byte[] AlertId { get; private set; }
        public string ListSubTypeId { get; private set; }
        public string ListRecord { get; set; }
        // Common
        /*
        public int ValidFlag { get; set; } = 1;
        public string CreateUser { get; set; } = "SYSTEM";
        public DateTime CreateDatetime { get; set; } = DateTime.Now;
        public string UpdateUser { get; set; } = "";
        public DateTime? UpdateDatetime { get; set; } = null;
        */
    }
}