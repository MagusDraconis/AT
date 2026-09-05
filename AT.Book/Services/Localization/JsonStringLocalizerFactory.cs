using Microsoft.Extensions.Localization;

namespace AT.Book.Services.Localization;

/// <summary>
/// Produces <see cref="JsonStringLocalizer"/> instances for any T. Scoped (not singleton)
/// so it may depend on the scoped <see cref="CultureService"/>.
/// </summary>
public sealed class JsonStringLocalizerFactory : IStringLocalizerFactory
{
    private readonly LocalizationStore _store;
    private readonly CultureService _culture;

    public JsonStringLocalizerFactory(LocalizationStore store, CultureService culture)
    {
        _store = store;
        _culture = culture;
    }

    public IStringLocalizer Create(Type resourceSource) => new JsonStringLocalizer(_store, _culture);

    public IStringLocalizer Create(string baseName, string location) => new JsonStringLocalizer(_store, _culture);
}
