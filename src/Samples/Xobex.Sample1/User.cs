// <copyright file="User.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Xobex.Data.Common;

namespace Xobex.Sample1;

public class User(IEnumerable<Claim> claims) : ClaimsIdentity(claims), IUser
{
    public int Id => int.Parse(Claims.First(t => t.Type == JwtRegisteredClaimNames.Sub).Value);
}
