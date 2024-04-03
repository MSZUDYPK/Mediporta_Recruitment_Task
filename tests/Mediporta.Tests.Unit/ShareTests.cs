using Xunit;
using Mediporta.Core.Tags;

namespace Mediporta.Tests.Unit;

public class ShareTests
{
    [Fact]
    public void Share_CreatesSuccessfully_WhenValueIsWithinRange()
    {
        var share = new Share(50.0);
        Assert.Equal(50.0, share.Value);
    }

    [Fact]
    public void Share_ThrowsException_WhenValueIsLessThanZero()
    {
        Assert.Throws<InvalidShareException>(() => new Share(-1.0));
    }

    [Fact]
    public void Share_ThrowsException_WhenValueIsGreaterThan100()
    {
        Assert.Throws<InvalidShareException>(() => new Share(101.0));
    }

    [Fact]
    public void Share_CastsToDoubleSuccessfully()
    {
        var share = new Share(50.0);
        double value = share;
        Assert.Equal(50.0, value);
    }

    [Fact]
    public void Share_CastsFromDoubleSuccessfully()
    {
        Share share = 50.0;
        Assert.Equal(50.0, share.Value);
    }
}