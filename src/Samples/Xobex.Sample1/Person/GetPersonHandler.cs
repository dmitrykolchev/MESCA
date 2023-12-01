// <copyright file="GetPersonHandler.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Xobex.Mediator;

namespace Xobex.Data.Sample1.Person;

public class GetPersonHandler : RequestHandler<GetPersonCommand, PersonModel>
{
    protected override Task<PersonModel> ProcessOverrideAsync(GetPersonCommand request, CancellationToken cancellation)
    {
        return Task.FromResult(PersonRepository.Get(request.PersonId));
    }
}
