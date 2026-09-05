using AT.Book.Components;
using AT.Book.Data;
using AT.Book.Exports;
using AT.Book.Services;
using AT.Book.Services.Calculations;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddMudServices();

// Theory registries and graph (singletons — the theory is fixed at startup).
builder.Services.AddSingleton<TheoryRegistry>();
builder.Services.AddSingleton<TheoryGraphService>();
builder.Services.AddSingleton<ExportService>();

// Executable calculation services (independent of the UI).
builder.Services.AddSingleton<SpectrumService>();
builder.Services.AddSingleton<OccupancyService>();
builder.Services.AddSingleton<InformationService>();
builder.Services.AddSingleton<CosmologyService>();
builder.Services.AddSingleton<PhysicsService>();
builder.Services.AddSingleton<QuantumService>();
builder.Services.AddSingleton<CalculationCatalog>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
