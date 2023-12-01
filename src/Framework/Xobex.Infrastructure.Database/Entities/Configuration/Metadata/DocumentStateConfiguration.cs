// <copyright file="DocumentStateConfiguration.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Xobex.Data.Entities.Metadata;
using Xobex.Data.EntityFramework;

namespace Xobex.Data.Entities.Configuration.Metadata;

public class DocumentStateConfiguration : EntityConfiguration<DocumentState>
{
    public DocumentStateConfiguration() : base(true)
    {
    }

    protected override void OnConfigureEntity(EntityTypeBuilder<DocumentState> entity)
    {
        ToTableWithKey(entity, e => e.Id, e => e.Id);
        entity.Property(e => e.Code).HasMaxLength(64).IsUnicode(false);
        entity.Property(e => e.Name).HasMaxLength(1024).IsUnicode(true);
        entity.Property(e => e.Description).IsUnicode(true);
        entity.Property(e => e.Color).HasMaxLength(32).IsUnicode(false);
        entity.Property(e => e.Comments).IsUnicode(true);
        entity.HasAuditProperties();
        entity.HasAlternateKey(e => new { e.DocumentTypeId, e.Value }).HasName("ak_document_state_value");

        entity.HasOne(d => d.DocumentType)
            .WithMany(p => p.States)
            .HasPrincipalKey(d => d.Id)
            .HasForeignKey(p => p.DocumentTypeId)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName($"fk_{TableName}__document_type");
    }
}
