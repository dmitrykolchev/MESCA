// <copyright file="IMetadataContext.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Microsoft.EntityFrameworkCore;
using Xobex.Data.EntityFramework;
using Xobex.Entities.Core;
using Xobex.Entities.Dictionaries;
using Xobex.Entities.Metadata;
using Xobex.Entities.Security;

namespace Xobex.Entities;

public interface IMetadataContext : IDatabaseContext
{
    DbSet<Document> Document { get; set; }
    DbSet<DocumentTransition> DocumentTransition { get; set; }
    DbSet<PropertyValue> PropertyValue { get; set; }

    DbSet<DataType> DataType { get; set; }
    DbSet<DocumentState> DocumentState { get; set; }
    DbSet<DocumentStateGlobal> DocumentStateGlobal { get; set; }
    DbSet<DocumentType> DocumentType { get; set; }
    DbSet<DocumentTypeGlobal> DocumentTypeGlobal { get; set; }
    DbSet<Property> Property { get; set; }
    DbSet<PropertyMapping> PropertyMapping { get; set; }
    DbSet<TransitionTemplate> TransitionTemplate { get; set; }

    DbSet<AccessRight> AccessRight { get; set; }

    DbSet<Country> Country { get; set; }
    DbSet<Currency> Currency { get; set; }
    DbSet<CurrencyRateProvider> CurrencyRateProvider { get; set; }
    DbSet<CurrencyRate> CurrencyRate { get; set; }
    DbSet<KindOfQuantity> KindOfQuantity { get; set; }
    DbSet<UnitOfMeasure> UnitOfMeasure { get; set; }
    DbSet<UnitOfMeasureConversion> UnitOfMeasureConversion { get; set; }
}
