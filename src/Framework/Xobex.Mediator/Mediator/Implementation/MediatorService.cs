// <copyright file="MediatorService.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

namespace Xobex.Mediator.Implementation;

public class MediatorService(IServiceProvider serviceProvider, IMediatorProvider mediatorProvider) : IMediator
{
    private readonly IServiceProvider _serviceProvider = serviceProvider
        ?? throw new ArgumentNullException(nameof(serviceProvider));
    private readonly IMediatorProvider _mediatorProvider = mediatorProvider
        ?? throw new ArgumentNullException(nameof(mediatorProvider));

    public PipelineBuilder CreatePipelineBuilder()
    {
        return new PipelineBuilder(this, _serviceProvider);
    }

    public Task RaiseEventAsync<TEvent>(TEvent notification, CancellationToken cancellationToken)
        where TEvent : IEvent
    {
        ArgumentNullException.ThrowIfNull(notification);
        return RaiseEventInternalAsync(notification, cancellationToken);
    }

    public Task SendAsync<TRequest>(TRequest request, CancellationToken cancellationToken)
        where TRequest : IRequest
    {
        ArgumentNullException.ThrowIfNull(request);
        return QueryInternalAsync(request, cancellationToken);
    }

    public async Task<TResult> QueryAsync<TResult>(IRequest<TResult> request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        return (TResult)await QueryInternalAsync(request, cancellationToken).ConfigureAwait(false);
    }

    Task<object> IMediatorServiceBase.QueryAsync(IRequest request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        return QueryInternalAsync(request, cancellationToken);
    }

    Task IMediatorServiceBase.SendAsync(IRequest request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        return QueryInternalAsync(request, cancellationToken);
    }

    Task IMediatorServiceBase.RaiseEventAsync(IEvent notification, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(notification);
        return RaiseEventInternalAsync(notification, cancellationToken);
    }

    private async Task<object> QueryInternalAsync(IRequest request, CancellationToken cancellationToken)
    {
        IReadOnlyList<IValidator> validators = _mediatorProvider.GetValidators(_serviceProvider, request.GetType());
        for (int i = 0; i < validators.Count; ++i)
        {
            await validators[i].ValidateAsync(request, cancellationToken).ConfigureAwait(false);
        }
        IRequestHandler requestHandler = _mediatorProvider.GetRequestHandler(_serviceProvider, request.GetType());
        object result = await requestHandler.HandleAsync(request, cancellationToken).ConfigureAwait(false);
        IReadOnlyList<IRequestPostProcessor> postProcessors = _mediatorProvider.GetRequestPostProcessors(_serviceProvider, request.GetType());
        for (int i = 0; i < postProcessors.Count; ++i)
        {
            await postProcessors[i].HandleAsync(request, result, cancellationToken).ConfigureAwait(false);
        }
        return result;
    }

    private async Task RaiseEventInternalAsync(IEvent notification, CancellationToken cancellationToken)
    {
        IReadOnlyList<IEventHandler> listeners = _mediatorProvider.GetEventHandlers(_serviceProvider, notification.GetType());
        for (int i = 0; i < listeners.Count; ++i)
        {
            await listeners[i].HandleAsync(notification, cancellationToken).ConfigureAwait(false);
        }
    }
}
