using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Mediporta.Application.Services;
using Mediporta.Core.Population;
using Mediporta.Core.Tags;
using Mediporta.Tests.Integration.Shared;
using Microsoft.Extensions.DependencyInjection;

namespace Mediporta.Tests.Integration.Endpoints;

public sealed class PostPopulationTests : ApiTests
{
    [Fact]
    public async Task Should_Return_OK_When_PullingAllTags_With_Empty_Body()
    {
        // Arrange
        var emptyBody = Array.Empty<string>();
        // Act
        var response = await Client.PostAsJsonAsync("/api/population", emptyBody);
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        TestDbContext.Populations.Should().NotBeEmpty();
        TestDbContext.SimplifiedTags.Should().NotBeEmpty();
        TestDbContext.SimplifiedTags.Should().HaveCount(1000);
    }
    
    [Fact]
    public async Task Should_Return_OK_When_PullingAllTags_With_Null_Body()
    {
        // Arrange
        var nullBody = (string[]?)null; 
        // Act
        var response = await Client.PostAsJsonAsync("/api/population", nullBody);
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        TestDbContext.Populations.Should().NotBeEmpty();
        TestDbContext.SimplifiedTags.Should().NotBeEmpty();
        TestDbContext.SimplifiedTags.Should().HaveCount(1000);
    }
    
    [Fact]
    public async Task Should_Return_BadRequest_When_PullingMissingTags_With_Empty_Database()
    {
        //Arrange
        var missingTagNames = new[]
        {
            "Tag1001",
            "Tag1002",
            "Tag1003",
            "Tag1004",
            "Tag1005"
        };
        
        // Act
        var response = await Client.PostAsJsonAsync("/api/population", missingTagNames);
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        TestDbContext.Populations.Should().BeEmpty();
        TestDbContext.SimplifiedTags.Should().BeEmpty();
        TestDbContext.SimplifiedTags.Should().HaveCount(0);
    }
    
    [Fact]
    public async Task Should_Return_OK_When_PullingDistinctMissingTags_With_Initialized_Database()
    {
        //Arrange Database
        var tags = new List<Tag>();
        
        for (var i = 0; i < 1000; i++)
        {
            tags.Add(new Tag($"Tag{i}", i));
        }
        
        var population = new Population(tags);
        await TestDbContext.Populations.AddAsync(population);
        await TestDbContext.SaveChangesAsync();
        
        // Arrange Request Body
        var missingTagNames = new[]
        {
            "Tag1001",
            "Tag1002",
            "Tag1003",
            "Tag1004",
            "Tag1005"
        };
        
        // Act
        var response = await Client.PostAsJsonAsync("/api/population", missingTagNames);
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        TestDbContext.Populations.Should().NotBeEmpty();
        TestDbContext.SimplifiedTags.Should().NotBeEmpty();
        TestDbContext.SimplifiedTags.Should().HaveCount(2005);
    }
    
    protected override Action<IServiceCollection>? ConfigureServices => services =>
    {
        services.AddScoped<ITagExtractor, TestTagExtractor>();
    };
    
}