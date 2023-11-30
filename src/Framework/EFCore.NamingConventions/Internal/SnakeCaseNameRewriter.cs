using System.Globalization;
using System.Text;

namespace EFCore.NamingConventions.Internal;

public class SnakeCaseNameRewriter : INameRewriter
{
    private readonly CultureInfo _culture;

    public SnakeCaseNameRewriter(CultureInfo culture)
    {
        _culture = culture;
    }

    public virtual string RewriteName(string name)
    {
        StringBuilder builder = new(name.Length + Math.Min(2, name.Length / 5));
        UnicodeCategory? previousCategory = default;

        for (int currentIndex = 0; currentIndex < name.Length; currentIndex++)
        {
            char currentChar = name[currentIndex];
            if (currentChar == '_')
            {
                builder.Append('_');
                previousCategory = null;
                continue;
            }

            UnicodeCategory currentCategory = char.GetUnicodeCategory(currentChar);
            switch (currentCategory)
            {
                case UnicodeCategory.UppercaseLetter:
                case UnicodeCategory.TitlecaseLetter:
                    if (previousCategory == UnicodeCategory.SpaceSeparator ||
                        previousCategory == UnicodeCategory.LowercaseLetter ||
                        previousCategory != UnicodeCategory.DecimalDigitNumber &&
                        previousCategory != null &&
                        currentIndex > 0 &&
                        currentIndex + 1 < name.Length &&
                        char.IsLower(name[currentIndex + 1]))
                    {
                        builder.Append('_');
                    }

                    currentChar = char.ToLower(currentChar, _culture);
                    break;

                case UnicodeCategory.LowercaseLetter:
                case UnicodeCategory.DecimalDigitNumber:
                    if (previousCategory == UnicodeCategory.SpaceSeparator)
                    {
                        builder.Append('_');
                    }
                    break;

                default:
                    if (previousCategory != null)
                    {
                        previousCategory = UnicodeCategory.SpaceSeparator;
                    }
                    continue;
            }

            builder.Append(currentChar);
            previousCategory = currentCategory;
        }

        return builder.ToString();
    }
}
