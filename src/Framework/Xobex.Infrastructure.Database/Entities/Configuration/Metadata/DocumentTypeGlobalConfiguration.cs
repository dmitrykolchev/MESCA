// <copyright file="DocumentTypeGlobalConfiguration.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Xobex.Data.Entities.Metadata;
using Xobex.Data.EntityFramework;

namespace Xobex.Data.Configuration.Metadata;

public class DocumentTypeGlobalConfiguration : EntityConfiguration<DocumentTypeGlobal>
{
    public DocumentTypeGlobalConfiguration() : base(false)
    {
    }

    protected override void OnConfigureEntity(EntityTypeBuilder<DocumentTypeGlobal> entity)
    {
        entity.ToTable(TableName, SchemaName);
        entity.Property(p => p.Language).IsUnicode(false).HasMaxLength(8);
        entity.Property(e => e.Name).HasMaxLength(1024).IsUnicode(true);
        entity.Property(e => e.Description).IsUnicode(true);
        entity.Property(e => e.Image).HasMaxLength(1024).IsUnicode(false);
        entity.HasAuditProperties();
        entity.HasKey(e => new { e.DocumentTypeId, e.Language }).HasName($"pk_{TableName}");

        entity.HasOne(d => d.DocumentType)
            .WithMany(p => p.Languages)
            .HasPrincipalKey(d => d.Id)
            .HasForeignKey(p => p.DocumentTypeId)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName($"fk_{TableName}__document_type");
    }
}
