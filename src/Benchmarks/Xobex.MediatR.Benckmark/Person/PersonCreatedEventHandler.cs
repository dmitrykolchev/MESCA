// <copyright file="PersonCreatedEventHandler.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using MediatR;

namespace Xobex.MediatR.Benchmark.Person;

public class PersonCreatedEventHandler : INotificationHandler<PersonCreatedEvent>
{
    public Task Handle(PersonCreatedEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
