using System;
using IssuePj.Api.Entities;
using IssuePJ.Api.Command;
using IssuePJ.Api.Context;
using Microsoft.EntityFrameworkCore;

namespace IssuePJ.Api.Handler;

public class GetIssueListHandler
{

    private readonly ApplicationContext _context;
    public GetIssueListHandler(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<List<Issue>> Handle(GetIssueListCommand command)
    {
        return await _context.Issues.AsNoTracking().ToListAsync();
    }
}
