// <copyright file="GetPeopleCommand.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using MediatR;
using Xobex.Mediator.Benchmark.Person;

namespace Xobex.MediatR.Benchmark.Person;

public class GetPeopleCommand : IRequest<IEnumerable<PersonModel>>
{
    public static readonly GetPeopleCommand Instance = new();

    private GetPeopleCommand()
    {
    }
}
