// <copyright file="PropertyValueConfiguration.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Xobex.Infrastructure.EntityFramework;
using Xobex.Mes.Entities.Core;

namespace Xobex.Mes.Infrastucture.Configuration.Core;

public class PropertyValueConfiguration : EntityConfiguration<PropertyValue>
{
    public PropertyValueConfiguration() : base(true)
    {
    }

    protected override void OnConfigureEntity(EntityTypeBuilder<PropertyValue> entity)
    {
        ToTableWithKey(entity, e => e.Id, e => e.Id);
        entity.Property(t => t.LeadingStringValue).HasMaxLength(300).IsUnicode(true);
        entity.Property(t => t.TrailingStringValue).IsUnicode(true);
        entity.Property(t => t.Kind).HasColumnType("smallint");
        entity.Property(t => t.DecimalValue).HasPrecision(38, 12);
        entity.HasOne(d => d.Document)
            .WithMany()
            .HasPrincipalKey(d => new { d.DocumentTypeId, d.DocumentId })
            .HasForeignKey(p => new { p.DocumentTypeId, p.DocumentId })
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName($"fk_{TableName}__document");

        entity.HasOne(d => d.Property)
            .WithMany()
            .HasPrincipalKey(d => d.Id)
            .HasForeignKey(p => p.PropertyId)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName($"fk_{TableName}__property");
    }
}
