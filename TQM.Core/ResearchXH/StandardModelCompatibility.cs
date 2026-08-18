namespace TQM.Core.ResearchXH;

/// <summary>
/// TQM-QG Phase 60 — Standard Model compatibility. Can the causal network (V, E) host gauge fields, fermions,
/// charge, and spin-1 interactions? The network natively produces spin-0 (the trace ρ) and spin-2 (the traceless ψ).
/// Gauge fields (spin-1) live on the LINKS as connections/edge phases (lattice gauge theory) — COMPATIBLE, though a
/// new degree of freedom. Charge is a SCALAR quantum-number label on the nodes — NATURAL. Fermions (spin-1/2) are
/// spinors, which are not native to a scalar-node + rank-2-link network — UNKNOWN (they would require a new
/// primitive). Spin-1 interactions are COMPATIBLE via the link connection. No new primitives added here.
/// </summary>
public static class StandardModelCompatibility
{
    /// <summary>The four Standard-Model ingredients audited.</summary>
    public static readonly string[] Ingredients =
    {
        "gauge-fields",
        "fermions",
        "charge",
        "spin-1-interactions",
    };

    /// <summary>Classification of each ingredient.</summary>
    public static string Classify(string ingredient) => ingredient switch
    {
        "gauge-fields" => "COMPATIBLE",        // links host connections (lattice gauge theory)
        "fermions" => "UNKNOWN",               // spinors are not native to scalar+rank-2
        "charge" => "NATURAL",                 // scalar quantum-number label on nodes
        "spin-1-interactions" => "COMPATIBLE", // via the link connection
        _ => throw new ArgumentOutOfRangeException(nameof(ingredient))
    };

    /// <summary>The network natively produces spin-0 (trace ρ) and spin-2 (traceless ψ) — no spin-1, no spin-1/2.</summary>
    public static double[] NativeSpins() => new[] { 0.0, 2.0 };

    /// <summary>Do gauge fields live on the links as connections/edge phases? Yes (lattice gauge theory).</summary>
    public static bool GaugeFieldsLiveOnLinks() => true;

    /// <summary>Are fermions (spinors) native to a scalar-node + rank-2-link network? No.</summary>
    public static bool FermionsNative() => false;

    /// <summary>Is charge a SCALAR quantum-number label (on nodes)? Yes.</summary>
    public static bool ChargeIsScalarLabel() => true;
}
