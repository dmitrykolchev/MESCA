// <copyright file="IMediatorProvider.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

namespace Xobex.Mediator;

public interface IMediatorProvider
{
    public IRequestHandler<TRequest, TResult> GetRequestHandler<TRequest, TResult>(IServiceProvider serviceProvider)
        where TRequest : IRequest<TResult>
        where TResult : notnull;

    public IRequestHandler GetRequestHandler(IServiceProvider serviceProvider, Type requestType);

    public IReadOnlyList<IRequestPostProcesor> GetRequestPostProcessors(IServiceProvider services, Type requestType);

    public IReadOnlyList<IEventHandler<TNotification>> GetEventHandlers<TNotification>(IServiceProvider serviceProvider)
        where TNotification : IEvent;

    public IReadOnlyList<IEventHandler> GetEventHandlers(IServiceProvider serviceProvider, Type notificationType);

    public IReadOnlyList<IValidator<TRequest>> GetValidators<TRequest>(IServiceProvider serviceProvider)
        where TRequest : IRequest;

    public IReadOnlyList<IValidator> GetValidators(IServiceProvider serviceProvider, Type requestType);

    public void Add(HandlerDesriptor desriptor);
}
