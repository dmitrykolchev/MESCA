// <copyright file="PropertyValue.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Xobex.Domain.Common;

namespace Xobex.Mes.Entities.Core;

public class PropertyValue : ISimpleAuditable
{
    public PropertyValue()
    {
    }

    public long Id { get; set; }
    public int DocumentTypeId { get; set; }
    public int DocumentId { get; set; }
    public int Revision { get; set; }
    public int PropertyId { get; set; }
    public DataKind Kind { get; set; }

    public int? IntValue { get; set; }
    public DateTime? DateTimeValue { get; set; }
    public string? LeadingStringValue { get; set; }
    public string? TrailingStringValue { get; set; }
    public decimal? DecimalValue { get; set; }
    public double? DoubleValue { get; set; }
    public byte[]? BinaryValue { get; set; }

    public DateTimeOffset ModifiedOn { get; set; }
    public int ModifiedBy { get; set; }

    public virtual Document? Document { get; set; }
    public virtual Property? Property { get; set; }
}
