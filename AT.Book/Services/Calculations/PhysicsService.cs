using System.Globalization;
using AT.Book.Domain;

namespace AT.Book.Services.Calculations;

/// <summary>
/// Physics scale content: the spectral triple A = Σm·#g·occ₂ = 95·44·87, the weak
/// anchor v = 137·ln(span) = 254.37 GeV, and the Planck content M_Pl = v·A³.
/// </summary>
public sealed class PhysicsService : ICalculationService
{
    public const double V = 254.37;                 // GeV, v = 137·ln(span)
    public const double A = 95.0 * 44.0 * 87.0;    // 363,660

    public string Name => "Physics";

    public IReadOnlyList<CalculationResult> Results { get; }

    public PhysicsService()
    {
        var modes = SpectrumService.Modes(SpectrumService.N);
        double w1 = modes[0];
        double span = modes[^1] / w1;
        double v = 137.0 * Math.Log(span);
        double a3 = A * A * A;
        double mPl = v * a3;
        Results =
        [
            new(
                "planck-scale",
                "Planck Content M_Pl = v · A³",
                "A = Σm·#g·occ₂ = 95·44·87,  v = 137·ln(span),  M_Pl = v·A³",
                [
                    new("Σm", "95", "mode count"),
                    new("#g", "44", "distinct frequencies"),
                    new("occ₂", "87", "top-octave modes"),
                    new("A", A.ToString("0", CultureInfo.InvariantCulture), "95·44·87"),
                    new("span", span.ToString("0.0000", CultureInfo.InvariantCulture)),
                    new("v", v.ToString("0.00", CultureInfo.InvariantCulture), "137·ln(span), GeV"),
                    new("M_Pl", mPl.ToString("0.0000e0", CultureInfo.InvariantCulture), "v·A³, GeV"),
                ],
                "M_Pl = 1.2234e19 GeV is a DERIVED ratio of the single D96 ring's spectral counts (QG_181/183)."),
        ];
    }
}
