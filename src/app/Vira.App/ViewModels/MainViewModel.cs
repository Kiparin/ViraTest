using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Vira.Network;

using Vira.App.Core.Models;
using Vira.App.Core.Services;
using Vira.App.ViewModels.BaseModel;
using Vira.Network.Service;
using Newtonsoft.Json;
using Vira.App.Core.Extensions;

namespace Vira.App.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    private SystemFilesService _fileService = new();
    private SystemFileNetworkService _networkFileService = new();


    [ObservableProperty]
    private ObservableCollection<DirectoryItemModel> _root = new();

    [ObservableProperty]
    private ObservableCollection<FileItemModel> _files = new();

    [ObservableProperty]
    private DirectoryItemModel? _selectDirectory;

    public IAsyncRelayCommand<DirectoryItemModel> LoadSubdirectoriesAsyncCommand { get; }


    public MainViewModel()
    {
        if (OperatingSystem.IsBrowser())
        {
            //test 

            Task.Run(async () =>
            {
                var result = await _networkFileService.GetDirectoryAsync("C:\\");
                Root.Add(JsonConvert.DeserializeObject<DirectoryItemModel>(result));
            });
        }
        else
        {
            Root.Add(_fileService.GetRootDirectory("C:\\").Result);
        }

        LoadSubdirectoriesAsyncCommand = new AsyncRelayCommand<DirectoryItemModel>(LoadSubdirectoriesAsync);
    }

    partial void OnSelectDirectoryChanging(DirectoryItemModel? value)
    {
        if (value != null)
        {
            LoadSubdirectoriesCommand.Execute(value);
        }
    }

    [RelayCommand]
    private async Task LoadSubdirectoriesAsync(DirectoryItemModel directory)
    {
        if (!directory.IsNodeLoaded)
        {
            if (OperatingSystem.IsBrowser())
            {
                var result =  await _networkFileService.LoadSubdirectoriesAsync(JsonConvert.SerializeObject(directory));
                if (!string.IsNullOrEmpty(result))
                {
                    var tmp = JsonConvert.DeserializeObject<DirectoryItemModel>(result);
                    directory.Subdirectories.AddRange(tmp.Subdirectories);
                    directory.Files.AddRange(tmp.Files);
                }
            }
            else
            {
                await _fileService.LoadDirectory(directory);
                await _fileService.LoadFiles(directory);
            }
        }

        await UpdateFileGridAsync(directory);
    }

    private async Task UpdateFileGridAsync(DirectoryItemModel directory)
    {
        Files.Clear();
        foreach (var file in directory.Files)
        {
            Files.Add(file);
        }

       await Task.CompletedTask;
    }
}
