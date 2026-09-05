using System.Globalization;

namespace AT.Book.Services.Localization;

/// <summary>
/// Scoped per-circuit culture state: the language a reader is using, sourced from the
/// URL segment ({Culture}) and persisted in a cookie so "/" can redirect correctly.
/// </summary>
public sealed class CultureService
{
    private const string CookieName = "atbook.culture";

    private readonly IHttpContextAccessor _http;

    public string Culture { get; private set; } = "en";

    public CultureInfo CultureInfo => new(Culture);

    public CultureService(IHttpContextAccessor http)
    {
        _http = http;
        var cookie = http.HttpContext?.Request.Cookies[CookieName];
        if (LanguageCatalog.IsSupported(cookie))
            Culture = cookie!;
    }

    public void Set(string? culture)
    {
        Culture = LanguageCatalog.IsSupported(culture) ? culture! : LanguageCatalog.DefaultCode;

        // The cookie may only be written while the response headers are still mutable
        // (i.e. during the initial server render). During interactive re-renders the
        // response has already started, so the cookie write must be skipped.
        var ctx = _http.HttpContext;
        if (ctx is not null && !ctx.Response.HasStarted)
        {
            ctx.Response.Cookies.Append(CookieName, Culture, new CookieOptions
            {
                Path = "/",
                Expires = DateTimeOffset.UtcNow.AddYears(1),
                HttpOnly = true,
                SameSite = SameSiteMode.Lax,
            });
        }

        var ci = CultureInfo;
        CultureInfo.CurrentCulture = ci;
        CultureInfo.CurrentUICulture = ci;
        CultureInfo.DefaultThreadCurrentCulture = ci;
        CultureInfo.DefaultThreadCurrentUICulture = ci;
    }
}
