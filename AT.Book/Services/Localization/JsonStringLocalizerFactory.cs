using Microsoft.Extensions.Localization;

namespace AT.Book.Services.Localization;

/// <summary>Produces <see cref="JsonStringLocalizer"/> instances for any T.</summary>
public sealed class JsonStringLocalizerFactory : IStringLocalizerFactory
{
    private readonly LocalizationStore _store;

    public JsonStringLocalizerFactory(LocalizationStore store)
    {
        _store = store;
    }

    public IStringLocalizer Create(Type resourceSource) => new JsonStringLocalizer(_store);

    public IStringLocalizer Create(string baseName, string location) => new JsonStringLocalizer(_store);
}
