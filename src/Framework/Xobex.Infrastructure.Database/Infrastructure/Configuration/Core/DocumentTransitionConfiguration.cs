// <copyright file="DocumentTransitionConfiguration.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Xobex.Entities.Core;
using Xobex.Infrastructure.EntityFramework;

namespace Xobex.Infrastucture.Configuration.Core;
public class DocumentTransitionConfiguration : EntityConfiguration<DocumentTransition>
{
    public DocumentTransitionConfiguration() : base(true)
    {
    }

    protected override void OnConfigureEntity(EntityTypeBuilder<DocumentTransition> entity)
    {
        ToTableWithKey(entity, e => e.Id, e => e.Id);

        entity.HasOne(d => d.Document)
            .WithMany()
            .HasPrincipalKey(d => new { d.DocumentTypeId, d.DocumentId })
            .HasForeignKey(p => new { p.DocumentTypeId, p.DocumentId })
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName($"fk_{TableName}__document");

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
    }
}
