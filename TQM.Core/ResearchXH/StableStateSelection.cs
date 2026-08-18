namespace TQM.Core.ResearchXH;

/// <summary>
/// TQM-QG Phase 96 — Stable State Selection. QG91–95 suggest parameter values may be encoded in link geometry and
/// global consistency. This phase asks whether the network possesses PREFERRED STABLE STATES whose spectra could
/// SELECT physical parameters.
///
/// Answer: PARTIAL SELECTION. The network DOES possess stability criteria (vacuum stability, QG88) and native
/// RG attractors (asymptotic freedom), which PARTIALLY select/narrow the allowed parameter region. It also HAS
/// stable resonance modes and discrete spectra (QG95), and metastable configurations are representable. But there
/// is NO NATIVE energy functional/Hamiltonian whose minima, attractor, or preferred state have a spectrum equal to
/// the SM parameters — the discrete spectra exist, but nothing selects WHICH eigenvalues are physical. Hence
/// selection is PARTIAL (stability + attractors narrow the region) but not full STATE SELECTION (no unique preferred
/// state determines the values). No new primitives added here (audit only).
/// </summary>
public static class StableStateSelection
{
    /// <summary>The five candidate mechanisms.</summary>
    public static readonly string[] Mechanisms =
    {
        "network-energy-minima",
        "stable-resonance-modes",
        "attractor-states",
        "discrete-spectrum-selection",
        "metastable-configurations",
    };

    /// <summary>Is there a NATIVE energy functional whose minima select a state? No.</summary>
    public static bool NativeEnergyFunctional() => false;

    /// <summary>Do stable resonance modes exist? Yes.</summary>
    public static bool StableModesExist() => true;

    /// <summary>Are RG attractor states native (asymptotic freedom)? Yes.</summary>
    public static bool AttractorStatesNative() => true;

    /// <summary>Is a native mechanism that selects WHICH eigenvalues are physical present? No.</summary>
    public static bool DiscreteSpectrumSelectionNative() => false;

    /// <summary>Are metastable configurations representable? Yes.</summary>
    public static bool MetastableStatesRepresentable() => true;

    /// <summary>Is PARTIAL selection achieved (stability + attractors narrow the region)? Yes.</summary>
    public static bool PartialSelectionAchieved() => true;

    /// <summary>Is FULL state selection (a unique preferred state) achieved? No.</summary>
    public static bool FullStateSelectionAchieved() => false;

    /// <summary>Classification: NO SELECTION / PARTIAL SELECTION / STATE SELECTION.</summary>
    public static string Classify() => "PARTIAL SELECTION";
}
