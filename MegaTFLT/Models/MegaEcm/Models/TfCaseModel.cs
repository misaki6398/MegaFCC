using System;
using CommonMegaAp11.Utilitys;

namespace MegaTFLT.Models.MegaEcm.Models
{
    public class TfCasesModel
    {
        //public TfCasesModel(byte[] messageId) => MessageId = messageId;
        public TfCasesModel(TfMessageModel tfMessageModel)
        {            
            this.MessageId = tfMessageModel.Id;
            this.MessageType = tfMessageModel.MessageType;
            this.SwallowId = tfMessageModel.SwallowId;
            this.BusinessMessageIdentifier = tfMessageModel.BusinessMessageIdentifier;
            this.Currency = tfMessageModel.Currency;
            this.Amount = tfMessageModel.Amount;
            this.BranchNO = tfMessageModel.BranchNO;
            this.OriginalCreateDate = tfMessageModel.OriginalCreateDate;
        }
        public byte[] Id { get; set; } = GuidUtility.ToRaw16(Guid.NewGuid());
        public byte[] MessageId { get; private set; }

        // For UI
        public string MessageType { get; private set; }
        public string SwallowId { get; private set; }
        public string BusinessMessageIdentifier { get; private set; }
        public string Currency { get; private set; }
        public float? Amount { get; private set; }
        public string BranchNO { get; private set; }
        public int CaseStatusCode { get; set; }
        public string Assignee { get; set; }
        public string Owner { get; set; }
        public DateTime? OriginalCreateDate { get; private set; }

        // Common
        public int ValidFlag { get; set; } = 1;
        public string CreateUser { get; set; } = "SYSTEM";
        public DateTime CreateDatetime { get; set; } = DateTime.Now;
        public string UpdateUser { get; set; } = "";
        public DateTime? UpdateDatetime { get; set; } = null;
    }
}