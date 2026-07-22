namespace TextFilter.Core.Filters;

public interface ITextFilter
{
    bool ShouldKeep(string word);
}
