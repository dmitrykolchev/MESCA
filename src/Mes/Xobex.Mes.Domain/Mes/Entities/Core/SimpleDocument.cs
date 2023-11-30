// <copyright file="SimpleDocument.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Xobex.Domain.Common;
using Xobex.Entities.Metadata;

namespace Xobex.Mes.Entities.Core;

public class SimpleDocument : DocumentBase<int, short>
{
    public int DocumentTypeId { get; set; }

    public virtual DocumentType? DocumentType { get; set; }
}
