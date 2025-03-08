using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Vira.Network.Interfaces;

namespace Vira.Network.Services
{
    public class HttpClientService : IHttpClientService
    {
        private readonly HttpClient _httpClient = new HttpClient();

        public async Task<string> PostAsync(string url, string jsonContent)
        {
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            try
            {
                var response = await _httpClient.PostAsync(url, content);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}