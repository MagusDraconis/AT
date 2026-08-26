using System.Globalization;
using System.Text;

namespace AT.Core.ResearchQG;

/// <summary>
/// Hostile audit of the upper bound N≤3 (why no N≥4), under the constraints: no new primitives,
/// no anthropics, no numerology, no hidden dimensions, no post-selection. Tests five derivation
/// routes: stability, anomaly cancellation, representation theory, defect saturation, information
/// capacity.
/// </summary>
public static class UpperBoundThreeAnalyzer
{
    /// <summary>Asymptotic-freedom bound: N_f < 11·N_c/2; N_gen = N_f/2 (2 quark flavors per generation).</summary>
    public static double MaxGenerationsAsymptoticFreedom(int nColors = 3)
        => Math.Floor(11.0 * nColors / 2.0 / 2.0);

    /// <summary>CP phases in N×N mixing: (N-1)(N-2)/2.</summary>
    public static double CPPhases(int n) => (n - 1.0) * (n - 2.0) / 2.0;

    /// <summary>The five derivation routes and their verdicts.</summary>
    public static (string Route, string Argument, string Verdict)[] Routes() => new[]
    {
        ("Stability", "defect excitation cutoff → 3 observable (X051, 5/6 models); Higgs quartic λ→negative with heavy 4th gen",
            "WEAK/partial: model-dependent (5/6 models; λ-running is quantitative, not a theorem)"),
        ("Anomaly cancellation", "each generation is SELF-anomaly-free (its own hypercharges cancel); replicating does not re-introduce anomalies",
            "FAILS: anomaly cancellation is per-generation, bounds the REPRESENTATION not the MULTIPLICITY"),
        ("Representation theory", "any number of copies of a chiral multiplet is a valid anomaly-free representation",
            "FAILS: no representation-theoretic bound on N; N copies always allowed"),
        ("Defect saturation", "no theorem fixes how many defect excitation levels survive; n=3 confinement is assumed, not derived",
            "FAILS: no saturation principle; same underived 3"),
        ("Information capacity", "no AT argument bounds generation count by moduli-space information content",
            "FAILS: no such argument exists"),
    };

    /// <summary>The classification of N≥4 and the empirical facts.</summary>
    public static string EmpiricalFacts()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"  Asymptotic freedom: N_f < 11·N_c/2 = 16.5 → up to {MaxGenerationsAsymptoticFreedom()} generations allowed.");
        sb.AppendLine("  Z-width: N_ν = 3 LIGHT neutrinos (m < M_Z/2) — a heavy 4th neutrino is NOT counted.");
        sb.AppendLine("  Higgs production: excludes a 4th SM-like generation up to ~TeV (O(1) Yukawa couplings).");
        return sb.ToString();
    }
}
