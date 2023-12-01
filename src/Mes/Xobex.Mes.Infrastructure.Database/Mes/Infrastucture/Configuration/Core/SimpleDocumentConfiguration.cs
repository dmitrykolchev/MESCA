// <copyright file="SimpleDocumentConfiguration.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Xobex.Data.EntityFramework;
using Xobex.Mes.Entities.Core;

namespace Xobex.Mes.Infrastucture.Configuration.Core;

public class SimpleDocumentConfiguration : EntityConfiguration<SimpleDocument>
{
    public SimpleDocumentConfiguration() : base(true)
    {
    }

    protected override void OnConfigureEntity(EntityTypeBuilder<SimpleDocument> entity)
    {
        ToTableWithKey(entity, e => e.Id, e => e.Id);
        entity.HasAlternateKey(e => new { e.DocumentTypeId, e.Id });

        entity.HasOne(d => d.DocumentType)
            .WithMany()
            .HasPrincipalKey(d => d.Id)
            .HasForeignKey(p => p.DocumentTypeId)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName($"fk_{TableName}__document_type");
    }
}
