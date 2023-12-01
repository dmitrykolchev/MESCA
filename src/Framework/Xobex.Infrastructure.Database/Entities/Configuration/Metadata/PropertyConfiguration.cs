// <copyright file="PropertyConfiguration.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Xobex.Data.Entities.Metadata;
using Xobex.Data.EntityFramework;

namespace Xobex.Data.Entities.Configuration.Metadata;

public class PropertyConfiguration : EntityConfiguration<Property>
{
    public PropertyConfiguration() : base(true)
    {
    }

    protected override void OnConfigureEntity(EntityTypeBuilder<Property> entity)
    {
        ToTableWithKey(entity, e => e.Id, e => e.Id);
        entity.HasStandardProperties();
        entity.Property(e => e.Flags);
        entity.Property(e => e.ParentId);
        entity.Property(e => e.DataTypeId);
        entity.HasAuditProperties();
        entity.HasAlternateKey(e => e.Code).HasName($"ak_{TableName}_code");
    }
}
