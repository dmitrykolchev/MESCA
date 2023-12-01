// <copyright file="Document.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

namespace Xobex.Data.Entities.Core;

public class Document
{
    public Document()
    {
    }

    public int DocumentTypeId { get; set; }
    public int DocumentId { get; set; }
    public int Revision { get; set; }
    public short State { get; set; }
    public int? ParentId { get; set; }
    public string? Code { get; set; }
    public string? Name { get; set; }

    public DateTimeOffset CreatedOn { get; set; }
    public int CreatedBy { get; set; }
    public DateTimeOffset ModifiedOn { get; set; }
    public int ModifiedBy { get; set; }
}
