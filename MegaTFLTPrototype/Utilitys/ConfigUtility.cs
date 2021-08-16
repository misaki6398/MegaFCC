using System.IO;
using MegaTFLT.Models.Edq.Models;
using Microsoft.Extensions.Configuration;

namespace MegaTFLT.Utilitys
{
    public static class ConfigUtility
    {
        public static IConfigurationBuilder builder = new ConfigurationBuilder ()
            .SetBasePath (Directory.GetCurrentDirectory ())
            .AddJsonFile ("appsettings.json");

        public static IConfiguration Configuration = builder.Build ();
        public static EdqConfigModel EdqConfigModel = Configuration.GetSection("EdqConfig").Get<EdqConfigModel>();
        public static string MegaEcmConnectionString = Configuration.GetSection("ConnectionStrings").GetValue<string>("MegaEcm");
    }
}