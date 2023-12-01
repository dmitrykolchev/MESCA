// <copyright file="ResourceClassMappingConfiguration.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Xobex.Data.EntityFramework;
using Xobex.Data.Mes.Entities.Resources;

namespace Xobex.Data.Mes.Infrastucture.Configuration.Resources;
public class ResourceClassMappingConfiguration : EntityConfiguration<ResourceClassMapping>
{
    public ResourceClassMappingConfiguration(): base(false)
    {
    }

    protected override void OnConfigureEntity(EntityTypeBuilder<ResourceClassMapping> entity)
    {
        entity.ToTable(TableName, SchemaName);
        entity.HasKey(e => new { e.ResourceClassId, e.ResourceId }).HasName($"pk_{TableName}");

        entity.HasOne(d => d.ResourceClass)
            .WithMany()
            .HasPrincipalKey(d => d.Id)
            .HasForeignKey(p => p.ResourceClassId)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName($"fk_{TableName}__resource_class");

        entity.HasOne(d => d.Resource)
            .WithMany()
            .HasPrincipalKey(d => d.Id)
            .HasForeignKey(p => p.ResourceId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName($"fk_{TableName}__resource");
    }
}
