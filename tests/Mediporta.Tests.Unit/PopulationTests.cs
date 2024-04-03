using Mediporta.Core.Tags;
using Mediporta.Core.Population;
using Mediporta.Core.Population.Exceptions;

namespace Mediporta.Tests.Unit;

public class PopulationTests
{
    [Fact]
    public void Population_CreatesSuccessfully_WhenTagCountIsGreaterOrEqual1000()
    {
        // Arrange
        var tags = new List<Tag>();
        
        for (var i = 0; i < 1000; i++)
        {
            tags.Add(new Tag($"Tag{i}", i));
        }

        // Act
        var population = new Population(tags);

        // Assert
        Assert.NotNull(population);
        Assert.Equal(1000, population.SimplifiedTags.Count);
    }

    [Fact]
    public void Population_ThrowsException_WhenTagCountIsLessThan1000()
    {
        // Arrange
        var tags = new List<Tag>();
        
        for (var i = 0; i < 999; i++)
        {
            tags.Add(new Tag($"Tag{i}", i));
        }

        // Act & Assert
        Assert.Throws<InvalidPopulationSizeException>(() => new Population(tags));
    }

    [Fact]
    public void Population_CalculatesTagSharesCorrectly()
    {
        // Arrange
        var tags = new List<Tag>();
        
        for (var i = 0; i < 1000; i++)
        {
            tags.Add(new Tag($"Tag{i}", 1));
        }
        
        // Act
        var population = new Population(tags);

        // Assert
        Assert.Equal(0.001, population.SimplifiedTags[0].Share.Value);
        Assert.Equal(0.001, population.SimplifiedTags[1].Share.Value);
        Assert.Equal(0.001, population.SimplifiedTags[2].Share.Value);
    }
}