using System;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Registration.Business.Concretes;
using Registration.Business.Contracts;
using Registration.Data;
using Registration.Helpers;

namespace Registration
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
            services.AddControllers();
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

            services.AddAutoMapper(typeof(Startup));
            services.AddTransient<IEmployeeRepository, EmployeeRepository>();
            services.AddTransient<Seeder>();

            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Version = "v1",
                    Title = "Registration Service",
                    Description = "Employee Registration"

                });
            });

            services.AddHealthChecks();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, Seeder seeder)
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
                s.SwaggerEndpoint(string.Format("{0}{1}", $"/{pathBase.TrimStart('/')}", "/swagger/v1/swagger.json"), "Registration Service");
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
