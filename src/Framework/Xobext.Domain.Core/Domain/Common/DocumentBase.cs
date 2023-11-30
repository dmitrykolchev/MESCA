// <copyright file="DocumentBase.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using System.ComponentModel.DataAnnotations.Schema;

namespace Xobex.Domain.Common;

public abstract class DocumentBase<TKey, TState> : IDocument<TKey>, IAuditable
    where TState : notnull
{
    protected DocumentBase() { }

    /// <summary>
    /// Document ID
    /// </summary>
    [Column(Order = 0)]
    public TKey Id { get; set; } = default!;
    /// <summary>
    /// Document State
    /// </summary>
    [Column(Order = 1)]
    public TState State { get; set; } = default!;
    /// <summary>
    /// Версия документа
    /// </summary>
    [Column(Order = 2)]
    public int Revision { get; set; }
    /// <summary>
    /// Родительский документ
    /// </summary>
    [Column(Order = 3)]
    public int? ParentId { get; set; }

    /// <summary>
    /// Document code / number
    /// </summary>
    [Column(Order = 4)]
    public string? Code { get; set; }
    /// <summary>
    /// Document name
    /// </summary>
    [Column(Order = 5)]
    public string? Name { get; set; }

    /// <summary>
    /// Comments
    /// </summary>
    public string? Comments { get; set; }

    /// <summary>
    /// UTC time when the document has been created
    /// </summary>
    public DateTimeOffset CreatedOn { get; set; }

    /// <summary>
    /// User ID who created the document
    /// </summary>
    public int CreatedBy { get; set; }

    /// <summary>
    /// UTC time when the document has been last modified
    /// </summary>
    public DateTimeOffset ModifiedOn { get; set; }

    /// <summary>
    /// User ID who last modified the document
    /// </summary>
    public int ModifiedBy { get; set; }

    object IDocument.Id { get => Id!; set => Id = (TKey)value; }

    short IDocument.State { get => (short)(object)State; set => State = (TState)(object)value; }
}
