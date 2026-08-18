namespace TQM.Core.ResearchXH;

/// <summary>
/// TQM-QG Phase 62 — origin of quantum amplitudes. QG61 showed the network reproduces gravity but not quantum
/// mechanics. Here we ask whether COMPLEX AMPLITUDES can emerge from network structure. Key facts: the network
/// (V, E) natively has scalar nodes + rank-2 links — NO phase (U(1)) content. Complex amplitudes require a phase.
/// The links CAN host a U(1) phase as a connection (lattice gauge theory, QG60) — so QM is COMPATIBLE — but a
/// closed loop WITHOUT such a phase has a trivial holonomy (no interference), so the phase does NOT EMERGE from the
/// loop structure: it is a NEW degree of freedom, i.e. QM REQUIRES A NEW PRIMITIVE (the amplitude/phase). No new
/// primitives added here (this is an audit).
/// </summary>
public static class OriginOfQuantumAmplitudes
{
    /// <summary>The five candidate mechanisms.</summary>
    public static readonly string[] Mechanisms =
    {
        "phase-link-variables",
        "closed-loop-holonomies",
        "oscillating-modes",
        "interference-conditions",
        "probability-amplitudes",
    };

    /// <summary>Does the network NATIVELY have a phase (U(1)) content? No.</summary>
    public static bool NetworkHasNativePhase() => false;

    /// <summary>Can the links HOST a U(1) phase as a connection? Yes (lattice gauge theory).</summary>
    public static bool LinksCanHostPhase() => true;

    /// <summary>Does a closed loop give a nontrivial holonomy WITHOUT a phase? No (holonomy = 1).</summary>
    public static bool HolonomyWithoutPhase() => false;

    /// <summary>Is QM COMPATIBLE with the network (via link phases)? Yes.</summary>
    public static bool Compatible() => true;

    /// <summary>Is QM EMERGENT (native, no new input)? No.</summary>
    public static bool Emergent() => false;

    /// <summary>Does QM REQUIRE a NEW PRIMITIVE (the amplitude/phase)? Yes.</summary>
    public static bool RequiresNewPrimitive() => true;

    /// <summary>Classification.</summary>
    public static string Classify() => "REQUIRES NEW PRIMITIVE";
}
