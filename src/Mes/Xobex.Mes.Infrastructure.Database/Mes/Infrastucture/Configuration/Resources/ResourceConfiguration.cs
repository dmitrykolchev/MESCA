// <copyright file="ResourceConfiguration.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Xobex.Data.EntityFramework;
using Xobex.Data.Mes.Entities.Resources;

namespace Xobex.Data.Mes.Infrastucture.Configuration.Resources;
public class ResourceConfiguration : EntityConfiguration<Resource>
{
    public ResourceConfiguration() : base(true)
    {
    }

    protected override void OnConfigureEntity(EntityTypeBuilder<Resource> entity)
    {
        ToTableWithKey(entity, e => e.Id, e => e.Id);

        entity.HasOne(d => d.DocumentType)
            .WithMany()
            .HasPrincipalKey(d => d.Id)
            .HasForeignKey(p => p.DocumentTypeId)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName($"fk_{TableName}__document_type");
    }
}
