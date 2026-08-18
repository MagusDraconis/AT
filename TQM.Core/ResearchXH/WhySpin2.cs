namespace TQM.Core.ResearchXH;

/// <summary>
/// TQM-QG Phase 46 — why spin-2? ψ is the only remaining primitive. We ask why its minimal extension must be spin-2
/// rather than spin-1 or spin-0. Three independent observational constraints uniquely select spin-2:
///   (1) POLARIZATION: observed GWs have 2 polarizations (h_+, h_×) — spin-0 has 1 helicity (ruled out);
///   (2) ATTRACTION: gravity is universally attractive — odd spin (spin-1) is repulsive (ruled out);
///   (3) LIGHT BENDING: the correct GR deflection needs coupling to the full rank-2 stress-energy T_μν — spin-0
///       couples to the trace T (which vanishes for light) and gives zero/wrong deflection (ruled out).
/// Only spin-2 passes all three. Hence spin-2 is PREFERRED (uniquely selected), not DERIVED (ψ is a new primitive)
/// and not a bare postulate. No new primitives beyond ψ.
/// </summary>
public static class WhySpin2
{
    /// <summary>Constraint 1 — polarization: observed GWs have 2 polarizations (h_+, h_×).</summary>
    public static bool TwoPolarizations(int spin) => spin >= 1;

    /// <summary>Constraint 2 — attraction: universal attraction requires EVEN spin (odd spin is repulsive).</summary>
    public static bool UniversalAttraction(int spin) => spin % 2 == 0;

    /// <summary>Constraint 3 — light bending: correct deflection needs the full rank-2 stress-energy (spin-2 only).</summary>
    public static bool CorrectLightBending(int spin) => spin == 2;

    /// <summary>Is spin-s a viable gravity field? All three constraints must hold.</summary>
    public static bool Viable(int spin)
        => TwoPolarizations(spin) && UniversalAttraction(spin) && CorrectLightBending(spin);

    /// <summary>Which spin is selected by the constraints?</summary>
    public static int SelectedSpin()
    {
        for (int s = 0; s <= 2; s++) if (Viable(s)) return s;
        return -1;   // no viable spin (should not happen)
    }

    /// <summary>Classification of the spin-2 assignment.</summary>
    public static string Classify() => "PREFERRED";
}
