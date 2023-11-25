// <copyright file="PersonCreatedEventHandler.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

namespace Xobex.Sample1.Person;

public class PersonCreatedEventHandler : Mediator.EventHandler<PersonCreatedEvent>
{
    public override Task HandleAsync(PersonCreatedEvent notification, CancellationToken cancellation)
    {
        Console.WriteLine($"Person with id = {notification.PersonId} created");
        return Task.CompletedTask;
    }
}
