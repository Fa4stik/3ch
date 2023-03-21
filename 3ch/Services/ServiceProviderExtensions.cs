namespace _3ch.Services
{
    public static class ServiceProviderExtensions
    {
        public static void AddFileManager(this IServiceCollection services)
        {
            services.AddTransient<FileManager>();
        }
    }
}
