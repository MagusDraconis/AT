namespace TQM.Core.ResearchXH;

/// <summary>
/// TQM-QG Phase 5 — derive the observable dimension. Tests whether the support rank of ρ (the number of active,
/// varying directions) is selected by the actualization dynamics, entropy, branching efficiency, density
/// dilution, or information capacity. No new primitives.
/// </summary>
public static class ObservableDimension
{
    /// <summary>Maximum configurational entropy over d active directions × K octaves = ln(d·K) = ln d + ln K
    /// (monotonic increasing in d — no interior maximum).</summary>
    public static double MaxEntropy(int d, int K) => Math.Log(d) + Math.Log(K);

    /// <summary>Density-dilution exponent: the deficit density dilutes as R^(−d) in d spatial dimensions.</summary>
    public static double DilutionExponent(int d) => -d;

    /// <summary>Critical branching ratio for a scale-free density in d dimensions: μ_crit = λ^d
    /// (the branching that exactly compensates the d-dim volume growth λ^(dk) per octave).</summary>
    public static double CriticalBranching(int d, double lambda) => Math.Pow(lambda, d);

    /// <summary>Branching efficiency (inverse cost): 1/μ_crit = λ^(−d) — monotonic decreasing in d.</summary>
    public static double BranchingEfficiency(int d, double lambda) => Math.Pow(lambda, -d);
}
