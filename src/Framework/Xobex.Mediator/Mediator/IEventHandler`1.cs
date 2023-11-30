// <copyright file="INotificationListener.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

namespace Xobex.Mediator;

public interface IEventHandler<in TEvent>: IEventHandler
    where TEvent : IEvent
{
    Task HandleAsync(TEvent notification, CancellationToken cancellation);
}
