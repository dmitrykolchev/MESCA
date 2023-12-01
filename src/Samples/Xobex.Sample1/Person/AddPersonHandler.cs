// <copyright file="CreatePersonHandler.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Xobex.Mediator;

namespace Xobex.Data.Sample1.Person;
public class AddPersonHandler : RequestHandler<AddPersonCommand, int>
{
    private readonly IMediatorService _mediator;
    private static int _id;

    public AddPersonHandler(IMediatorService mediator)
    {
        _mediator = mediator;
    }

    protected override async Task<int> ProcessOverrideAsync(AddPersonCommand request, CancellationToken cancellationToken)
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
