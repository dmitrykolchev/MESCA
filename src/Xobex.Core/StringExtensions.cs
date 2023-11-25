// <copyright file="StringExtensions.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using System.Text;

namespace Xobex;

public static class StringExtensions
{
    public static string ToSnakeCase(this string value)
    {
        ArgumentNullException.ThrowIfNull(value);

        StringBuilder buffer = new();
        for (int index = 0; index < value.Length; ++index)
        {
            if (char.IsUpper(value[index]))
            {
                if (index != 0)
                {
                    buffer.Append('_');
                }
                buffer.Append(char.ToLower(value[index]));
            }
            else
            {
                buffer.Append(value[index]);
            }
        }
        return buffer.ToString();
    }
}
