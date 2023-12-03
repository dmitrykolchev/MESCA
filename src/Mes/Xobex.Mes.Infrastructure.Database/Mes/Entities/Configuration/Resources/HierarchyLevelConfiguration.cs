// <copyright file="HierarchyLevelConfiguration.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Xobex.Data.EntityFramework;
using Xobex.Mes.Entities.Resources;

namespace Xobex.Mes.Entities.Configuration.Resources;
public class HierarchyLevelConfiguration : EntityConfiguration<HierarchyLevel>
{
    public HierarchyLevelConfiguration() : base(true)
    {
    }

    protected override void OnConfigureEntity(EntityTypeBuilder<HierarchyLevel> entity)
    {
        ToTableWithKey(entity, e => e.Id, e => e.Id);
    }
}
