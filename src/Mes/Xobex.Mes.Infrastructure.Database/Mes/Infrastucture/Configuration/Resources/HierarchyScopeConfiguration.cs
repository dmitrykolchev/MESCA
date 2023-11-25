// <copyright file="HierarchyScopeConfiguration.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Xobex.Infrastructure.EntityFramework;
using Xobex.Mes.Entities.Resources;

namespace Xobex.Mes.Infrastucture.Configuration.Resources;

public class HierarchyScopeConfiguration : EntityConfiguration<HierarchyScope>
{
    public HierarchyScopeConfiguration() : base(true)
    {
    }

    protected override void OnConfigureEntity(EntityTypeBuilder<HierarchyScope> entity)
    {
        ToTableWithKey(entity, e => e.Id, e => e.Id);

        entity.Property(e => e.Path).HasMaxLength(1024).IsUnicode(false);

        entity.HasOne(d => d.HierarchyLevel)
            .WithMany()
            .HasPrincipalKey(d => d.Id)
            .HasForeignKey(p => p.HierarchLevelId)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName($"fk_{TableName}__hierarchy_level");
    }
}
