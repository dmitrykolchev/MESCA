// <copyright file="ResourceClassMapping.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

namespace Xobex.Data.Mes.Entities.Resources;

public class ResourceClassMapping
{
    public ResourceClassMapping()
    {
    }

    public int ResourceClassId { get; set; }
    public int ResourceId { get; set; }

    public virtual ResourceClass? ResourceClass { get; set; }
    public virtual Resource? Resource { get; set; }
}
