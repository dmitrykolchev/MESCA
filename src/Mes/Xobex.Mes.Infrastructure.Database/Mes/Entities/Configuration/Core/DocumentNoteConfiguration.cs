// <copyright file="DocumentNoteConfiguration.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Xobex.Data.EntityFramework;
using Xobex.Mes.Entities.Core;

namespace Xobex.Mes.Entities.Configuration.Core;

public class DocumentNoteConfiguration : EntityConfiguration<DocumentNote>
{
    public DocumentNoteConfiguration() : base(true)
    {
    }

    protected override void OnConfigureEntity(EntityTypeBuilder<DocumentNote> entity)
    {
        ToTableWithKey(entity, e => e.Id, e => e.Id);
        entity.HasAuditProperties();
        entity.HasOne(d => d.Document)
            .WithMany()
            .HasPrincipalKey(d => new { d.DocumentTypeId, d.DocumentId })
            .HasForeignKey(p => new { p.DocumentTypeId, p.DocumentId })
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName($"fk_{TableName}__document");
    }
}
