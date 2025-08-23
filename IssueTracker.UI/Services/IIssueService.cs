using IssueTracker.UI.Models;

namespace IssueTracker.UI.Services;

public interface IIssueService
{
    Task<ResponseModel<IndexModel<Issue>>> GetIssuesAsync(int page = 1, int pageSize = 10);
    Task<ResponseModel<Issue?>> GetIssueByIdAsync(string id);
    Task<ResponseModel<string>> CreateIssueAsync(CreateIssueRequest request);
    Task<ResponseModel<string>> DeleteIssueAsync(string id);
}