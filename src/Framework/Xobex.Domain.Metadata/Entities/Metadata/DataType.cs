// <copyright file="DataType.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Xobex.Data.Common;

namespace Xobex.Data.Entities.Metadata;

public enum DataTypeState : short
{
    NotExists = 0,
    Active = 1,
    Inactive = 2
}

public enum DataKind : short
{
    Unknown = 0,
    Int,
    Boolean,
    DateTime,
    String,
    Decimal,
    Double,
    Binary,
    DocumentReference,
    Complex,

}


public class DataType: IAuditable
{
    public int Id { get; set; }
    public DataTypeState State { get; set; }
    public required string Code { get; set; }
    public required string Name { get; set; }
    public DataKind Kind { get; set; }
    public int? DocumentTypeId { get; set; }
    public DateTimeOffset CreatedOn { get; set; }
    public int CreatedBy { get; set; }
    public DateTimeOffset ModifiedOn { get; set; }
    public int ModifiedBy { get; set; }

    public virtual DocumentType? DocumentType { get; set; }
}
