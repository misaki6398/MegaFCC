
using System.Threading.Tasks;
using System.Collections.Generic;
using MegaTFLT.Services.EdqServices;
using MegaTFLT.Models.MegaEcm.Models;
using System;
using System.Text;
using MegaTFLT.Models.MegaEcm.Repositorys;
using System.Linq;
using Oracle.ManagedDataAccess.Client;
using CommonMegaAp11.Enums;
using MegaTFLT.Services.Parsers;
using MegaTFLT.Utilitys;
using IBM.WMQ;
using System.Threading;
using NLog;


namespace MegaTFLT
{
    class Program
    {
        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            int threadNum = ConfigUtility.ThreadNum;
            for (int i = 0; i < threadNum; i++)
            {
                Thread thread = new Thread(new ThreadStart(ReceiveMessage))
                {
                    IsBackground = true
                };
                thread.Start();
            }
            Console.ReadLine();
        }

        public static async void ReceiveMessage()
        {
            while (true)
            {
                // Decide Message Type
                try
                {
                    BaseMessageParser messagePaser = PaserFactory.PaserType(MessageSource.TxnObs);
                    // isReadSuccess = await messagePaser.ReadFromFile(@"./Sample/TXN/OBS/BlueTest.xml");
                    bool isReadSuccess = await messagePaser.ReadFromMq(ConfigUtility.MqModel, MessageSource.TxnObs);
                    if (isReadSuccess)
                    {
                        await WriteTfMessage(messagePaser.TfMessageModel);
                    }
                    else
                    {
                        continue;
                    }

                    // Screen
                    EdqService edqService = new EdqService();
                    List<TfAlertsModel> tfAlertsModels = await edqService.ProcessScreeningAsync(messagePaser.ScreeningInputTags);
                    if (tfAlertsModels.Count() > 0)
                    {
                        await WriteTfCaseAlerts(messagePaser.TfMessageModel, tfAlertsModels);
                    }
                }
                catch (OracleException ex)
                {
                    Console.WriteLine(ex.Message);                    
                    _logger.Error(ex.ToString());
                }
                catch (MQException ex)
                {
                    Console.WriteLine(ex.Message);
                    _logger.Error(ex.ToString());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    _logger.Error(ex.ToString());
                }
            }
        }
        public static async Task WriteTfMessage(TfMessageModel model)
        {
            using (MegaEcmUnitOfWork _unitOfWork = new MegaEcmUnitOfWork())
            {
                try
                {
                    await _unitOfWork.TfMessagesRepository.InsertAsync(model);
                    _unitOfWork.Commit();
                }
                catch (OracleException ex)
                {
                    _unitOfWork.Rollback();
                    Console.WriteLine(ex.Message, ex.ToString());
                }
                catch (Exception)
                {
                    _unitOfWork.Rollback();
                }                
            }
        }

        public static async Task WriteTfCaseAlerts(TfMessageModel tfMessageModel, List<TfAlertsModel> tfAlertsModels)
        {
            using (MegaEcmUnitOfWork _unitOfWork = new MegaEcmUnitOfWork())
            {
                TfCasesModel tfCasesModel = new TfCasesModel(tfMessageModel);
                tfCasesModel.CaseStatusCode = 0;
                tfAlertsModels.ForEach(c =>
                {
                    c.AlertStatusCode = 0;
                    c.CaseId = tfCasesModel.Id;
                });

                try
                {
                    await _unitOfWork.TfCasesRepository.InsertAsync(tfCasesModel);
                    await _unitOfWork.TfAlertsRepository.InsertAsync(tfAlertsModels);
                    _unitOfWork.Commit();
                }
                catch (OracleException ex)
                {
                    _unitOfWork.Rollback();
                    Console.WriteLine(ex.Message, ex.ToString());
                }
                catch (Exception)
                {
                    _unitOfWork.Rollback();
                }                
            }
        }
    }
}
