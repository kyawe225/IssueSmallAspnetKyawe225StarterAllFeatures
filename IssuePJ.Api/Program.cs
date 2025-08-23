using IssuePj.Api.Entities;
using IssuePJ.Api.Command;
using IssuePJ.Api.Context;
using IssuePJ.Api.Exceptions;
using IssuePJ.Api.Response;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wolverine;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddWolverine(x =>
{
    x.Durability.Mode = DurabilityMode.MediatorOnly;
});

builder.Services.AddDbContext<ApplicationContext>(p=> p.UseNpgsql(builder?.Configuration?.GetConnectionString("ConnectionString")));

builder.Services.AddExceptionHandler<NotFoundExceptionHandler>();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseExceptionHandler(_ => { });

app.MapPost("/issues", async ([FromBody] CreateIssueCommand command, IMessageBus messageBus) =>
{
    await messageBus.InvokeAsync(command);
    return Results.Ok("Created Issue Successfully!");
});

app.MapGet("/issues/{Id}", async (string Id, IMessageBus messageBus) =>
{
    Issue issue= await messageBus.InvokeAsync<Issue>(new GetIssueCommand(Id));
    return Results.Ok(issue);
});

app.MapGet("/issues", async (IMessageBus messageBus,[FromQuery] int? page , [FromQuery] int? pageSize ) =>
{
    IndexModel<Issue> issue = await messageBus.InvokeAsync<IndexModel<Issue>>(new GetIssueListCommand(page ?? 1,pageSize ?? 2));
    return Results.Ok(issue);
});

app.MapDelete("/issues/{Id}", async (string Id, IMessageBus messageBus) =>
{
    await messageBus.InvokeAsync(new DeleteIssueCommand(Id));
    return Results.Ok("Delete Successfully!");
});


app.Run();
