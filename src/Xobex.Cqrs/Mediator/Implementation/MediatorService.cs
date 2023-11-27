// <copyright file="MediatorService.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Microsoft.Extensions.DependencyInjection;

namespace Xobex.Mediator.Implementation;

public class MediatorService(IServiceProvider serviceProvider, IMediatorProvider mediatorProvider) : IMediatorService
{
    private readonly IServiceProvider _serviceProvider = serviceProvider 
        ?? throw new ArgumentNullException(nameof(serviceProvider));
    private readonly IMediatorProvider _mediatorProvider = mediatorProvider 
        ?? throw new ArgumentNullException(nameof(mediatorProvider));

    public PipelineBuilder<TResult> CreatePipelineBuilder<TResult>()
    {
        return ActivatorUtilities.CreateInstance<PipelineBuilder<TResult>>(_serviceProvider);
    }

    public async Task RaiseAsync<TEvent>(TEvent notification, CancellationToken cancellationToken)
        where TEvent : IEvent
    {
        ArgumentNullException.ThrowIfNull(notification);
        IEnumerable<IEventHandler<TEvent>> listeners = _mediatorProvider.GetEventHandlers<TEvent>(_serviceProvider);
        foreach (IEventHandler<TEvent> listener in listeners)
        {
            await listener.HandleAsync(notification, cancellationToken).ConfigureAwait(false);
        }
    }

    public async Task SendAsync<TRequest>(TRequest request, CancellationToken cancellationToken)
        where TRequest : IRequest
    {
        ArgumentNullException.ThrowIfNull(request);

        await ValidateAsync(request, cancellationToken);

        IRequestHandler requestHandler = _mediatorProvider.GetRequestHandler(_serviceProvider, request.GetType());
        await requestHandler.ProcessAsync(request, cancellationToken).ConfigureAwait(false);
    }

    public async Task<TResult> QueryAsync<TResult>(IRequest<TResult> request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        await ValidateAsync(request, cancellationToken);

        IRequestHandler requestHandler = _mediatorProvider.GetRequestHandler(_serviceProvider, request.GetType());
        return (TResult)(await requestHandler.ProcessAsync(request, cancellationToken).ConfigureAwait(false))!;
    }

    private async Task ValidateAsync(IRequest request, CancellationToken cancellationToken)
    {
        IEnumerable<IValidator> validators = _mediatorProvider.GetValidators(_serviceProvider, request.GetType());
        if (validators.Any())
        {
            foreach (IValidator validator in validators)
            {
                await validator.ValidateAsync(request, cancellationToken);
            }
        }
    }
    async Task<object> IMediatorServiceBase.QueryAsync(IRequest request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        IEnumerable<IValidator> validators = _mediatorProvider.GetValidators(_serviceProvider, request.GetType());
        if (validators.Any())
        {
            foreach (IValidator validator in validators)
            {
                await validator.ValidateAsync(request, cancellationToken);
            }
        }
        IRequestHandler requestHandler = _mediatorProvider.GetRequestHandler(_serviceProvider, request.GetType());
        return (await requestHandler.ProcessAsync(request, cancellationToken))!;
    }

    async Task IMediatorServiceBase.SendAsync(IRequest request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        IEnumerable<IValidator> validators = _mediatorProvider.GetValidators(_serviceProvider, request.GetType());
        if (validators.Any())
        {
            foreach (IValidator validator in validators)
            {
                await validator.ValidateAsync(request, cancellationToken);
            }
        }
        IRequestHandler requestHandler = _mediatorProvider.GetRequestHandler(_serviceProvider, request.GetType());
        await requestHandler.ProcessAsync(request, cancellationToken);
    }

    async Task IMediatorServiceBase.RaiseAsync(IEvent notification, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(notification);
        IEnumerable<IEventHandler> listeners = _mediatorProvider.GetEventHandlers(_serviceProvider, notification.GetType());
        foreach (IEventHandler listener in listeners)
        {
            await listener.HandleAsync(notification, cancellationToken).ConfigureAwait(false);
        }
    }
}
