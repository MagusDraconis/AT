using AT.Book.Domain;

namespace AT.Book.Services.Calculations;

/// <summary>Aggregates every calculation service and resolves calculations by id.</summary>
public sealed class CalculationCatalog
{
    private readonly IReadOnlyDictionary<string, CalculationResult> _byId;

    public IReadOnlyList<CalculationResult> All { get; }

    public CalculationCatalog(
        SpectrumService spectrum,
        OccupancyService occupancy,
        InformationService information,
        CosmologyService cosmology,
        PhysicsService physics,
        QuantumService quantum)
    {
        ICalculationService[] services = [spectrum, occupancy, information, cosmology, physics, quantum];
        All = services.SelectMany(s => s.Results).ToArray();
        _byId = All.ToDictionary(r => r.Id, StringComparer.OrdinalIgnoreCase);
    }

    public CalculationResult? Get(string? id) => id is null ? null : _byId.GetValueOrDefault(id);
}
