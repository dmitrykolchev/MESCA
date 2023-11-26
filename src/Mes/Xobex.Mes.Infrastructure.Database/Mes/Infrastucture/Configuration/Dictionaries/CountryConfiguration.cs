// <copyright file="CountryConfiguration.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Xobex.Infrastructure.EntityFramework;
using Xobex.Mes.Entities.Dictionaries;

namespace Xobex.Mes.Infrastucture.Configuration.Dictionaries;
public class CountryConfiguration : EntityConfiguration<Country>
{
    public CountryConfiguration() : base(true)
    {
    }
    protected override void OnConfigureEntity(EntityTypeBuilder<Country> entity)
    {
        ToTableWithKey(entity, e => e.Id, e => e.Id);
        entity.HasStandardProperties();
        entity.HasAuditProperties();
        entity.HasAlternateKey(e => e.Code).HasName($"ak_{TableName}_code");
    }
}
