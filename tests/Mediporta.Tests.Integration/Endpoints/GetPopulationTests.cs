using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Mediporta.Application.Services;
using Mediporta.Core.Population;
using Mediporta.Core.Tags;
using Mediporta.Tests.Integration.Shared;
using Microsoft.Extensions.DependencyInjection;

namespace Mediporta.Tests.Integration.Endpoints;

public sealed class GetPopulationTests : ApiTests
{
    
    [Theory]
    [InlineData(10, 1)]
    [InlineData(20, 1)]
    [InlineData(10, 2)]
    [InlineData(20, 2)]
    public async Task Should_Return_Population_Page(int pageSize, int pageNumber)
    {
        // Arrange
        var tags = new List<Tag>();
        
        for (var i = 0; i < 1000; i++)
        {
            tags.Add(new Tag($"Tag{i}", i));
        }
        
        var population = new Population(tags);
        await TestDbContext.Populations.AddAsync(population);
        await TestDbContext.SaveChangesAsync();
        
        // Act
        var response = await Client.GetAsync($"/api/population?page={pageNumber}&pageSize={pageSize}&sort=name&order=asc");
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var tagsPagedList = await response.Content.ReadFromJsonAsync<PopulationListResponse>();

        
        tagsPagedList.Should().NotBeNull();
        tagsPagedList.Page.Should().Be(pageNumber);
        tagsPagedList.PageSize.Should().Be(pageSize);
        tagsPagedList.Items.Should().HaveCount(pageSize);
        tagsPagedList.TotalCount.Should().Be(1000);
        tagsPagedList.HasNextPage.Should().Be(pageNumber * pageSize < 1000);
        tagsPagedList.HasPreviousPage.Should().Be(pageNumber > 1);
    }
    
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public async Task Should_Return_Bad_Request_When_PageSize_Is_Less_Than_1(int pageSize)
    {
        // Arrange
        var response = await Client.GetAsync($"/api/population?page=1&pageSize={pageSize}&sort=name&order=asc");
        
        // Act
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
    
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public async Task Should_Return_Bad_Request_When_Page_Is_Less_Than_1(int page)
    {
        // Arrange
        var response = await Client.GetAsync($"/api/population?page={page}&pageSize=10&sort=name&order=asc");
        
        // Act
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
    
    [Fact]
    public async Task Should_Return_Bad_Request_When_SortColumn_Is_Invalid()
    {
        // Arrange
        var response = await Client.GetAsync($"/api/population?page=1&pageSize=10&sort=invalid&order=asc");
        
        // Act
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
    
    [Fact]
    public async Task Should_Return_Bad_Request_When_SortOrder_Is_Invalid()
    {
        // Arrange
        var response = await Client.GetAsync($"/api/population?page=1&pageSize=10&sort=name&order=invalid");
        
        // Act
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
    
    [Fact]
    public async Task Should_Return_Bad_Request_When_SortColumn_And_SortOrder_Are_Invalid()
    {
        // Arrange
        var response = await Client.GetAsync($"/api/population?page=1&pageSize=10&sort=invalid&order=invalid");
        
        // Act
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
    
    [Fact]
    public async Task Should_Return_Population_When_SortColumn_Is_Null()
    {
        // Arrange
        var tags = new List<Tag>();
        
        for (var i = 0; i < 1000; i++)
        {
            tags.Add(new Tag($"Tag{i}", i));
        }
        
        var population = new Population(tags);
        await TestDbContext.Populations.AddAsync(population);
        await TestDbContext.SaveChangesAsync();
        
        // Act
        var response = await Client.GetAsync($"/api/population?page=1&pageSize=10");
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        
        // Assert
        var tagsPagedList = await response.Content.ReadFromJsonAsync<PopulationListResponse>();
        
        tagsPagedList.Items.Should().HaveCount(10);
    }
    
    [Fact]
    public async Task Should_Return_Population_When_SortOrder_Is_Null()
    {
        // Arrange
        var tags = new List<Tag>();
        
        for (var i = 0; i < 1000; i++)
        {
            tags.Add(new Tag($"Tag{i}", i));
        }
        
        var population = new Population(tags);
        await TestDbContext.Populations.AddAsync(population);
        await TestDbContext.SaveChangesAsync();
        
        // Act
        var response = await Client.GetAsync($"/api/population?page=1&pageSize=10&sort=name");
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        
        // Assert
        var tagsPagedList = await response.Content.ReadFromJsonAsync<PopulationListResponse>();
        
        tagsPagedList.Items.Should().HaveCount(10);
    }
    
    [Fact]
    public async Task Should_Return_Empty_Population_Page()
    {
        // Arrange
        var response = await Client.GetAsync($"/api/population?page=1&pageSize=10&sort=name&order=asc");
        
        // Act
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        
        // Assert
        var tagsPagedList = await response.Content.ReadFromJsonAsync<PopulationListResponse>();
        
        tagsPagedList.Should().NotBeNull();
        tagsPagedList.Page.Should().Be(1);
        tagsPagedList.PageSize.Should().Be(10);
        tagsPagedList.Items.Should().HaveCount(0);
        tagsPagedList.TotalCount.Should().Be(0);
        tagsPagedList.HasNextPage.Should().BeFalse();
        tagsPagedList.HasPreviousPage.Should().BeFalse();
    }
    
    [Fact]
    public async Task Should_Return_Empty_Population_Page_When_Page_Is_Out_Of_Range()
    {
        // Arrange
        var response = await Client.GetAsync($"/api/population?page=2&pageSize=10&sort=name&order=asc");
        
        // Act
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        
        // Assert
        var tagsPagedList = await response.Content.ReadFromJsonAsync<PopulationListResponse>();
        
        tagsPagedList.Should().NotBeNull();
        tagsPagedList.Page.Should().Be(2);
        tagsPagedList.PageSize.Should().Be(10);
        tagsPagedList.Items.Should().HaveCount(0);
        tagsPagedList.TotalCount.Should().Be(0);
    }

    [Fact]
    public async Task Should_Return_Population_Page_Sorted_Ascending_By_Name()
    {
        // Arrange
        var tags = new List<Tag>();
        
        for (var i = 0; i < 1000; i++)
        {
            tags.Add(new Tag($"Tag{i}", i));
        }
        
        var population = new Population(tags);
        await TestDbContext.Populations.AddAsync(population);
        await TestDbContext.SaveChangesAsync();
        
        // Act
        var response = await Client.GetAsync($"/api/population?page=1&pageSize=10&sort=name&order=asc");
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        
        // Assert
        var tagsPagedList = await response.Content.ReadFromJsonAsync<PopulationListResponse>();
        
        tagsPagedList.Items.Should().BeInAscendingOrder(tag => tag.Name);
    }
    
    [Fact]
    public async Task Should_Return_Population_Page_Sorted_Descending_By_Name()
    {
        // Arrange
        var tags = new List<Tag>();
        
        for (var i = 0; i < 1000; i++)
        {
            tags.Add(new Tag($"Tag{i}", i));
        }
        
        var population = new Population(tags);
        await TestDbContext.Populations.AddAsync(population);
        await TestDbContext.SaveChangesAsync();
        
        // Act
        var response = await Client.GetAsync($"/api/population?page=1&pageSize=10&sort=name&order=desc");
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        
        // Assert
        var tagsPagedList = await response.Content.ReadFromJsonAsync<PopulationListResponse>();
        
        tagsPagedList.Items.Should().BeInDescendingOrder(tag => tag.Name);
    }
    
    [Fact]
    public async Task Should_Return_Population_Page_Sorted_Ascending_By_Share()
    {
        // Arrange
        var tags = new List<Tag>();
        
        for (var i = 0; i < 1000; i++)
        {
            tags.Add(new Tag($"Tag{i}", i));
        }
        
        var population = new Population(tags);
        await TestDbContext.Populations.AddAsync(population);
        await TestDbContext.SaveChangesAsync();
        
        // Act
        var response = await Client.GetAsync($"/api/population?page=1&pageSize=10&sort=share&order=asc");
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        
        // Assert
        var tagsPagedList = await response.Content.ReadFromJsonAsync<PopulationListResponse>();
        
        tagsPagedList.Items.Should().BeInAscendingOrder(tag => tag.Share);
    }
    
    [Fact]
    public async Task Should_Return_Population_Page_Sorted_Descending_By_Share()
    {
        // Arrange
        var tags = new List<Tag>();
        
        for (var i = 0; i < 1000; i++)
        {
            tags.Add(new Tag($"Tag{i}", i));
        }
        
        var population = new Population(tags);
        await TestDbContext.Populations.AddAsync(population);
        await TestDbContext.SaveChangesAsync();
        
        // Act
        var response = await Client.GetAsync($"/api/population?page=1&pageSize=10&sort=share&order=desc");
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        
        // Assert
        var tagsPagedList = await response.Content.ReadFromJsonAsync<PopulationListResponse>();
        
        tagsPagedList.Items.Should().BeInDescendingOrder(tag => tag.Share);
    }

    protected override Action<IServiceCollection>? ConfigureServices => services =>
    {
        services.AddScoped<ITagExtractor, TestTagExtractor>();
    };
}