using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Vira.App.Core.Helpers;
using Vira.App.Core.Interfaces;
using Vira.App.Core.Models;
using Vira.App.ViewModels.BaseModel;

namespace Vira.App.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    private readonly ISystemFilesService _systemFileService;

    [ObservableProperty]
    private ObservableCollection<DirectoryItemModel> _root = new();

    [ObservableProperty]
    private ObservableCollection<FileItemModel> _files = new();

    [ObservableProperty]
    private DirectoryItemModel? _selectDirectory;

    public IAsyncRelayCommand<DirectoryItemModel> LoadSubdirectoriesAsyncCommand { get; }

    public MainViewModel(ISystemFilesService systemFilesService)
    {
        _systemFileService = systemFilesService;

        InitializeAsync();

        LoadSubdirectoriesAsyncCommand = new AsyncRelayCommand<DirectoryItemModel>(LoadSubdirectoriesAsync);
    }

    private async Task InitializeAsync()
    {
        try
        {
            var rootDirectory = await _systemFileService.GetDirectoryAsync(PathHelper.GetEnvironmentPath());
            if (rootDirectory != null)
            {
                Root.Add(rootDirectory);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
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
        try
        {
            if (!directory.IsNodeLoaded)
            {
                await _systemFileService.GetSubdirectoryAsync(directory);
            }

            await UpdateFileGrid(directory);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private Task UpdateFileGrid(DirectoryItemModel directory)
    {
        Files.Clear();
        foreach (var file in directory.Files)
        {
            Files.Add(file);
        }

        return Task.CompletedTask;
    }
}