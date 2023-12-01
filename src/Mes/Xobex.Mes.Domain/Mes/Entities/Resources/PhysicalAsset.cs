// <copyright file="PhysicalAsset.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

namespace Xobex.Data.Mes.Entities.Resources;

public enum PhysicalAssetState : short
{
    NotExists = 0,
    Active = 1,
    Inactive = 2
}

public class PhysicalAsset : ResourceBase<PhysicalAssetState>
{
    public PhysicalAsset()
    {
    }

    public virtual Resource? Resource { get; set; }
}
