// <copyright file="DocumentTypeConfiguration.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Xobex.Infrastructure.EntityFramework;
using Xobex.Entities.Metadata;

namespace Xobex.Mes.Infrastucture.Configuration.Metadata;

public class DocumentTypeConfiguration : EntityConfiguration<DocumentType>
{
    public DocumentTypeConfiguration() : base(true)
    {
    }

    protected override void OnConfigureModel(ModelBuilder modelBuilder)
    {
        modelBuilder.HasSequence<long>(TableName + "_seq", SchemaName).StartsAt(1000);
    }

    protected override void OnConfigureEntity(EntityTypeBuilder<DocumentType> entity)
    {
        ToTableWithKey(entity, e => e.Id, e => e.Id);
        entity.Property(e => e.State).HasColumnType("smallint");
        entity.Property(e => e.Flags).HasColumnType("smallint");
        entity.Property(e => e.Code).HasMaxLength(64).IsUnicode(false);
        entity.Property(e => e.Name).HasMaxLength(1024).IsUnicode(true);
        entity.Property(e => e.Description).IsUnicode(true);
        entity.Property(e => e.ImageName).HasMaxLength(1024).IsUnicode(false);
        entity.Property(e => e.Description).IsUnicode(true);
        entity.HasAuditProperties();
        entity.HasAlternateKey(e => e.Code).HasName($"ak_{TableName}_code");
    }
}
