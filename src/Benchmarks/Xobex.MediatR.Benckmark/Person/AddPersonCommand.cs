// <copyright file="AddPersonCommand.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using MediatR;

namespace Xobex.MediatR.Benchmark.Person;

public class AddPersonCommand : IRequest<int>
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
}

