// <copyright file="HierarchyScope.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Xobex.Data.Common;

namespace Xobex.Mes.Entities.Resources;

public enum HierarchyScopeState : short
{
    NotExists = 0,
    Active = 1,
    Inactive = 2
}

[Flags]
public enum HierarchyScopeFlags
{
    None = 0
}

public class HierarchyScope : DocumentBase<int, HierarchyScopeState>
{
    public HierarchyScope()
    {
    }

    public Guid? Uid { get; set; }
    public required string Path { get; set; }
    public int HierarchLevelId { get; set; }
    public HierarchyScopeFlags Flags { get; set; }

    public virtual HierarchyLevel? HierarchyLevel { get; set; }
}
