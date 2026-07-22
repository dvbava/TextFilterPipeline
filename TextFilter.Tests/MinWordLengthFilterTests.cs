using TextFilter.Core.Filters;

namespace TextFilter.Tests;

[TestFixture]
public sealed class MinWordLengthFilterTests
{
    private readonly MinWordLengthFilter _sut = new(3);

    [Test]
    public void ShouldKeep_EmptyString_ReturnsFalse()
    {
        Assert.That(_sut.ShouldKeep(string.Empty), Is.False);
    }

    [Test]
    public void ShouldKeep_LengthOne_ReturnsFalse()
    {
        Assert.That(_sut.ShouldKeep("a"), Is.False);
    }

    [Test]
    public void ShouldKeep_LengthThree_ReturnsTrue()
    {
        Assert.That(_sut.ShouldKeep("abc"), Is.True);
    }

    [Test]
    public void ShouldKeep_LongWord_ReturnsTrue()
    {
        Assert.That(_sut.ShouldKeep("longword"), Is.True);
    }

    [Test]
    public void ShouldKeep_NullInput_ThrowsArgumentNullException()
    {
        Assert.That(() => _sut.ShouldKeep(null!), Throws.TypeOf<ArgumentNullException>());
    }
}
