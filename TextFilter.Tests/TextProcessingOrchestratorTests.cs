using TextFilter.Core;
using TextFilter.Core.Filters;
using TextFilter.Core.Pipeline;

namespace TextFilter.Tests;

[TestFixture]
public sealed class TextProcessingOrchestratorTests
{
    [Test]
    public void Process_SuccessfulEndToEndProcessing_ReturnsFilteredWords()
    {
        var source = new FakeWordSource(new[] { "the", "what", "myth", "sky" });
        var pipeline = new TextFilterPipeline([new MiddleCharsFilter(new char[] { 'a', 'e', 'i', 'o', 'u' }), new WordLengthFilter(3), new ContainsCharFilter('t')]);
        var sut = new TextProcessingOrchestrator(source, pipeline);

        var result = sut.Process("ignored-path").ToArray();

        Assert.That(result, Is.EqualTo(new[] { "sky" }));
    }

    [Test]
    public void Process_EmptyInput_ReturnsEmpty()
    {
        var source = new FakeWordSource(Array.Empty<string>());
        var pipeline = new TextFilterPipeline([new WordLengthFilter(3)]);
        var sut = new TextProcessingOrchestrator(source, pipeline);

        var result = sut.Process("ignored-path").ToArray();

        Assert.That(result, Is.Empty);
    }

    [Test]
    public void Process_AllWordsRemoved_ReturnsEmpty()
    {
        var source = new FakeWordSource(["t", "to", "test"]);
        var pipeline = new TextFilterPipeline([new ContainsCharFilter('t')]);
        var sut = new TextProcessingOrchestrator(source, pipeline);

        var result = sut.Process("ignored-path").ToArray();

        Assert.That(result, Is.Empty);
    }





    private sealed class FakeWordSource : IWordSource
    {
        private readonly IEnumerable<string> _words;

        public FakeWordSource(IEnumerable<string> words)
        {
            _words = words;
        }

        public IEnumerable<string> ReadTokens(string path)
        {
            return _words;
        }
    }
}
