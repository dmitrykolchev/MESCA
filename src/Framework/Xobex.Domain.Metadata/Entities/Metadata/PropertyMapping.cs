// <copyright file="PropertyMapping.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Xobex.Data.Common;
using Xobex.Data.Entities.Core;

namespace Xobex.Data.Entities.Metadata;

public enum PropertyMappingState : short
{
    NotExists = 0,
}

[Flags]
public enum PropertyMappingFlags
{
    None = 0,
    Required = 1,
    AllowMultiple = 2,
}

public class PropertyMapping : IAuditable
{
    public int Id { get; set; }
    public PropertyMappingState State { get; set; }
    public PropertyMappingFlags Flags { get; set; }
    public required string Code { get; set; }
    public required string Name { get; set; }
    public int DocumentTypeId { get; set; }
    public int? DocumentId { get; set; }

    public int PropertyId { get; set; }
    public int Ordinal { get; set; }
    public string? Comments { get; set; }

    public DateTimeOffset CreatedOn { get; set; }
    public int CreatedBy { get; set; }
    public DateTimeOffset ModifiedOn { get; set; }
    public int ModifiedBy { get; set; }

    public virtual DocumentType? DocumentType { get; set; }
    public virtual Document? Document { get; set; }
    public virtual Property? Property { get; set; }
}
