namespace TextFilter.Core.Pipeline
{
    public interface ITextFilterPipeline
    {
        IEnumerable<string> Process(IEnumerable<string> words);
    }
}