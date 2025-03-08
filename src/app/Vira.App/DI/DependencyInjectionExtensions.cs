using Microsoft.Extensions.DependencyInjection;

using Vira.App.DI.Configuration;

namespace Vira.App.DI
{
    /// <summary>
    /// Внедрение зависимостей
    /// </summary>
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            ViewModelCollection.RegisterViewModels(services);
            AppServiceCollection.RegisterServices(services);
            return services;
        }
    }
}