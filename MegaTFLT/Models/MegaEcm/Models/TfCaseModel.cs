using System;
using CommonMegaAp11.Utilitys;

namespace MegaTFLT.Models.MegaEcm.Models
{
    public class TfCasesModel
    {
        //public TfCasesModel(byte[] messageId) => MessageId = messageId;
        public TfCasesModel(TfMessageModel tfMessageModel)
        {            
            this.MessageId = tfMessageModel.id;
            this.MxType = tfMessageModel.MessageDefinitionIdentifier;
            this.SwallowId = tfMessageModel.SwallowId;
            this.MsgId = tfMessageModel.MessageDefinitionIdentifier;
            this.MsgCurrency = tfMessageModel.Currency;
            this.MsgAmount = tfMessageModel.Amount;
            this.MsgBranchNO = tfMessageModel.BranchNO;
            this.MsgOriginalCreateDate = tfMessageModel.OriginalCreateDate;
        }
        public byte[] Id { get; set; } = GuidUtility.ToRaw16(Guid.NewGuid());
        public byte[] MessageId { get; private set; }

        // For UI
        public string MxType { get; private set; }
        public string SwallowId { get; private set; }
        public string MsgId { get; private set; }
        public string MsgCurrency { get; private set; }
        public float? MsgAmount { get; private set; }
        public string MsgBranchNO { get; private set; }
        public string CaseStatus { get; set; }
        public int CaseStatusCode { get; set; }
        public string Assignee { get; set; }
        public string Owner { get; set; }
        public DateTime? MsgOriginalCreateDate { get; private set; }

        // Common
        public int ValidFlag { get; set; } = 1;
        public string CreateUser { get; set; } = "SYSTEM";
        public DateTime CreateDatetime { get; set; } = DateTime.Now;
        public string UpdateUser { get; set; } = "";
        public DateTime? UpdateDatetime { get; set; } = null;
    }
}