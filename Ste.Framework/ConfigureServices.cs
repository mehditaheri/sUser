using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ste.Framework.Behaviour;
using Ste.Framework.Common;
using static Ste.Framework.Common.Utility;

namespace Ste.Framework;

public static class ConfigureServices
{
    public static IServiceCollection AddFrameworkServices(this IServiceCollection services, IConfiguration? configuration)
    {
        services.AddTransient<ICaptchaService, CaptchaService>();
        services.AddTransient<IUserContextService, UserContextService>();
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration?.GetSection("Redis")["ConnectionString"];
        });
        services.Add(ServiceDescriptor.Singleton<IDistributedCache, RedisCache>());
        ConfigurationHelper.Initialize(configuration);
        return services;
    }
}
