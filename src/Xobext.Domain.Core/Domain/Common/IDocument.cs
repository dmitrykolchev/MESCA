// <copyright file="IDocument.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using System.ComponentModel.DataAnnotations.Schema;

namespace Xobex.Domain.Common;

public interface IDocument: IAuditable
{
    public const int MaxColumnIndex = 5;
    /// <summary>
    /// Document ID
    /// </summary>
    [Column(Order = 0)]
    object Id { get; set; }

    /// <summary>
    /// Document State
    /// </summary>
    [Column(Order = 1)]
    short State { get; set; }
    /// <summary>
    /// Версия
    /// </summary>
    [Column(Order = 2)]
    int Revision { get; set; }

    /// <summary>
    /// Родительский документ
    /// </summary>
    [Column(Order = 3)]
    int? ParentId { get; set; }
    /// <summary>
    /// Document code / number
    /// </summary>
    [Column(Order = 4)]
    string? Code { get; set; }

    /// <summary>
    /// Document name
    /// </summary>
    [Column(Order = 5)]
    string? Name { get; set; }

    /// <summary>
    /// Comments
    /// </summary>
    string? Comments { get; set; }
}
