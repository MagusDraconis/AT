namespace TQM.Core.ResearchXH;

/// <summary>
/// TQM-QG Phase 29 — physical interpretation of Q-events. The foundation reduces to two primitives: Q-events and
/// the generation relation (which gives causal order). Here we fix the MINIMAL physical meaning of a Q-event.
/// A valid picture must satisfy four criteria: (1) actualization content — events must "happen" (be generated);
/// (2) counting compatibility — must yield the counting measure ρ; (3) causal-order compatibility — must support
/// the generation relation → causal order; (4) primitive status — must not require a deeper substrate (else the
/// Q-event would be emergent). The network-transition pictures satisfy all four; the bare "primitive point" fails
/// actualization content. Conclusion: a Q-event is a REAL-UNDERIVED primitive whose minimal physical content is a
/// NETWORK TRANSITION (a local time-state change / clock tick that ρ counts). No new primitives.
/// </summary>
public static class PhysicalMeaningOfQEvents
{
    /// <summary>The four criteria a physical picture must satisfy to be the minimal meaning of a Q-event.</summary>
    public static readonly string[] Criteria =
    {
        "actualization-content",        // events must 'happen' (be generated/updated)
        "counting-compatibility",       // must yield the counting measure rho
        "causal-order-compatibility",   // must support the generation relation -> causal order
        "primitive-status",             // must not require a deeper substrate (not emergent)
    };

    /// <summary>Network-transition pictures: each is a local time-state change in a temporal network.</summary>
    public static readonly string[] TransitionPictures =
    {
        "temporal-lattice",   // TRM temporal lattice: a Q-event is a lattice-site update
        "clock-network",      // a Q-event is a local clock tick
        "time-state-change",  // a Q-event is a local time-state (phase) change
        "network-update",     // a Q-event is a node update under the generation relation
    };

    /// <summary>Number of criteria satisfied by each picture (transition pictures = 4; bare point = 1).</summary>
    public static int CriteriaSatisfied(string picture) => picture switch
    {
        "temporal-lattice" => 4,
        "clock-network" => 4,
        "time-state-change" => 4,
        "network-update" => 4,
        "primitive-point" => 1,   // only primitive-status (it IS a primitive); no actualization content
        _ => throw new ArgumentOutOfRangeException(nameof(picture))
    };

    public static bool IsTransitionPicture(string picture)
        => Array.IndexOf(TransitionPictures, picture) >= 0;

    /// <summary>Is the bare "primitive point" reading sufficient? No — it lacks actualization content.</summary>
    public static bool PrimitivePointSufficient() => CriteriaSatisfied("primitive-point") == 4;

    /// <summary>Is a Q-event EMERGENT (derived from a deeper substrate)? No — it is a primitive.</summary>
    public static bool Emergent() => false;

    /// <summary>Is a Q-event REAL-UNDERIVED (a primitive, not reducible within TQM)? Yes.</summary>
    public static bool RealUnderived() => true;

    /// <summary>ρ is the counting measure = the density of Q-events (each Q-event is one counted unit).</summary>
    public static bool RhoCountsQEvents() => true;
}
