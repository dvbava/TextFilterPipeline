namespace TextFilter.Core
{
    public interface ITextProcessingOrchestrator
    {
        IEnumerable<string> Process(string path);
    }
}