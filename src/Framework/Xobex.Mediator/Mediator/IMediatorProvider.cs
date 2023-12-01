// <copyright file="IMediatorProvider.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

namespace Xobex.Mediator;

public interface IMediatorProvider
{
    IRequestHandler<TRequest, TResult> GetRequestHandler<TRequest, TResult>(IServiceProvider serviceProvider)
        where TRequest : IRequest<TResult>
        where TResult : notnull;

    IRequestHandler GetRequestHandler(IServiceProvider serviceProvider, Type requestType);

    IReadOnlyList<IRequestPostProcesor> GetRequestPostProcessors(IServiceProvider services, Type requestType);

    IReadOnlyList<IEventHandler<TNotification>> GetEventHandlers<TNotification>(IServiceProvider serviceProvider)
        where TNotification : IEvent;

    IReadOnlyList<IEventHandler> GetEventHandlers(IServiceProvider serviceProvider, Type notificationType);

    IReadOnlyList<IValidator<TRequest>> GetValidators<TRequest>(IServiceProvider serviceProvider)
        where TRequest : IRequest;

    IReadOnlyList<IValidator> GetValidators(IServiceProvider serviceProvider, Type requestType);

    void Add(HandlerDesriptor desriptor);
}
