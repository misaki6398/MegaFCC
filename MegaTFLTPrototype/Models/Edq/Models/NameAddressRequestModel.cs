using System.Text.Json.Serialization;

namespace MegaTFLT.Models.Edq.Models
{
    public class NameAddressRequestModel
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

        [JsonPropertyName("IdentifierCode")]
        public string IdentifierCode { get; set; }

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