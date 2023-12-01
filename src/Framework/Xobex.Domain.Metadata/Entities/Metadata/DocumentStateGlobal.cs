// <copyright file="DocumentStateGlobal.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Xobex.Data.Common;

namespace Xobex.Data.Entities.Metadata;

public class DocumentStateGlobal: IAuditable
{
    public int DocumentStateId { get; set; }
    public required string Language { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }

    public DateTimeOffset CreatedOn { get; set; }
    public int CreatedBy { get; set; }
    public DateTimeOffset ModifiedOn { get; set; }
    public int ModifiedBy { get; set; }

    public virtual DocumentState? State { get; set; }
}
