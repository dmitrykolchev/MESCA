// <copyright file="MediatorService.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

namespace Xobex.Mediator.Implementation;

public class MediatorService(IServiceProvider serviceProvider, IMediatorProvider mediatorProvider) : IMediatorService
{
    private readonly IServiceProvider _serviceProvider = serviceProvider
        ?? throw new ArgumentNullException(nameof(serviceProvider));
    private readonly IMediatorProvider _mediatorProvider = mediatorProvider
        ?? throw new ArgumentNullException(nameof(mediatorProvider));

    public PipelineBuilder CreatePipelineBuilder()
    {
        return new PipelineBuilder(this, _serviceProvider);
    }

    public async Task RaiseEventAsync<TEvent>(TEvent notification, CancellationToken cancellationToken)
        where TEvent : IEvent
    {
        ArgumentNullException.ThrowIfNull(notification);
        IReadOnlyList<IEventHandler<TEvent>> listeners = _mediatorProvider.GetEventHandlers<TEvent>(_serviceProvider);
        for (int i = 0; i < listeners.Count; ++i)
        {
            await listeners[i].HandleAsync(notification, cancellationToken).ConfigureAwait(false);
        }
    }

    public async Task SendAsync<TRequest>(TRequest request, CancellationToken cancellationToken)
        where TRequest : IRequest
    {
        ArgumentNullException.ThrowIfNull(request);

        await ValidateAsync(request, cancellationToken);

        IRequestHandler requestHandler = _mediatorProvider.GetRequestHandler(_serviceProvider, typeof(TRequest));
        object result = await requestHandler.ProcessAsync(request, cancellationToken).ConfigureAwait(false);
        IReadOnlyList<IRequestPostProcesor> postProcessors = _mediatorProvider.GetRequestPostProcessors(_serviceProvider, typeof(TRequest));
        for (int i = 0; i < postProcessors.Count; ++i)
        {
            await postProcessors[i].ProcessAsync(request, result, cancellationToken).ConfigureAwait(false);
        }
    }

    public async Task<TResult> QueryAsync<TResult>(IRequest<TResult> request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        await ValidateAsync(request, cancellationToken);

        IRequestHandler requestHandler = _mediatorProvider.GetRequestHandler(_serviceProvider, request.GetType());
        TResult result = (TResult)(await requestHandler.ProcessAsync(request, cancellationToken).ConfigureAwait(false))!;
        IReadOnlyList<IRequestPostProcesor> postProcessors = _mediatorProvider.GetRequestPostProcessors(_serviceProvider, request.GetType());
        for (int i = 0; i < postProcessors.Count; ++i)
        {
            await postProcessors[i].ProcessAsync(request, result, cancellationToken).ConfigureAwait(false);
        }
        return result;
    }

    private async Task ValidateAsync(IRequest request, CancellationToken cancellationToken)
    {
        IReadOnlyList<IValidator> validators = _mediatorProvider.GetValidators(_serviceProvider, request.GetType());
        for (int i = 0; i < validators.Count; ++i)
        {
            await validators[i].ValidateAsync(request, cancellationToken);
        }
    }

    async Task<object> IMediatorServiceBase.QueryAsync(IRequest request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        IReadOnlyList<IValidator> validators = _mediatorProvider.GetValidators(_serviceProvider, request.GetType());
        for (int i = 0; i < validators.Count; ++i)
        {
            await validators[i].ValidateAsync(request, cancellationToken);
        }
        IRequestHandler requestHandler = _mediatorProvider.GetRequestHandler(_serviceProvider, request.GetType());
        object result = await requestHandler.ProcessAsync(request, cancellationToken);
        IReadOnlyList<IRequestPostProcesor> postProcessors = _mediatorProvider.GetRequestPostProcessors(_serviceProvider, request.GetType());
        for (int i = 0; i < postProcessors.Count; ++i)
        {
            await postProcessors[i].ProcessAsync(request, result, cancellationToken).ConfigureAwait(false);
        }
        return result;
    }

    async Task IMediatorServiceBase.SendAsync(IRequest request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        IReadOnlyList<IValidator> validators = _mediatorProvider.GetValidators(_serviceProvider, request.GetType());
        for (int i = 0; i < validators.Count; ++i)
        {
            await validators[i].ValidateAsync(request, cancellationToken);
        }
        IRequestHandler requestHandler = _mediatorProvider.GetRequestHandler(_serviceProvider, request.GetType());
        object result = await requestHandler.ProcessAsync(request, cancellationToken);
        IReadOnlyList<IRequestPostProcesor> postProcessors = _mediatorProvider.GetRequestPostProcessors(_serviceProvider, request.GetType());
        if (postProcessors.Count > 0)
        {
            for (int i = 0; i < postProcessors.Count; ++i)
            {
                await postProcessors[i].ProcessAsync(request, result, cancellationToken).ConfigureAwait(false);
            }
        }
    }

    async Task IMediatorServiceBase.RaiseAsync(IEvent notification, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(notification);
        IReadOnlyList<IEventHandler> listeners = _mediatorProvider.GetEventHandlers(_serviceProvider, notification.GetType());
        for (int i = 0; i < listeners.Count; ++i)
        {
            await listeners[i].HandleAsync(notification, cancellationToken).ConfigureAwait(false);
        }
    }
}
