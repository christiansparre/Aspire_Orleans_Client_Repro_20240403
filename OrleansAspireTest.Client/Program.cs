using OrleansAspireTest.Client.Components;
using System.Collections;

if (Environment.GetEnvironmentVariable("USE_ORLEANS_CLIENT_HACK") is not null)
{
    // Hack to get around issue where UseOrleansClient thinks it needs to configure grain state providers...
    var environmentVariables = Environment.GetEnvironmentVariables();
    foreach (DictionaryEntry dictionaryEntry in environmentVariables)
    {
        if (dictionaryEntry.Key.ToString()!.StartsWith("Orleans__GrainStorage__Default"))
        {
            Environment.SetEnvironmentVariable(dictionaryEntry.Key.ToString()!, null);
        }
    }
}

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.AddKeyedAzureTableService("clustering");
builder.UseOrleansClient();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
