// <copyright file="UnitOfMeasureConversion.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Xobex.Data.Common;

namespace Xobex.Mes.Entities.Dictionaries;

public class UnitOfMeasureConversion : ISimpleAuditable
{
    public int Id { get; set; }
    public int FromId { get; set; }
    public int ToId { get; set; }
    public decimal Multiplier { get; set; }
    public decimal Divider { get; set; }
    public int Scale { get; set; }
    public DateTimeOffset ModifiedOn { get; set; }
    public int ModifiedBy { get; set; }

    public virtual UnitOfMeasure? From { get; set; }
    public virtual UnitOfMeasure? To { get; set; }
}
