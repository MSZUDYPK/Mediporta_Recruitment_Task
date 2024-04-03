using Mediporta.Application.Commands;
using Mediporta.Application.Models;
using Mediporta.Application.Queries;
using Mediporta.Application.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Mediporta.Api.Endpoints;

internal static class PopulationEndpoints
{
    public static IEndpointRouteBuilder UsePopulationEndpoints(this IEndpointRouteBuilder app)
    {
        var populationEndpoints = app.MapGroup("api/population");

        populationEndpoints.MapGet("", async (int page, int pageSize, string? sort, string? order,
                [FromServices] IQueryHandler<BrowseLastPopulationQuery, PagedList<SimplifiedTagDto>> queryHandler) =>
            {
                var query = new BrowseLastPopulationQuery(page, pageSize, sort, order);

                var result = await queryHandler.HandleAsync(query);

                return Results.Ok(result);
            })
            .Produces<PagedList<SimplifiedTagDto>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .WithName("Get Last Population");

        populationEndpoints.MapPost("", async (string[]? missingTags,
                [FromServices] ICommandHandler<PullTagsCommand> commandHandler) =>
            {
                var request = new PullTagsCommand(missingTags);

                await commandHandler.HandleAsync(request);

                return Results.Ok();
            })
            .Produces(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .WithName("Pull Tags");

        return populationEndpoints;
    }
}
