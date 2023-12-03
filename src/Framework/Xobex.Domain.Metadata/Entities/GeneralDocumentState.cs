// <copyright file="GeneralDocumentState.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

namespace Xobex.Entities;

public enum GeneralDocumentState : short
{
    NotExists = 0,
    Active = 1,
    Inactive = 2,
    Imported = 3,
}
