using System.Globalization;
using AT.Book.Domain;

namespace AT.Book.Services.Calculations;

/// <summary>
/// Information content: I_occ = KL(ρ‖uniform) = 0.7513 nats over the octave record,
/// and the cosmological density fractions ΩΛ = I_occ/ln K, Ωm = 1 − ΩΛ (K = 3 octaves).
/// </summary>
public sealed class InformationService : ICalculationService
{
    public static readonly double LnK = Math.Log(3.0); // K = 3 octave bands

    public string Name => "Information";

    public IReadOnlyList<CalculationResult> Results { get; }

    public InformationService()
    {
        var occ = OccupancyService.OctaveRecord();
        double total = occ.Sum();
        double iocc = occ.Sum(c => (c / total) * Math.Log(3.0 * c / total));
        double omegaLambda = iocc / LnK;
        double omegaMatter = 1 - omegaLambda;
        Results =
        [
            new(
                "iocc",
                "Information Content I_occ (KL divergence)",
                "I_occ = KL(ρ ‖ uniform) = Σ ρ_i · ln(ρ_i / (1/K)),  ρ = [4,4,87]/95, K = 3",
                [
                    new("ρ", $"[{occ[0]},{occ[1]},{occ[2]}] / {total}", "octave occupancy distribution"),
                    new("I_occ", iocc.ToString("0.0000", CultureInfo.InvariantCulture), "nats"),
                ],
                "I_occ = 0.7513 nats is a DERIVED order parameter measuring how non-uniform the D96 occupancy is (QG_228)."),
            new(
                "omegalambda",
                "ΩΛ = I_occ / ln K",
                "ΩΛ = I_occ / ln K,  Ωm = 1 − ΩΛ,  K = 3",
                [
                    new("I_occ", iocc.ToString("0.0000", CultureInfo.InvariantCulture)),
                    new("ln K", LnK.ToString("0.0000", CultureInfo.InvariantCulture), "K = 3 octave bands"),
                    new("ΩΛ", omegaLambda.ToString("0.0000", CultureInfo.InvariantCulture), "dark-energy fraction"),
                    new("Ωm", omegaMatter.ToString("0.0000", CultureInfo.InvariantCulture), "matter fraction"),
                ],
                "ΩΛ = 0.6839 matches the observed dark-energy fraction to 0.12% (QG_234)."),
        ];
    }

    public static double InformationContent(int[] octaveRecord)
    {
        double total = octaveRecord.Sum();
        return octaveRecord.Sum(c => (c / total) * Math.Log(3.0 * c / total));
    }
}
