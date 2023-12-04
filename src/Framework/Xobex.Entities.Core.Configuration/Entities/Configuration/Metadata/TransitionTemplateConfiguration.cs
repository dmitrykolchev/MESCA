// <copyright file="TransitionTemplateConfiguration.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Xobex.Entities.Metadata;
using Xobex.Data.EntityFramework;

namespace Xobex.Entities.Configuration.Metadata;

public class TransitionTemplateConfiguration : EntityConfiguration<TransitionTemplate>
{
    public TransitionTemplateConfiguration() : base(true)
    {
    }

    protected override void OnConfigureModel(ModelBuilder modelBuilder)
    {
        modelBuilder.HasSequence<long>(TableName + "_seq", SchemaName);
    }

    protected override void OnConfigureEntity(EntityTypeBuilder<TransitionTemplate> entity)
    {
        ToTableWithKey(entity, e => e.Id, e => e.Id);
        entity.Property(e => e.FromStateValue).HasColumnName("from_state");
        entity.Property(e => e.ToStateValue).HasColumnName("to_state");
        entity.HasAuditProperties();

        entity.HasOne(d => d.DocumentType)
            .WithMany(p => p.TransitionTemplates)
            .HasPrincipalKey(d => d.Id)
            .HasForeignKey(p => p.DocumentTypeId)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName($"fk_{TableName}__document_type");

        entity.HasOne(d => d.FromState)
            .WithMany()
            .HasPrincipalKey(d => new { d.DocumentTypeId, d.Value })
            .HasForeignKey(p => new { p.DocumentTypeId, p.FromStateValue })
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName($"fk_{TableName}__from_state");

        entity.HasOne(d => d.ToState)
            .WithMany()
            .HasPrincipalKey(d => new { d.DocumentTypeId, d.Value })
            .HasForeignKey(p => new { p.DocumentTypeId, p.ToStateValue })
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName($"fk_{TableName}__to_state");

        entity.HasOne(d => d.AccessRight)
            .WithMany()
            .HasPrincipalKey(d => d.Id)
            .HasForeignKey(p => p.AccessRightId)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName($"fk_{TableName}__access_right");
    }
}
