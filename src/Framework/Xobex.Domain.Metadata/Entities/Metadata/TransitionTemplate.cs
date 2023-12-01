// <copyright file="TransitionTemplate.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Xobex.Data.Common;
using Xobex.Data.Entities.Security;

namespace Xobex.Data.Entities.Metadata;

public enum TransitionTemplateState : short
{
    NotExists,
    Active,
    Inactive
}

[Flags]
public enum TransitionTemplateFlags : int
{
    None = 0,
}

public class TransitionTemplate: IAuditable
{
    public int Id { get; set; }
    public TransitionTemplateState State { get; set; }
    public TransitionTemplateFlags Flags { get; set; }
    public int DocumentTypeId { get; set; }
    public short FromStateValue { get; set; }
    public short ToStateValue { get; set; }
    public int AccessRightId { get; set; }
    public DateTimeOffset CreatedOn { get; set; }
    public int CreatedBy { get; set; }
    public DateTimeOffset ModifiedOn { get; set; }
    public int ModifiedBy { get; set; }

    public virtual DocumentType? DocumentType { get; set; }
    public virtual AccessRight? AccessRight { get; set; }
    public virtual DocumentState? FromState { get; set; }
    public virtual DocumentState? ToState { get; set; }
}
