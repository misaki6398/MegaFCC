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
        // Todo-----not to use static  
        public static TfMsgModel oneMsg;
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
            oneMsg = new TfMsgModel(strMx);
            oneMsg.SwallowId = oneMsg.CreateDatetime.ToString("yyyyMMddHHmmssffffff", DateTimeFormatInfo.InvariantInfo) + "I0";
            Console.WriteLine($"MsgGuid:{oneMsg.id}");
            Console.WriteLine($"CreateDatetime:{oneMsg.CreateDatetime}");
            Console.WriteLine($"SwallowId:{oneMsg.SwallowId}");
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
                    if (!isFindFrom && oneMsg.FromId == null && "Fr" == strElement)
                        isFindFrom = true;
                    else if (!isFindTo && oneMsg.ToId == null && "To" == strElement)
                        isFindTo = true;
                    else if (!isFindInstdAmt && (oneMsg.Amount == null || oneMsg.Currency == null) && "InstdAmt" == strElement)
                        isFindInstdAmt = true;
                    // ----Peocess Message----

                    if (reader.HasAttributes)
                    {
                        for (int i = 0; i < reader.AttributeCount; i++)
                        {
                            reader.MoveToAttribute(i);
                            Console.WriteLine($"{strElement}:[{i}][{reader.Name}]:{reader.Value}");

                            // ----Peocess Message----
                            if (isFindInstdAmt && oneMsg.Currency == null && "InstdAmt" == strElement && "Ccy" == reader.Name)
                                oneMsg.Currency = reader.Value;
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
                            if (oneMsg.BusinessMessageIdentifier == null)
                                oneMsg.BusinessMessageIdentifier = strValue;
                            break;
                        case "MsgDefIdr":
                            if (oneMsg.MessageDefinitionIdentifier == null)
                                oneMsg.MessageDefinitionIdentifier = strValue;
                            break;
                        case "BizSvc":
                            if (oneMsg.BusinessService == null)
                                oneMsg.BusinessService = strValue;
                            break;
                        case "CreDt":
                            if (oneMsg.OriginalCreateDate == null)
                                oneMsg.OriginalCreateDate = reader.ReadContentAsDateTime();
                            break;
                        case "BICFI":
                            if (isFindFrom && oneMsg.FromId == null && "BICFI" == strElement)
                            {
                                oneMsg.FromId = strValue;
                                isFindFrom = false;
                            }
                            else if (isFindTo && oneMsg.ToId == null && "BICFI" == strElement)
                            {
                                oneMsg.ToId = strValue;
                                isFindTo = false;
                            }
                            break;
                        case "InstdAmt":
                            if (isFindInstdAmt && oneMsg.Amount == null)
                            {
                                oneMsg.Amount = reader.ReadContentAsFloat();
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