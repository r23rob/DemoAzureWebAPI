using FileUpload.API.Core.ServiceExtensions;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FileUpload.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddMvcCore()

            services.AddSwagger();
            services.AddDatabaseContext(Configuration);
            services.AddFileService();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        // Now used IWebHostEnvironment
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.ConfigureCustomExceptionMiddleware();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.RegisterSwaggerMiddleWare();

            app.UseHttpsRedirection();

            // Changes the following in .Net core 3.0
            app.UseRouting();

            // Endpoint routing had changed
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Not longer used
            // app.UseMvc();
        }
    }
}
