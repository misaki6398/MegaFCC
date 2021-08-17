using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using MegaTFLT.MegaEcm.Models;
using MegaTFLT.Models.Edq.Models;
using MegaTFLT.Models.MegaEcm.Repositorys;
using Microsoft.Extensions.Configuration;

namespace MegaTFLT.Utilitys
{
    public static class ConfigUtility
    {
        public static IConfigurationBuilder builder = new ConfigurationBuilder()
            .SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

        public static IConfiguration Configuration = builder.Build();
        public static EdqConfigModel EdqConfigModel = Configuration.GetSection("EdqConfig").Get<EdqConfigModel>();
        public static string MegaEcmConnectionString = Configuration.GetSection("ConnectionStrings").GetValue<string>("MegaEcm");
        public static Dictionary<string, TfScreenConfigModel> ScreenConfigs;

        static ConfigUtility()
        {
            using (MegaEcmUnitOfWork _unitOfWork = new MegaEcmUnitOfWork())
            {
                IEnumerable<TfScreenConfigModel> screenConfigs = _unitOfWork.ScreenConfigRepository.Query();
                ScreenConfigs = screenConfigs.ToDictionary(c => c.TagName);
            }
        }
    }
}