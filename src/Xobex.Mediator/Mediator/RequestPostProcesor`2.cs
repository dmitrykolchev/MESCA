﻿// <copyright file="RequestPostProcesor`2.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

namespace Xobex.Mediator;

public abstract class RequestPostProcessor<TRequest, TResult> : IRequestPostProcessor<TRequest, TResult>
    where TRequest : IRequest<TResult>
    where TResult : notnull
{
    protected RequestPostProcessor() { }

    public abstract Task ProcessAsync(TRequest request, TResult response, CancellationToken cancellationToken);

    Task IRequestPostProcesor.ProcessAsync(IRequest request, object response, CancellationToken cancellationToken)
    {
        return ProcessAsync((TRequest)request, (TResult)response, cancellationToken);
    }
}
