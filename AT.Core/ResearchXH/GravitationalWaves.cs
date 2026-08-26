namespace AT.Core.ResearchXH;

/// <summary>
/// AT-QG Phase 18 — compatibility with observed gravitational waves. Tests whether observed GW phenomena can
/// arise in the scalar (conformal) sector, comparing polarization structure (breathing vs +/×) and trace content.
/// d = spatial dimension. No new primitives.
/// </summary>
public static class GravitationalWaves
{
    /// <summary>Scalar (conformal) sector polarizations = 1 (the breathing/monopole mode).</summary>
    public static double ScalarPolarizations() => 1.0;

    /// <summary>Tensor (graviton) polarizations = (d+1)(d−2)/2 (2 at d=3: the + and × modes).</summary>
    public static double TensorPolarizations(int d) => DimensionAnalysis.GravitonPolarizations(d);

    /// <summary>Trace of a scalar metric disturbance δg^μ_μ — NON-ZERO (breathing: isotropic volume change).</summary>
    public static double ScalarModeTrace(double N, int d) => SpacetimeFluctuations.MetricFluctuationTrace(N, d);

    /// <summary>Trace of a tensor (graviton) disturbance — ZERO (transverse-traceless: volume-preserving shear).</summary>
    public static double TensorModeTrace() => 0.0;
}
