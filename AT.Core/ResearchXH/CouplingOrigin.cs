namespace AT.Core.ResearchXH;

/// <summary>
/// AT-QG Phase 6 — origin of the coupling G. The gravity structure is derived; only the overall scale remains
/// imported. Here we test whether G can emerge from counting statistics or actualization dynamics, and whether the
/// conformal gravity has a free coupling at all. No new primitives.
/// </summary>
public static class CouplingOrigin
{
    /// <summary>
    /// Asymptotic effective gravitational mass (the GM product) of the power-law deficit
    /// ρ = ρ̄ − m₀/(1 + r/r₀): GM_eff = m₀·r₀/(d·ρ̄). This is the single native scale that plays the role of
    /// Newton's GM in the conformal point-mass form a = −GM_eff/r².
    /// </summary>
    public static double DeficitMass(double m0, double r0, int d, double rhoBar = 1.0)
        => m0 * r0 / (d * rhoBar);

    /// <summary>
    /// G–M degeneracy: GM_eff is invariant under m₀ → c·m₀, r₀ → r₀/c (deficit depth × inner scale trades off).
    /// G and M are therefore NOT separately determined — only the product m₀·r₀/(d·ρ̄) is physical.
    /// </summary>
    public static double RescaledDeficitMass(double m0, double r0, double c, int d, double rhoBar = 1.0)
        => DeficitMass(c * m0, r0 / c, d, rhoBar);
}
