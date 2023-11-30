// <copyright file="DocumentStateGlobalConfiguration.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Xobex.Entities.Metadata;
using Xobex.Infrastructure.EntityFramework;

namespace Xobex.Infrastructure.Configuration.Metadata;

public class DocumentStateGlobalConfiguration : EntityConfiguration<DocumentStateGlobal>
{
    public DocumentStateGlobalConfiguration() : base(false)
    {
    }

    protected override void OnConfigureEntity(EntityTypeBuilder<DocumentStateGlobal> entity)
    {
        entity.ToTable(TableName, SchemaName);
        entity.Property(p => p.Language).IsUnicode(false).HasMaxLength(8);
        entity.Property(e => e.Name).HasMaxLength(1024).IsUnicode(true);
        entity.Property(e => e.Description).IsUnicode(true);
        entity.HasKey(e => new { e.DocumentStateId, e.Language }).HasName($"pk_{TableName}");
        entity.HasOne(d => d.State)
            .WithMany(p => p.Languages)
            .HasPrincipalKey(d => d.Id)
            .HasForeignKey(p => p.DocumentStateId)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName($"fk_{TableName}__document_state");
    }
}
