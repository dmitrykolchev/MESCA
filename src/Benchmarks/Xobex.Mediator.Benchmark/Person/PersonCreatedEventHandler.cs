// <copyright file="PersonCreatedEventHandler.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Microsoft.Extensions.DependencyInjection;

namespace Xobex.Mediator.Benchmark.Person;

[MediatorLifetime(ServiceLifetime.Scoped)]
public class PersonCreatedEventHandler : Mediator.EventHandler<PersonCreatedEvent>
{
    public override Task HandleAsync(PersonCreatedEvent notification, CancellationToken cancellation)
    {
        return Task.CompletedTask;
    }
}
