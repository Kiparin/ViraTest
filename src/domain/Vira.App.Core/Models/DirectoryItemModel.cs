using System.Collections.ObjectModel;

using CommunityToolkit.Mvvm.ComponentModel;

namespace Vira.App.Core.Models
{
    public class DirectoryItemModel : ObservableObject
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public ObservableCollection<DirectoryItemModel> Subdirectories { get; set; } = new();
        public ObservableCollection<FileItemModel> Files { get; set; } = new();
        public bool IsNodeLoaded { get; set; }
    }
}