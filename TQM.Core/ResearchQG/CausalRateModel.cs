namespace TQM.Core.ResearchQG;

/// <summary>QG-091 causal rate model: H as the growth rate of causal depth. The number of
/// events in the observable causal volume is N ∝ a⁴ (4-volume), so d ln N/dt = 4H (not H),
/// and the causal-DEPTH growth rate is d ln(depth)/dt = H (the scale factor sets the depth).
/// a₀ = c × (causal growth rate) = cH.</summary>
public static class CausalRateModel
{
    public const double C = 299792458.0;
    public const double H0PerS = 67.4 / 3.0857e19; // 2.184e-18 s^-1

    /// <summary>Growth rate of the causal-event count: d ln N/dt = 4H (4-volume).</summary>
    public static double EventGrowthRateInUnitsOfH => 4.0;

    /// <summary>Growth rate of causal DEPTH: d ln(depth)/dt = H.</summary>
    public static double DepthGrowthRateInUnitsOfH => 1.0;

    /// <summary>a₀ = c × (causal-depth growth rate) = cH.</summary>
    public static double A0FromCausalRate() => C * H0PerS;
}
