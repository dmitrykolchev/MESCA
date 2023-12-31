﻿// <copyright file="EntityBuilderExtension.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Xobex.Data.Common;

namespace Xobex.Data.EntityFramework;

public static class EntityBuilderExtension
{
    public static void HasStandardProperties<TEntity>(this EntityTypeBuilder<TEntity> entity)
        where TEntity : class, IDocument
    {
        entity.Property(e => e.State)
            .HasColumnType("smallint");
        entity.Property(e => e.Code)
            .HasMaxLength(128)
            .IsUnicode(false);
        entity.Property(e => e.Name)
            .HasMaxLength(1024)
            .IsUnicode();
        entity.Property(e => e.Comments)
            .IsUnicode();
    }

    public static void HasAuditProperties<TEntity>(this EntityTypeBuilder<TEntity> entity)
        where TEntity : class, IAuditable
    {
        entity.Property(e => e.CreatedOn);
        entity.Property(e => e.CreatedBy);
        entity.Property(e => e.ModifiedOn);
        entity.Property(e => e.ModifiedBy);
    }
}
