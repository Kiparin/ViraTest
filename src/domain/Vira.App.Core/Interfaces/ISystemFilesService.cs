using System.Threading.Tasks;

using Vira.App.Core.Models;

namespace Vira.App.Core.Interfaces
{
    /// <summary>
    /// Интерфейс для службы файловой системы
    /// </summary>
    public interface ISystemFilesService
    {
        /// <summary>
        /// Получить директорию по указанному пути
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        Task<DirectoryItemModel> GetDirectoryAsync(string path);

        /// <summary>
        /// Получить поддиректории указанной директории
        /// </summary>
        /// <param name="directory"></param>
        /// <returns></returns>
        Task GetSubdirectoryAsync(DirectoryItemModel directory);
    }
}