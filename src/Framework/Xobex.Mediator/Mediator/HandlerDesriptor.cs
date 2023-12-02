// <copyright file="HandlerDesriptor.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Microsoft.Extensions.DependencyInjection;
using Xobex.Mediator.Implementation;

namespace Xobex.Mediator;

public class HandlerDesriptor
{
    private ServiceLifetime _lifetime;

    public Type ContractType { get; set; } = null!;

    public Type? HandlerType { get; set; }   

    public object? Instance { get; set; }

    public ServiceLifetime Lifetime
    {
        get => _lifetime;
        set
        {
            _lifetime = value;
            Factory = value switch
            {
                ServiceLifetime.Scoped => CreateScopedInstance,
                ServiceLifetime.Transient => CreateTransientInstance,
                ServiceLifetime.Singleton => CreateSingletonInstance,
                _ => throw new InvalidOperationException()
            };
        }
    }

    internal Func<IServiceProvider, object> Factory { get; set; } = null!;

    private object CreateTransientInstance(IServiceProvider serviceProvider)
    {
        return ActivatorUtilities.CreateInstance(serviceProvider, HandlerType!);
    }

    private object CreateScopedInstance(IServiceProvider serviceProvider)
    {
        MediatorScopedLifetimeManager ltm = serviceProvider.GetRequiredService<MediatorScopedLifetimeManager>();
        return ltm.GetOrCreate(HandlerType!);
    }

    private object CreateSingletonInstance(IServiceProvider serviceProvider)
    {
        MediatorSingletonLifetimeManager ltm = serviceProvider.GetRequiredService<MediatorSingletonLifetimeManager>();
        return ltm.GetOrCreate(HandlerType!);
    }
}
