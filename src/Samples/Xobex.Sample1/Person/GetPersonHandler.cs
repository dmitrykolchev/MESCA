// <copyright file="GetPersonHandler.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Xobex.Mediator;

namespace Xobex.Sample1.Person;

public class GetPersonHandler : RequestHandler<GetPersonCommand, PersonModel>
{
    public override Task<PersonModel> ProcessAsync(GetPersonCommand request, CancellationToken cancellation)
    {
        return Task.FromResult(PersonRepository.Get(request.PersonId));
    }
}
