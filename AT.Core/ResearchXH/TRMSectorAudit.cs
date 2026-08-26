namespace AT.Core.ResearchXH;

/// <summary>
/// AT-QG Phase 39 — separate TRM into derived and non-derived sectors. QG36/QG38 derived the regular-core mechanism
/// from Q-event counting (SATURATION physics, the scalar/derived sector); QG23/QG24/QG37 established ψ as the new
/// tensor primitive (PSI physics). Here we audit six TRM observables into three buckets:
///   SATURATION — follows from the derived scalar sector (counting/saturation/conformal g_00), no ψ;
///   PSI — requires the non-conformal tensor primitive;
///   BOTH — needs the scalar core AND the tensor horizon.
/// Result: redshift is scalar (SATURATION); lensing, PPN, horizon thermodynamics, and GWs are all PSI; regular
/// black holes are BOTH (regular core from saturation, horizon from ψ). No new primitives.
/// </summary>
public static class TRMSectorAudit
{
    /// <summary>The six TRM observables audited.</summary>
    public static readonly string[] Observables =
    {
        "redshift",
        "lensing",
        "ppn",
        "regular-black-hole",
        "horizon-thermodynamics",
        "gw",
    };

    /// <summary>Sector classification of each observable.</summary>
    public static string Classify(string observable) => observable switch
    {
        "redshift" => "SATURATION",         // g_00 = −ρ^(2/d) scalar effect; no ψ (QG34)
        "lensing" => "PSI",                 // non-conformal deflection (γ ≠ −1)
        "ppn" => "PSI",                     // γ −1 → +1 requires ψ
        "regular-black-hole" => "BOTH",     // regular core (saturation) + horizon (ψ)
        "horizon-thermodynamics" => "PSI",  // horizon surface gravity requires ψ
        "gw" => "PSI",                      // spin-2 (h_+, h_×)
        _ => throw new ArgumentOutOfRangeException(nameof(observable))
    };

    // ── Supporting facts (the ψ-needs established across QG34–QG37) ────────────────────

    /// <summary>Redshift needs no ψ — AT's conformal g_00 = −ρ^(2/d) already gives it (QG34).</summary>
    public static bool RedshiftNeedsPsi() => false;

    /// <summary>Lensing needs ψ (conformal null geodesics give no deflection, QG26/28).</summary>
    public static bool LensingNeedsPsi() => true;

    /// <summary>PPN recovery needs ψ (γ = −1 is conformal; γ = +1 requires the non-conformal factor).</summary>
    public static bool PpnNeedsPsi() => true;

    /// <summary>The regular CORE comes from saturation (Poisson profile, QG36), not from ψ.</summary>
    public static bool RegularCoreNeedsSaturation() => true;

    /// <summary>The HORIZON needs ψ (the conformal metric has no horizon, QG33/35).</summary>
    public static bool HorizonNeedsPsi() => true;

    /// <summary>GWs (spin-2) need ψ (QG25: TENSOR REQUIRED).</summary>
    public static bool GwNeedsPsi() => true;
}
