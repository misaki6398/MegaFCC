using System;
using MegaTFLT.Utilitys;

namespace MegaTFLT.Models.MegaEcm.Models
{
    public class TfAlertsModel
    {

        public byte[] id { get; set; } = GuidUtility.ToRaw16(Guid.NewGuid());
        public byte[] CaseId { get; set; }
        public string AlertStatus { get; set; }
        public int AlertStatusCode { get; set; }
        public string Input { get; set; }
        // Country&city
        // public string InputString { get; set; }


        // 分數 ex. 90
        // Bic, Country&City,
        // public int? MatchScore { get; set; }
        // Name&Address, Narrative
        public int? Score { get; set; }
        // Port, Goods(待確認)
        // public int? RiskScore { get; set; }

        // 名單 Id
        // Name Address, Narrative
        public string MatchedListRecordId { get; set; }
        // bic, goods, port
        // public string Id { get; set; }
        // Country&City
        // public string RecordId { get; set; }


        // 命中類型 ex. Entity, Port
        // Bic, Goods, NameAddress, Narrative, Port,
        public string MatchType { get; set; }
        // Bic, Goods, CountryCity, Port. 
        // public string MatchedListType { get; set; }


        // Name&Address, Marrative ex. DJW
        public string MatchedListKey { get; set; }



        // Bic ex. DJW-SAN
        // public string MatchedSubList { get; set; }
        // Name&Address, Narrative ex. DJW-SAN
        public string MatchedListSubKey { get; set; }

        // Name&Address, Narrative.
        public string MatchedIndividualName { get; set; }
        public string MatchedEntityName { get; set; }
        public string MatchedKeyword { get; set; }

        // Port only
        public string DetailsOfPort { get; set; }

        // BIC only
        public string DetailsOfBIC { get; set; }

        // Goods only
        public string DetailsOfGoods { get; set; }
        
        //Narrative & Country
        public string CountryName { get; set; }

        // Narrative only
        // public string PortDetails { get; set; }
        // public string GoodsDetails { get; set; }

        // public string CityName { get; set; }
        // public string BICCodeName { get; set; }


        // 名單來源 ex. AU-DFAT; CA-DFATD; CH-SECO; 
        // Bic, Goods, Port
        public string DataSource { get; set; }

        // 命中規則 ex. "[E001D] Part-standardized name exact only"
        public string Rule { get; set; }
        // CountryCity
        // public string MatchRule { get; set; }


        // NameAddress only
        public string IdentifierCode { get; set; }
        public string OriginalName { get; set; }

        // Goods only
        public string ImportCountry { get; set; }
        public string ExportCountry { get; set; }
        public string ImportCountryFrom { get; set; }
        public string ExportCountryTo { get; set; }



        // Common
        public string ListSubTypeID { get; set; }
        public string IdentifierTagName { get; set; }
        public string IdentifierValue { get; set; }
        public string ExpressionCode { get; set; }
        public string BlockName { get; set; }
        public string SequenceName { get; set; }
        public string TagName { get; set; }
        public int? WebServiceID { get; set; }
        public string Jurisdiction { get; set; }
        public string GroupName { get; set; }
        public string GroupTagIdentifierName { get; set; }
        public string GroupTagIdentifierValue { get; set; }
        public int ValidFlag { get; set; } = 1;
        public string CreateUser { get; set; } = "SYSTEM";
        public DateTime CreateDatetime { get; set; } = DateTime.Now;
        public string UpdateUser { get; set; } = "";
        public DateTime? UpdateDatetime { get; set; } = null;
    }
}