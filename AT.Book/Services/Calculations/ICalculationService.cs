using AT.Book.Domain;

namespace AT.Book.Services.Calculations;

/// <summary>A calculation service: an independent, UI-free source of executable derivations.</summary>
public interface ICalculationService
{
    string Name { get; }
    IReadOnlyList<CalculationResult> Results { get; }
}
