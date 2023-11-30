// <copyright file="DocumentConfiguration.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Xobex.Entities.Core;
using Xobex.Infrastructure.EntityFramework;

namespace Xobex.Mes.Infrastucture.Configuration.Core;

public class DocumentConfiguration : EntityConfiguration<Document>
{
    public DocumentConfiguration() : base(false)
    {
    }

    protected override void OnConfigureEntity(EntityTypeBuilder<Document> entity)
    {
        entity.ToTable(TableName, SchemaName);
        entity.HasKey(e => new { e.DocumentTypeId, e.DocumentId }).HasName($"pk_{TableName}");
        entity.Property(e => e.State)
            .HasColumnType("smallint");
        entity.Property(e => e.Code)
            .HasMaxLength(128)
            .IsUnicode(false);
        entity.Property(e => e.Name)
            .HasMaxLength(1024)
            .IsUnicode();
        entity.Property(e => e.CreatedOn);
        entity.Property(e => e.CreatedBy);
        entity.Property(e => e.ModifiedOn);
        entity.Property(e => e.ModifiedBy);
    }
}
