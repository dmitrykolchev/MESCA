// <copyright file="IRequestHandler.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

namespace Xobex.Mediator;

public interface IRequestHandler<in TRequest, TResult>: IRequestHandler
    where TRequest : IRequest<TResult>
    where TResult: notnull
{
    Task<TResult> ProcessAsync(TRequest request, CancellationToken cancellation);
}


