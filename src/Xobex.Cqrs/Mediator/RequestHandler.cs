// <copyright file="RequestHandler.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

namespace Xobex.Mediator;

public abstract class RequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public abstract Task<TResponse> ProcessAsync(TRequest request, CancellationToken cancellationToken);

    async Task<object?> IRequestHandler.ProcessAsync(IRequest request, CancellationToken cancellationToken)
    {
        return await ProcessAsync((TRequest)request, cancellationToken).ConfigureAwait(false);
    }
}

public abstract class RequestHandler<TRequest> : IRequestHandler<TRequest>
    where TRequest : IRequest
{
    public abstract Task ProcessAsync(TRequest request, CancellationToken cancellationToken);

    async Task<object?> IRequestHandler.ProcessAsync(IRequest request, CancellationToken cancellationToken)
    {
        await ProcessAsync((TRequest)request, cancellationToken).ConfigureAwait(false);
        return null;
    }
}
