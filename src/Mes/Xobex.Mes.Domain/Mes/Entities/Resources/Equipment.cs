// <copyright file="Equipment.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

namespace Xobex.Mes.Entities.Resources;

public enum EquipmentState: short
{
    NotExists = 0,
    Active = 1,
    Inactive = 2
}

public class Equipment : ResourceBase<EquipmentState>
{
    public Equipment() { }

    public virtual Resource? Resource { get; set; }
    public virtual Equipment? Parent { get; set; }
}
