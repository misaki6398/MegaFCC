using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MegaTFLT.Utilitys;

namespace MegaTFLT.Services
{
    public class HttpClientSerivce : IDisposable
    {
        private readonly HttpClient _client;
        private bool _disposed = false;

        private readonly JsonSerializerOptions _options;

        public HttpClientSerivce()
        {
            _client = new HttpClient();
            _options = new()
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };
            _client.DefaultRequestHeaders.Add("Accept", "application/json");
            _client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
            _client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"Basic {ConfigUtility.EdqConfigModel.Secret}");
        }
        public async Task<TResponse> PostAsync<TRequest, TResponse>(string path, TRequest requestModel)
        {
            string jsonString = JsonSerializer.Serialize(requestModel);
            HttpResponseMessage response = await _client.PostAsync(path, new StringContent(jsonString, Encoding.UTF8, "application/json"));
            TResponse model = default(TResponse);
            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                model = JsonSerializer.Deserialize<TResponse>(responseString, _options);
            }
            else
            {
                var responseString = await response.Content.ReadAsStringAsync();
                Console.WriteLine($@"Not 200 ok detect {responseString}");
            }

            return model;
        }

        public void Dispose()
        {
            dispose(true);
            GC.SuppressFinalize(this);
        }

        private void dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _client.Dispose();
                }
                _disposed = true;
            }
        }

        ~HttpClientSerivce()
        {
            dispose(false);
        }

    }
}