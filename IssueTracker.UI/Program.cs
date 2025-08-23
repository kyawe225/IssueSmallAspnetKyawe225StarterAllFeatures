using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using IssueTracker.UI;
using IssueTracker.UI.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5088/") });
builder.Services.AddScoped<IIssueService, IssueService>();
builder.Services.AddSingleton<ILoadingService, LoadingService>();

await builder.Build().RunAsync();
