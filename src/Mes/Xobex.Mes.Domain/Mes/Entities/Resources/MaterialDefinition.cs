// <copyright file="MaterialDefinition.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

namespace Xobex.Data.Mes.Entities.Resources;

public enum MaterialDefinitionState
{
    NotExists = 0,
    Active = 1,
    Inactive = 2
}

public class MaterialDefinition : ResourceBase<MaterialDefinitionState>
{
    public MaterialDefinition()
    {
    }

    public virtual Resource? Resource { get; set; }
}
