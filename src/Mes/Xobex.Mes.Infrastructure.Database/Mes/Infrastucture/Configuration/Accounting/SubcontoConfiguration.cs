// <copyright file="SubcontoConfiguration.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Xobex.Data.EntityFramework;
using Xobex.Data.Mes.Entities.Accounting;

namespace Xobex.Data.Mes.Infrastucture.Configuration.Accounting;
public class SubcontoConfiguration : EntityConfiguration<Subconto>
{
    public SubcontoConfiguration() : base(true) { }
    protected override void OnConfigureEntity(EntityTypeBuilder<Subconto> entity)
    {
        ToTableWithKey(entity, e => e.Id, e => e.Id);
        entity.HasStandardProperties();
        entity.HasAuditProperties();
        entity.HasAlternateKey(e => e.Code).HasName($"ak_{TableName}_code");

        entity.HasOne(d => d.Account)
            .WithMany()
            .HasPrincipalKey(d => d.Id)
            .HasForeignKey(p => p.AccountId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName($"fk_{TableName}__account");
        entity.HasOne(d => d.DocumentType)
            .WithMany()
            .HasPrincipalKey(d => d.Id)
            .HasForeignKey(p => p.DocumentTypeId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName($"fk_{TableName}__document_type");
    }
}
