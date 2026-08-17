namespace TQM.Core.ResearchXH;

/// <summary>
/// TQM-QG Phase 8 — the dimension landscape. Profiles each dimension d=1..20 across eight native criteria
/// (Einstein richness, graviton modes, curvature complexity, deficit gravity, rotation curves, entropy
/// production, information density, frozen metric fraction) and classifies each as FORBIDDEN / ALLOWED /
/// PREFERRED. No new primitives.
/// </summary>
public static class DimensionLandscape
{
    /// <summary>
    /// Eight-dimension profile for dimension d:
    /// (richness, graviton, weyl, deficitGravity, rotationCurve, entropy, infoDensity, frozenFraction).
    /// </summary>
    public static (double richness, double graviton, double weyl, double deficitGravity,
                   double rotationCurve, double entropy, double infoDensity, double frozenFraction)
        Profile(int d, double s = 1.0, int K = 8)
    {
        double richness = DimensionAnalysis.EinsteinRichness(d);
        double graviton = DimensionAnalysis.GravitonPolarizations(d);
        double weyl = DimensionAnalysis.WeylComponents(d);
        double deficit = 1.0 / d;                          // geodesic acceleration prefactor ∝ 1/d
        double rotation = Math.Abs(s) / d;                 // flat rotation value v² = |s|/d
        double entropy = ObservableDimension.MaxEntropy(d, K);   // ln d + ln K
        double info = DimensionAnalysis.ComplexityPerDof(d);
        double frozen = DimensionAnalysis.FrozenFraction(d);
        return (richness, graviton, weyl, deficit, rotation, entropy, info, frozen);
    }

    /// <summary>
    /// Dimension classification (d = spatial dimension, spacetime = d+1):
    /// FORBIDDEN  — d≤2 (Einstein tensor degenerate: no gravity);
    /// PREFERRED  — d=3 (3+1: first non-trivial gravity AND minimal propagating, 2 graviton modes);
    /// ALLOWED    — d≥4 (increasing frozen metric fraction).
    /// </summary>
    public static string Classify(int d)
    {
        if (d <= 2) return "FORBIDDEN";
        if (d == 3) return "PREFERRED";
        return "ALLOWED";
    }

    /// <summary>Whether dimension d supports non-trivial gravity (d≥3).</summary>
    public static bool HasGravity(int d) => DimensionAnalysis.Einstein11Prefactor(d) != 0.0;

    /// <summary>Whether dimension d is conformal-complete (Weyl vanishes, nothing frozen).</summary>
    public static bool ConformalComplete(int d) => DimensionAnalysis.WeylComponents(d) == 0.0;
}
