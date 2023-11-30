// <copyright file="PropertyMappingConfiguration.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Xobex.Entities.Metadata;
using Xobex.Infrastructure.EntityFramework;

namespace Xobex.Mes.Infrastucture.Configuration.Core;
public class PropertyMappingConfiguration : EntityConfiguration<PropertyMapping>
{
    public PropertyMappingConfiguration() : base(true)
    {
    }

    protected override void OnConfigureEntity(EntityTypeBuilder<PropertyMapping> entity)
    {
        ToTableWithKey(entity, e => e.Id, e => e.Id);

        entity.HasAuditProperties();

        entity.HasOne(d => d.DocumentType)
            .WithMany(p => p.Properties)
            .HasPrincipalKey(d => d.Id)
            .HasForeignKey(p => p.DocumentTypeId)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName($"fk_{TableName}__document_type");

        entity.HasOne(d => d.Document)
            .WithMany()
            .HasPrincipalKey(d => new { d.DocumentTypeId, d.DocumentId })
            .HasForeignKey(p => new { p.DocumentTypeId, p.DocumentId })
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName($"fk_{TableName}__document");

        entity.HasOne(d => d.Property)
            .WithMany()
            .HasPrincipalKey(d => d.Id)
            .HasForeignKey(p => p.PropertyId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName($"fk_{TableName}__property");
    }
}
