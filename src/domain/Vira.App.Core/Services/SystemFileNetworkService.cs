using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

using Vira.App.Core.Extensions;
using Vira.App.Core.Interfaces;
using Vira.App.Core.Models;
using Vira.Network.Interfaces;
using Vira.Network.Utilities;

namespace Vira.App.Core.Services
{
    public class SystemFileNetworkService : ISystemFilesService
    {
        private IHttpClientService _httpClientService;

        public SystemFileNetworkService(IHttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
        }

        public async Task<DirectoryItemModel> GetDirectoryAsync(string path)
        {
            var url = "https://localhost:7032/api/filesystem/directories";// TODO <--- здесь должен быть конфиг
            var jsonContent = JsonConverter.Serialize(path);

            try
            {
                var response = await _httpClientService.PostAsync(url, jsonContent);
                if (!string.IsNullOrEmpty(response))
                {
                    return JsonConverter.Deserialize<DirectoryItemModel>(response);
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

        
        public async Task GetSubdirectoryAsync(DirectoryItemModel directory)
        {
            var url = "https://localhost:7032/api/filesystem/subdirectories";// TODO <--- здесь должен быть конфиг
            var jsonContent = JsonConverter.Serialize(directory);

            try
            {
                var response = await _httpClientService.PostAsync(url, jsonContent);
                if (!string.IsNullOrEmpty(response))
                {
                    var result = JsonConverter.Deserialize<DirectoryItemModel>(response);
                    AddData(directory, result.Subdirectories, result.Files);
                    directory.IsNodeLoaded = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void AddData(DirectoryItemModel directory, ObservableCollection<DirectoryItemModel> subdirectories, ObservableCollection<FileItemModel> files)
        {
            directory.Subdirectories.AddRange(subdirectories);
            directory.Files.AddRange(files);
        }
    }
}