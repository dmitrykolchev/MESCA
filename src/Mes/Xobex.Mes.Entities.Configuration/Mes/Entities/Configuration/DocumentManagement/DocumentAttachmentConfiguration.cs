// <copyright file="DocumentAttachmentConfiguration.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Xobex.Data.EntityFramework;
using Xobex.Mes.Entities.DocumentManagement;

namespace Xobex.Mes.Entities.Configuration.DocumentManagement;

public class DocumentAttachmentConfiguration : EntityConfiguration<DocumentAttachment>
{
    public DocumentAttachmentConfiguration() : base(true)
    {
    }

    protected override void OnConfigureEntity(EntityTypeBuilder<DocumentAttachment> entity)
    {
        ToTableWithKey(entity, e => e.Id, e => e.Id);

        entity.HasOne(d => d.Document)
            .WithMany()
            .HasPrincipalKey(d => new { d.DocumentTypeId, d.DocumentId })
            .HasForeignKey(p => new { p.DocumentTypeId, p.DocumentId })
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName($"fk_{TableName}__document");

        entity.HasOne(d => d.File)
            .WithMany()
            .HasPrincipalKey(d => d.Id)
            .HasForeignKey(p => p.FileId)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName($"fk_{TableName}__file_blob");
    }
}
