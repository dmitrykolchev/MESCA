// <copyright file="Currency.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Xobex.Domain.Common;

namespace Xobex.Mes.Entities.Dictionaries;

public enum CurrencyState : short
{
    NotExists = 0,
    Active = 1,
    Inactive = 2
}

public class Currency : DocumentBase<int, CurrencyState>
{
}
