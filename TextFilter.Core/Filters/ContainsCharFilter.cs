namespace TextFilter.Core.Filters;

public sealed class ContainsCharFilter : ITextFilter
{
    private readonly char _charToCheck;
    public ContainsCharFilter(char charToCheck)
    {
        _charToCheck = charToCheck;
    }

    public bool ShouldKeep(string word)
    {
        ArgumentNullException.ThrowIfNull(word);
        return word.IndexOf(_charToCheck, StringComparison.OrdinalIgnoreCase) < 0;
    }
}
