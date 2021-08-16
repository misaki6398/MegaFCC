using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MegaTFLT.Utilitys;

namespace MegaTFLT.Services
{
    public class HttpClientSerivce
    {
        private HttpClient client = new HttpClient();
        private JsonSerializerOptions options = new()
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        public HttpClientSerivce()
        {
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
            client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"Basic {ConfigUtility.EdqConfigModel.Secret}");
        }
        public async Task<TResponse> PostAsync<TRequest, TResponse>(string path, TRequest requestModel)
        {
            string jsonString = JsonSerializer.Serialize(requestModel);
            HttpResponseMessage response = await client.PostAsync(path, new StringContent(jsonString, Encoding.UTF8, "application/json"));
            TResponse model = default(TResponse);
            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                model = JsonSerializer.Deserialize<TResponse>(responseString, options);
            } else {
                var responseString = await response.Content.ReadAsStringAsync();
                Console.WriteLine($@"Not 200 ok detect {responseString}");
            }
            return model;
        }

    }
}