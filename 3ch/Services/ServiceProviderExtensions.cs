using _3ch.DAL;
using Microsoft.Extensions.DependencyInjection;

namespace _3ch.Services
{
    public static class ServiceProviderExtensions
    {
        public static void AddAllServices(this IServiceCollection services)
        {
            services.AddTransient<IFileManager, FileManager>();
            services.AddTransient<UnitOfWork>();
        }
    }
}
