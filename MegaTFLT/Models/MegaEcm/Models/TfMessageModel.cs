using System;
using CommonMegaAp11.Utilitys;

namespace MegaTFLT.Models.MegaEcm.Models
{
    public class TfMessageModel
    {
        // Constractor
        public TfMessageModel(string rawMessage) => this.RawMessage = rawMessage;
        public byte[] id { get; private set; } = GuidUtility.ToRaw16(Guid.NewGuid());
        //----From Original Message---
        public string RawMessage { get; private set; }

        //----From Message Header---
        //<AppHdr>
        public string MessageDefinitionIdentifier { get; set; }
        //<MsgDefIdr>
        public string BusinessMessageIdentifier { get; set; }
        //<BizMsgIdr>
        public string BusinessService { get; set; }
        //<BizSvc>
        public DateTime? OriginalCreateDate { get; set; }
        //<CreDt>
        public string FromId { get; set; }
        //<Fr><FIId><FinInstnId><BICFI>
        public string ToId { get; set; }
        //<To><FIId><FinInstnId><BICFI>

        //----From Message Body----
        //<Document><FIToFICstmrCdtTrf><CdtTrfTxInf><InstdAmt>
        public string Currency { get; set; }
        //<InstdAmt Ccy>
        public float? Amount { get; set; }
        //<InstdAmt>

        //----From Swallow----
        public string SwallowId { get; set; }
        public string BranchNO { get; set; }

        // Common
        public int ValidFlag { get; set; } = 1;
        public string CreateUser { get; set; } = "SYSTEM";
        public DateTime CreateDatetime { get; set; } = DateTime.Now;
        public string UpdateUser { get; set; } = "";
        public DateTime? UpdateDatetime { get; set; } = null;
    }
}