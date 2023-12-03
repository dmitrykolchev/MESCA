// <copyright file="ServiceCollectionExtensions.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xobex.Entities;
using Xobex.Data.EntityFramework.Interceptors;

namespace Xobex.Mes.Entities;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMesSqlServerDatabase(this IServiceCollection services, string connectionString)
    {
        services.AddDbContextPool<MesSqlServerDbContext>((serviceProvider, options) =>
        {
            options.UseSqlServer(connectionString)
                .ConfigureWarnings(warnings => warnings.Default(WarningBehavior.Log))
                .AddInterceptors(ActivatorUtilities.CreateInstance<AuditingInterceptor>(serviceProvider))
                .UseSnakeCaseNamingConvention();
            ILoggerFactory? loggerFactory = serviceProvider.GetService<ILoggerFactory>();
            if (loggerFactory != null)
            {
#if DEBUG
                options
                    .EnableSensitiveDataLogging()
                    .UseLoggerFactory(loggerFactory)
                    .EnableDetailedErrors();
#else
                options.UseLoggerFactory(loggerFactory).EnableDetailedErrors();
#endif
            }
        });
        services.AddScoped<IMetadataContext>(serviceProvider =>
        {
            return serviceProvider.GetRequiredService<MesSqlServerDbContext>();
        });
        services.AddScoped<IMesDbContext>(serviceProvider =>
        {
            return serviceProvider.GetRequiredService<MesSqlServerDbContext>();
        });
        return services;
    }
}
