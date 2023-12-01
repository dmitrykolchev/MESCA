// <copyright file="IModelConfiguration.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Microsoft.EntityFrameworkCore;

namespace Xobex.Data.EntityFramework;

public interface IModelConfiguration
{
    void Configure(ModelBuilder modelBuilder, DbContext context);
}

