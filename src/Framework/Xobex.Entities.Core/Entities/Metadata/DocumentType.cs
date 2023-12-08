// <copyright file="DocumentType.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Xobex.Data.Common;

namespace Xobex.Entities.Metadata;

[Flags]
public enum DocumentTypeFlags : int
{
    None = 0,
}

public class DocumentType : IAuditable
{
    public int Id { get; set; }
    public CommonStates State { get; set; }
    public DocumentTypeFlags Flags { get; set; }
    public required string Code { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required string Image { get; set; }
    public string? Comments { get; set; }

    public DateTimeOffset CreatedOn { get; set; }
    public int CreatedBy { get; set; }
    public DateTimeOffset ModifiedOn { get; set; }
    public int ModifiedBy { get; set; }

    public virtual ICollection<DocumentTypeGlobal>? Languages { get; set; }
    public virtual ICollection<DocumentState>? States { get; set; }
    public virtual ICollection<TransitionTemplate>? TransitionTemplates { get; set; }
    public virtual ICollection<PropertyMapping>? Properties { get; set; }
}
