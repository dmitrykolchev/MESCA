// <copyright file="AddPersonHandler.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using MediatR;
using Xobex.Mediator.Benchmark.Person;

namespace Xobex.MediatR.Benchmark.Person;

public class AddPersonHandler : IRequestHandler<AddPersonCommand, int>
{
    private readonly IMediator _mediator;

    public AddPersonHandler(IMediator mediator, PersonRepository personRepository)
    {
        _mediator = mediator;
        PersonRepository = personRepository;
    }

    public PersonRepository PersonRepository { get; }

    public async Task<int> Handle(AddPersonCommand request, CancellationToken cancellationToken)
    {
        PersonModel model = new()
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
        };
        PersonRepository.Add(model);
        await _mediator.Publish(new PersonCreatedEvent { PersonId = model.Id }, cancellationToken);
        return model.Id;
    }
}
