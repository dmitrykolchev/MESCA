﻿// <copyright file="Property.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Xobex.Data.Common;

namespace Xobex.Entities.Metadata;

[Flags]
public enum PropertyFlags : int
{
    None = 0,
}

public class Property : DocumentBase<int, CommonStates>
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
