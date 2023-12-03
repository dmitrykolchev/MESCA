// <copyright file="CurrencyRate.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Xobex.Data.Common;

namespace Xobex.Entities.Dictionaries;

public class CurrencyRate : ISimpleAuditable
{
    public int Id { get; set; }
    public int CurrencyRateProviderId { get; set; }
    public DateTimeOffset RateDate { get; set; }
    public int FromId { get; set; }
    public int ToId { get; set; }
    public decimal Multiplier { get; set; }
    public decimal Divider { get; set; }

    public DateTimeOffset ModifiedOn { get; set; }
    public int ModifiedBy { get; set; }

    public virtual Currency? From { get; set; }
    public virtual Currency? To { get; set; }
}
