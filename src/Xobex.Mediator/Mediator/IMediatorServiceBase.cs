// <copyright file="IMediatorServiceBase.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

namespace Xobex.Mediator;

public interface IMediatorServiceBase
{
    public Task SendAsync(IRequest request, CancellationToken cancellationToken);

    public Task<object> QueryAsync(IRequest request, CancellationToken cancellationToken);

    public Task RaiseAsync(IEvent data, CancellationToken cancellationToken);
}
