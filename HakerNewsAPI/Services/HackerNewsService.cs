using HakerNewsAPI.Helper;
using HakerNewsAPI.Models;
using Microsoft.Extensions.Caching.Memory;

namespace HakerNewsAPI.Services;

public class HackerNewsService
{
    private readonly HttpNewsClientService _httpNewsClient;
    private readonly IMemoryCache _cache;

    public HackerNewsService(HttpNewsClientService httpNewsClient, IMemoryCache cache)
    {
        _httpNewsClient = httpNewsClient;
        _cache = cache;
    }

    public async Task<List<int>> GetBestStoryIdsAsync()
    {
        if (_cache.TryGetValue<List<int>>(Constans.BestStoryIds, out var cachedStoryIds))
        {
            return cachedStoryIds ?? new List<int>();
        }

        var response = await _httpNewsClient.GetBestStoryIdsAsync();
        var storyIds = response ?? new List<int>();

        // Cache the story IDs for a certain duration 5 minutes in this case
        _cache.Set(Constans.BestStoryIds, storyIds, TimeSpan.FromMinutes(5));

        return storyIds;
    }

    public async Task<StoryDetail> GetStoryDetailAsync(int storyId) 
    {
        if (_cache.TryGetValue<StoryDetail>(storyId, out var cachedStory))
        {
            return cachedStory ?? new StoryDetail();
        }

        var storyDetail = await _httpNewsClient.GetStoryDetailAsync(storyId);

        _cache.Set(storyId, storyDetail, TimeSpan.FromMinutes(1));

        return storyDetail;
    }

    public async Task<List<StoryDetail>> GetAllStoriesDetailsAsync(int number)
    {
        var bestStoryIds = await GetBestStoryIdsAsync();
        if (!bestStoryIds.Any())
        {
            return  new List<StoryDetail>();
        }

        var storyDetails = new List<StoryDetail>();

        foreach (var storyId in bestStoryIds.Take(number))
        {
            var storyDetail = await GetStoryDetailAsync(storyId);
            storyDetails.Add(storyDetail);
        }

        storyDetails = storyDetails.OrderByDescending(s => s.Score).ToList();

        return storyDetails;
    }
}
