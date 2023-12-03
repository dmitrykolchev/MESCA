// <copyright file="IMesDbContext.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Microsoft.EntityFrameworkCore;
using Xobex.Entities;
using Xobex.Mes.Entities.Core;
using Xobex.Mes.Entities.Resources;


namespace Xobex.Mes.Entities;

public interface IMesDbContext : IMetadataContext
{
    DbSet<DocumentNote> DocumentNote { get; set; }

    DbSet<HierarchyLevel> HierarchyLevel { get; set; }
    DbSet<HierarchyScope> HierarchyScope { get; set; }
    DbSet<HierarchyScopeMapping> HierarchyScopeMapping { get; set; }
    DbSet<Resource> Resource { get; set; }
    DbSet<ResourceClass> ResourceClass { get; set; }
    DbSet<ResourceClassMapping> ResourceClassMapping { get; set; }
    DbSet<Equipment> Equipment { get; set; }
    DbSet<Person> Person { get; set; }
    DbSet<PhysicalAsset> PhysicalAsset { get; set; }
    DbSet<MaterialDefinition> MaterialDefinition { get; set; }
}
