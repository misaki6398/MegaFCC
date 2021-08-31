using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using MegaTFLT.MegaEcm.Models;
using MegaTFLT.Models.Edq.Models;
using MegaTFLT.Models.MegaEcm.Repositorys;
using MegaTFLT.Models.MQ;
using Microsoft.Extensions.Configuration;

namespace MegaTFLT.Utilitys
{
    public static class ConfigUtility
    {
        public static IConfigurationBuilder builder = new ConfigurationBuilder()
            .SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

        public static IConfiguration Configuration = builder.Build();
        public static readonly EdqConfigModel EdqConfigModel = Configuration.GetSection("EdqConfig").Get<EdqConfigModel>();
        public static readonly string MegaEcmConnectionString = Configuration.GetSection("ConnectionStrings").GetValue<string>("MegaEcm");
        public static readonly int ThreadNum = Configuration.GetValue<int>("ThreadNum");
        public static readonly string FccmAtomicSchema = Configuration.GetSection("ConnectionStrings").GetValue<string>("FccmAtomicSchema");
        public static readonly MqModel MqModel = Configuration.GetSection("MqConfig").Get<MqModel>();
        public static readonly Dictionary<string, TfScreenConfigModel> ScreenConfigs;
        public static readonly Dictionary<string, TfScreenSubConfigModel> ScreenSubConfigs;

        static ConfigUtility()
        {
            using (MegaEcmUnitOfWork _unitOfWork = new MegaEcmUnitOfWork())
            {
                IEnumerable<TfScreenConfigModel> screenConfigs = _unitOfWork.ScreenConfigRepository.Query();
                ScreenConfigs = screenConfigs.ToDictionary(c => new TfScreenConfigKeyModel(c.MessageSourceCode, c.TagName).ToString());
                IEnumerable<TfScreenSubConfigModel> screenSubConfigs = _unitOfWork.ScreenSubConfigRepository.Query();
                ScreenSubConfigs = screenSubConfigs.ToDictionary(c => new TfScreenConfigKeyModel(c.MessageSourceCode, c.TagName, c.EntityType).ToStringWithSubType());
            }
        }
    }
}