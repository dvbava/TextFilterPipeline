using TextFilter.Core.Filters;

namespace TextFilter.Core.Pipeline;

// streaming linear pipeline of predicates
public sealed class TextFilterPipeline : ITextFilterPipeline
{
    private readonly ITextFilter[] _filters;

    public TextFilterPipeline(IEnumerable<ITextFilter> filters)
    {
        ArgumentNullException.ThrowIfNull(filters);
        _filters = filters.ToArray();
    }

    public IEnumerable<string> Process(IEnumerable<string> words)
    {
        ArgumentNullException.ThrowIfNull(words);

        foreach (var word in words)
        {
            ArgumentNullException.ThrowIfNull(word); // should not be null, but just in case

            var shouldKeep = true;
            foreach (var filter in _filters)
            {
                if (!filter.ShouldKeep(word))
                {
                    shouldKeep = false;
                    break;
                }
            }

            if (shouldKeep)
            {
                yield return word;
            }
        }
    }
}
