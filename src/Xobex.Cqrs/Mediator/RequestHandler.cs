// <copyright file="RequestHandler.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

namespace Xobex.Mediator;

public abstract class RequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    protected RequestHandler()
    {
    }

    public virtual Task<TResponse> ProcessAsync(TRequest request, CancellationToken cancellationToken)
    {
        return ProcessOverrideAsync(request, cancellationToken);
    }

    protected abstract Task<TResponse> ProcessOverrideAsync(TRequest request, CancellationToken cancellationToken);

    async Task<object> IRequestHandler.ProcessAsync(IRequest request, CancellationToken cancellationToken)
    {
        return (await ProcessAsync((TRequest)request, cancellationToken).ConfigureAwait(false))!;
    }
}

