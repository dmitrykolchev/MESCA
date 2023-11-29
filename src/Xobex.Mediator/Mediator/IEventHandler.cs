// <copyright file="IEventHandler.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

namespace Xobex.Mediator;

public interface IEventHandler
{
    int Ordinal => 0;
    Task HandleAsync(IEvent notification, CancellationToken cancellation);
}

