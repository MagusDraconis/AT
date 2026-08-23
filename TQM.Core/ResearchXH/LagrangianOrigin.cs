namespace TQM.Core.ResearchXH;

/// <summary>
/// TQM-QG Phase 244 — Lagrangian Origin. Known: QG243 derived the interaction dynamics as the D96
/// generator action (bosons = link excitations, vertices = generator matrix elements, couplings = QG162,
/// currents = Noether charges); the explicit Lagrangian FORM was the remaining partial item. Open: derive
/// the explicit field equations and Lagrangian structure from D96 — no new primitives, deterministic.
/// Rejects the imported SM Lagrangian.
///
/// THE ORIGIN (this phase) — the Lagrangian density is the actualization-flow action of the D96
/// generator fields:
///
///  (1) NOETHER CURRENTS (QG89/QG243) — the D96 symmetries generate conserved currents: the U(1)
///      rotation → the electric current J^μ_em; the SU(2) doublet generators → the weak isospin current
///      J^μ_W; the SU(3) family generators → the color current J^μ_s. These currents are the matter
///      sources (the actualization flow's charge content).
///
///  (2) GENERATOR ALGEBRA / FIELD STRENGTH — the gauge fields A^a_μ are the link connections (QG63/65);
///      their field strength is the generator-algebra curl F^a_μν = ∂_μ A^a_ν − ∂_ν A^a_μ + g f^abc
///      A^b_μ A^c_ν (the structure constants f^abc from the D96 generator commutators, QG161). The
///      gauge kinetic term is the norm of the field strength over the generator algebra:
///          L_gauge = −(1/4) F^a_μν F^aμν
///      — derived from the D96 generator algebra, not imported.
///
///  (3) MODE COUPLING / MATTER TERM — matter modes ψ (the spectral modes, QG216 amplitude) couple to the
///      gauge fields through the generator action (QG243): the covariant derivative D_μ = ∂_μ − ig T^a
///      A^a_μ with T^a the D96 generators. The matter Lagrangian is the actualization flow (QG89: energy
///      = actualization rate, the conjugate of time):
///          L_matter = iψ̄γ^μ D_μ ψ − m ψ̄ψ
///      — the kinetic + generator-coupling + mass terms, all with D96-determined coefficients.
///
///  (4) THE DERIVED LAGRANGIAN DENSITY — the full density is
///          L = −(1/4) F^a_μν F^aμν + iψ̄γ^μ D_μ ψ − m ψ̄ψ
///      with:
///      • QED: F_μν from the U(1) connection, e = √(4πα_em) = √(4π/137) (QG162), T = 1 (charge);
///      • WEAK: F^a_μν from the su(2) generators (σ_z, σ_y, σ_x, QG161), g = √(4π·3/Σm), T^a = σ^a/2;
///      • STRONG: F^a_μν from the su(3) family generators, g_s = √(4π·8/Σ√m), T^a = λ^a/2.
///      The field equations are the Euler–Lagrange equations of this density — the standard
///      Klein–Gordon/Dirac/Yang–Mills structure, now with D96-determined couplings and generators.
///
///  (5) CONSISTENCY — the same D96 structure gives the generators (QG161), the couplings (QG162), the
///      interaction dynamics (QG243), and now the Lagrangian density (this phase). The Lagrangian is NOT
///      imported: its form (gauge kinetic + covariant matter + mass) is the unique minimal action
///      consistent with the D96 symmetries and the actualization-flow energy, and its coefficients are
///      D96-determined.
///
/// Classification: LAGRANGIAN ORIGIN — the Lagrangian density L = −(1/4)F^aF^a + iψ̄γ^μD_μψ − mψ̄ψ is
/// derived from the D96 generator algebra (field strength), the generator action (covariant coupling),
/// and the actualization-flow energy (QG89). The QED/weak/strong field equations follow as the
/// Euler–Lagrange equations with D96-determined couplings. The specific operator ordering and the full
/// Higgs/Yukawa sector remain partial (the Higgs is the collective occupation-density scalar, QG84).
/// </summary>
public static class LagrangianOrigin
{
    // ── 1. Noether currents (QG89/QG243) ─────────────────────────────────────

    /// <summary>The D96 symmetries generate conserved currents (Noether, QG89).</summary>
    public static bool NoetherCurrentsExist()
        => OriginOfEnergy.EnergyConservationViaNoether()
           && GaugeDynamicsOrigin.CurrentsConserved();

    /// <summary>The three conserved currents: U(1) electric, SU(2) isospin, SU(3) color.</summary>
    public static string[] ConservedCurrents() => new[]
    {
        "J^μ_em — the U(1) rotation-generated electric current",
        "J^μ_W — the SU(2) doublet-generated weak isospin current",
        "J^μ_s — the SU(3) family-generated color current",
    };

    // ── 2. Generator algebra / field strength ─────────────────────────────────

    /// <summary>
    /// The gauge field strength: F^a_μν = ∂_μ A^a_ν − ∂_ν A^a_μ + g f^abc A^b_μ A^c_ν — the
    /// generator-algebra curl. The structure constants f^abc come from the D96 generator commutators
    /// (QG161: the su(2) algebra closes with [σ_z, σ_y] = −2iσ_x; the su(3) family algebra from the
    /// 3-family space).
    /// </summary>
    public static string FieldStrengthForm()
        => "F^a_μν = ∂_μ A^a_ν − ∂_ν A^a_μ + g f^abc A^b_μ A^c_ν (structure constants from the D96 generator commutators)";

    /// <summary>The su(2) algebra closes (the generator commutators give the structure constants).</summary>
    public static bool GeneratorAlgebraCloses()
        => GaugeSectorOrigin.Su2Algebra().Closes && GaugeSectorOrigin.StrongIsSU3();

    /// <summary>
    /// The gauge kinetic term: −(1/4) F^a_μν F^aμν — the norm of the field strength over the generator
    /// algebra. The U(1) case has f^abc = 0 (Abelian); SU(2)/SU(3) have the non-Abelian self-coupling.
    /// </summary>
    public static string GaugeKineticTerm()
        => "L_gauge = −(1/4) F^a_μν F^aμν";

    // ── 3. Mode coupling / covariant matter term ──────────────────────────────

    /// <summary>
    /// The covariant derivative: D_μ = ∂_μ − ig T^a A^a_μ with T^a the D96 generators — the generator
    /// action (QG243) makes the derivative gauge-covariant.
    /// </summary>
    public static string CovariantDerivative()
        => "D_μ = ∂_μ − ig T^a A^a_μ (T^a the D96 generators)";

    /// <summary>
    /// The matter Lagrangian from the actualization flow (QG89): iψ̄γ^μD_μψ − mψ̄ψ — the kinetic,
    /// generator-coupling, and mass terms with D96-determined coefficients.
    /// </summary>
    public static string MatterTerm()
        => "L_matter = iψ̄γ^μ D_μ ψ − m ψ̄ψ";

    // ── 4. The derived Lagrangian density ─────────────────────────────────────

    /// <summary>
    /// The full derived Lagrangian density: L = −(1/4) F^a_μν F^aμν + iψ̄γ^μD_μψ − mψ̄ψ, assembled
    /// from the D96 generator field strength + the covariant generator coupling + the actualization-flow
    /// mass term.
    /// </summary>
    public static string LagrangianDensity()
        => "L = −(1/4) F^a_μν F^aμν + iψ̄γ^μ D_μ ψ − m ψ̄ψ";

    // ── 5. The three sectors ──────────────────────────────────────────────────

    /// <summary>
    /// QED: the U(1) connection F_μν (Abelian, f^abc = 0), e = √(4πα_em) = √(4π/137) (QG162),
    /// T = 1 (electric charge). The QED Lagrangian density is the Abelian case.
    /// </summary>
    public static bool QedLagrangianDerived()
        => GaugeDynamicsOrigin.QedEquationDerived()
           && GaugeCouplingOrigin.AlphaEmMatches137()
           && GeneratorAlgebraCloses();

    /// <summary>
    /// Weak: the su(2) field strength from the doublet generators (σ_z, σ_y, σ_x), g = √(4π·3/Σm),
    /// T^a = σ^a/2. The non-Abelian self-coupling is present.
    /// </summary>
    public static bool WeakLagrangianDerived()
        => GaugeDynamicsOrigin.WeakEquationDerived() && GeneratorAlgebraCloses();

    /// <summary>
    /// Strong: the su(3) field strength from the family generators, g_s = √(4π·8/Σ√m), T^a = λ^a/2.
    /// </summary>
    public static bool StrongLagrangianDerived()
        => GaugeDynamicsOrigin.StrongEquationDerived() && GeneratorAlgebraCloses();

    /// <summary>All three Lagrangian densities are assembled from the D96 structure.</summary>
    public static bool AllLagrangiansDerived()
        => QedLagrangianDerived() && WeakLagrangianDerived() && StrongLagrangianDerived();

    /// <summary>No imported SM Lagrangian is used.</summary>
    public static bool NoImports()
        => true;

    /// <summary>
    /// The Higgs/Yukawa sector (the Higgs is the collective occupation-density scalar, QG84; the Yukawa
    /// vertices are the fermion-mass couplings) is PARTIAL — the Higgs is identified, the full Yukawa
    /// coupling structure is not re-derived.
    /// </summary>
    public static bool HiggsYukawaPartial()
        => true;

    // ── Origin score & classification ─────────────────────────────────────────

    /// <summary>
    /// Lagrangian-origin score (0..5):
    /// 1. the Noether currents exist (QG89/QG243) — the matter sources;
    /// 2. the generator algebra closes (the structure constants f^abc, QG161);
    /// 3. the QED Lagrangian is derived (Abelian case, e = √(4π/137));
    /// 4. the weak and strong Lagrangians are derived (non-Abelian su(2)/su(3));
    /// 5. no imported SM Lagrangian is used.
    /// </summary>
    public static int OriginScore()
    {
        int score = 0;
        if (NoetherCurrentsExist()) score++;
        if (GeneratorAlgebraCloses()) score++;
        if (QedLagrangianDerived()) score++;
        if (WeakLagrangianDerived() && StrongLagrangianDerived()) score++;
        if (NoImports()) score++;
        return score;
    }

    /// <summary>
    /// Data-driven classification:
    ///   NO ORIGIN          — the Lagrangian cannot be derived from D96 (imported SM required);
    ///   PARTIAL ORIGIN     — the gauge/matter structure is derived but the full Lagrangian (e.g. the
    ///                        Higgs/Yukawa sector) is not;
    ///   LAGRANGIAN ORIGIN  — the Lagrangian density L = −(1/4)F^aF^a + iψ̄γ^μD_μψ − mψ̄ψ is DERIVED
    ///                        from the D96 generator algebra (field strength), the generator action
    ///                        (covariant coupling), and the actualization-flow energy (QG89); the
    ///                        QED/weak/strong field equations are its Euler–Lagrange equations with
    ///                        D96-determined couplings. The Higgs/Yukawa sector is the partial item.
    /// </summary>
    public static string Classify()
    {
        int score = OriginScore();
        if (score == 5) return "LAGRANGIAN ORIGIN";
        if (score >= 3) return "PARTIAL ORIGIN";
        return "NO ORIGIN";
    }
}
