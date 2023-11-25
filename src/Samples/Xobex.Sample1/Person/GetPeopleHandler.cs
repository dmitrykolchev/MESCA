// <copyright file="GetPeopleHandler.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Xobex.Mediator;

namespace Xobex.Sample1.Person;

public class GetPeopleHandler : RequestHandler<GetPeopleCommand, IEnumerable<PersonModel>>
{
    public override Task<IEnumerable<PersonModel>> ProcessAsync(GetPeopleCommand request, CancellationToken cancellation)
    {
        return Task.FromResult(PersonRepository.All());
    }
}
