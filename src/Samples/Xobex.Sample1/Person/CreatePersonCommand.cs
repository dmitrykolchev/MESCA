﻿// <copyright file="CreatePersonCommand.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Xobex.Mediator;

namespace Xobex.Sample1.Person;

public class CreatePersonCommand : IRequest<int>
{
    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;
}
