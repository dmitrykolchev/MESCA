// <copyright file="RequestPostProcesor`2.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

namespace Xobex.Mediator;

public abstract class RequestPostProcessor<TRequest, TResult> : IRequestPostProcessor<TRequest, TResult>
    where TRequest : IRequest<TResult>
    where TResult : notnull
{
    protected RequestPostProcessor() { }

    public virtual int Ordinal => 0;

    public abstract Task HandleAsync(TRequest request, TResult response, CancellationToken cancellationToken);

    Task IRequestPostProcessor.HandleAsync(IRequest request, object response, CancellationToken cancellationToken)
    {
        return HandleAsync((TRequest)request, (TResult)response, cancellationToken);
    }
}
