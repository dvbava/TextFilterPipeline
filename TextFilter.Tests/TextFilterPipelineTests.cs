using TextFilter.Core.Filters;
using TextFilter.Core.Pipeline;

namespace TextFilter.Tests;

[TestFixture]
public sealed class TextFilterPipelineTests
{
    [Test]
    public void Process_NoFilters_ReturnsOriginalWords()
    {
        var sut = new TextFilterPipeline(Array.Empty<ITextFilter>());
        var input = new[] { "alpha", "beta" };

        var result = sut.Process(input).ToArray();

        Assert.That(result, Is.EqualTo(input));
    }

    [Test]
    public void Process_SingleFilter_AppliesFilter()
    {
        var sut = new TextFilterPipeline([new MinWordLengthFilter(3)]);
        var input = new[] { "a", "abc", "de" };

        var result = sut.Process(input).ToArray();

        Assert.That(result, Is.EqualTo(new[] { "abc" }));
    }


    [Test]
    public void Process_AllFiltersAppliedCorrectly_ReturnsExpectedOutput()
    {
        var sut = new TextFilterPipeline([new MiddleCharsFilter(new char[] { 'a', 'e', 'i', 'o', 'u' }), new MinWordLengthFilter(3), new ContainsCharFilter('t')]);
        var input = new[] { "the", "what", "myth", "rather", "cat", "sky" };

        var result = sut.Process(input).ToArray();

        Assert.That(result, Is.EqualTo(new[] { "sky" }));
    }

    [Test]
    public void Process_EmptyInput_ReturnsEmpty()
    {
        var sut = new TextFilterPipeline([new MinWordLengthFilter(3)]);

        var result = sut.Process(Array.Empty<string>()).ToArray();

        Assert.That(result, Is.Empty);
    }

    [Test]
    public void Process_NoWordsRemoved_ReturnsAllWords()
    {
        var sut = new TextFilterPipeline([new ContainsCharFilter('t')]);
        var input = new[] { "alpha", "echo" };

        var result = sut.Process(input).ToArray();

        Assert.That(result, Is.EqualTo(input));
    }

    [Test]
    public void Process_IsLazy_EnumeratesOnDemand()
    {
        var sut = new TextFilterPipeline(Array.Empty<ITextFilter>());
        var moveNextCount = 0;

        IEnumerable<string> Input()
        {
            moveNextCount++;
            yield return "first";
            moveNextCount++;
            yield return "second";
        }

        var output = sut.Process(Input());

        Assert.That(moveNextCount, Is.EqualTo(0));

        var first = output.First();

        Assert.That(first, Is.EqualTo("first"));
        Assert.That(moveNextCount, Is.EqualTo(1));
    }
}
