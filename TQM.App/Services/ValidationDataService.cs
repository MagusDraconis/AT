using System.Net.Http.Json;
using TQM.App.Models;

namespace TQM.App.Services;

/// <summary>
/// Loads the validation single-source-of-truth JSON files (Docs/TQMQG_PhysicsCoverage.json and
/// Docs/TQMQG_Predictions.json, mirrored into wwwroot/data for the app). Cached per HTTP client;
/// additive-only data, so a once-per-session load is sufficient.
/// </summary>
public class ValidationDataService
{
    private readonly HttpClient _http;
    private static readonly System.Text.Json.JsonSerializerOptions _json = new()
    {
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.SnakeCaseLower,
    };

    private PhysicsCoverageFile? _coverage;
    private PredictionRegistryFile? _registry;
    private PredictionOutcomeFile? _outcomes;

    public ValidationDataService(HttpClient http)
    {
        _http = http;
    }

    public async Task<PhysicsCoverageFile> GetCoverageAsync()
    {
        if (_coverage is null)
            _coverage = await _http.GetFromJsonAsync<PhysicsCoverageFile>("data/TQMQG_PhysicsCoverage.json", _json);
        return _coverage!;
    }

    public async Task<PredictionRegistryFile> GetRegistryAsync()
    {
        if (_registry is null)
            _registry = await _http.GetFromJsonAsync<PredictionRegistryFile>("data/TQMQG_Predictions.json", _json);
        return _registry!;
    }

    public async Task<PredictionOutcomeFile> GetOutcomesAsync()
    {
        if (_outcomes is null)
            _outcomes = await _http.GetFromJsonAsync<PredictionOutcomeFile>("data/TQMQG_PredictionOutcomes.json", _json);
        return _outcomes!;
    }
}
