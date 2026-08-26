namespace AT.Core.ResearchXH;

/// <summary>
/// AT-QG Phase 45 — minimal coupling of ψ. QG44 gave ψ the massless spin-2 wave equation. Here we ask for the
/// WEAKEST coupling between ψ and the derived scalar backbone (ρ, the deficit, saturation, Q-event density)
/// required to recover the GW POLARIZATION observable. Key fact: the two helicities (h_+, h_×) are intrinsic to
/// the FREE massless spin-2 field — the polarization structure requires ZERO coupling to the scalar sector. A
/// coupling is needed only to SOURCE ψ (give it a nonzero amplitude from matter), and that source coupling is the
/// WEAK gravitational coupling (κ = 8πG). So ψ is INDEPENDENT for the polarization observable and WEAKLY coupled
/// only when sourced. No new primitives beyond ψ.
/// </summary>
public static class MinimalPsiCoupling
{
    /// <summary>The four candidate couplings between ψ and the scalar backbone.</summary>
    public static readonly string[] Couplings =
    {
        "psi-rho",            // ψ ↔ counting measure ρ
        "psi-deficit",        // ψ ↔ matter deficit m = ρ̄ − ρ
        "psi-saturation",     // ψ ↔ finite-density saturation
        "psi-qevent-density", // ψ ↔ Q-event density
    };

    /// <summary>Coupling strength required to recover the GW POLARIZATION (2 helicities): ZERO.</summary>
    public static double PolarizationCouplingRequired() => 0.0;

    /// <summary>Is a given coupling required for the POLARIZATION observable? No — none of the four is.</summary>
    public static bool RequiredForPolarization(string coupling)
    {
        if (Array.IndexOf(Couplings, coupling) < 0)
            throw new ArgumentOutOfRangeException(nameof(coupling));
        return false;
    }

    /// <summary>A WEAK coupling (κ = 8πG) is needed only to SOURCE ψ (give GWs a nonzero amplitude).</summary>
    public static bool WeakCouplingForSourcing() => true;

    /// <summary>Is the source coupling weak (κ = 8πG ≪ 1 in natural units)? Yes.</summary>
    public static double GravitationalCouplingWeakness() => 8.0 * Math.PI * 1.0;   // κ = 8πG, G small → weak

    /// <summary>Classification of ψ's coupling.</summary>
    public static string Classify() => "INDEPENDENT";   // for polarization; weakly coupled when sourced
}
