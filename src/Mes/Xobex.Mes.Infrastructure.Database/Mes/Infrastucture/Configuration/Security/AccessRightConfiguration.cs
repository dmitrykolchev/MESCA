// <copyright file="AccessRightConfiguration.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Xobex.Infrastructure.EntityFramework;
using Xobex.Mes.Entities.Security;

namespace Xobex.Mes.Infrastucture.Configuration.Security;

public class AccessRightConfiguration : EntityConfiguration<AccessRight>
{
    public AccessRightConfiguration() : base(true)
    {
    }

    protected override void OnConfigureEntity(EntityTypeBuilder<AccessRight> entity)
    {
        ToTableWithKey(entity, e => e.Id, e => e.Id);
        entity.Property(e => e.Code).HasMaxLength(128).IsUnicode(false);
        entity.Property(e => e.Name).HasMaxLength(1024).IsUnicode(true);
        entity.Property(e => e.Category).HasMaxLength(1024).IsUnicode(true);
        entity.Property(e => e.Comments).IsUnicode(true);
        entity.HasAuditProperties();
        entity.HasAlternateKey(e => e.Code).HasName($"ak_{TableName}_code");
    }
}
