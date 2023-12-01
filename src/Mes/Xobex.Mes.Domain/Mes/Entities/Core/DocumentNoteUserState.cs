// <copyright file="DocumentNoteState.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Xobex.Data.Common;

namespace Xobex.Data.Mes.Entities.Core;

public class DocumentNoteUserState : ISimpleAuditable
{
    public int DocumentNoteId { get; set; }
    public int UserId { get; set; }
    public bool IsRead { get; set; }
    public DateTimeOffset ModifiedOn { get; set; }
    public int ModifiedBy { get; set; }

    public virtual DocumentNote? DocumentNote { get; set; }
}
