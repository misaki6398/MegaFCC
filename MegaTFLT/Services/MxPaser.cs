using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml;
using MegaTFLT.Models.MegaEcm.Models;

namespace MegaTFLT.Utilitys
{
    public static class MxPaser
    {
        // Todo-----not to use static  , critical section 愛你, 不要用匈牙利命名法XD
        public static TfMessageModel TfMessageModel;
        // Todo-----

        public static Dictionary<string, List<MxInputTagModel>> ReadFromFile(string strPath)
        {
            string strMx = File.ReadAllText(strPath);
            //Console.WriteLine($"{strMx}");
            Console.WriteLine("-------------------------");
            Console.WriteLine(value: $"ReadFromFile:{strPath}");
            Console.WriteLine("-------------------------");
            return MxPaser.ReadFromText(strMx);
        }
        public static Dictionary<string, List<MxInputTagModel>> ReadFromText(string strMx)
        {
            XmlReader reader = XmlReader.Create(new StringReader(strMx));
            Dictionary<string, List<MxInputTagModel>> mxMessages = new Dictionary<string, List<MxInputTagModel>>();
            string strElement = "";
            string strValue = "";

            // ----Peocess Message----
            TfMessageModel = new TfMessageModel(strMx);
            TfMessageModel.SwallowId = TfMessageModel.CreateDatetime.ToString("yyyyMMddHHmmssffffff", DateTimeFormatInfo.InvariantInfo) + "I0";
            Console.WriteLine($"MsgGuid:{TfMessageModel.id}");
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
                    strElement = reader.Name;

                    // ----Peocess Message----
                    if (!isFindFrom && TfMessageModel.FromId == null && "Fr" == strElement)
                        isFindFrom = true;
                    else if (!isFindTo && TfMessageModel.ToId == null && "To" == strElement)
                        isFindTo = true;
                    else if (!isFindInstdAmt && (TfMessageModel.Amount == null || TfMessageModel.Currency == null) && "InstdAmt" == strElement)
                        isFindInstdAmt = true;
                    // ----Peocess Message----

                    if (reader.HasAttributes)
                    {
                        for (int i = 0; i < reader.AttributeCount; i++)
                        {
                            reader.MoveToAttribute(i);
                            Console.WriteLine($"{strElement}:[{i}][{reader.Name}]:{reader.Value}");

                            // ----Peocess Message----
                            if (isFindInstdAmt && TfMessageModel.Currency == null && "InstdAmt" == strElement && "Ccy" == reader.Name)
                                TfMessageModel.Currency = reader.Value;
                            // ----Peocess Message----

                        }
                        reader.MoveToElement();
                    }
                }
                else if (reader.NodeType == XmlNodeType.Text && (reader.HasValue))
                {
                    strValue = reader.Value;
                    Console.WriteLine($"{strElement}:{strValue}");

                    // ----Peocess Message----
                    switch (strElement)
                    {
                        case "BizMsgIdr":
                            if (TfMessageModel.BusinessMessageIdentifier == null)
                                TfMessageModel.BusinessMessageIdentifier = strValue;
                            break;
                        case "MsgDefIdr":
                            if (TfMessageModel.MessageDefinitionIdentifier == null)
                                TfMessageModel.MessageDefinitionIdentifier = strValue;
                            break;
                        case "BizSvc":
                            if (TfMessageModel.BusinessService == null)
                                TfMessageModel.BusinessService = strValue;
                            break;
                        case "CreDt":
                            if (TfMessageModel.OriginalCreateDate == null)
                                TfMessageModel.OriginalCreateDate = reader.ReadContentAsDateTime();
                            break;
                        case "BICFI":
                            if (isFindFrom && TfMessageModel.FromId == null && "BICFI" == strElement)
                            {
                                TfMessageModel.FromId = strValue;
                                isFindFrom = false;
                            }
                            else if (isFindTo && TfMessageModel.ToId == null && "BICFI" == strElement)
                            {
                                TfMessageModel.ToId = strValue;
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
                    List<MxInputTagModel> lstMxInputTag = null;
                    if (mxMessages.ContainsKey(strElement))
                    {
                        lstMxInputTag = mxMessages[strElement];
                    }
                    else
                    {
                        lstMxInputTag = new List<MxInputTagModel>();
                        mxMessages.Add(strElement, lstMxInputTag);
                    }
                    MxInputTagModel tempMxInputTagModel = new MxInputTagModel();
                    tempMxInputTagModel.Input = strValue;
                    tempMxInputTagModel.TagName = strElement;
                    lstMxInputTag.Add(tempMxInputTagModel);
                    // ----Peocess Screening----
                }
            }
            //Console.WriteLine($"{mxMessages}");
            return mxMessages;
        }
    }
}