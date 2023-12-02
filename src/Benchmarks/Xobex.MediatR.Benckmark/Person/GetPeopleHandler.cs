// <copyright file="GetPeopleHandler.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using MediatR;
using Xobex.Mediator.Benchmark.Person;

namespace Xobex.MediatR.Benchmark.Person;

public class GetPeopleHandler : IRequestHandler<GetPeopleCommand, IEnumerable<PersonModel>>
{
    public GetPeopleHandler(PersonRepository personRepository)
    {
        PersonRepository = personRepository;
    }

    public PersonRepository PersonRepository { get; }

    public Task<IEnumerable<PersonModel>> Handle(GetPeopleCommand request, CancellationToken cancellationToken)
    {
        return Task.FromResult(PersonRepository.All());
    }
}
