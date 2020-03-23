using FileUpload.API.Data;
using FileUpload.API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FileUpload.API.Core.ServiceExtensions
{
    public static class DatabaseServiceExtension
    {
        public static IServiceCollection AddDatabaseContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<FilesDbContext>(options =>
               options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddTransient<IFileRepository, FileRepository>();

            return services;
        }
    }
}
