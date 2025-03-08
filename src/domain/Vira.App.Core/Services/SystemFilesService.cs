using System.IO;
using System;
using Vira.App.Core.Models;
using System.Threading.Tasks;

namespace Vira.App.Core.Services
{
    public class SystemFilesService
    {
        /// <summary>
        /// Получение информации о корневом каталоге
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public async Task<DirectoryItemModel> GetRootDirectory(string path)
        {
            var directory = AddSubdirectory(path);
            await LoadDirectory(directory);
            await LoadFiles(directory);

            return directory;
        }

        /// <summary>
        /// Получение информации о файлах
        /// </summary>
        /// <param name="directory"></param>
        public async Task LoadFiles(DirectoryItemModel directory)
        {
            try
            {
                foreach (var file in Directory.GetFiles(directory.Path))
                {
                    var fileInfo = new FileInfo(file);
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
        }

        /// <summary>
        /// Получение информации о подкаталогах
        /// </summary>
        /// <param name="directory"></param>
        public async Task LoadDirectory(DirectoryItemModel directory)
        {
            try
            {
                foreach (var dir in Directory.GetDirectories(directory.Path))
                {
                    directory.Subdirectories.Add(AddSubdirectory(dir));
                }
                directory.IsNodeLoaded = true;
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Skipped: {0}", directory.Path);
            }
        }

        /// <summary>
        /// Добавление подкаталога
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private DirectoryItemModel AddSubdirectory(string path)
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
