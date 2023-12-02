// <copyright file="IMetadataContext.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Microsoft.EntityFrameworkCore;
using Xobex.Data.Entities.Core;
using Xobex.Data.Entities.Metadata;
using Xobex.Data.Entities.Security;
using Xobex.Data.EntityFramework;

namespace Xobex.Data.Entities;

public interface IMetadataContext : ITransactionProvider
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

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
