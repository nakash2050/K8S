using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CacheService.Data;
using CacheService.Installer;
using CacheService.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CacheService
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
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                    .SetIsOriginAllowed((host) => true)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });
            services.AddDbContext<DataContext>();
            services.InstallServicesInAssembly(Configuration);
            services.AddTransient<ICacheRepository, CacheRepository>();

            services.AddControllers();

            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Version = "v1",
                    Title = "Cache Service",
                    Description = "Getting Cached Data"

                });
            });

            services.AddRouting(options => options.LowercaseUrls = true);
            services.AddHealthChecks();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            if (env.IsProduction())
            {
                //seeder.Seed();
            }

            var pathBase = Environment.GetEnvironmentVariable("API_PATH_BASE");

            if (!string.IsNullOrWhiteSpace(pathBase))
            {
                app.UsePathBase($"/{pathBase.TrimStart('/')}");
            }

            app.UseCors("CorsPolicy");
            app.UseHealthChecks("/health");

            app.UseSwagger(c =>
            {
                c.RouteTemplate = "swagger/{documentName}/swagger.json";

            });

            app.UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint(string.Format("{0}{1}", $"/{pathBase.TrimStart('/')}", "/swagger/v1/swagger.json"), "Cache Service");
            });

            app.UseStaticFiles();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
