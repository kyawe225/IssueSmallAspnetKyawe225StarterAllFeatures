using IssueTracker.UI.Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace IssueTracker.UI.Services;

public class IssueService : IIssueService
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonOptions;

    public IssueService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
    }

    public async Task<IndexModel<Issue>> GetIssuesAsync(int page = 1, int pageSize = 10)
    {
        try
        {
            var response = await _httpClient.GetAsync($"/issues?page={page}&pageSize={pageSize}");
            response.EnsureSuccessStatusCode();
            
            var result = await response.Content.ReadFromJsonAsync<IndexModel<Issue>>(_jsonOptions);
            return result ?? new IndexModel<Issue>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching issues: {ex.Message}");
            return new IndexModel<Issue>();
        }
    }

    public async Task<Issue?> GetIssueByIdAsync(string id)
    {
        try
        {
            var response = await _httpClient.GetAsync($"/issues/{id}");
            response.EnsureSuccessStatusCode();
            
            return await response.Content.ReadFromJsonAsync<Issue>(_jsonOptions);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching issue {id}: {ex.Message}");
            return null;
        }
    }

    public async Task<string> CreateIssueAsync(CreateIssueRequest request)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("/issues", request);
            response.EnsureSuccessStatusCode();
            
            return await response.Content.ReadAsStringAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error creating issue: {ex.Message}");
            return $"Error: {ex.Message}";
        }
    }

    public async Task<string> DeleteIssueAsync(string id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"/issues/{id}");
            response.EnsureSuccessStatusCode();
            
            return await response.Content.ReadAsStringAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting issue {id}: {ex.Message}");
            return $"Error: {ex.Message}";
        }
    }
}