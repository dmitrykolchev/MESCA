// <copyright file="DocumentTransition.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Xobex.Data.Common;
using Xobex.Data.Entities.Metadata;

namespace Xobex.Data.Entities.Core;

public class DocumentTransition : ISimpleAuditable
{
    public long Id { get; set; }
    public int DocumentTypeId { get; set; }
    public int DocumentId { get; set; }
    public short FromStateValue { get; set; }
    public short ToStateValue { get; set; }
    public string? Comments { get; set; }
    public DateTimeOffset ModifiedOn { get; set; }
    public int ModifiedBy { get; set; }

    public virtual Document? Document { get; set; }
    public virtual DocumentState? FromState { get; set; }
    public virtual DocumentState? ToState { get; set; }
}
