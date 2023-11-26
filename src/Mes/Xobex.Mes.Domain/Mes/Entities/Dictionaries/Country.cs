// <copyright file="Country.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Xobex.Domain.Common;

namespace Xobex.Mes.Entities.Dictionaries;

public enum CountryState
{
    NotExists = 0,
    Active = 1,
    Inactive = 2
}

public class Country : DocumentBase<int, CountryState>
{
}
