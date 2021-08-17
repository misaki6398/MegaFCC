
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
            Dictionary<string, List<MxInputTagModel>> mxMessages = null;//MxPaser.ReadFromFile(@"sample.xml");
            mxMessages = MxPaser.ReadFromFile(@"./sample_pacs.008.xml");

            EdqService edqService = new EdqService();
            List<TfAlertsModel> tfAlertsModels = await edqService.ProcessScreeningAsync(mxMessages);
            using (MegaEcmUnitOfWork _unitOfWork = new MegaEcmUnitOfWork())
            {
                if (tfAlertsModels.Count() > 0)
                {
                    TfCasesModel tfCasesModel = new TfCasesModel(MxPaser.TfMsgModel);
                    tfCasesModel.CaseStatus = "New Case";
                    tfCasesModel.CaseStatusCode = 0;
                    tfAlertsModels.ForEach(c =>
                    {
                        c.AlertStatus = "New Case";
                        c.AlertStatusCode = 0;
                        c.CaseId = tfCasesModel.Id;
                    });

                    try
                    {
                        await _unitOfWork.TfMessagesRepository.InsertAsync(MxPaser.TfMsgModel);
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
