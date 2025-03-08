using System;

using Microsoft.Extensions.DependencyInjection;

using Vira.App.Core.Interfaces;
using Vira.App.Core.Services;
using Vira.Network.Interfaces;
using Vira.Network.Services;

namespace Vira.App.DI.Configuration
{
    public class AppServiceCollection
    {
        public static void RegisterServices(IServiceCollection collection)
        {
            RegisterFileSystemServices(collection);
            RegisterNetworkServices(collection);
        }

        private static void RegisterFileSystemServices(IServiceCollection collection)
        {
            collection.AddSingleton<IOSFileSystemService, OSFileSystemService>();
            if (OperatingSystem.IsBrowser())
            {
                collection.AddSingleton<ISystemFilesService, SystemFileNetworkService>();
            }
            else
            {
                collection.AddSingleton<ISystemFilesService, SystemFilesOSService>();
            }
        }

        private static void RegisterNetworkServices(IServiceCollection collection)
        {
            collection.AddSingleton<IHttpClientService, HttpClientService>();
        }
    }
}