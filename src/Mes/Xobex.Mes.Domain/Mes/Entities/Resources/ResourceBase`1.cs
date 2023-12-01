// <copyright file="ResourceBase.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using System.ComponentModel.DataAnnotations.Schema;
using Xobex.Data.Common;

namespace Xobex.Data.Mes.Entities.Resources;

public abstract class ResourceBase<TState> : DocumentBase<int, TState>
    where TState : notnull
{
    protected ResourceBase() { }

    [Column(Order = IDocument.MaxColumnIndex + 1)]
    public Guid? Uid { get; set; }
}
