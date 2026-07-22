namespace TextFilter.Core.Filters;

public sealed class MinWordLengthFilter : ITextFilter
{
    private readonly int _minLength;

    public MinWordLengthFilter(int minLength)
    {
        _minLength = minLength;
    }

    public bool ShouldKeep(string word)
    {
        ArgumentNullException.ThrowIfNull(word);
        return word.Length >= _minLength || word == Environment.NewLine;
    }
}
