namespace TQM.Core.ResearchXH;

/// <summary>
/// TQM-QG Phase 69 — first unique prediction of the unified network theory. The network (V, E) with sectors
/// ρ, ψ, θ, S reproduces GR (ψ tensor GWs/lensing) and the gauge/spinor content of the Standard Model, so those
/// signatures are NOT unique. The genuinely unique prediction is NETWORK DISCRETENESS: spacetime (and all four
/// sectors) is granular at a single common scale, because the link is a discrete object carrying all four sectors
/// — a minimum length / common discreteness scale that neither GR nor the Standard Model predicts. It is UNIQUE,
/// TESTABLE (in principle), and FALSIFIABLE (in principle), though the discreteness scale is a free parameter
/// (QG14/QG38), which makes falsification challenging. No new primitives added here.
/// </summary>
public static class FirstPrediction
{
    /// <summary>The five signature candidates.</summary>
    public static readonly string[] Signatures =
    {
        "gw",
        "lensing",
        "black-hole",
        "quantum-coherence",
        "network-discreteness",
    };

    /// <summary>Is the signature UNIQUE to the network (absent from GR + Standard Model)?</summary>
    public static bool Unique(string signature) => signature switch
    {
        "gw" => false,                  // matches GR tensor GWs (QG44)
        "lensing" => false,             // matches GR lensing (γ=+1)
        "black-hole" => false,          // regular core matches Hayward/Bardeen models
        "quantum-coherence" => false,   // overlaps with Standard Model / QM
        "network-discreteness" => true, // spacetime granularity, absent from GR/SM
        _ => throw new ArgumentOutOfRangeException(nameof(signature))
    };

    /// <summary>Is the network discreteness TESTABLE (in principle)? Yes.</summary>
    public static bool Testable() => true;

    /// <summary>Is it FALSIFIABLE (in principle)? Yes.</summary>
    public static bool Falsifiable() => true;

    /// <summary>Is the discreteness scale FIXED by the theory? No — it is a free parameter (QG14/QG38).</summary>
    public static bool DiscretenessScaleFixed() => false;

    /// <summary>Does the unified link predict a COMMON discreteness scale for all four sectors? Yes.</summary>
    public static bool CommonDiscretenessScale() => true;

    /// <summary>Classification.</summary>
    public static string Classify() => "UNIQUE";
}
