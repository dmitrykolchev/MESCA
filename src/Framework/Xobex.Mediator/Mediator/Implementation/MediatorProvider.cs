// <copyright file="MediatorProvider.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using System.Collections.Concurrent;
using System.Collections.Immutable;

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
            else if (typeof(IRequestPostProcessor).IsAssignableFrom(desriptor.HandlerType))
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

    public IReadOnlyList<IEventHandler> GetEventHandlers(IServiceProvider serviceProvider, Type eventType)
    {
        ArgumentNullException.ThrowIfNull(serviceProvider);
        ArgumentNullException.ThrowIfNull(eventType);
        Type? temp = eventType;
        while (temp != null && typeof(IEvent).IsAssignableFrom(temp))
        {
            if (_eventHandlers.TryGetValue(temp, out List<HandlerDesriptor>? descriptors))
            {
                IEventHandler[] handlers = new IEventHandler[descriptors.Count];
                for (int index = 0; index < handlers.Length; ++index)
                {
                    HandlerDesriptor descriptor = descriptors[index];
                    handlers[index] = (IEventHandler)descriptor.Factory(serviceProvider);
                }
                if (handlers.Length > 1)
                {
                    Array.Sort(handlers, 0, handlers.Length, Comparer<IEventHandler>.Create((a1, a2) => a1.Ordinal - a2.Ordinal));
                }
                return ImmutableArray.Create(handlers);
            }
            temp = temp.BaseType!;
        }
        return Array.Empty<IEventHandler>();
    }

    public IRequestHandler GetRequestHandler(IServiceProvider serviceProvider, Type requestType)
    {
        ArgumentNullException.ThrowIfNull(serviceProvider);
        ArgumentNullException.ThrowIfNull(requestType);
        Type? temp = requestType;
        while (temp != null && typeof(IRequest).IsAssignableFrom(temp))
        {
            if (_requestHandlers.TryGetValue(temp, out HandlerDesriptor? descriptor))
            {
                return (IRequestHandler)descriptor.Factory(serviceProvider);
            }
            temp = temp.BaseType!;
        }
        throw new InvalidOperationException($"cannot find handler for request {requestType}");
    }

    public IReadOnlyList<IRequestPostProcessor> GetRequestPostProcessors(IServiceProvider serviceProvider, Type requestType)
    {
        ArgumentNullException.ThrowIfNull(serviceProvider);
        ArgumentNullException.ThrowIfNull(requestType);
        Type? temp = requestType;
        while (temp != null && typeof(IRequest).IsAssignableFrom(temp))
        {
            if (_postProcessors.TryGetValue(temp, out List<HandlerDesriptor>? descriptors))
            {
                IRequestPostProcessor[] handlers = new IRequestPostProcessor[descriptors.Count];
                for (int index = 0; index < handlers.Length; ++index)
                {
                    HandlerDesriptor descriptor = descriptors[index];
                    handlers[index] = (IRequestPostProcessor)descriptor.Factory(serviceProvider);
                }
                if (handlers.Length > 1)
                {
                    Array.Sort(handlers, 0, handlers.Length, Comparer<IRequestPostProcessor>.Create((a1, a2) => a1.Ordinal - a2.Ordinal));
                }
                return ImmutableArray.Create(handlers);
            }
            temp = temp.BaseType!;
        }
        return Array.Empty<IRequestPostProcessor>();
    }

    public IReadOnlyList<IValidator> GetValidators(IServiceProvider serviceProvider, Type requestType)
    {
        ArgumentNullException.ThrowIfNull(serviceProvider);
        ArgumentNullException.ThrowIfNull(requestType);
        Type? temp = requestType;
        while (temp != null && typeof(IRequest).IsAssignableFrom(temp))
        {
            if (_validators.TryGetValue(temp, out List<HandlerDesriptor>? descriptors))
            {
                IValidator[] handlers = new IValidator[descriptors.Count];
                for (int index = 0; index < handlers.Length; ++index)
                {
                    HandlerDesriptor descriptor = descriptors[index];
                    handlers[index] = (IValidator)descriptor.Factory(serviceProvider);
                }
                if (handlers.Length > 1)
                {
                    Array.Sort(handlers, 0, handlers.Length, Comparer<IValidator>.Create((a1, a2) => a1.Ordinal - a2.Ordinal));
                }
                return ImmutableArray.Create(handlers);
            }
            temp = temp.BaseType!;
        }
        return Array.Empty<IValidator>();
    }
}

