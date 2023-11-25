// <copyright file="PropertyMappingConfiguration.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Xobex.Infrastructure.EntityFramework;
using Xobex.Mes.Entities.Core;

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

        entity.HasOne(d => d.ResourceClass)
            .WithMany(p => p.Properties)
            .HasPrincipalKey(d => d.Id)
            .HasForeignKey(p => p.ResourceClassId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName($"fk_{TableName}__resource_class");

        entity.HasOne(d => d.Property)
            .WithMany()
            .HasPrincipalKey(d => d.Id)
            .HasForeignKey(p => p.PropertyId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName($"fk_{TableName}__property");
    }
}
