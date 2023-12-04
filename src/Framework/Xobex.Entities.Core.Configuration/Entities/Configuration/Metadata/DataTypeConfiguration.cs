// <copyright file="DataTypeConfiguration.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Xobex.Data.EntityFramework;
using Xobex.Entities.Metadata;

namespace Xobex.Entities.Configuration.Metadata;

public class DataTypeConfiguration : EntityConfiguration<DataType>
{
    public DataTypeConfiguration() : base(false)
    {
    }

    protected override void OnConfigureEntity(EntityTypeBuilder<DataType> entity)
    {
        ToTableWithKey(entity, e => e.Id, e => e.Id);
        entity.Property(e => e.State).HasColumnType("smallint");
        entity.Property(e => e.Code).HasMaxLength(64).IsUnicode(false);
        entity.Property(e => e.Name).HasMaxLength(1024).IsUnicode(true);
        entity.Property(e => e.Kind).HasColumnType("smallint");
        entity.Property(e => e.DocumentTypeId);
        entity.HasOne(d => d.DocumentType)
            .WithMany()
            .HasPrincipalKey(d => d.Id)
            .HasForeignKey(p => p.DocumentTypeId)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName($"fk_{TableName}__document_type");
    }
}
