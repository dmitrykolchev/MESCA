// <copyright file="RequestHandler.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

namespace Xobex.Mediator;

public abstract class RequestHandler<TRequest, TResult> : IRequestHandler<TRequest, TResult>
    where TRequest : IRequest<TResult>
    where TResult : notnull
{
    protected RequestHandler()
    {
    }

    public virtual Task<TResult> HandleAsync(TRequest request, CancellationToken cancellationToken)
    {
        return HandleOverrideAsync(request, cancellationToken);
    }

    protected abstract Task<TResult> HandleOverrideAsync(TRequest request, CancellationToken cancellationToken);

    async Task<object> IRequestHandler.HandleAsync(IRequest request, CancellationToken cancellationToken)
    {
        return (await HandleAsync((TRequest)request, cancellationToken).ConfigureAwait(false))!;
    }
}

