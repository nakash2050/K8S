using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using CacheService.Cache;
using CacheService.Services;
using System;

namespace CacheService.Installer
{
    public class CacheInstaller : IInstaller
    {
        public CacheInstaller()
        {
        }

        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            var redisCacheSettings = new RedisCacheSettings();
            configuration.GetSection(nameof(RedisCacheSettings)).Bind(redisCacheSettings);
            services.AddSingleton(redisCacheSettings);

            if(!redisCacheSettings.Enabled)
            {
                return;
            }

            var redisConnectionString = !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("REDIS_CONNECTION_STRING")) ? Environment.GetEnvironmentVariable("REDIS_CONNECTION_STRING") : redisCacheSettings.ConnectionString;

            services.AddStackExchangeRedisCache(options => options.Configuration = redisConnectionString);
            services.AddSingleton<IResponseCacheService, ResponseCacheService>();
        }
    }
}
