// <copyright file="OperationPart.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Xobex.Mes.Entities.Dictionaries;

namespace Xobex.Mes.Entities.Accounting;

public enum OperationSide: short
{
    Debit = 0,
    Credit = 1
}

public class OperationPart
{
    public long OperationId { get; set; }
    public OperationSide Side { get; set; }
    public int AccountId { get; set; }
    public decimal Amount { get; set; }
    public decimal Value { get; set; }
    public int CurrencyId { get; set; }

    public int? Subconto00 { get; set; }
    public int? Subconto01 { get; set; }
    public int? Subconto02 { get; set; }
    public int? Subconto03 { get; set; }
    public int? Subconto04 { get; set; }
    public int? Subconto05 { get; set; }
    public int? Subconto06 { get; set; }
    public int? Subconto07 { get; set; }
    public int? Subconto08 { get; set; }
    public int? Subconto09 { get; set; }
    public int? Subconto10 { get; set; }
    public int? Subconto11 { get; set; }
    public int? Subconto12 { get; set; }
    public int? Subconto13 { get; set; }
    public int? Subconto14 { get; set; }
    public int? Subconto15 { get; set; }
    public int? Subconto16 { get; set; }
    public int? Subconto17 { get; set; }
    public int? Subconto18 { get; set; }
    public int? Subconto19 { get; set; }

    public string? Tag00 { get; set; }
    public string? Tag01 { get; set; }
    public string? Tag02 { get; set; }
    public string? Tag03 { get; set; }

    public virtual Operation? Operation { get; set; }
    public virtual Account? Account { get; set; }
    public virtual Currency? Currency { get; set; }
}
