// <copyright file="CreatePersonCommand.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Xobex.Mediator;

namespace Xobex.Sample1.Person;

public class AddPersonCommand : Request<int>
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
}

