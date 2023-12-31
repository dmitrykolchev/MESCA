﻿// <copyright file="GetPeopleHandler.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Xobex.Mediator;

namespace Xobex.Data.Sample1.Person;

public class GetPeopleHandler : RequestHandler<GetPeopleCommand, IEnumerable<PersonModel>>
{
    protected override Task<IEnumerable<PersonModel>> HandleOverrideAsync(GetPeopleCommand request, CancellationToken cancellation)
    {
        return Task.FromResult(PersonRepository.All());
    }
}
