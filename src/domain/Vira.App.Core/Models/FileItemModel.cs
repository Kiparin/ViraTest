using System;

namespace Vira.App.Core.Models
{
    public class FileItemModel
    {
        public string Name { get; set; }
        public long Size { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public string Attributes { get; set; }
    }
}