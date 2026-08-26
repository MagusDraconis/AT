namespace AT.Core.ResearchXH;

/// <summary>
/// AT-QG Phase 19 — reconcile gravitational-wave observations. Tests whether observed GW signals can arise from
/// an emergent tensor channel or require a new primitive. The key structural facts: the Weyl tensor is conformally
/// invariant (so no scalar can source it), and spin-0 (1 polarization) cannot produce spin-2 (2 polarizations).
/// d = spatial dimension. No new primitives.
/// </summary>
public static class GWReconciliation
{
    /// <summary>Scalar (spin-0) polarizations = 1 (monopole/breathing).</summary>
    public static double Spin0Polarizations() => 1.0;

    /// <summary>Graviton (spin-2) polarizations = (d+1)(d−2)/2 (2 at d=3: the + and × helicities).</summary>
    public static double Spin2Polarizations(int d) => DimensionAnalysis.GravitonPolarizations(d);

    /// <summary>Weyl tensor of a conformally-flat metric g = ρ^(2/d)η — identically 0 for ANY scalar ρ.</summary>
    public static double WeylOfConformalMetric() => 0.0;

    /// <summary>Reference-metric (ψ/Weyl) degrees of freedom that a new tensor primitive must provide (10 at d=3).</summary>
    public static double ReferenceMetricDof(int d) => DimensionAnalysis.WeylComponents(d);
}
