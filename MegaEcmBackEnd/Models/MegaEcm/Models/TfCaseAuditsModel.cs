using System;
using CommonMegaAp11.Utilitys;
using MegaEcmBackEnd.Enums;

namespace MegaEcmBackEnd.Models.MegaEcm.Models
{
    public class TfCaseAuditsModel
    {
        public byte[] Id { get; set; } = GuidUtility.ToRaw16(Guid.NewGuid());
        public byte[] CaseId { get; set ; }
        public string UserComment { get; set; }
        public CaseStatus CaseStatusCode { get; set; }
        public int ValidFlag { get; set; } = 1;
        public string CreateUser { get; set; }
        public DateTime CreateDatetime { get; set; }
        public string UpdateUser { get; set; }
        public DateTime? UpdateDatetime { get; set; }
       
    }
}