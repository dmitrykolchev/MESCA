// <copyright file="DocumentNote.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Xobex.Data.Common;
using Xobex.Data.Entities.Core;

namespace Xobex.Data.Mes.Entities.Core;

public enum DocumentNoteState: short
{
    NotExists = 0,
    Created = 1,
    Edited = 2,
    Deleted = 3,
}

public class DocumentNote : IAuditable
{
    public int Id { get; set; }
    public DocumentNoteState State { get; set; }
    public int DocumentTypeId { get; set; }
    public int DocumentId { get; set; }
    public required string Note { get; set; }

    public DateTimeOffset CreatedOn { get; set; }
    public int CreatedBy { get; set; }
    public DateTimeOffset ModifiedOn { get; set; }
    public int ModifiedBy { get; set; }

    public Document? Document { get; set; }
}
