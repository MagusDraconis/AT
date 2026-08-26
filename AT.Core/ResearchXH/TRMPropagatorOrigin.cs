namespace AT.Core.ResearchXH;

/// <summary>
/// AT-QG Phase 31 — derive the TRM propagator from Q-event network dynamics. A Q-event is a local tick (QG29). The
/// tick propagates along the GENERATION RELATION, whose boundary is the light cone (the conformal class) — so the
/// native propagation law is the null (massless) propagator with effective profile M_eff = n − 1 = 0. TRM's kernel
/// M_eff(r) = e^Φ − 1 is a NONZERO refractive/mass profile = the non-conformal (ψ ≠ 0) sector. They share the causal
/// (retarded, light-cone) structure but differ in refractive content, coinciding only at ψ = 0. No new primitives.
/// </summary>
public static class TRMPropagatorOrigin
{
    /// <summary>The tick propagates along the generation relation → its boundary is the light cone (conformal class).</summary>
    public static bool TickPropagatesAlongLightCone() => true;

    /// <summary>Native propagation index n = 1 (conformal; the conformal factor cancels).</summary>
    public static double NativeIndex() => 1.0;

    /// <summary>Native effective profile M_eff = n − 1 = 0 (massless null propagation).</summary>
    public static double NativeMeff() => 0.0;

    /// <summary>TRM kernel M_eff(r) = e^Φ − 1: a nonzero refractive/mass profile (the ψ sector).</summary>
    public static double TrmMeff(double phi) => Math.Exp(phi) - 1.0;

    /// <summary>Both share the causal (retarded, light-cone) structure.</summary>
    public static bool SharesCausalStructure() => true;

    /// <summary>The two coincide only when M_eff = 0 (ψ = 0, conformal limit).</summary>
    public static bool Coincide(double meff) => meff == 0.0;

    /// <summary>Is TRM's kernel derivable as the NATIVE propagation law? No — the native law gives M_eff = 0 only.</summary>
    public static bool DerivableAsPropagation() => false;
}
