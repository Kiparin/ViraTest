using System;
using System.IO;
using System.Threading.Tasks;

using Vira.App.Core.Interfaces;
using Vira.App.Core.Models;

namespace Vira.App.Core.Services
{
    public class SystemFilesOSService : ISystemFilesService
    {
        private IOSFileSystemService _osFileSystemService;

        public SystemFilesOSService(IOSFileSystemService oSFileSystemService)
        {
            _osFileSystemService = oSFileSystemService;
        }

        public async Task<DirectoryItemModel> GetDirectoryAsync(string path)
        {
            var directory = CreateDirectory(path);

            await GetSubdirectoryAsync(directory);

            return directory;
        }

        public async Task GetSubdirectoryAsync(DirectoryItemModel directory)
        {
            await LoadDirectory(directory);
            await LoadFiles(directory);
        }

        public Task LoadDirectory(DirectoryItemModel directory)
        {
            try
            {
                foreach (var dir in _osFileSystemService.GetDirectories(directory.Path))
                {
                    directory.Subdirectories.Add(CreateDirectory(dir));
                }
                directory.IsNodeLoaded = true;
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Skipped: {0}", directory.Path);
            }

            return Task.CompletedTask;
        }

        private Task LoadFiles(DirectoryItemModel directory)
        {
            try
            {
                foreach (var file in _osFileSystemService.GetFiles(directory.Path))
                {
                    var fileInfo = _osFileSystemService.GetFileInfo(file);
                    directory.Files.Add(new FileItemModel
                    {
                        Name = fileInfo.Name,
                        Size = fileInfo.Length,
                        CreationDate = fileInfo.CreationTime,
                        ModificationDate = fileInfo.LastWriteTime,
                        Attributes = fileInfo.Attributes.ToString()
                    });
                }
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Skipped: {0}", directory.Path);
            }

            return Task.CompletedTask;
        }

        private DirectoryItemModel CreateDirectory(string path)
        {
            var directory = new DirectoryItemModel
            {
                Name = Path.GetFileName(path),
                Path = path
            };

            if (string.IsNullOrEmpty(directory.Name))
            {
                directory.Name = path;
            }

            return directory;
        }
    }
}