// <copyright file="PersonCreatedEvent.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using MediatR;

namespace Xobex.MediatR.Benchmark.Person;

public class PersonCreatedEvent : INotification
{
    public int PersonId { get; set; }
}
