// <copyright file="HierarchyScopeMappingConfiguration.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Xobex.Data.EntityFramework;
using Xobex.Mes.Entities.Resources;

namespace Xobex.Mes.Infrastucture.Configuration.Resources;
public class HierarchyScopeMappingConfiguration : EntityConfiguration<HierarchyScopeMapping>
{
    public HierarchyScopeMappingConfiguration() : base(true)
    {
    }

    protected override void OnConfigureEntity(EntityTypeBuilder<HierarchyScopeMapping> entity)
    {
        ToTableWithKey(entity, e => e.Id, e => e.Id);
        entity.HasAuditProperties();

        entity.HasOne(d => d.Document)
            .WithMany()
            .HasPrincipalKey(d => new { d.DocumentTypeId, d.DocumentId })
            .HasForeignKey(p => new { p.DocumentTypeId, p.DocumentId })
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName($"fk_{TableName}__document");

        entity.HasOne(d => d.HierarchyScope)
            .WithMany()
            .HasPrincipalKey(d => d.Id)
            .HasForeignKey(p => p.HierarchyScopeId)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName($"fk_{TableName}__hierarchy_scope");
    }
}
