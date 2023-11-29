// <copyright file="NotificationListener`1.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

namespace Xobex.Mediator;

public abstract class EventHandler<TEvent> : IEventHandler<TEvent>
    where TEvent : IEvent
{
    protected EventHandler() { }

    public virtual int Ordinal => 0;

    public abstract Task HandleAsync(TEvent notification, CancellationToken cancellationToken);

    Task IEventHandler.HandleAsync(IEvent notification, CancellationToken cancellationToken)
    {
        return HandleAsync((TEvent)notification, cancellationToken);
    }
}
