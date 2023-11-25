// <copyright file="DocumentNameTemplate.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Xobex.Domain.Common;

namespace Xobex.Mes.Entities.Core;

public enum DocumentNameTemplateState : short
{
    NotExists = 0,
    Active = 1,
    Inactive = 2
}

[Flags]
public enum DocumentNameTemplateFlags : int
{
    None = 0
}

public class DocumentNameTemplate : DocumentBase<int, DocumentNameTemplateState>
{
    public DocumentNameTemplate()
    {
    }
    public DocumentNameTemplateFlags Flags { get; set; }
    public int DocumentTypeId { get; set; }
    public int Ordinal { get; set; }
    public required string ModelTypeName { get; set; }
    public required string Template { get; set; }
    public string? Selector { get; set; }
}
