using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml;
using System.Linq;
using MegaTFLT.Models.MegaEcm.Models;

namespace MegaTFLT.Utilitys
{
    public class MxPaser : BaseMessagePaser
    {
        public override bool ReadFromText(string text)
        {
            bool isSuccess = false;
            XmlReader reader = XmlReader.Create(new StringReader(text));
            ScreeningInputTags = new Dictionary<string, List<ScreeningInputTagModel>>();
            string ElementText = "";
            string ValueText = "";

            // ----Peocess Message----
            TfMessageModel = new TfMessageModel(text);
            TfMessageModel.SwallowId = TfMessageModel.CreateDatetime.ToString("yyyyMMddHHmmssffffff", DateTimeFormatInfo.InvariantInfo) + "I0";
            Console.WriteLine($"MessageGuid:{TfMessageModel.Id}");
            Console.WriteLine($"CreateDatetime:{TfMessageModel.CreateDatetime}");
            Console.WriteLine($"SwallowId:{TfMessageModel.SwallowId}");
            bool isFindFrom = false;
            bool isFindTo = false;
            bool isFindInstdAmt = false;
            // ----Peocess Message----

            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    ElementText = reader.Name;

                    // ----Peocess Message----
                    if (!isFindFrom && TfMessageModel.FromId == null && "Fr" == ElementText)
                        isFindFrom = true;
                    else if (!isFindTo && TfMessageModel.ToId == null && "To" == ElementText)
                        isFindTo = true;
                    else if (!isFindInstdAmt && (TfMessageModel.Amount == null || TfMessageModel.Currency == null) && "InstdAmt" == ElementText)
                        isFindInstdAmt = true;
                    // ----Peocess Message----

                    if (reader.HasAttributes)
                    {
                        for (int i = 0; i < reader.AttributeCount; i++)
                        {
                            reader.MoveToAttribute(i);
                            Console.WriteLine($"{ElementText}:[{i}][{reader.Name}]:{reader.Value}");

                            // ----Peocess Message----
                            if (isFindInstdAmt && TfMessageModel.Currency == null && "InstdAmt" == ElementText && "Ccy" == reader.Name)
                                TfMessageModel.Currency = reader.Value;
                            // ----Peocess Message----

                        }
                        reader.MoveToElement();
                    }
                }
                else if (reader.NodeType == XmlNodeType.Text && (reader.HasValue))
                {
                    ValueText = reader.Value;
                    Console.WriteLine($"{ElementText}:{ValueText}");

                    // ----Peocess Message----
                    switch (ElementText)
                    {
                        case "BizMsgIdr":
                            if (TfMessageModel.BusinessMessageIdentifier == null)
                                TfMessageModel.BusinessMessageIdentifier = ValueText;
                            break;
                        case "MsgDefIdr":
                            if (TfMessageModel.MessageType == null)
                                TfMessageModel.MessageType = ValueText;
                            break;
                        case "BizSvc":
                            if (TfMessageModel.BusinessService == null)
                                TfMessageModel.BusinessService = ValueText;
                            break;
                        case "CreDt":
                            if (TfMessageModel.OriginalCreateDate == null)
                                TfMessageModel.OriginalCreateDate = reader.ReadContentAsDateTime();
                            break;
                        case "BICFI":
                            if (isFindFrom && TfMessageModel.FromId == null && "BICFI" == ElementText)
                            {
                                TfMessageModel.FromId = ValueText;
                                isFindFrom = false;
                            }
                            else if (isFindTo && TfMessageModel.ToId == null && "BICFI" == ElementText)
                            {
                                TfMessageModel.ToId = ValueText;
                                isFindTo = false;
                            }
                            break;
                        case "InstdAmt":
                            if (isFindInstdAmt && TfMessageModel.Amount == null)
                            {
                                TfMessageModel.Amount = reader.ReadContentAsFloat();
                                isFindInstdAmt = false;
                            }
                            break;
                        default:
                            break;
                    }
                    // ----Peocess Message----

                    // ----Peocess Screening----
                    List<ScreeningInputTagModel> InputTagList = null;
                    if (ScreeningInputTags.ContainsKey(ElementText))
                    {
                        InputTagList = ScreeningInputTags[ElementText];
                    }
                    else
                    {
                        InputTagList = new List<ScreeningInputTagModel>();
                        ScreeningInputTags.Add(ElementText, InputTagList);
                    }
                    ScreeningInputTagModel tempInputTagModel = new ScreeningInputTagModel();
                    tempInputTagModel.Input = ValueText;
                    tempInputTagModel.TagName = ElementText;
                    InputTagList.Add(tempInputTagModel);
                    // ----Peocess Screening----
                }
            }
            foreach (string ScreeningKey in ScreeningInputTags.Keys)
            {
                List<ScreeningInputTagModel> InputTagList = ScreeningInputTags[ScreeningKey];
                IEnumerable<ScreeningInputTagModel> noduplicates = (InputTagList.Distinct());
                ScreeningInputTags[ScreeningKey] = noduplicates.ToList();
            }

            isSuccess = true;
            return isSuccess;
        }
    }
}