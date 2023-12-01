// <copyright file="EntityConfigurationAttribute.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

namespace Xobex.Data.EntityFramework;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Class)]
public class EntityConfigurationAttribute : Attribute
{
    public EntityConfigurationAttribute(Type entityConfigurationType)
    {
        EntityConfigurationType = entityConfigurationType;
    }

    public Type EntityConfigurationType { get; }
}

