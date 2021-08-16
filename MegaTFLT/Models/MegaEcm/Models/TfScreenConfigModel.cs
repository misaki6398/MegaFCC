using System;
using MegaTFLT.Utilitys;

namespace MegaTFLT.MegaEcm.Models
{
    public class TfScreenConfigModel
    {
        public byte[] Id { get; set; } = GuidUtility.ToRaw16(Guid.NewGuid());
        public string TagName { get; set; }
        public bool NameAddress { get; set; }
        public bool CountryAndCity { get; set; }
        public bool Goods { get; set; }
        public bool Narrative { get; set; }
        public bool BicCode { get; set; }
        public bool Port { get; set; }
        public bool ValidFlag { get; set; }
        public string CreateUser { get; set; }
        public DateTime CreateDatetime { get; set; }
        public string UpdateUser { get; set; }
        public DateTime UpdateDatetime { get; set; }        
        
    }
}