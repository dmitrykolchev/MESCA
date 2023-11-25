// <copyright file="HierarchyScopeMapping.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Xobex.Domain.Common;
using Xobex.Mes.Entities.Core;

namespace Xobex.Mes.Entities.Resources;

public class HierarchyScopeMapping : IAuditable
{
    public int Id { get; set; }

    public int DocumentTypeId { get; set; }
    public int DocumentId { get; set; }
    public int HierarchyScopeId { get; set; }

    public DateTimeOffset CreatedOn { get; set; }
    public int CreatedBy { get; set; }
    public DateTimeOffset ModifiedOn { get; set; }
    public int ModifiedBy { get; set; }

    public virtual Document? Document { get; set; }
    public virtual HierarchyScope? HierarchyScope { get; set; }
}
