using TextFilter.Core.Filters;

namespace TextFilter.Tests;

[TestFixture]
public sealed class ContainsCharFilterTests
{
    private readonly ContainsCharFilter _sut = new('t');

    [Test]
    public void ShouldKeep_LowercaseT_ReturnsFalse()
    {
        Assert.That(_sut.ShouldKeep("cat"), Is.False);
    }

    [Test]
    public void ShouldKeep_UppercaseT_ReturnsFalse()
    {
        Assert.That(_sut.ShouldKeep("Tea"), Is.False);
    }

    [Test]
    public void ShouldKeep_MultipleTCharacters_ReturnsFalse()
    {
        Assert.That(_sut.ShouldKeep("tattletale"), Is.False);
    }

    [Test]
    public void ShouldKeep_NoTPresent_ReturnsTrue()
    {
        Assert.That(_sut.ShouldKeep("clear"), Is.True);
    }
}
