// <copyright file="ServiceCollectionExtensions.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Microsoft.Extensions.DependencyInjection;
using Xobex.Mediator.Implementation;

namespace Xobex.Mediator;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMediator(this IServiceCollection services, Action<IMediatorProvider>? action = default)
    {
        services.AddSingleton<IMediatorProvider, MediatorProvider>((serviceProvider) =>
        {
            MediatorProvider mediatorProvider = ActivatorUtilities.CreateInstance<MediatorProvider>(serviceProvider);
            action?.Invoke(mediatorProvider);
            return mediatorProvider;
        });
        return services.AddScoped<IMediatorService, MediatorService>();
    }
}
