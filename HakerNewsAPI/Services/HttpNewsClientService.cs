using HakerNewsAPI.Helper;
using HakerNewsAPI.Models;

namespace HakerNewsAPI.Services;

public class HttpNewsClientService
{
    private readonly HttpClient _httpClient;

    public HttpNewsClientService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<List<int>> GetBestStoryIdsAsync()
    {

        var response = await _httpClient.GetFromJsonAsync<List<int>>($"{Constans.BaseUrl}beststories.json");
        var storyIds = response ?? new List<int>();
    
        return storyIds;
    }

    public async Task<StoryDetail> GetStoryDetailAsync(int storyId)
    {
        var storyDetail = await _httpClient.GetFromJsonAsync<StoryDetail>($"{Constans.BaseUrl}item/{storyId}.json");

        return storyDetail ?? new StoryDetail();
    }
}