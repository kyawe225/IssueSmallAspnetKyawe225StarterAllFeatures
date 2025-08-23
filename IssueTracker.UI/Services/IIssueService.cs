using IssueTracker.UI.Models;

namespace IssueTracker.UI.Services;

public interface IIssueService
{
    Task<IndexModel<Issue>> GetIssuesAsync(int page = 1, int pageSize = 10);
    Task<Issue?> GetIssueByIdAsync(string id);
    Task<string> CreateIssueAsync(CreateIssueRequest request);
    Task<string> DeleteIssueAsync(string id);
}