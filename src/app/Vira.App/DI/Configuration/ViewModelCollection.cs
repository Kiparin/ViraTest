using Microsoft.Extensions.DependencyInjection;

using Vira.App.ViewModels;

namespace Vira.App.DI.Configuration
{
    public class ViewModelCollection
    {
        public static void RegisterViewModels(IServiceCollection collection)
        {
            collection.AddTransient<MainViewModel>();
        }
    }
}