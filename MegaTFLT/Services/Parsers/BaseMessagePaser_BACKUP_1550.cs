using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using System.Xml;
using MegaTFLT.MegaEcm.Models;
using MegaTFLT.Models.MegaEcm.Models;
using System.Linq;
using MegaTFLT.Models.MQ;
using IBM.WMQ;
using CommonMegaAp11.Enums;

namespace MegaTFLT.Services.Parsers
{
    public abstract class BaseMessageParser
    {
        public TfMessageModel TfMessageModel { get; protected set; }
        public Dictionary<string, List<ScreeningInputTagModel>> ScreeningInputTags { get; protected set; }

        public virtual async Task<bool> ReadFromFile(string filePath)
        {
            bool isSuccess = false;
            Console.WriteLine("-------------------------");
            Console.WriteLine(value: $"ReadFromFile:{filePath}");
            Console.WriteLine("-------------------------");
            try
            {
                string Text = File.ReadAllText(filePath);
                isSuccess = await this.ReadFromText(Text);
            }
            catch (DirectoryNotFoundException ex)
            {
                Console.WriteLine("DirectoryNotFoundException");
                Console.WriteLine(ex.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {

            }
            return isSuccess;
        }

        public virtual async Task<bool> ReadFromMq(MqModel model,  MessageSource messageSource)
        {
<<<<<<< HEAD
            bool isSuccess = false;           
=======
            bool isSuccess = false;
            Console.WriteLine("-------------------------");
            Console.WriteLine(value: $"ReadFromQueueManager:{model.MqManagerName},{messageSource}:{model.LocalQueueName[messageSource]}");
            Console.WriteLine("-------------------------");
>>>>>>> aa504d5 (Multi Queue)
            MqSerivce mqSerivce = null;
            try
            {
                mqSerivce = new MqSerivce(model);
                string Text = mqSerivce.ReceiveMessage(model.LocalQueueName[messageSource]);
                if (!Text.Equals(string.Empty))
                {
                    Console.WriteLine("-------------------------");
                    Console.WriteLine(value: $"ReadFromQueueManager:{model.MqManagerName}");
                    Console.WriteLine("-------------------------");
                    isSuccess = await this.ReadFromText(Text);
                }
            }
            catch (MQException ex)
            {
                Console.WriteLine("MQException");
                Console.WriteLine(ex.ToString());
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
            finally
            {
                mqSerivce.Disconnect();
            }
            return isSuccess;
        }

        public abstract Task<bool> ReadFromText(string text);

        public Dictionary<string, List<T>> DistinctDictionary<T>(Dictionary<string, List<T>> inputs)
        {
            foreach (string key in inputs.Keys)
            {
                inputs[key] = inputs[key].Distinct().ToList();
            }
            return inputs;
        }
    }
}