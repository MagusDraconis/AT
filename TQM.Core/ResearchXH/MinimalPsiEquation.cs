namespace TQM.Core.ResearchXH;

/// <summary>
/// TQM-QG Phase 44 — minimal ψ field equation. ψ is the spin-2 (graviton) primitive. Its simplest dynamics
/// consistent with the observed ψ effects (light-speed, two-polarization gravitational waves) is the MASSLESS
/// SPIN-2 WAVE EQUATION — the Fierz-Pauli / linearized-Einstein equation □ψ_μν = 0 in the transverse-traceless
/// gauge, with 2 helicities, propagating at light speed, reducing to linearized GR in the weak-field limit. This
/// form is PREFERRED (it is the UNIQUE ghost-free, Lorentz-invariant, massless spin-2 theory), but its STATUS is
/// POSTULATED — ψ is a new primitive, so its equation of motion is a new input, not derivable from TQM's scalar
/// sector (QG23/24/37). No new primitives beyond ψ.
/// </summary>
public static class MinimalPsiEquation
{
    /// <summary>Propagating spin-2 helicities at spatial dimension d: (d+1)(d−2)/2 = 2 at d=3.</summary>
    public static double Helicities(int d) => DimensionAnalysis.GravitonPolarizations(d);

    /// <summary>Massless ⇒ propagation speed = c (=1 in natural units).</summary>
    public static double PropagationSpeed() => 1.0;

    /// <summary>Weak-field limit = linearized Einstein (matches the GW detector observables).</summary>
    public static bool MatchesWeakFieldGr() => true;

    /// <summary>Is the ψ dynamics DERIVED from TQM? No — ψ is a new primitive.</summary>
    public static bool Derived() => false;

    /// <summary>Is the SPECIFIC form PREFERRED (unique massless spin-2)? Yes.</summary>
    public static bool FormIsPreferred() => true;

    /// <summary>Is the equation POSTULATED (a new input for the new primitive)? Yes.</summary>
    public static bool Postulated() => true;

    /// <summary>Classification of the minimal ψ field equation.</summary>
    public static string Classify() => "POSTULATED";
}
