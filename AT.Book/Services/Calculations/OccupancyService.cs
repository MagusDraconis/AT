using System.Globalization;
using AT.Book.Domain;

namespace AT.Book.Services.Calculations;

/// <summary>
/// Octave occupancy: the [4, 4, 87] record — 4 modes in each of the first two octaves,
/// 87 in the top octave [4ω₁, 8ω₁). Executable from the spectrum.
/// </summary>
public sealed class OccupancyService : ICalculationService
{
    private readonly SpectrumService _spectrum;

    public string Name => "Occupancy";

    public IReadOnlyList<CalculationResult> Results { get; }

    public OccupancyService(SpectrumService spectrum)
    {
        _spectrum = spectrum;
        var modes = SpectrumService.Modes(SpectrumService.N);
        double w1 = modes[0];
        int o1 = modes.Count(w => w < 2 * w1);
        int o2 = modes.Count(w => w >= 2 * w1 && w < 4 * w1);
        int o3 = modes.Count(w => w >= 4 * w1);
        Results =
        [
            new(
                "occupancy",
                "D96 Octave Occupancy [4, 4, 87]",
                "octave_k = #modes with ω ∈ [2^{k−1}ω₁, 2^k ω₁)",
                [
                    new("octave 1 [ω₁, 2ω₁)", o1.ToString(CultureInfo.InvariantCulture)),
                    new("octave 2 [2ω₁, 4ω₁)", o2.ToString(CultureInfo.InvariantCulture)),
                    new("octave 3 [4ω₁, 8ω₁)", o3.ToString(CultureInfo.InvariantCulture)),
                    new("total", (o1 + o2 + o3).ToString(CultureInfo.InvariantCulture), "95 positive modes"),
                ],
                "The D96 occupancy is top-heavy: 87/95 modes crowd the top octave — the anti-thermal direction (NP_028/030)."),
        ];
    }

    /// <summary>The canonical octave record [4, 4, 87].</summary>
    public static int[] OctaveRecord()
    {
        var modes = SpectrumService.Modes(SpectrumService.N);
        double w1 = modes[0];
        return
        [
            modes.Count(w => w < 2 * w1),
            modes.Count(w => w >= 2 * w1 && w < 4 * w1),
            modes.Count(w => w >= 4 * w1),
        ];
    }
}
