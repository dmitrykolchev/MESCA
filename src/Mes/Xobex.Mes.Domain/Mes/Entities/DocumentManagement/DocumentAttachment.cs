// <copyright file="DocumentAttachment.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Xobex.Data.Common;
using Xobex.Data.Entities.Core;

namespace Xobex.Mes.Entities.DocumentManagement;

public enum AttachmentType: short
{
    Unknown = 0,
    File = 1,
}

public class DocumentAttachment : ISimpleAuditable
{
    public int Id { get; set; }
    public int DocumentTypeId { get; set; }
    public int DocumentId { get; set; }
    public AttachmentType AttachmentType { get; set; }
    public int? FileId { get; set; }
    public string? Comments { get; set; }

    public DateTimeOffset ModifiedOn { get; set; }
    public int ModifiedBy { get; set; }

    public virtual Document? Document { get; set; }
    public virtual FileBlob? File { get; set; }
}
