// <copyright file="GetPersonCommand.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using MediatR;
using Xobex.Mediator.Benchmark.Person;

namespace Xobex.MediatR.Benchmark.Person;

public class GetPersonCommand(int personId) : IRequest<PersonModel>
{
    public int PersonId { get; set; } = personId;
}
