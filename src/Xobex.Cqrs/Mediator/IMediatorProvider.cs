﻿// <copyright file="IMediatorHandlerCollection.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

namespace Xobex.Mediator;

public interface IMediatorProvider 
{
    public IRequestHandler<TRequest> GetRequestHandler<TRequest>(IServiceProvider serviceProvider)
        where TRequest : IRequest;

    public IRequestHandler<TRequest, TResponse> GetRequestHandler<TRequest, TResponse>(IServiceProvider serviceProvider)
        where TRequest: IRequest<TResponse>;

    public IRequestHandler GetRequestHandler(IServiceProvider serviceProvider, Type requestType);

    public IEnumerable<IEventHandler<TNotification>> GetNotificationListeners<TNotification>(IServiceProvider serviceProvider)
        where TNotification : IEvent;

    public IEnumerable<IEventHandler> GetNotificationListeners(IServiceProvider serviceProvider, Type notificationType);

    public IEnumerable<IValidator<TRequest>> GetValidators<TRequest>(IServiceProvider serviceProvider)
        where TRequest : IRequest;

    public IEnumerable<IValidator> GetValidators(IServiceProvider serviceProvider, Type requestType);

    public void Add(HandlerDesriptor desriptor);
}
