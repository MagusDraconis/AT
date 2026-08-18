namespace TQM.Core.ResearchXH;

/// <summary>
/// TQM-QG Phase 40 — final quantum-gravity boundary audit. After all phases, settle what is DERIVED, what is a
/// NEW PRIMITIVE, what is IMPORTED, and whether anything is EMERGENT. The arc's conclusions, per item:
///   Q-events (REAL-UNDERIVED, QG29) and the tensor ψ (QG23/24/37) are the TWO primitives;
///   counting measure, causal order, geometry, Einstein structure, matter, scalar gravity, and saturation physics
///   are all DERIVED from the primitives + principles;
///   GW and lensing observables are IMPORTED (observationally supplied) and are exactly what force ψ.
///   NOTHING is EMERGENT. No new primitives (audit only).
/// </summary>
public static class FinalBoundaryAudit
{
    /// <summary>The eleven items of the final boundary audit.</summary>
    public static readonly string[] Items =
    {
        "q-events",
        "counting-measure",
        "causal-order",
        "geometry",
        "einstein-structure",
        "matter",
        "scalar-gravity",
        "saturation-physics",
        "tensor-sector",
        "gw-observables",
        "lensing-observables",
    };

    /// <summary>Boundary classification of each item.</summary>
    public static string Classify(string item) => item switch
    {
        "q-events" => "NEW PRIMITIVE",        // REAL-UNDERIVED actualization substrate (QG29)
        "counting-measure" => "DERIVED",      // ρ = density of Q-events
        "causal-order" => "DERIVED",          // from the generation relation (QG11)
        "geometry" => "DERIVED",              // g = ρ^(2/d)η from causal order + ρ (η preferred)
        "einstein-structure" => "DERIVED",    // G_μν from the metric (scalar part)
        "matter" => "DERIVED",                // m = ρ̄ − ρ deficit (G4-ME Phase 5)
        "scalar-gravity" => "DERIVED",        // a = −(1/d)∇ln ρ (kinematic)
        "saturation-physics" => "DERIVED",    // discreteness ⇒ max density (QG38); profile (QG36)
        "tensor-sector" => "NEW PRIMITIVE",   // ψ (spin-2) cannot emerge (QG23/24/37)
        "gw-observables" => "IMPORTED",       // observationally required; force ψ (QG25)
        "lensing-observables" => "IMPORTED",  // observationally required; force ψ (QG26/28)
        _ => throw new ArgumentOutOfRangeException(nameof(item))
    };

    /// <summary>Is anything EMERGENT (arising from collective behavior, not derived)? No.</summary>
    public static bool AnythingEmergent() => false;

    /// <summary>The number of primitives in the final theory: Q-events + ψ = 2.</summary>
    public static int Primitives() => 2;
}
