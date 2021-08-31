using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml;
using System.Linq;
using MegaTFLT.Models.MegaEcm.Models;
using MegaTFLT.MegaEcm.Models;
using CommonMegaAp11.Enums;
using System.Threading.Tasks;

namespace MegaTFLT.Services.Parsers
{
    public class MxParser : BaseMessageParser
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

            // ----Process Message----
            TfMessageModel = new TfMessageModel(text, "MX");
            TfMessageModel.SwallowId = TfMessageModel.CreateDatetime.ToString("yyyyMMddHHmmssffffff", DateTimeFormatInfo.InvariantInfo) + "I0";
            Console.WriteLine($"MessageGuid:{TfMessageModel.Id}");
            Console.WriteLine($"CreateDatetime:{TfMessageModel.CreateDatetime}");
            Console.WriteLine($"SwallowId:{TfMessageModel.SwallowId}");
            bool isFindFrom = false;
            bool isFindTo = false;
            bool isFindInstdAmt = false;
            // ----Process Message----

            while (await reader.ReadAsync())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    ElementText = reader.Name;

                    // ----Process Message----
                    if (!isFindFrom && TfMessageModel.FromId == null && "Fr" == ElementText)
                        isFindFrom = true;
                    else if (!isFindTo && TfMessageModel.ToId == null && "To" == ElementText)
                        isFindTo = true;
                    else if (!isFindInstdAmt && (TfMessageModel.Amount == null || TfMessageModel.Currency == null) && "InstdAmt" == ElementText)
                        isFindInstdAmt = true;
                    // ----Process Message----

                    if (reader.HasAttributes)
                    {
                        for (int i = 0; i < reader.AttributeCount; i++)
                        {
                            reader.MoveToAttribute(i);
                            Console.WriteLine($"{ElementText}:[{i}][{reader.Name}]:{reader.Value}");

                            // ----Process Message----
                            if (isFindInstdAmt && TfMessageModel.Currency == null && "InstdAmt" == ElementText && "Ccy" == reader.Name)
                                TfMessageModel.Currency = reader.Value;
                            // ----Process Message----

                        }
                        reader.MoveToElement();
                    }
                }
                else if (reader.NodeType == XmlNodeType.Text && (reader.HasValue))
                {
                    ValueText = reader.Value;
                    Console.WriteLine($"{ElementText}:{ValueText}");

                    // ----Process Message----
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
                    // ----Process Message----

                    // ----Process Screening----
                    List<ScreeningInputTagModel> InputTagList = null;
                    string tfScreenConfigKey = new TfScreenConfigKeyModel(MessageSource.Mx, ElementText).ToString();
                    if (ScreeningInputTags.ContainsKey(tfScreenConfigKey))
                    {
                        InputTagList = ScreeningInputTags[tfScreenConfigKey];
                    }
                    else
                    {
                        InputTagList = new List<ScreeningInputTagModel>();
                        ScreeningInputTags.Add(tfScreenConfigKey, InputTagList);
                    }
                    ScreeningInputTagModel tempInputTagModel = new ScreeningInputTagModel();
                    tempInputTagModel.Input = ValueText;
                    tempInputTagModel.TagName = ElementText;
                    InputTagList.Add(tempInputTagModel);
                    // ----Process Screening----
                }
            }

            this.DistinctDictionary(ScreeningInputTags);

            isSuccess = true;
            return isSuccess;
        }
    }
}