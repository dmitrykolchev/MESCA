// <copyright file="NamespaceMappingAttribute.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

namespace Xobex.Infrastructure.EntityFramework;

[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
public class NamespaceMappingAttribute : Attribute
{
    public NamespaceMappingAttribute(string ns, string schemaName)
    {
        Namespace = ns;
        SchemaName = schemaName;
    }

    public string Namespace { get; }
    public string SchemaName { get; }
}
