using System.Linq;

using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;

using Microsoft.Extensions.DependencyInjection;

using Vira.App.DI;
using Vira.App.ViewModels;
using Vira.App.Views;
using Vira.App.Views.BaseWindow;

namespace Vira.App;

public partial class App : Application
{
    /// <summary>
    /// Сервис провайдер Dependency Injection
    /// </summary>
    public static ServiceProvider ServiceProvider { get; private set; }

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
        RegisterAppServices();
    }

    public override void OnFrameworkInitializationCompleted()
    {
        var vm = ServiceProvider.GetService<MainViewModel>();
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            // Avoid duplicate validations from both Avalonia and the CommunityToolkit.
            // More info: https://docs.avaloniaui.net/docs/guides/development-guides/data-validation#manage-validationplugins
            DisableAvaloniaDataAnnotationValidation();
            desktop.MainWindow = new MainWindow
            {
                DataContext = vm
            };
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = new MainView
            {
                DataContext = vm
            };
        }

        base.OnFrameworkInitializationCompleted();
    }

    private void DisableAvaloniaDataAnnotationValidation()
    {
        // Get an array of plugins to remove
        var dataValidationPluginsToRemove =
            BindingPlugins.DataValidators.OfType<DataAnnotationsValidationPlugin>().ToArray();

        // remove each entry found
        foreach (var plugin in dataValidationPluginsToRemove)
        {
            BindingPlugins.DataValidators.Remove(plugin);
        }
    }

    private void RegisterAppServices()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddApplicationServices();
        ServiceProvider = serviceCollection.BuildServiceProvider();
    }
}