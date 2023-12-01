// <copyright file="AccountConfiguration.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Xobex.Data.EntityFramework;
using Xobex.Mes.Entities.Accounting;

namespace Xobex.Mes.Infrastucture.Configuration.Accounting;

public class AccountConfiguration : EntityConfiguration<Account>
{
    public AccountConfiguration() : base(true)
    {
    }

    protected override void OnConfigureEntity(EntityTypeBuilder<Account> entity)
    {
        ToTableWithKey(entity, e => e.Id, e => e.Id);
        entity.HasStandardProperties();
        entity.HasAuditProperties();
        entity.HasAlternateKey(e => e.Code).HasName($"ak_{TableName}_code");
    }
}
