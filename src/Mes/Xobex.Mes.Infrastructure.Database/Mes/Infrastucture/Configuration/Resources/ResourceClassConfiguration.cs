// <copyright file="ResourceClassConfiguration.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Xobex.Data.EntityFramework;
using Xobex.Mes.Entities.Resources;

namespace Xobex.Mes.Infrastucture.Configuration.Resources;

public class ResourceClassConfiguration : EntityConfiguration<ResourceClass>
{
    public ResourceClassConfiguration(): base(true)
    {
    }

    protected override void OnConfigureEntity(EntityTypeBuilder<ResourceClass> entity)
    {
        ToTableWithKey(entity, e => e.Id, e => e.Id);
        entity.HasStandardProperties();
        entity.HasAuditProperties();
        entity.HasOne(d => d.ResourceType)
            .WithMany()
            .HasPrincipalKey(d => d.Id)
            .HasForeignKey(p => p.ResourceTypeId)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName($"fk_{TableName}__resource_type");
    }
}
