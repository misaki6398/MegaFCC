
using System.Threading.Tasks;
using System.Collections.Generic;
using MegaTFLT.Services.EdqServices;
using MegaTFLT.Utilitys;
using MegaTFLT.Models.MegaEcm.Models;
using System;
using System.Text;

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
            mxMessages = MxPaser.ReadFromFile(@"./MegaTFLT/sample_pacs.008.xml");

            EdqService edqService = new EdqService();

            await edqService.ProcessScreeningAsync(mxMessages);

           



        }

    }
}
