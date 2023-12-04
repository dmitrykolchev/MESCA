// <copyright file="IDocument`1.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using System.ComponentModel.DataAnnotations.Schema;

namespace Xobex.Data.Common;

public interface IDocument<TKey> : IDocument
{
    /// <summary>
    /// Document ID
    /// </summary>
    [Column(Order = 0)]
    new TKey Id { get; set; }
}
