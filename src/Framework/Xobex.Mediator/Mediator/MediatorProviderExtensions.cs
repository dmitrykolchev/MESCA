// <copyright file="MediatorProviderExtensions.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Xobex.Mediator;

public static class MediatorProviderExtensions
{
    public static IMediatorProvider AddEvent<TNotification, TNotificationListener>(
        this IMediatorProvider provider, ServiceLifetime serviceLifetime = ServiceLifetime.Transient)
        where TNotification : IEvent
        where TNotificationListener : IEventHandler<TNotification>
    {
        HandlerDesriptor handlerDesriptor = new()
        {
            ContractType = typeof(TNotification),
            HandlerType = typeof(TNotificationListener),
            Lifetime = serviceLifetime
        };
        provider.Add(handlerDesriptor);
        return provider;
    }

    public static IMediatorProvider AddRequest<TRequest, TRequestHandler>(
        this IMediatorProvider provider, ServiceLifetime serviceLifetime = ServiceLifetime.Transient)
        where TRequest : IRequest
        where TRequestHandler : IRequestHandler
    {
        HandlerDesriptor handlerDesriptor = new()
        {
            ContractType = typeof(TRequest),
            HandlerType = typeof(TRequestHandler),
            Lifetime = serviceLifetime
        };
        provider.Add(handlerDesriptor);
        return provider;
    }

    public static IMediatorProvider AddRequestPostProcessor<TRequest, TRequestPostProcessor>(
        this IMediatorProvider provider, ServiceLifetime serviceLifetime = ServiceLifetime.Transient)
        where TRequest : IRequest
        where TRequestPostProcessor : IRequestPostProcessor
    {
        HandlerDesriptor handlerDesriptor = new()
        {
            ContractType = typeof(TRequest),
            HandlerType = typeof(TRequestPostProcessor),
            Lifetime = serviceLifetime
        };
        provider.Add(handlerDesriptor);
        return provider;
    }

    public static IMediatorProvider AddValidator<TRequest, TValidator>(
        this IMediatorProvider provider,
        ServiceLifetime serviceLifetime = ServiceLifetime.Transient)
        where TRequest : IRequest
        where TValidator : IValidator
    {
        HandlerDesriptor handlerDesriptor = new()
        {
            ContractType = typeof(TRequest),
            HandlerType = typeof(TValidator),
            Lifetime = serviceLifetime
        };
        provider.Add(handlerDesriptor);
        return provider;
    }

    public static IMediatorProvider AddAssembly(this IMediatorProvider provider, Assembly assembly)
    {
        IEnumerable<Type> types = assembly.GetTypes()
            .Where(t => t.GetCustomAttribute<MediatorIgnoreAttribute>() == null && !t.IsAbstract);
        foreach (Type type in types)
        {
            if (typeof(IRequestHandler).IsAssignableFrom(type))
            {
                Type[]? arguments = type.GetInterfaces()
                    .Single(t => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IRequestHandler<,>))
                    .GetGenericArguments();
                if (typeof(IRequest).IsAssignableFrom(arguments[0]))
                {
                    HandlerDesriptor handlerDesriptor = new()
                    {
                        ContractType = arguments[0],
                        HandlerType = type,
                        Lifetime = GetHandlerLifetime(type)
                    };
                    provider.Add(handlerDesriptor);
                }
                else
                {
                    throw new InvalidOperationException($"Type {arguments[0]} does not implement IRequest");
                }
            }
            else if (typeof(IRequestPostProcessor).IsAssignableFrom(type))
            {
                Type[]? arguments = type.GetInterfaces()
                    .Single(t => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IRequestPostProcessor<,>))
                    .GetGenericArguments();
                if (typeof(IRequest).IsAssignableFrom(arguments[0]))
                {
                    HandlerDesriptor handlerDesriptor = new()
                    {
                        ContractType = arguments[0],
                        HandlerType = type,
                        Lifetime = GetHandlerLifetime(type)
                    };
                    provider.Add(handlerDesriptor);
                }
                else
                {
                    throw new InvalidOperationException($"Type {arguments[0]} does not implement IRequest");
                }
            }
            else if (typeof(IValidator).IsAssignableFrom(type))
            {
                Type? validator = type.GetInterfaces()
                    .SingleOrDefault(t => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IValidator<>));
                if (validator != null)
                {
                    Type argument = validator.GetGenericArguments().Single();
                    if (typeof(IRequest).IsAssignableFrom(argument))
                    {
                        HandlerDesriptor handlerDesriptor = new()
                        {
                            ContractType = argument,
                            HandlerType = type,
                            Lifetime = GetHandlerLifetime(type)
                        };
                        provider.Add(handlerDesriptor);
                    }
                    else
                    {
                        throw new InvalidOperationException($"Type {argument} does not implement IRequest");
                    }
                }
                else
                {
                    throw new InvalidOperationException($"Type {type} does not implement IValidator<TRequest>");
                }
            }
            else if (typeof(IEventHandler).IsAssignableFrom(type))
            {
                Type? eventHandler = type.GetInterfaces()
                    .SingleOrDefault(t => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IEventHandler<>));
                if (eventHandler != null)
                {
                    Type argument = eventHandler.GetGenericArguments().Single();
                    if (typeof(IEvent).IsAssignableFrom(argument))
                    {
                        HandlerDesriptor handlerDesriptor = new()
                        {
                            ContractType = argument,
                            HandlerType = type,
                            Lifetime = GetHandlerLifetime(type)
                        };
                        provider.Add(handlerDesriptor);
                    }
                    else
                    {
                        throw new InvalidOperationException($"Type {argument} does not implement IEvent");
                    }
                }
                else
                {
                    throw new InvalidOperationException($"Type {type} does not implement IEventHandler<TRequest>");
                }
            }
        }

        return provider;
    }

    private static ServiceLifetime GetHandlerLifetime(Type type)
    {
        return type.GetCustomAttribute<MediatorLifetimeAttribute>()?.Lifetime ?? ServiceLifetime.Transient;
    }
}
