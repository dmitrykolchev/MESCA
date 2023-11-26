﻿// <copyright file="Empty.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

namespace Xobex.Mediator;

public class Empty
{
    public static readonly Empty Instance = new();

    private Empty() { }
}
