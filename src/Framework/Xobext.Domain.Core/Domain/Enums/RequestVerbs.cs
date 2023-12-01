// <copyright file="RequestVerbs.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

namespace Xobex.Domain.Enums;

public enum RequestVerbs : short
{
    Unknown = 0,
    Get,
    Add,
    Change,
    ChangeState,
    Delete,
    Remove,
    Process,
    Cancel,
    Import,
    Export,
    Custom = 100
}
