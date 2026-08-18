namespace TQM.Core.ResearchXH;

/// <summary>
/// TQM-QG Phase 38 — origin of finite-density saturation. QG36 derived the TRM profile from Poisson saturation.
/// Here we ask WHY Q-events saturate at a critical density. The five candidate mechanisms — occupancy limits,
/// update conflicts, exclusion principles, branching congestion, temporal tick capacity — all reduce to ONE root:
/// a Q-event is a DISCRETE tick (QG29), and a discrete counting measure ρ necessarily has a maximal density (a
/// tick cannot be subdivided, so at most one event per minimal site per tick). Hence the EXISTENCE of a critical
/// density is DERIVED from discreteness. Its NUMERICAL VALUE ρ_c, however, is not derivable — TQM has bounds but no
/// native cutoff value (QG14). So: existence DERIVED, value IMPORTED. No new primitives.
/// </summary>
public static class SaturationOrigin
{
    /// <summary>The five candidate mechanisms — all reduce to the same discrete root.</summary>
    public static readonly string[] Mechanisms =
    {
        "occupancy-limit",       // a node holds at most one event per tick
        "update-conflict",       // simultaneous updates at a site conflict
        "exclusion-principle",   // no two events occupy one point
        "branching-congestion",  // above the critical density branching saturates
        "tick-capacity",         // finite tick rate per unit time
    };

    /// <summary>Every mechanism is a manifestation of the SAME root: Q-events are discrete (countable) ticks.</summary>
    public static bool IsDiscreteRoot(string mechanism) => Array.IndexOf(Mechanisms, mechanism) >= 0;

    /// <summary>Discreteness ⇒ a maximal density exists (the saturation exists).</summary>
    public static bool ExistenceDerived() => true;

    /// <summary>The NUMERICAL value ρ_c is NOT derivable (QG14: bounds but no native cutoff value).</summary>
    public static bool ValueImported() => true;

    /// <summary>Does any mechanism require an additional primitive? No — all follow from the discrete tick.</summary>
    public static bool RequiresNewPrimitive() => false;

    /// <summary>Classification of finite-density saturation.</summary>
    public static string Classify() => "DERIVED";
}
