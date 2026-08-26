namespace AT.Core.ResearchXH;

/// <summary>
/// AT-QG Phase 20 — temporal-wave interpretation of gravitational-wave observations. Tests whether propagating
/// time-rate (temporal) oscillations can generate the same detector observables as GR tensor waves. The key fact:
/// null geodesics (light) are conformally invariant, so the conformal factor ρ^(2/d) does NOT affect light travel
/// times — a scalar/temporal wave is invisible to a light-based (Michelson) interferometer. No new primitives.
/// </summary>
public static class TemporalWaveObservables
{
    /// <summary>Round-trip light travel time along an arm of length L in g = ρ^(2/d)η. Null geodesics are
    /// conformally invariant: τ = 2L, INDEPENDENT of ρ.</summary>
    public static double RoundTripTime(double L) => 2.0 * L;

    /// <summary>Change in round-trip time from a temporal wave δρ: ZERO (conformal invariance of light).</summary>
    public static double RoundTripTimeChange(double L) => 0.0;

    /// <summary>Differential arm strain (the LIGO observable) for a breathing (scalar) mode: ZERO (common-mode —
    /// both arms stretch equally, so the phase difference vanishes).</summary>
    public static double BreathingDifferentialStrain(double h0) => 0.0;

    /// <summary>Differential arm strain for a tensor (+) mode: 2·h0 (one arm stretches, the perpendicular one
    /// squeezes — a differential signal).</summary>
    public static double TensorDifferentialStrain(double h0) => 2.0 * h0;
}
