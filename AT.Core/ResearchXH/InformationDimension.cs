namespace AT.Core.ResearchXH;

/// <summary>
/// AT-QG Phase 10 — information-theoretic dimension selection. Measures how much information an actualization
/// support of dimension d can carry (information capacity, entropy density, causal connectivity, propagation
/// efficiency, geometry complexity), and whether any dimension maximizes information efficiency.
/// d = spatial dimension (spacetime = d+1). No new primitives.
/// </summary>
public static class InformationDimension
{
    /// <summary>Information capacity = independent metric components = (d+1)(d+2)/2 (monotonic increasing).</summary>
    public static double InformationCapacity(int d) => (d + 1.0) * (d + 2.0) / 2.0;

    /// <summary>Entropy density per dimension = (ln d + ln K)/d (monotonic decreasing in d).</summary>
    public static double EntropyDensity(int d, int K = 8) => (Math.Log(d) + Math.Log(K)) / d;

    /// <summary>Causal connectivity = number of events in a causal interval of fixed height ∝ λ^d (monotonic increasing).</summary>
    public static double CausalConnectivity(int d, double lambda = 2.0) => Math.Pow(lambda, d);

    /// <summary>Reach = events in a ball of radius R ∝ R^d (monotonic increasing in d).</summary>
    public static double Reach(double R, int d) => Math.Pow(R, d);

    /// <summary>Intensity = dilution over the (d−1)-sphere ∝ R^(−(d−1)) (monotonic decreasing in d).</summary>
    public static double Intensity(double R, int d) => Math.Pow(R, -(d - 1.0));

    /// <summary>Propagation efficiency = reach × intensity = R^d · R^(−(d−1)) = R — DIMENSION-INDEPENDENT.</summary>
    public static double PropagationEfficiency(double R, int d) => Reach(R, d) * Intensity(R, d);

    /// <summary>Geometry complexity = Weyl components of the (d+1)-dim spacetime (0 for d≤2, monotonic increasing for d≥3).</summary>
    public static double GeometryComplexity(int d) => DimensionAnalysis.WeylComponents(d);

    /// <summary>Information efficiency = useful/(useful + frozen) = 1/(1 + graviton) (max 1 at d=2, then d=3 among allowed).</summary>
    public static double InformationEfficiency(int d) => EffectiveDimension.ConformalEfficiency(d);
}
