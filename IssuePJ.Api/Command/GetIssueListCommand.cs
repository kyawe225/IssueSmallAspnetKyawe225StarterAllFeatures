namespace IssuePJ.Api.Command;

public class GetIssueListCommand
{
    public int Page { set; get; }
    public int PageSize { set; get; }

    public GetIssueListCommand()
    {

    }
    
    public GetIssueListCommand(int page, int pageSize)
    {
        Page = page;
        PageSize = pageSize;
    }
}