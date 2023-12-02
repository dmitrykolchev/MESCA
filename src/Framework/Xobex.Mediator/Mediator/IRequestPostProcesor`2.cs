// <copyright file="IRequestPostProcesor`2.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

namespace Xobex.Mediator;

public interface IRequestPostProcessor<in TRequest, TResult> : IRequestPostProcesor
    where TRequest : IRequest<TResult>
    where TResult : notnull
{
    Task HandleAsync(TRequest request, TResult response, CancellationToken cancellationToken);
}


public interface IRequestPostProcesor
{
    int Ordinal { get; }
    Task HandleAsync(IRequest request, object response, CancellationToken cancellationToken);
}
