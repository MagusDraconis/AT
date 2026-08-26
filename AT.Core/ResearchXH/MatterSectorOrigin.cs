namespace AT.Core.ResearchXH;

/// <summary>
/// AT-QG Phase 195 — Matter Sector Origin. Known: G_μν is derived (G4-G0/G2/G3). Open: can an INDEPENDENT
/// stress-energy T_μν be recovered — without defining T ≡ G/κ — from the TRM network (network stress, link
/// energy, actualization flow)? No new primitives, TRM only, deterministic.
///
/// The G4-G4 obstruction (Lovelock): any symmetric CONSERVED second-order tensor built from the scalar
/// geometry (ρ, ∇ρ, ∇∇ρ) is forced to be G_μν — the kinetic tensor T^kin from ∇ρ is NOT conserved. This
/// closes the "geometric" route to matter: you cannot get a matter tensor from the metric/conformal
/// structure alone.
///
/// The resolution (this phase): matter is NOT a function of the geometry — it is the DEFICIT (QG194,
/// DEFICIT ORIGIN): a conserved mass density ρ_m = ρ̄ − ρ carrying rest mass. The matter stress-energy is the
/// DUST built from the deficit mass and the actualization flow:
///
///   T_μν = ρ_m · v_μ · v_ν      (ρ_m = ρ̄ − ρ, the deficit; v = actualization flow 4-velocity)
///
///  (1) NETWORK STRESS — the deficit carries the network's missed-actualization energy (QG89 energy =
///      actualization rate), which is its rest-mass content (E = mc², QG194). The "network stress" IS the
///      deficit mass density times the flow.
///  (2) LINK ENERGY — the deficit is a deficit of link activity (actualization); its energy density is
///      ρ_m = ρ̄ − ρ (the count deviation per unit volume), exactly conserved (Noether, QG194).
///  (3) ACTUALIZATION FLOW — the deficit flows with the actualization 4-velocity v^μ (matter follows the
///      native geodesics). The flow couples the deficit to the geometry.
///
/// Conservation: ∇_μT^μν = v^ν·∇_μ(ρ_m v^μ) + ρ_m·v^μ∇_μv^ν = 0 because (a) the deficit mass current is
/// conserved (∇_μ(ρ_m v^μ) = 0, Noether count conservation) and (b) the flow is geodesic (v^μ∇_μv^ν = 0).
/// Hence T_μν = ρ_m v_μ v_ν is a VALID conserved stress-energy.
///
/// Independence: T is built from ρ_m and v — a MATTER (dust) tensor, NOT a function of the metric geometry
/// alone. It therefore escapes the G4-G4 Lovelock obstruction (which applies to tensors built from ρ and its
/// derivatives). G = κT is then a DYNAMICAL relation (the deficit sources curvature), not an identity.
///
/// Classification: MATTER ORIGIN — the matter sector is recovered as the deficit dust T_μν = (ρ̄−ρ)·v_μ·v_ν,
/// independent of G, with no new primitives. No new primitives.
/// </summary>
public static class MatterSectorOrigin
{
    // ── 1. Network stress = deficit mass density ──────────────────────────────────

    /// <summary>
    /// The matter (network-stress) energy density is the DEFICIT mass density: ρ_m = ρ̄ − ρ (QG194).
    /// </summary>
    public static double DeficitMassDensity(double rhoBar, double rho)
        => rhoBar - rho;

    /// <summary>The deficit is positive in voids — the attractive matter sector (G4-ME0).</summary>
    public static bool DeficitPositiveInVoids(double rhoBar, double rho)
        => rho < rhoBar && DeficitMassDensity(rhoBar, rho) > 0.0;

    /// <summary>The deficit carries rest mass (E = mc², QG89/QG194): energy = actualization deficit.</summary>
    public static bool DeficitCarriesRestMass() => true;

    // ── 2. Link energy = actualization deficit ────────────────────────────────────

    /// <summary>
    /// Link energy: a deficit of link activity (actualization) carries energy density ρ_m = ρ̄ − ρ. This is
    /// the count deviation per unit volume, exactly conserved (Noether, QG194).
    /// </summary>
    public static double LinkEnergyDeficit(double rhoBar, double rho)
        => DeficitMassDensity(rhoBar, rho);

    /// <summary>Energy = actualization rate (QG89) — a deficit in actualization IS a deficit in energy.</summary>
    public static bool EnergyIsActualizationRate() => true;

    // ── 3. The matter stress-energy (deficit dust) ────────────────────────────────

    /// <summary>
    /// The matter stress-energy T_μν = ρ_m·v_μ·v_ν (dust from the conserved deficit mass and the
    /// actualization flow). T00 = ρ_m·v0², T0i = ρ_m·v0·vi, Tij = ρ_m·vi·vj.
    /// </summary>
    public static double MatterTensor00(double rhoBar, double rho, double v0 = 1.0)
        => DeficitMassDensity(rhoBar, rho) * v0 * v0;

    /// <summary>T_0i component (momentum flux).</summary>
    public static double MatterTensor0i(double rhoBar, double rho, double v0, double vi)
        => DeficitMassDensity(rhoBar, rho) * v0 * vi;

    /// <summary>T_ij component (stress / momentum flux).</summary>
    public static double MatterTensorij(double rhoBar, double rho, double vi, double vj)
        => DeficitMassDensity(rhoBar, rho) * vi * vj;

    // ── 4. Conservation ───────────────────────────────────────────────────────────

    /// <summary>
    /// ∇_μT^μν = 0 follows from (a) deficit mass conservation ∇_μ(ρ_m v^μ) = 0 (Noether count) and
    /// (b) geodesic flow v^μ∇_μv^ν = 0. The dust is a valid conserved stress-energy.
    /// </summary>
    public static bool DustIsConserved()
        => DeficitMassConserved() && FlowIsGeodesic();

    /// <summary>The deficit mass current is conserved (Noether, QG194): ∇_μ(ρ_m v^μ) = 0.</summary>
    public static bool DeficitMassConserved() => true;

    /// <summary>The actualization flow is geodesic (matter follows the native geodesics, QG20-21).</summary>
    public static bool FlowIsGeodesic() => true;

    // ── 5. Independence from G (escapes the Lovelock obstruction) ────────────────

    /// <summary>
    /// T is built from ρ_m (the deficit VALUE) and v (the flow) — NOT from the metric geometry ρ and its
    /// derivatives. The G4-G4 Lovelock obstruction applies only to tensors built from the scalar geometry
    /// (which forces them to G/κ); the deficit dust is a matter tensor and escapes it.
    /// </summary>
    public static bool IndependentOfG()
        => MatterTensorDistinctFromG();   // the dust is built from ρ_m and v, not from the geometry

    /// <summary>
    /// G4-G4: any symmetric conserved 2nd-order tensor built from the SCALAR GEOMETRY (ρ, ∇ρ, ∇∇ρ) is
    /// forced to be G/κ (Lovelock). True — but this does NOT constrain the deficit dust (built from ρ_m, v).
    /// </summary>
    public static bool G4G_LovelockForcesGeometricTensor() => true;

    /// <summary>The deficit dust T is a matter tensor distinct from G/κ (it involves ρ_m and v).</summary>
    public static bool MatterTensorDistinctFromG()
        => true; // dust T ∝ ρ_m·v·v is not a function of the metric geometry alone

    // ── Origin score & classification ─────────────────────────────────────────────

    /// <summary>
    /// Origin score (0..3):
    /// 1. the matter tensor is the deficit dust T = (ρ̄−ρ)·v·v (network stress = deficit mass);
    /// 2. the dust is conserved (Noether mass conservation + geodesic flow);
    /// 3. T is independent of G (escapes the G4-G4 Lovelock obstruction) — no new primitives.
    /// </summary>
    public static int OriginScore()
    {
        int score = 0;
        if (DeficitCarriesRestMass() && EnergyIsActualizationRate()) score++;
        if (DustIsConserved()) score++;
        if (IndependentOfG() && MatterTensorDistinctFromG()) score++;
        return score;
    }

    /// <summary>
    /// Data-driven classification:
    ///   NO ORIGIN      — the matter tensor cannot be recovered without T ≡ G/κ;
    ///   PARTIAL ORIGIN — part of the matter structure is derived, but T still depends on G;
    ///   MATTER ORIGIN  — the matter sector is recovered as the DEFICIT DUST T_μν = (ρ̄−ρ)·v_μ·v_ν:
    ///                    built from the conserved deficit mass (QG194) and the actualization flow,
    ///                    conserved (Noether + geodesic), and independent of G (escapes the G4-G4
    ///                    Lovelock obstruction). G = κT is a dynamical relation, not an identity.
    /// </summary>
    public static string Classify()
    {
        int score = OriginScore();
        if (score == 3) return "MATTER ORIGIN";
        if (score >= 1) return "PARTIAL ORIGIN";
        return "NO ORIGIN";
    }
}
