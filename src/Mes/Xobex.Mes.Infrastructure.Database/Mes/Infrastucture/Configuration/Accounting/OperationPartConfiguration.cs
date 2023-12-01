// <copyright file="OperationPartConfiguration.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Xobex.Data.EntityFramework;
using Xobex.Data.Mes.Entities.Accounting;

namespace Xobex.Data.Mes.Infrastucture.Configuration.Accounting;

public class OperationPartConfiguration : EntityConfiguration<OperationPart>
{
    public OperationPartConfiguration() : base(false)
    {
    }

    protected override void OnConfigureEntity(EntityTypeBuilder<OperationPart> entity)
    {
        entity.ToTable(TableName, SchemaName);
        entity.HasKey(e => new { e.OperationId, e.Side }).HasName($"pk_{TableName}");
        entity.Property(e => e.Amount).HasPrecision(38, 12);
        entity.Property(e => e.Value).HasPrecision(38, 12);
        entity.Property(e => e.Tag00).HasMaxLength(32).IsUnicode(false);
        entity.Property(e => e.Tag01).HasMaxLength(32).IsUnicode(false);
        entity.Property(e => e.Tag02).HasMaxLength(32).IsUnicode(false);
        entity.Property(e => e.Tag03).HasMaxLength(32).IsUnicode(false);

        entity.HasOne(d => d.Operation)
            .WithMany()
            .HasPrincipalKey(d => d.Id)
            .HasForeignKey(p => p.OperationId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName($"fk_{TableName}__operation");

        entity.HasOne(d => d.Account)
            .WithMany()
            .HasPrincipalKey(d => d.Id)
            .HasForeignKey(p => p.AccountId)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName($"fk_{TableName}__account");

        entity.HasOne(d => d.Currency)
            .WithMany()
            .HasPrincipalKey(d => d.Id)
            .HasForeignKey(p => p.CurrencyId)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName($"fk_{TableName}__currency");
    }
}
