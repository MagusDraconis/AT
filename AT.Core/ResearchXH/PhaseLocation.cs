namespace AT.Core.ResearchXH;

/// <summary>
/// AT-QG Phase 63 — physical location of the quantum phase. QG62 showed amplitudes require a new U(1) phase
/// primitive. Here we ask WHERE that phase lives in the network. Lattice gauge theory gives the canonical answer:
/// the gauge connection (the U(1) phase) is a LINK variable, matter wavefunction phases are NODE variables, and
/// loop holonomies (Wilson loops) are DERIVED (gauge-invariant products of link phases). So the gauge phase is
/// naturally attached to LINKS, the matter phase to NODES, and loops are the derived gauge-invariant observables —
/// NO new object is needed. No new primitives added here (audit only).
/// </summary>
public static class PhaseLocation
{
    /// <summary>The four candidate locations.</summary>
    public static readonly string[] Locations =
    {
        "node-phases",
        "link-phases",
        "loop-holonomies",
        "new-object",
    };

    /// <summary>Is the gauge connection phase a LINK variable? Yes (lattice gauge theory).</summary>
    public static bool GaugePhaseOnLinks() => true;

    /// <summary>Is the matter wavefunction phase a NODE variable? Yes.</summary>
    public static bool MatterPhaseOnNodes() => true;

    /// <summary>Are loop holonomies DERIVED (products of link phases, gauge-invariant)? Yes.</summary>
    public static bool LoopHolonomyDerived() => true;

    /// <summary>Is a NEW OBJECT needed (beyond nodes/links)? No — the existing structure suffices.</summary>
    public static bool RequiresNewObject() => false;

    /// <summary>Classification — the U(1) gauge phase's natural home.</summary>
    public static string Classify() => "LINKS";
}
