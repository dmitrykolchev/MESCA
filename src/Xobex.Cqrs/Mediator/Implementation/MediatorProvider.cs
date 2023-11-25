// <copyright file="MediatorProvider.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using System.Collections.Concurrent;
using Microsoft.Extensions.DependencyInjection;

namespace Xobex.Mediator.Implementation;

internal class MediatorProvider : IMediatorProvider
{
    private readonly ConcurrentDictionary<Type, HandlerDesriptor> _requestHandlers = new();
    private readonly ConcurrentDictionary<Type, List<HandlerDesriptor>> _validators = new();
    private readonly ConcurrentDictionary<Type, List<HandlerDesriptor>> _eventHandlers = new();

    public MediatorProvider()
    {
    }

    public void Add(HandlerDesriptor desriptor)
    {
        ArgumentNullException.ThrowIfNull(desriptor);
        if(typeof(IRequest).IsAssignableFrom(desriptor.ContractType))
        {
            if (typeof(IRequestHandler).IsAssignableFrom(desriptor.HandlerType))
            {
                _requestHandlers.TryAdd(desriptor.ContractType, desriptor);
            }
            else if(typeof(IValidator).IsAssignableFrom(desriptor.HandlerType))
            {
                List<HandlerDesriptor> validators = _validators.GetOrAdd(
                    desriptor.ContractType,
                    _ => new List<HandlerDesriptor>());
                validators.Add(desriptor);
            }
            else
            {
                throw new InvalidOperationException($"unsupported handler type {desriptor.HandlerType}");
            }
        }
        else if(typeof(IEvent).IsAssignableFrom(desriptor.ContractType))
        {
            if(typeof(IEventHandler).IsAssignableFrom(desriptor.HandlerType))
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

    public IEnumerable<IEventHandler<TEvent>> GetEventHandlers<TEvent>(IServiceProvider serviceProvider) 
        where TEvent : IEvent
    {
        return GetEventHandlers(serviceProvider, typeof(TEvent)).Cast<IEventHandler<TEvent>>();
    }

    public IEnumerable<IEventHandler> GetEventHandlers(IServiceProvider serviceProvider, Type notificationType)
    {
        ArgumentNullException.ThrowIfNull(notificationType);
        Type? temp = notificationType;
        while (temp != null && typeof(IEvent).IsAssignableFrom(temp))
        {
            if (_eventHandlers.TryGetValue(notificationType, out List<HandlerDesriptor>? descriptors))
            {
                IEventHandler[] handlers = new IEventHandler[descriptors.Count];
                for (int index = 0; index < handlers.Length; ++index)
                {
                    HandlerDesriptor descriptor = descriptors[index];
                    handlers[index] = GetOrCreateEventHandler(serviceProvider, descriptor);
                }
                return handlers;
            }
            temp = temp.BaseType!;
        }
        return Enumerable.Empty<IEventHandler>();
    }

    public IRequestHandler<TRequest> GetRequestHandler<TRequest>(IServiceProvider serviceProvider) 
        where TRequest : IRequest
    {
        return (IRequestHandler<TRequest>)GetRequestHandler(serviceProvider, typeof(TRequest));
    }

    public IRequestHandler<TRequest, TResponse> GetRequestHandler<TRequest, TResponse>(IServiceProvider serviceProvider) 
        where TRequest : IRequest<TResponse>
    {
        return (IRequestHandler<TRequest, TResponse>)GetRequestHandler(serviceProvider, typeof(TRequest));
    }

    public IRequestHandler GetRequestHandler(IServiceProvider serviceProvider, Type requestType)
    {
        ArgumentNullException.ThrowIfNull(requestType);
        Type? temp = requestType;
        while (temp != null && typeof(IRequest).IsAssignableFrom(temp))
        {
            if (_requestHandlers.TryGetValue(requestType, out HandlerDesriptor? descriptor))
            {
                return GetOrCreateRequestHandler(serviceProvider, descriptor);
            }
            temp = temp.BaseType!;
        }
        throw new InvalidOperationException();
    }

    public IEnumerable<IValidator<TRequest>> GetValidators<TRequest>(IServiceProvider serviceProvider) 
        where TRequest : IRequest
    {
        return GetValidators(serviceProvider, typeof(TRequest)).Cast<IValidator<TRequest>>();
    }

    public IEnumerable<IValidator> GetValidators(IServiceProvider serviceProvider, Type requestType)
    {
        ArgumentNullException.ThrowIfNull(requestType);
        Type? temp = requestType;
        while(temp != null && typeof(IRequest).IsAssignableFrom(temp))
        {
            if (_validators.TryGetValue(requestType, out List<HandlerDesriptor>? descriptors))
            {
                IValidator[] handlers = new IValidator[descriptors.Count];
                for(int index = 0; index < handlers.Length; ++index)
                {
                    HandlerDesriptor descriptor = descriptors[index];
                    handlers[index] = GetOrCreateValidator(serviceProvider, descriptor);
                }
                return handlers;
            }
            temp = temp.BaseType!;
        }
        return Enumerable.Empty<IValidator>();
    }

    private IValidator GetOrCreateValidator(IServiceProvider serviceProvider, HandlerDesriptor descriptor)
    {
        return GetOrCreateHandler<IValidator>(serviceProvider, descriptor);
    }

    private IEventHandler GetOrCreateEventHandler(IServiceProvider serviceProvider, HandlerDesriptor descriptor)
    {
        return GetOrCreateHandler<IEventHandler>(serviceProvider, descriptor);
    }

    private IRequestHandler GetOrCreateRequestHandler(IServiceProvider serviceProvider, HandlerDesriptor descriptor)
    {
        return GetOrCreateHandler<IRequestHandler>(serviceProvider, descriptor);
    }

    private THandler GetOrCreateHandler<THandler>(IServiceProvider serviceProvider, HandlerDesriptor descriptor)
    {
        if (descriptor.ServiceLifetime == ServiceLifetime.Singleton)
        {
            MediatorSingletonLifetimeManager ltm = serviceProvider.GetRequiredService<MediatorSingletonLifetimeManager>();
            return (THandler)ltm.GetOrCreate(descriptor.HandlerType!);
        }

        if (descriptor.ServiceLifetime == ServiceLifetime.Scoped)
        {
            MediatorScopedLifetimeManager ltm = serviceProvider.GetRequiredService<MediatorScopedLifetimeManager>();
            return (THandler)ltm.GetOrCreate(descriptor.HandlerType!);
        }

        return (THandler)ActivatorUtilities.CreateInstance(serviceProvider, descriptor.HandlerType!);
    }


}
