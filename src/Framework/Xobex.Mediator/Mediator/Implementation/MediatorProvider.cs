// <copyright file="MediatorProvider.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using System.Collections.Concurrent;
using System.Collections.Immutable;
using Microsoft.Extensions.DependencyInjection;

namespace Xobex.Mediator.Implementation;

internal class MediatorProvider : IMediatorProvider
{
    private readonly ConcurrentDictionary<Type, HandlerDesriptor> _requestHandlers = new();
    private readonly ConcurrentDictionary<Type, List<HandlerDesriptor>> _validators = new();
    private readonly ConcurrentDictionary<Type, List<HandlerDesriptor>> _postProcessors = new();
    private readonly ConcurrentDictionary<Type, List<HandlerDesriptor>> _eventHandlers = new();

    public MediatorProvider()
    {
    }

    public void Add(HandlerDesriptor desriptor)
    {
        ArgumentNullException.ThrowIfNull(desriptor);
        if (typeof(IRequest).IsAssignableFrom(desriptor.ContractType))
        {
            if (typeof(IRequestHandler).IsAssignableFrom(desriptor.HandlerType))
            {
                _requestHandlers.TryAdd(desriptor.ContractType, desriptor);
            }
            else if (typeof(IValidator).IsAssignableFrom(desriptor.HandlerType))
            {
                List<HandlerDesriptor> validators = _validators.GetOrAdd(
                    desriptor.ContractType,
                    _ => new List<HandlerDesriptor>());
                validators.Add(desriptor);
            }
            else if (typeof(IRequestPostProcesor).IsAssignableFrom(desriptor.HandlerType))
            {
                List<HandlerDesriptor> postProcessors = _postProcessors.GetOrAdd(
                    desriptor.ContractType,
                    _ => new List<HandlerDesriptor>());
                postProcessors.Add(desriptor);
            }
            else
            {
                throw new InvalidOperationException($"unsupported handler type {desriptor.HandlerType}");
            }
        }
        else if (typeof(IEvent).IsAssignableFrom(desriptor.ContractType))
        {
            if (typeof(IEventHandler).IsAssignableFrom(desriptor.HandlerType))
            {
                List<HandlerDesriptor> listeners = _eventHandlers.GetOrAdd(
                    desriptor.ContractType,
                    _ => new List<HandlerDesriptor>());
                listeners.Add(desriptor);
            }
            else
            {
                throw new InvalidOperationException($"unsupported handler type {desriptor.HandlerType}");
            }
        }
        else
        {
            throw new InvalidOperationException($"usupported contract type {desriptor.ContractType}");
        }
    }

    public IReadOnlyList<IEventHandler<TEvent>> GetEventHandlers<TEvent>(IServiceProvider serviceProvider)
        where TEvent : IEvent
    {
        return GetEventHandlers<IEventHandler<TEvent>>(serviceProvider, typeof(TEvent));
    }

    public IReadOnlyList<IEventHandler> GetEventHandlers(IServiceProvider serviceProvider, Type eventType)
    {
        ArgumentNullException.ThrowIfNull(eventType);
        return GetEventHandlers<IEventHandler>(serviceProvider, eventType);
    }

    public IRequestHandler<TRequest, TResult> GetRequestHandler<TRequest, TResult>(IServiceProvider serviceProvider)
        where TRequest : IRequest<TResult>
        where TResult : notnull
    {
        return (IRequestHandler<TRequest, TResult>)GetRequestHandler(serviceProvider, typeof(TRequest));
    }

    public IRequestHandler GetRequestHandler(IServiceProvider serviceProvider, Type requestType)
    {
        ArgumentNullException.ThrowIfNull(requestType);
        Type? temp = requestType;
        while (temp != null && typeof(IRequest).IsAssignableFrom(temp))
        {
            if (_requestHandlers.TryGetValue(temp, out HandlerDesriptor? descriptor))
            {
                return GetOrCreate<IRequestHandler>(serviceProvider, descriptor);
            }
            temp = temp.BaseType!;
        }
        throw new InvalidOperationException($"cannot find handler for request {requestType}");
    }

    public IReadOnlyList<IValidator<TRequest>> GetValidators<TRequest>(IServiceProvider serviceProvider)
        where TRequest : IRequest
    {
        return GetValidators<IValidator<TRequest>>(serviceProvider, typeof(TRequest));
    }

    public IReadOnlyList<IRequestPostProcessor<TRequest, TResult>> GetRequestPostProcessors<TRequest, TResult>(IServiceProvider serviceProvider)
        where TRequest : IRequest<TResult>
        where TResult : notnull
    {
        ArgumentNullException.ThrowIfNull(serviceProvider);
        return GetRequestPostProcessors<IRequestPostProcessor<TRequest, TResult>>(serviceProvider, typeof(TRequest));
    }

    public IReadOnlyList<IRequestPostProcesor> GetRequestPostProcessors(IServiceProvider serviceProvider, Type requestType)
    {
        ArgumentNullException.ThrowIfNull(serviceProvider);
        ArgumentNullException.ThrowIfNull(requestType);
        return GetRequestPostProcessors<IRequestPostProcesor>(serviceProvider, requestType);
    }

    public IReadOnlyList<IValidator> GetValidators(IServiceProvider serviceProvider, Type requestType)
    {
        ArgumentNullException.ThrowIfNull(requestType);
        return GetValidators<IValidator>(serviceProvider, requestType);
    }

    private IReadOnlyList<TEventHandler> GetEventHandlers<TEventHandler>(IServiceProvider serviceProvider, Type eventType)
        where TEventHandler : IEventHandler
    {
        Type? temp = eventType;
        while (temp != null && typeof(IEvent).IsAssignableFrom(temp))
        {
            if (_eventHandlers.TryGetValue(temp, out List<HandlerDesriptor>? descriptors))
            {
                TEventHandler[] handlers = new TEventHandler[descriptors.Count];
                for (int index = 0; index < handlers.Length; ++index)
                {
                    HandlerDesriptor descriptor = descriptors[index];
                    handlers[index] = GetOrCreate<TEventHandler>(serviceProvider, descriptor);
                }
                if (handlers.Length > 1)
                {
                    Array.Sort(handlers, 0, handlers.Length, Comparer<TEventHandler>.Create((a1, a2) => a1.Ordinal - a2.Ordinal));
                }
                return ImmutableArray.Create(handlers);
            }
            temp = temp.BaseType!;
        }
        return Array.Empty<TEventHandler>();
    }

    private IReadOnlyList<TValidator> GetValidators<TValidator>(IServiceProvider serviceProvider, Type requestType)
        where TValidator : IValidator
    {
        Type? temp = requestType;
        while (temp != null && typeof(IRequest).IsAssignableFrom(temp))
        {
            if (_validators.TryGetValue(temp, out List<HandlerDesriptor>? descriptors))
            {
                TValidator[] handlers = new TValidator[descriptors.Count];
                for (int index = 0; index < handlers.Length; ++index)
                {
                    HandlerDesriptor descriptor = descriptors[index];
                    handlers[index] = GetOrCreate<TValidator>(serviceProvider, descriptor);
                }
                if (handlers.Length > 1)
                {
                    Array.Sort(handlers, 0, handlers.Length, Comparer<TValidator>.Create((a1, a2) => a1.Ordinal - a2.Ordinal));
                }
                return ImmutableArray.Create(handlers);
            }
            temp = temp.BaseType!;
        }
        return Array.Empty<TValidator>();
    }

    private IReadOnlyList<TPostProcessor> GetRequestPostProcessors<TPostProcessor>(IServiceProvider serviceProvider, Type requestType)
        where TPostProcessor : IRequestPostProcesor
    {
        Type? temp = requestType;
        while (temp != null && typeof(IRequest).IsAssignableFrom(temp))
        {
            if (_postProcessors.TryGetValue(temp, out List<HandlerDesriptor>? descriptors))
            {
                TPostProcessor[] handlers = new TPostProcessor[descriptors.Count];
                for (int index = 0; index < handlers.Length; ++index)
                {
                    HandlerDesriptor descriptor = descriptors[index];
                    handlers[index] = GetOrCreate<TPostProcessor>(serviceProvider, descriptor);
                }
                if (handlers.Length > 1)
                {
                    Array.Sort(handlers, 0, handlers.Length, Comparer<TPostProcessor>.Create((a1, a2) => a1.Ordinal - a2.Ordinal));
                }
                return ImmutableArray.Create(handlers);
            }
            temp = temp.BaseType!;
        }
        return Array.Empty<TPostProcessor>();
    }

    private THandler GetOrCreate<THandler>(IServiceProvider serviceProvider, HandlerDesriptor descriptor)
    {
        if (descriptor.ServiceLifetime == ServiceLifetime.Scoped)
        {
            MediatorScopedLifetimeManager ltm = serviceProvider.GetRequiredService<MediatorScopedLifetimeManager>();
            return (THandler)ltm.GetOrCreate(descriptor.HandlerType!);
        }

        if (descriptor.ServiceLifetime == ServiceLifetime.Singleton)
        {
            MediatorSingletonLifetimeManager ltm = serviceProvider.GetRequiredService<MediatorSingletonLifetimeManager>();
            return (THandler)ltm.GetOrCreate(descriptor.HandlerType!);
        }

        return (THandler)ActivatorUtilities.CreateInstance(serviceProvider, descriptor.HandlerType!);
    }
}

