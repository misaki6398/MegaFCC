using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml;
using System.Linq;
using MegaTFLT.Models.MegaEcm.Models;

namespace MegaTFLT.Utilitys
{
    public class TxnPaser : BaseMessagePaser
    {
        public override bool ReadFromText(string txnText)
        {
            bool isSuccess = false;
            XmlReader reader = XmlReader.Create(new StringReader(txnText));
            mxMessages = new Dictionary<string, List<ScreeningInputTagModel>>();
            string ElementText = "";
            string ValueText = "";

            //for One Seq
            string checkSeq = "";
            string entityType = "";
            string entityRelationship = "";
            string entityRelationshipDesc = "";

            // ----Peocess Message----
            TfMessageModel = new TfMessageModel(txnText);
            // ----Peocess Message----
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    ElementText = reader.Name;
                    // ----Peocess Message----
                    if ("seq" == ElementText)
                    {
                    }
                    // ----Peocess Message----
                }
                else if (reader.NodeType == XmlNodeType.Text && (reader.HasValue))
                {
                    ValueText = reader.Value;
                    Console.WriteLine($"{ElementText}:{ValueText}");

                    // ----Peocess Message----
                    switch (ElementText)
                    {
                        case "unique_key":
                            if (TfMessageModel.SwallowId == null)
                                TfMessageModel.SwallowId = ValueText;
                            break;
                        case "reference_number":
                            if (TfMessageModel.MessageDefinitionIdentifier == null)
                                TfMessageModel.MessageDefinitionIdentifier = ValueText;
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
                            // ----Peocess Screening----
                            List<ScreeningInputTagModel> InputTagList = null;
                            if (mxMessages.ContainsKey(ElementText))
                            {
                                InputTagList = mxMessages[ElementText];
                            }
                            else
                            {
                                InputTagList = new List<ScreeningInputTagModel>();
                                mxMessages.Add(ElementText, InputTagList);
                            }
                            ScreeningInputTagModel tempMxInputTagModel = new ScreeningInputTagModel();
                            tempMxInputTagModel.Input = ValueText;
                            tempMxInputTagModel.TagName = ElementText;
                            InputTagList.Add(tempMxInputTagModel);
                            // ----Peocess Screening----
                            break;
                        default:
                            break;
                    }
                    // ----Peocess Message----
                }
            }
            //Console.WriteLine($"{mxMessages}");
            foreach (string mxMessageKey in mxMessages.Keys)
            {
                List<ScreeningInputTagModel> InputTagList = mxMessages[mxMessageKey];
                IEnumerable<ScreeningInputTagModel> noduplicates = (InputTagList.Distinct());
                mxMessages[mxMessageKey] = noduplicates.ToList();
            }

            isSuccess = true;
            return isSuccess;
        }
    }
}