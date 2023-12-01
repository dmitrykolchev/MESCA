// <copyright file="MaterialDefinitionConfiguration.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Xobex.Data.EntityFramework;
using Xobex.Mes.Entities.Resources;

namespace Xobex.Mes.Infrastucture.Configuration.Resources;

public class MaterialDefinitionConfiguration : EntityConfiguration<MaterialDefinition>
{
    public MaterialDefinitionConfiguration(): base(false)
    {
    }

    protected override void OnConfigureEntity(EntityTypeBuilder<MaterialDefinition> entity)
    {
        ToTableWithKey(entity, e => e.Id, e => e.Id);
        entity.HasStandardProperties();
        entity.HasAuditProperties();
        entity.HasOne(d => d.Resource)
            .WithOne()
            .HasForeignKey<MaterialDefinition>(p => p.Id)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName($"fk_{TableName}__resource");
    }
}
