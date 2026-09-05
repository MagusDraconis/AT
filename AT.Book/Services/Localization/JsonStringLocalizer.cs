using System.Globalization;
using Microsoft.Extensions.Localization;

namespace AT.Book.Services.Localization;

/// <summary>
/// An IStringLocalizer backed by the in-memory <see cref="LocalizationStore"/>.
/// The culture is read from the ambient <see cref="CultureInfo.CurrentUICulture"/>
/// (set per request by the localization middleware), so this localizer is safely
/// culture-agnostic and can be cached by a singleton factory.
/// </summary>
public sealed class JsonStringLocalizer : IStringLocalizer
{
    private readonly LocalizationStore _store;

    public JsonStringLocalizer(LocalizationStore store)
    {
        _store = store;
    }

    private static string Culture => CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;

    public LocalizedString this[string name] => new(name, _store.Get(Culture, name), resourceNotFound: false);

    public LocalizedString this[string name, params object[] arguments]
    {
        get
        {
            var raw = _store.Get(Culture, name);
            return new LocalizedString(name, string.Format(CultureInfo.InvariantCulture, raw, arguments), resourceNotFound: false);
        }
    }

    public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures) => [];
}
