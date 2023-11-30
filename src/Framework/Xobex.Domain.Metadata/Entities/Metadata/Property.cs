// <copyright file="Property.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Xobex.Domain.Common;

namespace Xobex.Entities.Metadata;

public enum PropertyState : short
{
    NotExists = 0,
    Active = 1,
    Inactive = 2
}

[Flags]
public enum PropertyFlags : int
{
    None = 0,
}

public class Property : DocumentBase<int, PropertyState>
{
    public Property()
    {
    }
    public PropertyFlags Flags { get; set; }
    public DataKind Kind { get; set; }
    public int DataTypeId { get; set; }

    public virtual Property? Parent { get; set; }
    public virtual DataType? DataType { get; set; }
}
