

using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

using Vira.Network.Responces;

namespace Vira.Network.Service
{
    public class SystemFileNetworkService
    {

        private static readonly HttpClient client = new HttpClient();


        public SystemFileNetworkService()
        {

        }

        public async Task<string> GetDirectoryAsync(string path)
        {

            var url = "https://localhost:7032/api/filesystem/directories";

            var content = new StringContent(JsonConvert.SerializeObject(path), Encoding.UTF8, "application/json");

            try
            {
                var response = await client.PostAsync(url, content);
                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<FileSystemResponce>(responseString);
                    return result.Data;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<string> LoadSubdirectoriesAsync(string directoryItemModelJson)
        {
            var url = "https://localhost:7032/api/filesystem/subdirectories";

            var content = new StringContent(JsonConvert.SerializeObject(directoryItemModelJson), Encoding.UTF8, "application/json");

            try
            {
                var response = await client.PostAsync(url, content);
                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<FileSystemResponce>(responseString);
                    return result.Data;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
