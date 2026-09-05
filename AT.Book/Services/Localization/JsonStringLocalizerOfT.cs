using Microsoft.Extensions.Localization;

namespace AT.Book.Services.Localization;

/// <summary>The typed wrapper used by <c>@inject IStringLocalizer&lt;T&gt;</c> in components.</summary>
public sealed class JsonStringLocalizer<T> : IStringLocalizer<T>
{
    private readonly JsonStringLocalizer _inner;

    public JsonStringLocalizer(JsonStringLocalizer inner)
    {
        _inner = inner;
    }

    public LocalizedString this[string name] => _inner[name];

    public LocalizedString this[string name, params object[] arguments] => _inner[name, arguments];

    public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures) => _inner.GetAllStrings(includeParentCultures);
}
