// <copyright file="IDataItem`1.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

namespace Xobex.Data.Common;

public interface IDataItem<TKey> : IDataItemBase
{
    new TKey Id { get; }
}
