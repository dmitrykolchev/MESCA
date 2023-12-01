// <copyright file="Operation.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Xobex.Data.Common;
using Xobex.Data.Entities.Core;

namespace Xobex.Data.Mes.Entities.Accounting;

public enum OperationState : short
{
    NotExists = 0,
    Active = 1,
}

public class Operation : DocumentBase<long, OperationState>
{
    public int? DocumentTypeId { get; set; }
    public int? DocumentId { get; set; }
    public DateOnly OperationDate { get; set; }

    public virtual Document? Document { get; set; }
}
