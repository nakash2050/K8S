using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using CacheService.Cache;
using CacheService.Services;

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

            services.AddStackExchangeRedisCache(options => options.Configuration = redisCacheSettings.ConnectionString);
            services.AddSingleton<IResponseCacheService, ResponseCacheService>();
        }
    }
}
