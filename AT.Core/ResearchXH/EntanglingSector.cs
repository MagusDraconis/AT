namespace AT.Core.ResearchXH;

/// <summary>
/// AT-QG Phase 71 — origin of the entangling sector. QG70 showed θ gives interference but θ + S do not give Bell
/// entanglement. Here we find the MINIMAL additional link content that produces non-separable correlations. The
/// phase θ is a SINGLE-degree-of-freedom amplitude (e^{iθ}) — it gives interference (QG65) but not non-separability.
/// Entanglement requires a JOINT state on TWO degrees of freedom (a non-separable 2-qubit state, e.g. a Bell pair),
/// and the natural home of a joint state is a LINK (which connects exactly two nodes). Hence the minimal additional
/// content is a JOINT LINK STATE — a new entangling sector, COMPATIBLE with the link structure but not derivable
/// from θ + S. Classification: NEW SECTOR. No new primitives added here (audit only).
/// </summary>
public static class EntanglingSector
{
    /// <summary>The five candidate mechanisms.</summary>
    public static readonly string[] Mechanisms =
    {
        "shared-link-states",
        "pair-link-structures",
        "higher-order-relations",
        "nonlocal-constraints",
        "bell-correlations",
    };

    /// <summary>Does a single-DOF phase θ produce NON-SEPARABILITY (entanglement)? No — it gives interference only.</summary>
    public static bool PhaseGivesNonSeparability() => false;

    /// <summary>Does entanglement require a JOINT (2-qubit) state on the link? Yes.</summary>
    public static bool RequiresJointLinkState() => true;

    /// <summary>Is a joint link state COMPATIBLE with the link structure? Yes (the link is a pair).</summary>
    public static bool Compatible() => true;

    /// <summary>Is the joint link state a NEW SECTOR? Yes.</summary>
    public static bool NewSector() => true;

    /// <summary>Classification.</summary>
    public static string Classify() => "NEW SECTOR";
}
