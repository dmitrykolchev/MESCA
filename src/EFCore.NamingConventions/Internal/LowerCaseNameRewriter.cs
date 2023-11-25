using System.Globalization;

namespace EFCore.NamingConventions.Internal;

public class LowerCaseNameRewriter : INameRewriter
{
    private readonly CultureInfo _culture;

    public LowerCaseNameRewriter(CultureInfo culture)
    {
        _culture = culture;
    }

    public string RewriteName(string name)
    {
        return name.ToLower(_culture);
    }
}
