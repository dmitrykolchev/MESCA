// <copyright file="DocumentNoteUserStateConfiguration.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Xobex.Data.EntityFramework;
using Xobex.Mes.Entities.Core;

namespace Xobex.Mes.Entities.Configuration.Core;

public class DocumentNoteUserStateConfiguration : EntityConfiguration<DocumentNoteUserState>
{
    public DocumentNoteUserStateConfiguration() : base(false)
    {
    }

    protected override void OnConfigureEntity(EntityTypeBuilder<DocumentNoteUserState> entity)
    {
        entity.ToTable(TableName, SchemaName);
        entity.HasKey(e => new { e.DocumentNoteId, e.UserId }).HasName($"pk_{TableName}");
        entity.HasOne(d => d.DocumentNote)
            .WithMany()
            .HasPrincipalKey(d => d.Id)
            .HasForeignKey(p => p.DocumentNoteId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName($"fk_{TableName}__document_note");
    }
}
