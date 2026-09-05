using System.Globalization;
using AT.Book.Components;
using AT.Book.Data;
using AT.Book.Exports;
using AT.Book.Services;
using AT.Book.Services.Calculations;
using AT.Book.Services.Localization;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Localization;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddMudServices();
builder.Services.AddHttpContextAccessor();

// ── Localization: reader-facing text comes from /Content/{culture}/*.json ──
builder.Services.AddSingleton<LocalizationStore>();
builder.Services.AddScoped<CultureService>();
builder.Services.AddSingleton<IStringLocalizerFactory, JsonStringLocalizerFactory>();
builder.Services.AddLocalization();

// ── The book: chapter registry + the theory engine ─────────────────────────
builder.Services.AddSingleton<ChapterRegistry>();
builder.Services.AddSingleton<TheoryRegistry>();
builder.Services.AddSingleton<TheoryGraphService>();
builder.Services.AddSingleton<ExportService>();
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

// ── Culture routing: /en/... and /de/... ──────────────────────────────────
var supportedCultures = new[] { new CultureInfo("en"), new CultureInfo("de") };
app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("en"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures,
    RequestCultureProviders =
    [
        new RouteRequestCultureProvider(),
    ],
});

// Flow the resolved culture onto the ambient thread culture (so interactive
// server renders see it — Blazor Server needs the Default* cultures set too).
app.Use(async (ctx, next) =>
{
    var culture = ctx.Features.Get<IRequestCultureFeature>()?.RequestCulture?.UICulture;
    if (culture is not null)
    {
        CultureInfo.CurrentCulture = culture;
        CultureInfo.CurrentUICulture = culture;
        CultureInfo.DefaultThreadCurrentCulture = culture;
        CultureInfo.DefaultThreadCurrentUICulture = culture;
    }
    await next();
});

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();

/// <summary>Reads the culture from the first URL segment (a supported language code).</summary>
sealed class RouteRequestCultureProvider : RequestCultureProvider
{
    public override Task<ProviderCultureResult?> DetermineProviderCultureResult(HttpContext httpContext)
    {
        var segment = httpContext.Request.Path.Value?
            .Split('/', StringSplitOptions.RemoveEmptyEntries)
            .FirstOrDefault();

        return LanguageCatalog.IsSupported(segment)
            ? Task.FromResult<ProviderCultureResult?>(new ProviderCultureResult(segment!))
            : Task.FromResult<ProviderCultureResult?>(null);
    }
}
