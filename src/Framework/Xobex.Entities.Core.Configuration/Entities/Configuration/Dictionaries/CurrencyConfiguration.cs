﻿// <copyright file="CurrencyConfiguration.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Xobex.Data.EntityFramework;
using Xobex.Entities.Dictionaries;

namespace Xobex.Entities.Configuration.Dictionaries;

public class CurrencyConfiguration : EntityConfiguration<Currency>
{
    public CurrencyConfiguration() : base(true)
    {
    }

    protected override void OnConfigureEntity(EntityTypeBuilder<Currency> entity)
    {
        ToTableWithKey(entity, e => e.Id, e => e.Id);
        entity.HasStandardProperties();
        entity.HasAuditProperties();
        entity.HasAlternateKey(e => e.Code).HasName($"ak_{TableName}_code");
    }
}
