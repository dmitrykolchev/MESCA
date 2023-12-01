// <copyright file="DocumentExtension.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Xobex.Data.Common;
using Xobex.Data.Entities.Core;

namespace Xobex.Data.Mes.Entities.Core;

public class DocumentExtension : ISimpleAuditable
{
    public int DocumentTypeId { get; set; }
    public int DocumentId { get; set; }
    public int GlobalFlags { get; set; }
    public string? ApplicationData { get; set; }
    public DateTimeOffset ModifiedOn { get; set; }
    public int ModifiedBy { get; set; }

    public virtual Document? Document { get; set; }
}
