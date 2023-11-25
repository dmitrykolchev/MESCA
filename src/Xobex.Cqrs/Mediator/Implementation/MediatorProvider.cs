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
    private readonly ConcurrentDictionary<Type, List<HandlerDesriptor>> _notificationListeners = new();

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
                if (!_validators.TryGetValue(desriptor.ContractType, out List<HandlerDesriptor>? validators))
                {
                    validators = new();
                    _validators.TryAdd(desriptor.ContractType, validators);
                }
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
                if(!_notificationListeners.TryGetValue(desriptor.ContractType, out List<HandlerDesriptor>? listeners))
                {
                    listeners = new();
                    _notificationListeners.TryAdd(desriptor.ContractType, listeners);
                }
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

    public IEnumerable<IEventHandler<TNotification>> GetNotificationListeners<TNotification>(IServiceProvider serviceProvider) 
        where TNotification : IEvent
    {
        return GetNotificationListeners(serviceProvider, typeof(TNotification)).Cast<IEventHandler<TNotification>>();
    }

    public IEnumerable<IEventHandler> GetNotificationListeners(IServiceProvider serviceProvider, Type notificationType)
    {
        ArgumentNullException.ThrowIfNull(notificationType);
        Type? temp = notificationType;
        while (temp != null && typeof(IEvent).IsAssignableFrom(temp))
        {
            if (_notificationListeners.TryGetValue(notificationType, out List<HandlerDesriptor>? descriptors))
            {
                IEventHandler[] handlers = new IEventHandler[descriptors.Count];
                for (int index = 0; index < handlers.Length; ++index)
                {
                    HandlerDesriptor descriptor = descriptors[index];
                    handlers[index] = GetOrCreateNotificationListener(serviceProvider, descriptor);
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
                    handlers[index] = GetOrCreateValidateHandler(serviceProvider, descriptor);
                }
                return handlers;
            }
            temp = temp.BaseType!;
        }
        return Enumerable.Empty<IValidator>();
    }

    private IValidator GetOrCreateValidateHandler(IServiceProvider serviceProvider, HandlerDesriptor descriptor)
    {
        if (descriptor.ServiceLifetime == ServiceLifetime.Singleton)
        {
            throw new NotImplementedException();
        }
        
        if(descriptor.ServiceLifetime == ServiceLifetime.Scoped)
        {
            throw new NotImplementedException();
        }
        
        return (IValidator)ActivatorUtilities.CreateInstance(serviceProvider, descriptor.HandlerType!);
    }
    
    private IEventHandler GetOrCreateNotificationListener(IServiceProvider serviceProvider, HandlerDesriptor descriptor)
    {
        if (descriptor.ServiceLifetime == ServiceLifetime.Singleton)
        {
            throw new NotImplementedException();
        }
        
        if (descriptor.ServiceLifetime == ServiceLifetime.Scoped)
        {
            throw new NotImplementedException();
        }
        
        return (IEventHandler)ActivatorUtilities.CreateInstance(serviceProvider, descriptor.HandlerType!);
    }

    private IRequestHandler GetOrCreateRequestHandler(IServiceProvider serviceProvider, HandlerDesriptor descriptor)
    {
        if (descriptor.ServiceLifetime == ServiceLifetime.Singleton)
        {
            throw new NotImplementedException();
        }

        if (descriptor.ServiceLifetime == ServiceLifetime.Scoped)
        {
            throw new NotImplementedException();
        }

        return (IRequestHandler)ActivatorUtilities.CreateInstance(serviceProvider, descriptor.HandlerType!);
    }
}
