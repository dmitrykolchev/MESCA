// <copyright file="EquipmentConfiguration.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Xobex.Data.EntityFramework;
using Xobex.Data.Mes.Entities.Resources;

namespace Xobex.Data.Mes.Infrastucture.Configuration.Resources;

public class EquipmentConfiguration : EntityConfiguration<Equipment>
{
    public EquipmentConfiguration() : base(false)
    {
    }

    protected override void OnConfigureEntity(EntityTypeBuilder<Equipment> entity)
    {
        ToTableWithKey(entity, e => e.Id, e => e.Id);
        entity.HasStandardProperties();
        entity.HasAuditProperties();
        entity.HasOne(d => d.Resource)
            .WithOne()
            .HasForeignKey<Equipment>(p => p.Id)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName($"fk_{TableName}__resource");
        entity.HasOne(d => d.Parent)
            .WithMany()
            .HasPrincipalKey(d => d.Id)
            .HasForeignKey(p => p.ParentId)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName($"fk_{TableName}__parent");
    }
}
