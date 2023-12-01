// <copyright file="DocumentNumberTemplateCounter.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

namespace Xobex.Data.Mes.Entities.Core;

public class DocumentNumberTemplateCounter
{
    public int DocumentNumberTemplateId { get; set; }
    public required string Selector { get; set; }
    public long NextValue { get; set; }

    public virtual DocumentNumberTemplate? DocumentNumberTemplate { get; set; }
}
