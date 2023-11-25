// <copyright file="HIerarchyLevel.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Xobex.Domain.Common;

namespace Xobex.Mes.Entities.Resources;

public enum HierarchyLevelState : short
{
    NotExists = 0,
    Active = 1,
    Inactive = 2
}

[Flags]
public enum HierarchyLevelFlags: int
{
    None = 0,
}

public class HierarchyLevel : DocumentBase<int, HierarchyLevelState>
{
    public HierarchyLevel()
    {
    }

    public HierarchyScopeFlags Flags { get; set; }
}
