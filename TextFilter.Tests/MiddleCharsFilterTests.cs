using TextFilter.Core.Filters;

namespace TextFilter.Tests;

[TestFixture]
public sealed class MiddleCharsFilterTests
{
    private readonly MiddleCharsFilter _sut = new(new char[] { 'a', 'e', 'i', 'o', 'u' });

    [Test]
    public void ShouldKeep_OddLengthWithVowelInMiddle_ReturnsFalse()
    {
        Assert.That(_sut.ShouldKeep("clean"), Is.False);
    }

    [Test]
    public void ShouldKeep_OddLengthWithoutVowelInMiddle_ReturnsTrue()
    {
        Assert.That(_sut.ShouldKeep("rather"), Is.True);
    }

    [Test]
    public void ShouldKeep_EvenLengthContainingVowelInMiddle_ReturnsFalse()
    {
        Assert.That(_sut.ShouldKeep("what"), Is.False);
    }

    [Test]
    public void ShouldKeep_OneLetterWord_ReturnsExpectedResult()
    {
        Assert.Multiple(() =>
        {
            Assert.That(_sut.ShouldKeep("a"), Is.False);
            Assert.That(_sut.ShouldKeep("b"), Is.True);
        });
    }

    [Test]
    public void ShouldKeep_NullInput_ThrowsArgumentNullException()
    {
        Assert.That(() => _sut.ShouldKeep(null!), Throws.TypeOf<ArgumentNullException>());
    }
}
