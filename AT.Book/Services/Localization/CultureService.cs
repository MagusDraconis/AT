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

    public bool IsEnglish => Culture == "en";

    public CultureInfo CultureInfo => new(Culture);

    public CultureService(IHttpContextAccessor http)
    {
        _http = http;
        var cookie = http.HttpContext?.Request.Cookies[CookieName];
        if (cookie is "en" or "de")
            Culture = cookie;
    }

    public void Set(string culture)
    {
        Culture = culture is "de" ? "de" : "en";
        _http.HttpContext?.Response.Cookies.Append(CookieName, Culture, new CookieOptions
        {
            Path = "/",
            Expires = DateTimeOffset.UtcNow.AddYears(1),
            HttpOnly = true,
            SameSite = SameSiteMode.Lax,
        });
        var ci = CultureInfo;
        CultureInfo.CurrentCulture = ci;
        CultureInfo.CurrentUICulture = ci;
        CultureInfo.DefaultThreadCurrentCulture = ci;
        CultureInfo.DefaultThreadCurrentUICulture = ci;
    }
}
