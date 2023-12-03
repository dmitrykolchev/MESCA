// <copyright file="ResourceClass.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Xobex.Data.Common;
using Xobex.Entities.Metadata;

namespace Xobex.Mes.Entities.Resources;

public enum ResourceClassState
{
    NotExists = 0,
    Active = 1,
    Inactive = 2
}

public class ResourceClass : DocumentBase<int, ResourceClassState>
{
    public ResourceClass()
    {
    }

    public int ResourceTypeId { get; set; }

    public virtual DocumentType? ResourceType { get; set; }
    public virtual ICollection<PropertyMapping>? Properties { get; set; }
}
