namespace AT.Core.ResearchXH;

/// <summary>
/// AT-QG Phase 66 — origin of spin-1/2. The network natively hosts INTEGER spins: spin-0 (nodes → ρ), spin-2
/// (links → ψ), and spin-1 (the U(1) gauge phase). Fermions are spin-1/2 — a SPINOR (half-integer) representation,
/// which is fundamentally different: it requires a DOUBLE COVER of the rotation group (SU(2) covering SO(3)). A link
/// orientation gives only a Z2 sign, not a spinor; a genuine spinor requires a SPIN STRUCTURE (double cover), a new
/// degree of freedom not present in scalar nodes + rank-2 links. Hence spin-1/2 is COMPATIBLE (a spin structure can
/// be added to the network) but REQUIRES A NEW PRIMITIVE (the spinor/double-cover). No new primitives added here.
/// </summary>
public static class OriginOfSpinHalf
{
    /// <summary>The five candidate mechanisms.</summary>
    public static readonly string[] Mechanisms =
    {
        "link-orientation",
        "double-cover",
        "graph-topology",
        "spinor-representations",
        "exchange-symmetry",
    };

    /// <summary>The network natively hosts integer spins: 0 (nodes), 2 (links), 1 (U(1) gauge).</summary>
    public static double[] NativeSpins() => new[] { 0.0, 2.0, 1.0 };

    /// <summary>Does link ORIENTATION give a spinor? No — it gives a Z2 sign, not SU(2).</summary>
    public static bool OrientationGivesSpinor() => false;

    /// <summary>Does spin-1/2 require a DOUBLE COVER (SU(2) spin structure)? Yes.</summary>
    public static bool RequiresDoubleCover() => true;

    /// <summary>Is spin-1/2 DERIVED from the integer-spin content? No.</summary>
    public static bool Derived() => false;

    /// <summary>Is spin-1/2 COMPATIBLE (via a spin structure)? Yes.</summary>
    public static bool Compatible() => true;

    /// <summary>Does spin-1/2 REQUIRE A NEW PRIMITIVE (spinor/double-cover)? Yes.</summary>
    public static bool RequiresNewPrimitive() => true;

    /// <summary>Classification.</summary>
    public static string Classify() => "REQUIRES NEW PRIMITIVE";
}
