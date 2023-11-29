// <copyright file="CreatePersonHandler.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Xobex.Mediator;

namespace Xobex.Sample1.Person;
public class CreatePersonHandler : RequestHandler<CreatePersonCommand, int>
{
    private readonly IMediatorService _mediator;
    private static int _id;

    public CreatePersonHandler(IMediatorService mediator)
    {
        _mediator = mediator;
    }

    protected override async Task<int> ProcessOverrideAsync(CreatePersonCommand request, CancellationToken cancellationToken)
    {
        PersonModel model = new()
        {
            Id = ++_id,
            FirstName = request.FirstName,
            LastName = request.LastName,
        };
        PersonRepository.Add(model);
        await _mediator.RaiseEventAsync(new PersonCreatedEvent { PersonId = model.Id }, cancellationToken);
        return model.Id;
    }
}
