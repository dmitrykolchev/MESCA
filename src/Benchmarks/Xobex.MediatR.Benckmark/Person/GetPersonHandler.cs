// <copyright file="GetPersonHandler.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using MediatR;
using Xobex.Mediator.Benchmark.Person;

namespace Xobex.MediatR.Benchmark.Person;

public class GetPersonHandler : IRequestHandler<GetPersonCommand, PersonModel>
{
    public GetPersonHandler(PersonRepository personRepository)
    {
        PersonRepository = personRepository;
    }

    public PersonRepository PersonRepository { get; }

    public Task<PersonModel> Handle(GetPersonCommand request, CancellationToken cancellationToken)
    {
        return Task.FromResult(PersonRepository.Get(request.PersonId));
    }
}
