using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ste.User.Domain.Interface;
using Ste.User.Infrastructure.Persist;

namespace Ste.User.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IUserPersist, UserPersist>();
        services.AddTransient<ISecurityPersist, SecurityPersist>();
        services.AddScoped<IDbConnection>(
            serviceProvider =>
                new SqlConnection(serviceProvider.GetService<IConfiguration>().GetConnectionString("SqlConnection")));
        return services;
    }
}
