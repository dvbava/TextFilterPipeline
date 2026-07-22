using TextFilter.Core.Pipeline;

namespace TextFilter.Core;

public sealed class TextProcessingOrchestrator : ITextProcessingOrchestrator
{
    private readonly IWordSource _wordSource;
    private readonly ITextFilterPipeline _pipeline;

    public TextProcessingOrchestrator(IWordSource wordSource, ITextFilterPipeline pipeline)
    {
        _wordSource = wordSource ?? throw new ArgumentNullException(nameof(wordSource));
        _pipeline = pipeline ?? throw new ArgumentNullException(nameof(pipeline));
    }

    public IEnumerable<string> Process(string path)
    {
        var words = _wordSource.ReadTokens(path);
        return _pipeline.Process(words);
    }
}
