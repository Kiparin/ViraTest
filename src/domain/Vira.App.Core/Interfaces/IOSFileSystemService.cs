using System.IO;

namespace Vira.App.Core.Interfaces
{
    /// <summary>
    /// Интерфейс для службы файловой системы, специфичный для настольных операционных систем
    /// </summary>
    public interface IOSFileSystemService
    {
        /// <summary>
        /// Получить файлы в указанной директории
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        string[] GetFiles(string path);

        /// <summary>
        /// Получить директории в указанной директории
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        string[] GetDirectories(string path);

        /// <summary>
        /// Получить информацию о файле
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        FileInfo GetFileInfo(string path);
    }
}