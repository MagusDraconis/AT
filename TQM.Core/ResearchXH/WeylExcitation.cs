namespace TQM.Core.ResearchXH;

/// <summary>
/// TQM-QG Phase 57 — excitation of the traceless link sector. QG56 showed the Weyl CAPACITY is forced but its
/// excitation is contingent. Here we ask WHAT excites the traceless content of network links. Key fact: a spin-2
/// field couples to the full stress-energy T_μν, so its traceless (Weyl) part is sourced by the traceless
/// (quadrupole) part of the matter distribution — anisotropic sources, moving deficits, binary systems, network
/// stress. "Propagation stability" (massless, light-speed) is a necessary PROPERTY, not a source. Hence the
/// excitation MECHANISM (quadrupole → Weyl) is DERIVED from spin-2 coupling; the specific instances (binary
/// mergers) are OBSERVATION-TRIGGERED. No new primitives beyond ψ.
/// </summary>
public static class WeylExcitation
{
    /// <summary>The five candidate exciters/properties.</summary>
    public static readonly string[] Candidates =
    {
        "anisotropic-sources",
        "moving-deficits",
        "binary-systems",
        "network-stress",
        "propagation-stability",
    };

    /// <summary>Does the candidate carry quadrupole (traceless) content that can source Weyl?</summary>
    public static bool HasQuadrupole(string candidate) => candidate switch
    {
        "anisotropic-sources" => true,
        "moving-deficits" => true,
        "binary-systems" => true,
        "network-stress" => true,
        "propagation-stability" => false,   // a necessary property, not a source
        _ => throw new ArgumentOutOfRangeException(nameof(candidate))
    };

    /// <summary>Spin-2 couples to T_μν, so the traceless (quadrupole) part sources the Weyl content.</summary>
    public static bool QuadrupoleSourcesWeyl() => true;

    /// <summary>Is the excitation MECHANISM DERIVED from the spin-2 coupling? Yes.</summary>
    public static bool MechanismDerived() => true;

    /// <summary>Are the specific excitations (binary mergers) OBSERVATION-TRIGGERED? Yes.</summary>
    public static bool InstancesObservationTriggered() => true;

    /// <summary>Is propagation stability a SOURCE of excitation? No — it is a necessary condition.</summary>
    public static bool StabilityIsSource() => false;

    /// <summary>Classification.</summary>
    public static string Classify() => "DERIVED";
}
