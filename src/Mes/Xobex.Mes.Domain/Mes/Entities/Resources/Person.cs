// <copyright file="Person.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

namespace Xobex.Data.Mes.Entities.Resources;

public enum PersonState: short
{
    NotExists = 0,
    Active = 1,
    Inactive = 2
}

public class Person : ResourceBase<PersonState>
{
    public Person()
    {
    }

    public virtual Resource? Resource { get; set; }
}
