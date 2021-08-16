using System.Text.Json.Serialization;

namespace MegaTFLT.Models.Edq.Models
{
    public class CountryCityResponseModel
    {
        
        [JsonPropertyName("Input String")]
        public string InputString { get; set; }

        [JsonPropertyName("Record ID")]
        public string RecordId { get; set; }

        [JsonPropertyName("Country Name")]
        public string CountryName { get; set; }

        [JsonPropertyName("City Name")]
        public string CityName { get; set; }

        [JsonPropertyName("Match Score")]
        public int? MatchScore { get; set; }

        [JsonPropertyName("Match Rule")]
        public string MatchRule { get; set; }

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

        [JsonPropertyName("Web Service ID")]
        public int? WebServiceID { get; set; }

        [JsonPropertyName("List Sub Type ID")]
        public string ListSubTypeID { get; set; }

        [JsonPropertyName("Matched List Type")]
        public string MatchedListType { get; set; }

        [JsonPropertyName("Risk Score")]
        public int? RiskScore { get; set; }

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