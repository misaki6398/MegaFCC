using IBM.WMQ;
using MegaTFLT.Models.MQ;
// using IBM.WMQAX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace MegaTFLT.Services
{
    public class MqSerivce
    {
        private MQQueueManager QueueManager;

        private int Encoding;
        private string ApplicationName;
        private int CharacterSet;
        private string UserName;

        public MqSerivce(MqModel model)
        {
            MQEnvironment.Hostname = model.MqServerIp;
            MQEnvironment.Channel = model.MqChannelName;
            MQEnvironment.Port = model.MqServerPort;
            UserName = model.UserName;
            //MQEnvironment.UserId = model.UserName;
            //MQEnvironment.Password =  model.Password;
            Environment.SetEnvironmentVariable("MQCCSID", model.MqManagerCcsId);
            Encoding = model.Encoding;
            ApplicationName = model.ApplicationName;
            CharacterSet = model.CharacterSet;
            QueueManager = new MQQueueManager(model.MqManagerName);
        }

        public int GetCurrentDepth(string queueName)
        {
            int openOptions = MQC.MQOO_INPUT_AS_Q_DEF | MQC.MQOO_OUTPUT | MQC.MQOO_INQUIRE | MQC.MQOO_BROWSE;

            try
            {
                using (MQQueue receiveQueue = QueueManager.AccessQueue(queueName, openOptions))
                {
                    MQQueue receiveBaseQueue = receiveQueue;
                    //Console.WriteLine($"{receiveQueue.QueueType}{receiveQueue.BaseQueueName}");
                    if (receiveQueue.QueueType == MQC.MQQT_ALIAS)
                        receiveBaseQueue = QueueManager.AccessQueue(receiveQueue.BaseQueueName, openOptions);
                    return receiveBaseQueue.CurrentDepth;
                }
            }
            catch (MQException ex)
            {
                // _logger.Error(ex.ToString());
                return -1;
            }
        }

        public void PutMessage(string queueName, string messageString)
        {
            MQQueue putQueue = QueueManager.AccessQueue(queueName, 10256 | MQC.MQOO_INQUIRE);

            MQMessage message = new MQMessage
            {
                UserId = UserName,
                Encoding = this.Encoding,
                CharacterSet = CharacterSet,
                Format = "MQSTR",
                CorrelationId = MQC.MQCI_NONE,
                ApplicationIdData = "W",
                PutDateTime = DateTime.Now,
                PutApplicationName = ApplicationName,
                PutApplicationType = 11,
                MessageId = System.Text.Encoding.UTF8.GetBytes("M" + DateTime.Now.ToShortDateString() + "Q"),
                MessageType = 8,
                Report = MQC.MQRO_COPY_MSG_ID_TO_CORREL_ID
            };

            MQPutMessageOptions putMessageOptions = new MQPutMessageOptions();

            try
            {
                message.WriteString(messageString);
                if (putQueue.InhibitPut.Equals(MQC.MQQA_PUT_ALLOWED))
                {
                    putQueue.Put(message, putMessageOptions);
                }
                else
                {
                    throw new Exception("Can not put message");
                }
            }
            catch (MQException ex)
            {
                // _logger.Error(ex.ToString());
                throw;
            }
            finally
            {
                if (putQueue.OpenStatus)
                    putQueue.Close();
            }

        }

        public string ReceiveMessage(string queueName)
        {
            int openOptions = MQC.MQOO_INPUT_AS_Q_DEF | MQC.MQOO_OUTPUT | MQC.MQOO_INQUIRE | MQC.MQOO_BROWSE;
            MQGetMessageOptions mqGetMessageOptions = new MQGetMessageOptions();
            //    MQC.MQGMO_WAIT; // Wait for the message to arrive.
            mqGetMessageOptions.Options = MQC.MQGMO_SYNCPOINT //錯誤會到回去
                | MQC.MQGMO_WAIT; //等Message
            //mqGetMessageOptions.Options = MQC.MQGMO_FAIL_IF_QUIESCING
            //    | MQC.MQGMO_WAIT; //等Message
            mqGetMessageOptions.MatchOptions = MQC.MQMO_NONE;

            try
            {
                MQQueue receiveQueue = QueueManager.AccessQueue(queueName, openOptions);
                mqGetMessageOptions.WaitInterval = 5000;
                MQMessage mqMessage = new MQMessage();

                //if receiveQueue is an Alias queue definition, Can't get CurrentDepth
                MQQueue receiveBaseQueue = receiveQueue;
                if (receiveQueue.QueueType == MQC.MQQT_ALIAS)
                    receiveBaseQueue = QueueManager.AccessQueue(receiveQueue.BaseQueueName, openOptions);

                if (receiveBaseQueue.CurrentDepth > 0 && receiveQueue.InhibitGet.Equals(MQC.MQQA_GET_ALLOWED))
                {
                    receiveQueue.Get(mqMessage, mqGetMessageOptions);
                    string message = mqMessage.ReadString(mqMessage.DataLength);
                    receiveQueue.Close();
                    return message;
                }
                else
                {
                    receiveQueue.Close();
                    return string.Empty;
                }
            }
            catch (MQException ex)
            {
                if (ex.ReasonCode.CompareTo(MQC.MQRC_NO_MSG_AVAILABLE) != 0)
                {
                    // _logger.Error(ex.ToString());
                }
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Backout()
        {
            try
            {
                QueueManager.Backout();
            }
            catch (MQException ex)
            {
                // _logger.Error(ex.ToString());
            }
        }

        public void Disconnect()
        {
            try
            {
                if (QueueManager.IsOpen)
                {
                    QueueManager.Close();
                }
                if (QueueManager.IsConnected)
                {
                    QueueManager.Disconnect();
                }

            }
            catch (MQException ex)
            {
                // _logger.Error(ex.ToString());
            }
        }
    }
}