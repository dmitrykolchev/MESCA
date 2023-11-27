// <copyright file="IMediatorHandlerCollection.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

namespace Xobex.Mediator;

public interface IMediatorProvider 
{
    public IRequestHandler<TRequest, TResult> GetRequestHandler<TRequest, TResult>(IServiceProvider serviceProvider)
        where TRequest: IRequest<TResult>;

    public IRequestHandler GetRequestHandler(IServiceProvider serviceProvider, Type requestType);

    public IEnumerable<IEventHandler<TNotification>> GetEventHandlers<TNotification>(IServiceProvider serviceProvider)
        where TNotification : IEvent;

    public IEnumerable<IEventHandler> GetEventHandlers(IServiceProvider serviceProvider, Type notificationType);

    public IEnumerable<IValidator<TRequest>> GetValidators<TRequest>(IServiceProvider serviceProvider)
        where TRequest : IRequest;

    public IEnumerable<IValidator> GetValidators(IServiceProvider serviceProvider, Type requestType);

    public void Add(HandlerDesriptor desriptor);
}
