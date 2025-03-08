using System.IO;

using Vira.App.Core.Interfaces;

namespace Vira.App.Core.Services
{
    public class OSFileSystemService : IOSFileSystemService
    {
        public string[] GetDirectories(string path)
        {
            return Directory.GetDirectories(path);
        }

        public FileInfo GetFileInfo(string path)
        {
            return new FileInfo(path);
        }

        public string[] GetFiles(string path)
        {
            return Directory.GetFiles(path);
        }
    }
}