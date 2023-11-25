// <copyright file="DocumentNumberTemplate.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Xobex.Domain.Common;

namespace Xobex.Mes.Entities.Core;

public enum DocumentNumberTemplateState : short
{
    NotExists = 0,
    Active = 1,
    Inactive = 2
}

[Flags]
public enum DocumentNumberTemplateFlags : int
{
    None = 0
}

public enum DocumentNumberResetPeriod : short
{
    None,
    Day,
    Week,
    Month,
    Quarter,
    Year
}

public class DocumentNumberTemplate : DocumentBase<int, DocumentNumberTemplateState>
{
    public DocumentNumberTemplate()
    {
    }

    public DocumentNameTemplateFlags Flags { get; set; }
    public int DocumentTypeId { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public string? Selector { get; set; }
    public DocumentNumberResetPeriod ResetPeriod { get; set; }
    public DateOnly ResetDate { get; set; }
    public long InitialValue { get; set; }
    public long Increment { get; set; }
    public long? MaxValue { get; set; }
    public long NextValue { get; set; }
    public required string ModelTypeName { get; set; }
    public required string Template { get; set; }
    public string? SelectorTemplate { get; set; }

    public virtual ICollection<DocumentNumberTemplateCounter>? Counters { get; set; }
}
