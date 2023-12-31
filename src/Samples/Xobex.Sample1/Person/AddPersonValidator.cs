﻿// <copyright file="CreatePersonValidator.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Xobex.Mediator;

namespace Xobex.Data.Sample1.Person;
public class AddPersonValidator : Validator<AddPersonCommand>
{
    public override Task ValidateAsync(AddPersonCommand request, CancellationToken cancellationToken)
    {
        return string.IsNullOrEmpty(request.FirstName) || string.IsNullOrEmpty(request.LastName)
            ? throw new ArgumentException("FirstName and LastName must be set")
            : Task.CompletedTask;
    }
}
