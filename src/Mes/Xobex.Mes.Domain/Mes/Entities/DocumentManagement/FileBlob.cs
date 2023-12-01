// <copyright file="FileAttachment.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Xobex.Data.Common;

namespace Xobex.Data.Mes.Entities.DocumentManagement;

public enum FileAttachmentState : short
{
    NotExists = 0,
    Temporary = 1,
    Active = 2
}

public class FileBlob : ISimpleAuditable
{
    public int Id { get; set; }
    public FileAttachmentState State { get; set; }
    public required string Name { get; set; }
    public required string OriginalFileName { get; set; }
    public string? Extension { get; set; }
    public required string ContentType { get; set; }
    public required string RelativePath { get; set; }
    public required string HashValue { get; set; }
    public string? Comments { get; set; }

    public DateTimeOffset ModifiedOn { get; set; }
    public int ModifiedBy { get; set; }
}
