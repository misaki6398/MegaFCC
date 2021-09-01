using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml;
using System.Linq;
using MegaTFLT.Models.MegaEcm.Models;
using MegaTFLT.MegaEcm.Models;
using CommonMegaAp11.Enums;
using MegaTFLT.Utilitys;
using System.Threading.Tasks;

namespace MegaTFLT.Services.Parsers
{
    public class TxnParser : BaseMessageParser
    {
        public override async Task<bool> ReadFromText(string text)
        {
            bool isSuccess = false;
            XmlReaderSettings settings = new XmlReaderSettings
            {
                Async = true
            };
            XmlReader reader = XmlReader.Create(new StringReader(text), settings);
            ScreeningInputTags = new Dictionary<string, List<ScreeningInputTagModel>>();
            string ElementText = "";
            string ValueText = "";

            //for One Seq
            string checkSeq = "";
            string entityType = "";
            string entityRelationship = "";
            string entityRelationshipDesc = "";

            // ----Process Message----
            TfMessageModel = new TfMessageModel(text, "TxnObs");
            TfMessageModel.MessageType = "Transaction";
            // ----Process Message----
            while (await reader.ReadAsync())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    ElementText = reader.Name;
                    // ----Process Message----
                    if ("seq" == ElementText)
                    {
                    }
                    // ----Process Message----
                }
                else if (reader.NodeType == XmlNodeType.Text && (reader.HasValue))
                {
                    ValueText = reader.Value;
                    Console.WriteLine($"{ElementText}:{ValueText}");

                    // ----Process Message----
                    switch (ElementText)
                    {
                        case "unique_key":
                            if (TfMessageModel.SwallowId == null)
                                TfMessageModel.SwallowId = ValueText;
                            break;
                        case "reference_number":
                            if (TfMessageModel.BusinessMessageIdentifier == null)
                                TfMessageModel.BusinessMessageIdentifier = ValueText;
                            break;
                        case "transaction_date":
                            if (TfMessageModel.OriginalCreateDate == null)
                            {
                                string pattern = "yyyyMMdd";
                                DateTime parsedDate;
                                if (DateTime.TryParseExact(ValueText, pattern, null,
                                       DateTimeStyles.None, out parsedDate))
                                {
                                    TfMessageModel.OriginalCreateDate = parsedDate;
                                }
                            }
                            break;
                        case "branch_number":
                            if (TfMessageModel.BranchNO == null)
                                TfMessageModel.BranchNO = ValueText;
                            break;
                        case "check_seq":
                            checkSeq = ValueText;
                            break;
                        case "entity_type":
                            entityType = ValueText;
                            break;
                        case "entity_relationship":
                            entityRelationship = ValueText;
                            break;
                        case "entity_relationship_desc":
                            entityRelationshipDesc = ValueText;
                            break;
                        case "english_name":
                        case "non_english_name":
                        case "ccc_code":
                        case "id_number":
                        case "bic_swift_code":
                        case "free_format_text":
                        case "country":
                        case "year_of_birth":
                            ScreeningInputTagModel tempInputTagModel = new ScreeningInputTagModel();
                            tempInputTagModel.Input = ValueText;
                            tempInputTagModel.TagName = ElementText;
                            string tfScreenConfigKey = new TfScreenConfigKeyModel(MessageSource.TxnObs, ElementText, entityType).ToString();
                            string tfScreenSubConfigKey = new TfScreenConfigKeyModel(MessageSource.TxnObs, ElementText, entityType).ToStringWithSubType();

                            //if (TagName || EntityType) in ScreenSubConfigs
                            if (ConfigUtility.ScreenSubConfigs.ContainsKey(tfScreenSubConfigKey))
                            {
                                tfScreenConfigKey = tfScreenSubConfigKey;
                            }

                            // ----Process Screening----
                            List<ScreeningInputTagModel> InputTagList = null;
                            if (ScreeningInputTags.ContainsKey(tfScreenConfigKey))
                            {
                                InputTagList = ScreeningInputTags[tfScreenConfigKey];
                            }
                            else
                            {
                                InputTagList = new List<ScreeningInputTagModel>();
                                ScreeningInputTags.Add(tfScreenConfigKey, InputTagList);
                            }
                            InputTagList.Add(tempInputTagModel);
                            // ----Process Screening----
                            break;
                        default:
                            break;
                    }
                    // ----Process Message----
                }
            }

            this.DistinctDictionary(ScreeningInputTags);

            isSuccess = true;
            return isSuccess;
        }
    }
}