// <copyright file="DataType.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Xobex.Data.Common;

namespace Xobex.Entities.Metadata;

public enum DataKind : short
{
    Unknown = 0,
    /// <summary>
    /// Integer
    /// </summary>
    Int,
    /// <summary>
    /// Boolean
    /// </summary>
    Boolean,
    /// <summary>
    /// Date and time
    /// </summary>
    DateTime,
    /// <summary>
    /// Text
    /// </summary>
    String,
    /// <summary>
    /// Decimal with fixed point
    /// </summary>
    Decimal,
    /// <summary>
    /// Double precisition floating-point
    /// </summary>
    Double,
    /// <summary>
    /// Binary data
    /// </summary>
    Binary,
    /// <summary>
    /// Decimal with currency
    /// </summary>
    Money,
    /// <summary>
    /// Decimal with unit of measure
    /// </summary>
    Amount,
    /// <summary>
    /// Document Reference 
    /// </summary>
    DocumentReference,
    /// <summary>
    /// Complex
    /// </summary>
    Complex,
}


public class DataType: IAuditable
{
    public int Id { get; set; }
    public CommonStates State { get; set; }
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
