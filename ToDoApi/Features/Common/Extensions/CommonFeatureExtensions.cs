using Microsoft.EntityFrameworkCore;

namespace ToDoApi.Features.Common.Extensions;

public static class CommonFeatureExtensions
{
    public static IServiceCollection AddCommonFeature(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<ToDoContext>(options => options.UseSqlServer(connectionString));

        return services;
    }
}
