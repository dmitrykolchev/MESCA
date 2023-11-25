// <copyright file="DocumentState.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Xobex.Domain.Common;

namespace Xobex.Mes.Entities.Metadata;

public enum DocumentStateState : short
{
    NotExists,
    Active,
    Inactive
}

[Flags]
public enum DocumentStateFlags : int
{
    None = 0
}

public class DocumentState: IAuditable
{
    public int Id { get; set; }
    public DocumentStateState State { get; set; }
    public DocumentStateFlags Flags { get; set; }
    public int DocumentTypeId { get; set; }
    public short Value { get; set; }
    public required string Code { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public string? LaberColor { get; set; }
    public string? Comments { get; set; }

    public DateTimeOffset CreatedOn { get; set; }
    public int CreatedBy { get; set; }
    public DateTimeOffset ModifiedOn { get; set; }
    public int ModifiedBy { get; set; }


    public virtual DocumentType DocumentType { get; set; } = null!;
}
