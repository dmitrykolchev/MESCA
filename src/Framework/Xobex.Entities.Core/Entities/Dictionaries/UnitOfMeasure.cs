﻿// <copyright file="UnitOfMeasure.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Xobex.Data.Common;

namespace Xobex.Entities.Dictionaries;

public class UnitOfMeasure: DocumentBase<int, CommonStates>
{
    public string? InternationalName { get; set; }
    public required string PrintSymbol { get; set; }
    public required string Symbol { get; set; }
    public int KindOfQuantityId { get; set; }
    public bool IsMetric { get; set; }

    public virtual KindOfQuantity? KindOfQuantity { get; set; }
    public virtual UnitOfMeasure? Base { get; set; }
}
