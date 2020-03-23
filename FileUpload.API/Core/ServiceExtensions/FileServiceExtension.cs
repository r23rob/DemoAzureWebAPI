using FileUpload.API.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FileUpload.API.Core.ServiceExtensions
{
    public static class FileServiceExtension
    {
        public static IServiceCollection AddFileService(this IServiceCollection services)
        {
            services.AddTransient<IFileService, FileService>();
            return services;
        }
    }
}
