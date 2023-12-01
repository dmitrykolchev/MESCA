// <copyright file="GetPeopleCommand.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Xobex.Mediator;

namespace Xobex.Data.Sample1.Person;

public class GetPeopleCommand : Request<IEnumerable<PersonModel>>
{
    public static readonly GetPeopleCommand Instance = new();

    public GetPeopleCommand()
    {
    }

    public int Id { get; set; }
}
