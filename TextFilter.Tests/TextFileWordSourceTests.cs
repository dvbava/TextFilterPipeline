
using TextFilter.Core;

namespace TextFilter.Tests;

[TestFixture]
public sealed class TextFileWordSourceTests
{
    [Test]
    public void ReadWords_ValidFile_ReturnsAllWords()
    {
        var path = CreateTempFile("alpha beta\r\ngamma");
        var sut = new TextFileWordSource();

        var result = sut.ReadTokens(path).ToArray();

        Assert.That(result, Is.EqualTo(new[] { "alpha", "beta", "\r\n", "gamma", "\r\n" }));
        File.Delete(path);
    }

    [Test]
    public void ReadWords_EmptyFile_ReturnsEmpty()
    {
        var path = CreateTempFile(string.Empty);
        var sut = new TextFileWordSource();

        var result = sut.ReadTokens(path).ToArray();

        Assert.That(result, Is.Empty);
        File.Delete(path);
    }

    [Test]
    public void ReadWords_MissingFile_ThrowsFileNotFoundException()
    {
        var sut = new TextFileWordSource();
        var path = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".txt");

        Assert.That(() => sut.ReadTokens(path).ToArray(), Throws.TypeOf<FileNotFoundException>());
    }

    [Test]
    public void ReadWords_InvalidPath_ThrowsArgumentException()
    {
        var sut = new TextFileWordSource();

        Assert.Multiple(() =>
        {
            Assert.That(() => sut.ReadTokens(null!).ToArray(), Throws.TypeOf<ArgumentNullException>());
            Assert.That(() => sut.ReadTokens(" ").ToArray(), Throws.TypeOf<ArgumentException>());
        });
    }

    [Test]
    public void ReadWords_MultipleLines_ReturnsWordsAcrossLines()
    {
        var path = CreateTempFile("one two\r\nthree\r\nfour five");
        var sut = new TextFileWordSource();

        var result = sut.ReadTokens(path).ToArray();

        Assert.That(result, Is.EqualTo(new[] { "one", "two", "\r\n", "three", "\r\n", "four", "five", "\r\n" }));
        File.Delete(path);
    }

    [Test]
    public void ReadWords_StreamingBehavior_CanYieldFirstWordWithoutMaterializingAll()
    {
        var lines = Enumerable.Repeat("later", 10000).ToList();
        lines.Insert(0, "first");
        var path = CreateTempFile(string.Join(Environment.NewLine, lines));
        var sut = new TextFileWordSource();

        var firstWord = sut.ReadTokens(path).First();

        Assert.That(firstWord, Is.EqualTo("first"));
        File.Delete(path);
    }

    private static string CreateTempFile(string content)
    {
        var path = Path.Combine(Path.GetTempPath(), $"text-filter-{Guid.NewGuid():N}.txt");
        File.WriteAllText(path, content);
        return path;
    }
}
