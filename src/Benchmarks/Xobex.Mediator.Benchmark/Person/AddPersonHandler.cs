// <copyright file="AddPersonHandler.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Microsoft.Extensions.DependencyInjection;

namespace Xobex.Mediator.Benchmark.Person;

[MediatorLifetime(ServiceLifetime.Scoped)]
public class AddPersonHandler : RequestHandler<AddPersonCommand, int>
{
    private readonly IMediator _mediator;

    public AddPersonHandler(IMediator mediator, PersonRepository personRepository)
    {
        _mediator = mediator;
        PersonRepository = personRepository;
    }

    public PersonRepository PersonRepository { get; }

    protected override async Task<int> HandleOverrideAsync(AddPersonCommand request, CancellationToken cancellationToken)
    {
        PersonModel model = new()
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
        };
        PersonRepository.Add(model);
        await _mediator.RaiseEventAsync(new PersonCreatedEvent { PersonId = model.Id }, cancellationToken);
        return model.Id;
    }
}
