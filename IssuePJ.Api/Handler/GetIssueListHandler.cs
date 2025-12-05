using System;
using IssuePj.Api.Entities;
using IssuePJ.Api.Command;
using IssuePJ.Api.Context;
using IssuePJ.Api.Response;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;

namespace IssuePJ.Api.Handler;

public class GetIssueListHandler
{

    private readonly ApplicationContext _context;
    public GetIssueListHandler(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<ResponseModel<IndexModel<Issue>>> Handle(GetIssueListCommand command)
    {
        var start = (command.Page - 1) * command.PageSize;
        var issues = await _context.Issues.AsNoTracking().OrderByDescending(i=> i.CreatedAt).Skip(start).Take(command.PageSize).ToListAsync();
        return new ResponseModel<IndexModel<Issue>>
        {
            Status = "200",
            Message = "Issues retrieved successfully",
            Data = new IndexModel<Issue>
            {
                Items = issues,
                TotalCount = await _context.Issues.CountAsync(),
                Page = command.Page,
                PageSize = command.PageSize
            }
        };
    }
}
