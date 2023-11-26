// <copyright file="DocumentHistory.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Xobex.Domain.Common;

namespace Xobex.Mes.Entities.Core;

public class DocumentHistory : ISimpleAuditable
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int DocumentTypeId { get; set; }
    public int DocumentId { get; set; }
    public bool IsFavorite { get; set; }
    public int Flags { get; set; }

    public DateTimeOffset ModifiedOn { get; set; }
    public int ModifiedBy { get; set; }

    public virtual Document? Document { get; set; }
}
