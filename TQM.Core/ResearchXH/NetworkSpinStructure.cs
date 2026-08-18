namespace TQM.Core.ResearchXH;

/// <summary>
/// TQM-QG Phase 67 — network spin structure. QG66 showed spin-1/2 requires a spin structure. Here we ask whether the
/// CAUSAL NETWORK can naturally carry one. Key facts: a graph orientation is a Z2 structure (link direction), which
/// is NOT a spin structure; a spin structure is a DOUBLE COVER of the graph with a consistent sign on each cycle,
/// and the spin connection is an SU(2) (not U(1)) link variable. The network (V, E) naturally has orientation (Z2)
/// and a U(1) phase, but NOT the double-cover/SU(2) data. Hence the network CAN carry a spin structure (COMPATIBLE)
/// but it is new data, not naturally present — spin-1/2 REQUIRES A NEW PRIMITIVE (the spin structure/SU(2) connection).
/// No new primitives added here (audit only).
/// </summary>
public static class NetworkSpinStructure
{
    /// <summary>The five candidate mechanisms.</summary>
    public static readonly string[] Mechanisms =
    {
        "graph-orientation",
        "double-cover",
        "su2-representations",
        "spin-connection",
        "spinor-transport",
    };

    /// <summary>Does graph ORIENTATION (Z2) give a spin structure (double cover)? No.</summary>
    public static bool OrientationGivesSpinStructure() => false;

    /// <summary>Can the network CARRY a spin structure (double cover / SU(2))? Yes (compatible).</summary>
    public static bool CanCarrySpinStructure() => true;

    /// <summary>Is the spin structure NATURALLY present in (V, E)? No — it is new data.</summary>
    public static bool NaturallyPresent() => false;

    /// <summary>Does spin-1/2 REQUIRE A NEW PRIMITIVE (spin structure/SU(2))? Yes.</summary>
    public static bool RequiresNewPrimitive() => true;

    /// <summary>Classification.</summary>
    public static string Classify() => "REQUIRES NEW PRIMITIVE";
}
