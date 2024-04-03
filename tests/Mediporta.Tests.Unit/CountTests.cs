using Mediporta.Core.Tags;

namespace Mediporta.Tests.Unit;

public class CountTests
{
    [Fact]
    public void Count_CreatesSuccessfully_WhenValueIsNonNegative()
    {
        var count = new Count(0);
        Assert.Equal(0, count.Value);
    }

    [Fact]
    public void Count_ThrowsException_WhenValueIsNegative()
    {
        Assert.Throws<InvalidCountException>(() => new Count(-1));
    }

    [Fact]
    public void Count_CastsToIntSuccessfully()
    {
        var count = new Count(50);
        int value = count;
        Assert.Equal(50, value);
    }

    [Fact]
    public void Count_CastsFromIntSuccessfully()
    {
        Count count = 50;
        Assert.Equal(50, count.Value);
    }
}