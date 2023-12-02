// <copyright file="GetPeopleHandler.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Microsoft.Extensions.DependencyInjection;

namespace Xobex.Mediator.Benchmark.Person;

[MediatorLifetime(ServiceLifetime.Scoped)]
public class GetPeopleHandler : RequestHandler<GetPeopleCommand, IEnumerable<PersonModel>>
{
    public GetPeopleHandler(PersonRepository personRepository)
    {
        PersonRepository = personRepository;
    }

    public PersonRepository PersonRepository { get; }

    protected override Task<IEnumerable<PersonModel>> HandleOverrideAsync(GetPeopleCommand request, CancellationToken cancellation)
    {
        return Task.FromResult(PersonRepository.All());
    }
}
