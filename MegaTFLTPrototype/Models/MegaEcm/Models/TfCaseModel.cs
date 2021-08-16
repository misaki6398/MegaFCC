using System;
using MegaTFLT.Utilitys;

namespace MegaTFLT.Models.MegaEcm.Models
{
    public class TfCasesModel
    {
        public TfCasesModel(byte[] messageId) => MessageId = messageId;
        public byte[] Id { get; set; } = GuidUtility.ToRaw16(Guid.NewGuid());
        public byte[] MessageId { get; set; }
        public string CaseStatus { get; set; }
        public int CaseStatusCode { get; set; }
        // Common
        public int ValidFlag { get; set; } = 1;
        public string CreateUser { get; set; } = "SYSTEM";
        public DateTime CreateDatetime { get; set; } = DateTime.Now;
        public string UpdateUser { get; set; } = "";
        public DateTime? UpdateDatetime { get; set; } = null;
    }
}