// <copyright file="DocumentGlobal.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Xobex.Data.Common;
using Xobex.Entities.Core;

namespace Xobex.Mes.Entities.Core;

public class DocumentGlobal : IAuditable
{
    public int DocumentTypeId { get; set; }
    public int DocumentId { get; set; }
    public required string Language { get; set; }
    public required string Name { get; set; }

    public DateTimeOffset CreatedOn { get; set; }
    public int CreatedBy { get; set; }
    public DateTimeOffset ModifiedOn { get; set; }
    public int ModifiedBy { get; set; }

    public virtual Document? Document { get; set; }
}
