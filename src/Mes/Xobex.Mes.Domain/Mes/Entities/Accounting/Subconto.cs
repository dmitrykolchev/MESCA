// <copyright file="Subconto.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Xobex.Domain.Common;
using Xobex.Entities.Metadata;

namespace Xobex.Mes.Entities.Accounting;

public enum SubcontoState : byte
{
    NotExists = 0,
    Active = 1,
}

[Flags]
public enum SubcontoFlags : int
{
    None = 0,
}

public class Subconto : DocumentBase<int, SubcontoState>
{
    public SubcontoFlags Flags { get; set; }
    public int AccountId { get; set; }
    public int? DocumentTypeId { get; set; }
    public int Ordinal { get; set; }

    public virtual Account? Account { get; set; }
    public virtual DocumentType? DocumentType { get; set; }
}
