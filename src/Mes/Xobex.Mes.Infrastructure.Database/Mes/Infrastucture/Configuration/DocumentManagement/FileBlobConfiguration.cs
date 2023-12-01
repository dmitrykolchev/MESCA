// <copyright file="FileBlobConfiguration.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Xobex.Data.EntityFramework;
using Xobex.Data.Mes.Entities.DocumentManagement;

namespace Xobex.Data.Mes.Infrastucture.Configuration.DocumentManagement;

public class FileBlobConfiguration : EntityConfiguration<FileBlob>
{
    public FileBlobConfiguration() : base(true)
    {
    }

    protected override void OnConfigureEntity(EntityTypeBuilder<FileBlob> entity)
    {
        ToTableWithKey(entity, e => e.Id, e => e.Id);
    }
}
