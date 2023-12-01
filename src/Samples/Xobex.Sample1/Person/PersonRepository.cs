// <copyright file="PersonRepository.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

namespace Xobex.Data.Sample1.Person;
internal class PersonRepository
{
    public static Dictionary<int, PersonModel> _people = [];

    public static void Add(PersonModel person)
    {
        _people.Add(person.Id, person);
    }

    public static PersonModel Get(int personId)
    {
        return _people[personId];
    }

    public static IEnumerable<PersonModel> All()
    {
        return _people.Values.OrderBy(x => x.LastName).ThenBy(x => x.FirstName);
    }
}
