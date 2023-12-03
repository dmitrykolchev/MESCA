// <copyright file="Resource.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>


// <copyright file="Resource.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Xobex.Entities.Metadata;

namespace Xobex.Mes.Entities.Resources;

public class Resource : ResourceBase<short>
{
    public Resource() { }

    public int DocumentTypeId { get; set; }

    public virtual DocumentType? DocumentType { get; set; }
}
