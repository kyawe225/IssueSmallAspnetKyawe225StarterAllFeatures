using IssuePj.Api.Entities;
using IssuePJ.Api.Command;
using IssuePJ.Api.Context;
using IssuePJ.Api.Exceptions;
using IssuePJ.Api.Resources;
using IssuePJ.Api.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Globalization;
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

builder.Services.AddLocalization(options => options.ResourcesPath ="");

builder.Services.AddExceptionHandler<NotFoundExceptionHandler>();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

var supportedCultures = new[]
{
    new CultureInfo("en"),
    new CultureInfo("fr"),
    new CultureInfo("de"),
    new CultureInfo("my")
};

var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture("fr")
    .AddSupportedCultures(supportedCultures.Select(c => c.Name).ToArray())
    .AddSupportedUICultures(supportedCultures.Select(c => c.Name).ToArray());

app.UseRequestLocalization(localizationOptions);

app.UseHttpsRedirection();

app.UseExceptionHandler(_ => { });

app.UseCors(policy =>
{
    policy.AllowAnyHeader();
    policy.AllowAnyMethod();
    policy.AllowAnyOrigin();
});


app.MapGet("/hello", async (IStringLocalizer<SharedResource> localizer) =>
{
    return Results.Ok(localizer["Hello"].Value);
});

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
