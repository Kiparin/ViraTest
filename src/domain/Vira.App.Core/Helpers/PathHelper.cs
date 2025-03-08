using System;
using System.IO;

namespace Vira.App.Core.Helpers
{
    public class PathHelper
    {
        /// <summary>
        /// Получить путь к корневой директории
        /// </summary>
        /// <returns></returns>
        public static string GetEnvironmentPath()
        {
            string currentDirectory = Environment.CurrentDirectory;
            return Path.GetPathRoot(currentDirectory) ?? currentDirectory;
        }
    }
}