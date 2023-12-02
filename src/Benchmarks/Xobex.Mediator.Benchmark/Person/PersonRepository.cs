// <copyright file="PersonRepository.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

namespace Xobex.Mediator.Benchmark.Person;

public class PersonRepository
{
    private readonly Dictionary<int, PersonModel> _people = [];
    private int _id;

    public void Add(PersonModel person)
    {
        if (person.Id == 0)
        {
            person.Id = ++_id;
        }
        _people.Add(person.Id, person);
    }

    public PersonModel Get(int personId)
    {
        return _people[personId];
    }

    public IEnumerable<PersonModel> All()
    {
        return _people.Values.OrderBy(x => x.LastName).ThenBy(x => x.FirstName);
    }
}
