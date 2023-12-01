// <copyright file="LongRunningLoggerBehaviour.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Xobex.Mediator;

namespace Xobex.Data.Mes.Application;

internal class LongRunningLoggerBehaviour : Behavior
{
    public override Task<object?> ProcessAsync(IRequest request, Func<Task<object>>? next, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
