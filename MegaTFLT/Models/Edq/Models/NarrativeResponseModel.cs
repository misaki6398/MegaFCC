using System.Text.Json.Serialization;

namespace MegaTFLT.Models.Edq.Models
{
    public class NarrativeResponseModel
    {
        [JsonPropertyName("Input")]
        public string Input { get; set; }

        [JsonPropertyName("Block Name")]
        public string BlockName { get; set; }

        [JsonPropertyName("Sequence Name")]
        public string SequenceName { get; set; }

        [JsonPropertyName("Tag Name")]
        public string TagName { get; set; }

        [JsonPropertyName("Expression Code")]
        public string ExpressionCode { get; set; }

        [JsonPropertyName("Identifier Tag Name")]
        public string IdentifierTagName { get; set; }

        [JsonPropertyName("Identifier Value")]
        public string IdentifierValue { get; set; }       

        [JsonPropertyName("Rule")]
        public string Rule { get; set; }

        [JsonPropertyName("Score")]
        public int? Score { get; set; }

        [JsonPropertyName("Match Type")]
        public string MatchType { get; set; }

        [JsonPropertyName("Matched List Key")]
        public string MatchedListKey { get; set; }

        [JsonPropertyName("Matched List Sub Key")]
        public string MatchedListSubKey { get; set; }

        [JsonPropertyName("Matched List Record Id")]
        public string MatchedListRecordId { get; set; }

        [JsonPropertyName("Matched Individual Name")]
        public string MatchedIndividualName { get; set; }

        [JsonPropertyName("Matched Entity Name")]
        public string MatchedEntityName { get; set; }      

        [JsonPropertyName("Web Service ID")]
        public int? WebServiceID { get; set; }

        [JsonPropertyName("Country Name")]
        public string CountryName { get; set; }

        [JsonPropertyName("City Name")]
        public string CityName { get; set; }

         [JsonPropertyName("BIC Code Name")]
        public string BICCodeName { get; set; }

        [JsonPropertyName("Matched Keyword")]
        public string MatchedKeyword { get; set; }

        [JsonPropertyName("Port Details")]
        public string PortDetails { get; set; }

        [JsonPropertyName("Goods Details")]
        public string GoodsDetails { get; set; }

        [JsonPropertyName("List Sub Type Id")]
        public string ListSubTypeId { get; set; }

        [JsonPropertyName("Jurisdiction")]
        public string Jurisdiction { get; set; }
        
        [JsonPropertyName("GroupName")]
        public string GroupName { get; set; }

        [JsonPropertyName("GroupTagIdentifierName")]
        public string GroupTagIdentifierName { get; set; }

        [JsonPropertyName("GroupTagIdentifierValue")]
        public string GroupTagIdentifierValue { get; set; }

    }
}