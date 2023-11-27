// <copyright file="IBehavior.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

namespace Xobex.Mediator;

public interface IBehavior<TResult> : IBehavior
{
    Task<TResult?> ProcessAsync(IRequest<TResult> request, Func<Task<TResult>>? next, CancellationToken cancellationToken);
}

public interface IBehavior
{
    Task<object?> ProcessAsync(IRequest request, Func<Task<object>>? next, CancellationToken cancellationToken);
}
