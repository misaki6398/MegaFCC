
using System.Threading.Tasks;
using System.Collections.Generic;
using MegaTFLT.Services.EdqServices;
using MegaTFLT.Utilitys;
using MegaTFLT.Models.MegaEcm.Models;
using System;
using System.Text;
using MegaTFLT.Models.MegaEcm.Repositorys;
using System.Linq;
using Oracle.ManagedDataAccess.Client;
using CommonMegaAp11.Enums;

namespace MegaTFLT
{
    class Program
    {
        static async Task Main(string[] args)
        {

            /*
                        Dictionary<string, List<string>> mxMessages = new Dictionary<string, List<string>>()
                        {
                            {"BICFI", new List<string>{"Bank of dandong","ZHANG YONG YUAN","Crimea","KERCH","SABRRUMM","Billions No.18"}},
                            {"CountryCode", new List<string>{"IRAN"}},
                            {"PortCode", new List<string>{"PORT OF ABADAN"}},
                            {"TheTestCode", new List<string>{"Oh My God"}}
                        };
                        */
            await ReadMessageFile(@"./sample.xml", MessageSource.Mx);
            await ReadMessageFile(@"./sample_NO_hit.xml", MessageSource.Mx);
            await ReadMessageFile(@"./sample_pacs.008.xml", MessageSource.Mx);
            await ReadMessageFile(@"./sample_ILoveYou200.xml", MessageSource.Mx);
            await ReadMessageFile(@"./Sample/TXN/OBS/BlueTest.xml", MessageSource.TxnObs);
            await ReadMessageFile(@"./Sample/TXN/OBS/BlueTest2.xml", MessageSource.TxnObs);

        }

        public static async Task ReadMessageFile(string filePath, MessageSource messageSource)
        {
            BaseMessagePaser myPaser = null;
            switch (messageSource)
            {
                case MessageSource.Mx:
                    myPaser = new MxPaser();
                    break;
                case MessageSource.TxnObs:
                    myPaser = new TxnPaser();
                    break;
                default:
                    break;
            }
            myPaser.ReadFromFile(filePath);
            if (myPaser != null)
                await InputMessage(myPaser);
        }
        public static async Task InputMessage(BaseMessagePaser myPaser)
        {

            //*/
            using (MegaEcmUnitOfWork _unitOfWork = new MegaEcmUnitOfWork())
            {
                try
                {
                    await _unitOfWork.TfMessagesRepository.InsertAsync(myPaser.TfMessageModel);
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
                finally
                {
                    _unitOfWork.Commit();
                }
            }

            EdqService edqService = new EdqService();
            List<TfAlertsModel> tfAlertsModels = await edqService.ProcessScreeningAsync(myPaser.ScreeningInputTags);
            using (MegaEcmUnitOfWork _unitOfWork = new MegaEcmUnitOfWork())
            {
                if (tfAlertsModels.Count() > 0)
                {
                    TfCasesModel tfCasesModel = new TfCasesModel(myPaser.TfMessageModel);
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
                    finally
                    {
                        _unitOfWork.Commit();
                    }
                }
            }
        }
    }
}
