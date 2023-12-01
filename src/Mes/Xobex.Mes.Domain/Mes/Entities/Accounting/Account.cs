// <copyright file="Account.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Xobex.Data.Common;

namespace Xobex.Data.Mes.Entities.Accounting;

public enum AccountState : short
{
    NotExists = 0,
    Active = 1,
    Inactive = 2
}

[Flags]
public enum AccountFlags : int
{
    None = 0,
}

public class Account : DocumentBase<int, AccountState>
{
    public AccountFlags Flags { get; set; }
}
