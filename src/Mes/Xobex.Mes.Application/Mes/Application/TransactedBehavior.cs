// <copyright file="TransactedBehavior.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Xobex.Mediator;

namespace Xobex.Mes.Application;

public class TransactedBehavior<TRequest, TResult> : IBehavior<TRequest, TResult>
    where TRequest : IRequest<TResult>
{
    public async Task<TResult?> ProcessAsync(IRequest<TResult> request, Func<Task<TResult>>? next, CancellationToken cancellationToken)
    {
        if (next != null)
        {
            return await next();
        }
        return default;
    }

    async Task<object?> IBehavior.ProcessAsync(IRequest request, Func<Task<object>>? next, CancellationToken cancellationToken)
    {
        if (next != null)
        {
            Func<Task<TResult>> func = async () =>
            {
                return (TResult)(await next())!;
            };
            return await ProcessAsync((IRequest<TResult>)request, func, cancellationToken);
        }
        return await ProcessAsync((IRequest<TResult>)request, null, cancellationToken);
    }
}
