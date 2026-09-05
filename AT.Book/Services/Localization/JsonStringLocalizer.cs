using System.Globalization;
using Microsoft.Extensions.Localization;

namespace AT.Book.Services.Localization;

/// <summary>
/// An IStringLocalizer backed by the in-memory <see cref="LocalizationStore"/>.
/// The culture is read from the scoped <see cref="CultureService"/> (set from the URL
/// segment), so it is deterministic per circuit and immune to Blazor Server's
/// ambient-thread-culture pitfalls.
/// </summary>
public sealed class JsonStringLocalizer : IStringLocalizer
{
    private readonly LocalizationStore _store;
    private readonly CultureService _culture;

    public JsonStringLocalizer(LocalizationStore store, CultureService culture)
    {
        _store = store;
        _culture = culture;
    }

    public LocalizedString this[string name] => new(name, _store.Get(_culture.Culture, name), resourceNotFound: false);

    public LocalizedString this[string name, params object[] arguments]
    {
        get
        {
            var raw = _store.Get(_culture.Culture, name);
            return new LocalizedString(name, string.Format(CultureInfo.InvariantCulture, raw, arguments), resourceNotFound: false);
        }
    }

    public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures) => [];
}
