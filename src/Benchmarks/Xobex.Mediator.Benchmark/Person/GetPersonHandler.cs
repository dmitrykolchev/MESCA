// <copyright file="GetPersonHandler.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Microsoft.Extensions.DependencyInjection;

namespace Xobex.Mediator.Benchmark.Person;

[MediatorLifetime(ServiceLifetime.Scoped)]
public class GetPersonHandler : RequestHandler<GetPersonCommand, PersonModel>
{
    public GetPersonHandler(PersonRepository personRepository)
    {
        PersonRepository = personRepository;
    }

    public PersonRepository PersonRepository { get; }

    protected override Task<PersonModel> HandleOverrideAsync(GetPersonCommand request, CancellationToken cancellation)
    {
        return Task.FromResult(PersonRepository.Get(request.PersonId));
    }
}
