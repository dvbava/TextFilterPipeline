namespace TextFilter.Core.Filters;

public sealed class MiddleCharsFilter : ITextFilter
{
    private readonly char[] _middleChars;

    public MiddleCharsFilter(char[] middleChars)
    {
        _middleChars = middleChars;
    }

    public bool ShouldKeep(string word)
    {
        ArgumentNullException.ThrowIfNull(word);

        if (word.Length == 0)
        {
            return true;
        }

        var middleIndex = word.Length / 2;

        if (word.Length % 2 == 1)
        {
            return !IsMiddleChar(word[middleIndex]);
        }

        return !IsMiddleChar(word[middleIndex - 1]) && !IsMiddleChar(word[middleIndex]);
    }

    private bool IsMiddleChar(char c)
    {
        return _middleChars.Contains(char.ToLowerInvariant(c));
    }
}
