using System;
using IssuePj.Api.Entities;
using IssuePJ.Api.Command;
using IssuePJ.Api.Context;
using IssuePJ.Api.Exceptions;
using IssuePJ.Api.Response;
using Microsoft.EntityFrameworkCore;

namespace IssuePJ.Api.Handler;

public class GetIssueHandler
{
    private readonly ApplicationContext _context;
    public GetIssueHandler(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<ResponseModel<Issue>> Handle(GetIssueCommand m)
    {
        var i = await _context.Issues.AsNoTracking().FirstOrDefaultAsync(i => i.Id == m.IssueId);
        if (i == null) throw new NotFoundException("Not found", "Issue");
        return new ResponseModel<Issue>()
        {
            Status = "200",
            Message = "Issue retrieved successfully",
            Data = i
        };
    }
}
