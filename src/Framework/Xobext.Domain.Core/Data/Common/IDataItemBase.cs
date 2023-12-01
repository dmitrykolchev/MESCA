// <copyright file="IDataItemBase.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

namespace Xobex.Data.Common;

public interface IDataItemBase
{
    public object Id { get; }

    public short State { get; }

    public string Name { get; }
}
