using Carter;
using HakerNewsAPI.Models;
using HakerNewsAPI.Services;
using Microsoft.AspNetCore.Http.HttpResults;

namespace HakerNewsAPI.Controllers;

public class BestStoriesEndPoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/stories");
        
        group.MapGet("best/{number:int:min(1)}", GetBestStories).WithName(nameof(GetBestStories));    
    }

    public static async Task<Results<Ok<List<StoryDetail>>, NotFound<string>>> GetBestStories(int number, HackerNewsService hackerNewsService)
    {
        try
        {
            var bestStoryIds = await hackerNewsService.GetAllStoriesDetailsAsync(number);

            return TypedResults.Ok(bestStoryIds);
        }
        catch (Exception ex)
        {
            return TypedResults.NotFound(ex.Message);
        }
    }
}
