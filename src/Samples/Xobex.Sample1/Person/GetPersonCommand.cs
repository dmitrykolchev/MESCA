// <copyright file="GetPersonCommand.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Xobex.Mediator;

namespace Xobex.Sample1.Person;

public class GetPersonCommand(int personId) : Request<PersonModel>
{
    public int PersonId { get; set; } = personId;
}
