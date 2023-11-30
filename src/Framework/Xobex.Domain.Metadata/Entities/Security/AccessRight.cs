// <copyright file="AccessRight.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Xobex.Domain.Common;

namespace Xobex.Entities.Security;

public enum AccessRightState : short
{
    NotExists = 0,
    Active = 1,
    Inactive = 2,
}

[Flags]
public enum AccessRightFlags : int
{
    None = 0,
}

public class AccessRight : IAuditable
{
    public int Id { get; set; }
    public AccessRightState State { get; set; }
    public AccessRightFlags Flags { get; set; }
    public required string Code { get; set; }
    public required string Name { get; set; }
    public string? Category { get; set; }
    public string? Comments { get; set; }

    public DateTimeOffset CreatedOn { get; set; }
    public int CreatedBy { get; set; }
    public DateTimeOffset ModifiedOn { get; set; }
    public int ModifiedBy { get; set; }
}
