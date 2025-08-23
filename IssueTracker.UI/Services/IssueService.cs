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

    public async Task<ResponseModel<IndexModel<Issue>>> GetIssuesAsync(int page = 1, int pageSize = 10)
    {
        try
        {
            var response = await _httpClient.GetAsync($"/issues?page={page}&pageSize={pageSize}");
            response.EnsureSuccessStatusCode();
            
            var result = await response.Content.ReadFromJsonAsync<ResponseModel<IndexModel<Issue>>>(_jsonOptions);
            return result ?? new ResponseModel<IndexModel<Issue>>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching issues: {ex.Message}");
            return new ResponseModel<IndexModel<Issue>>();
        }
    }

    public async Task<ResponseModel<Issue?>> GetIssueByIdAsync(string id)
    {
        try
        {
            var response = await _httpClient.GetAsync($"/issues/{id}");
            response.EnsureSuccessStatusCode();
            
            return await response.Content.ReadFromJsonAsync<ResponseModel<Issue>>(_jsonOptions);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching issue {id}: {ex.Message}");
            return new ResponseModel<Issue>();
        }
    }

    public async Task<ResponseModel<string>> CreateIssueAsync(CreateIssueRequest request)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("/issues", request);
            response.EnsureSuccessStatusCode();
            
            var result = await response.Content.ReadFromJsonAsync<ResponseModel<string>>(_jsonOptions);
            return result ?? new ResponseModel<string>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error creating issue: {ex.Message}");
            return new ResponseModel<string>();
        }
    }

    public async Task<ResponseModel<string>> DeleteIssueAsync(string id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"/issues/{id}");
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<ResponseModel<string>>(_jsonOptions);
            return result ?? new ResponseModel<string>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting issue {id}: {ex.Message}");
            return new ResponseModel<string>();
        }
    }
}