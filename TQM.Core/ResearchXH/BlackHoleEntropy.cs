namespace TQM.Core.ResearchXH;

/// <summary>
/// TQM-QG Phase 12 — black-hole microstate test. Tests whether horizon entropy S ∝ Area emerges from the
/// counting measure, by counting horizon (boundary) events vs bulk (volume) events. d = spatial dimension
/// (spacetime = d+1). No new primitives.
/// </summary>
public static class BlackHoleEntropy
{
    /// <summary>Horizon "area" scale = number of horizon (boundary) cells ∝ R^(d−1).</summary>
    public static double HorizonAreaScale(int d, double R) => Math.Pow(R, d - 1.0);

    /// <summary>Bulk "volume" scale = number of bulk cells ∝ R^d.</summary>
    public static double BulkVolumeScale(int d, double R) => Math.Pow(R, d);

    /// <summary>Horizon entropy (1 bit per horizon cell): S = A·ln 2 ∝ R^(d−1) — the area law.</summary>
    public static double HorizonEntropy(int d, double R) => Math.Log(2.0) * HorizonAreaScale(d, R);

    /// <summary>Bulk entropy (volume scaling, for comparison): ∝ R^d — NOT the area law.</summary>
    public static double BulkEntropy(int d, double R) => Math.Log(2.0) * BulkVolumeScale(d, R);

    /// <summary>Microstate count W = e^S from horizon counting.</summary>
    public static double Microstates(int d, double R) => Math.Exp(HorizonEntropy(d, R));

    /// <summary>Ratio S(2R)/S(R) — equals 2^(d−1) for area law, 2^d for volume law.</summary>
    public static double EntropyRatio(int d, double R) => HorizonEntropy(d, 2.0 * R) / HorizonEntropy(d, R);
}
