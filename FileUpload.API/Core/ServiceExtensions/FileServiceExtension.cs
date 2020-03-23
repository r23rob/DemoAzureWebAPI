using FileUpload.API.Data;
using FileUpload.API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
