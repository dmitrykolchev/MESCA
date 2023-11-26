﻿// <copyright file="IBehavior.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

namespace Xobex.Mediator;

public interface IBehavior<TRequest, TResponse> : IBehavior
    where TRequest : IRequest<TResponse>
{
    Task<TResponse> ProcessAsync(TRequest request, Func<Task<TRequest>> callback, CancellationToken cancellation);
}

public interface IBehavior
{
    Task<object> ProcessAsync(IRequest request, Func<Task<object>> callback, CancellationToken cancellation);
}
