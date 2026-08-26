namespace AT.Core.ResearchXH;

/// <summary>
/// AT-QG Phase 242 — Standard Model Dynamics Audit. Determines whether SM dynamics are actually DERIVED
/// or only HOSTED. Reviews QG60 (SM compatibility), QG76 (completeness), QG78-85 (color/SU(3)/
/// generations/flavor/Higgs/SM parameters), QG149-180 (the D96 mass/coupling derivation era). Classifies
/// gauge interactions, gauge symmetry origin, SU(3)/SU(2)/U(1) origin, and interaction vertices.
/// Audit only — no new physics.
///
/// THE SIX CHECKS:
///  1. GAUGE SYMMETRY ORIGIN — DERIVED. QG161 (GAUGE ORIGIN): the observable-sector automorphism group is
///     D96; the 1+3+8 = 12 gauge generators are derived from the D96 spectral geometry (rotation subgroup
///     Z_96 = U(1); the 2D irreps restricted to a doublet span su(2) with exactly 3 generators; the 3
///     octave families give su(3) with 3²−1 = 8). The 12 link-directions of the C_96(1..6) circulant ARE
///     the 12 gauge generators.
///  2. U(1) ORIGIN — DERIVED. QG161: the rotation subgroup Z_96 ⊂ D96 is the U(1) charge (photon), the
///     unique neutral global generator.
///  3. SU(2) ORIGIN — DERIVED. QG161: restricted to a Z2 doublet, the D96 generators span su(2) —
///     reflection = σ_z (isospin T3), rotation generator = σ_y, commutator = σ_x; exactly 3 generators
///     (the weak sector). The algebra closes (Su2Algebra().Closes verified).
///  4. SU(3) ORIGIN — PARTIAL. QG161 derives su(3) (3²−1 = 8 generators) from the 3 octave families; but
///     QG79 (WhySU3) notes the 3-COLOR COUNT itself was a NEW POSTULATE pre-D96 (forced by fermion
///     statistics, the Δ++ uuu antisymmetrization); given 3 colors, SU(3) is PREFERRED (unique), and QG161
///     now derives the 8-generator structure from the 3 families. So the SU(3) STRUCTURE is derived from
///     D96, but the identification of the 3-family space with the color space retains a postulate trace.
///  5. GAUGE INTERACTIONS (the dynamics) — HOSTED. QG60 (SM compatibility) and QG76 (completeness) classify
///     gauge theory as COMPATIBLE/HOSTED: the network HOSTS the gauge structure (the 12 generators, the
///     gauge-group form) but the INTERACTION DYNAMICS — the Lagrangian, the Feynman vertices, the
///     propagators, the strength and momentum dependence of the couplings — is NOT derived from Q-events.
///     The couplings' VALUES are derived (QG162/163: 1/α_em = 137, α_s = 8/Σ√m), but the dynamical
///     equations that generate the interactions are imported/hosted.
///  6. INTERACTION VERTICES — OPEN. No QG phase derives the specific interaction vertices (e.g. γ-e-e,
///     W-u-d, gluon-quark, the Higgs Yukawa vertices) as dynamical consequences of the network. The
///     vertices are the missing dynamics — the actual "gauge interactions" content.
///
/// SUMMARY: 3 DERIVED (gauge symmetry origin, U(1), SU(2)), 1 PARTIAL (SU(3) — structure derived, color-
/// count identification retains a postulate trace), 1 HOSTED (gauge interactions/dynamics), 1 OPEN
/// (interaction vertices). Derived fraction 3/6, weighted (0.5 SU3 + 0.75 hosted) = 4.25/6 ≈ 71%.
///
/// EXACT MISSING DYNAMICS:
///  (a) the gauge INTERACTION LAGRANGIAN / equations of motion (QG60/76 host the structure, not the
///      dynamics);
///  (b) the INTERACTION VERTICES (γ-e-e, W-u-d, gluon-quark, Yukawa) — no QG phase derives them;
///  (c) the PROPAGATORS / momentum dependence of the interactions;
///  (d) the SU(3)-color-count identification with the 3-family space (QG79 postulate trace).
///
/// CONCLUSION: SM dynamics are NOT fully derived — the gauge SYMMETRY (generator structure) IS derived
/// (QG161), but the interaction DYNAMICS and VERTICES remain HOSTED/OPEN. This is the exact content of the
/// QG241 "SM dynamics" partial criterion.
/// </summary>
public static class StandardModelDynamicsAudit
{
    public enum Status { Derived, Hosted, Partial, Open }

    /// <summary>A dynamics check.</summary>
    public sealed record Check(
        string Name,
        Status Status,
        string Evidence);

    /// <summary>The six dynamics checks.</summary>
    public static Check[] Checks() => new[]
    {
        new Check("Gauge symmetry origin", Status.Derived,
            "QG161 (GAUGE ORIGIN): the D96 automorphism group gives 1+3+8 = 12 generators; the 12 link-directions of C_96(1..6) ARE the 12 gauge generators"),
        new Check("U(1) origin", Status.Derived,
            "QG161: the rotation subgroup Z_96 ⊂ D96 is the U(1) charge (photon) — the unique neutral global generator"),
        new Check("SU(2) origin", Status.Derived,
            "QG161: restricted to a Z2 doublet the D96 generators span su(2) — reflection = σ_z (T3), rotation generator = σ_y, commutator = σ_x; exactly 3 generators, algebra closes"),
        new Check("SU(3) origin", Status.Partial,
            "QG161 derives su(3) (3²−1 = 8) from the 3 octave families; but QG79 notes the 3-COLOR COUNT was a NEW POSTULATE pre-D96 (fermion statistics); given 3 colors, SU(3) is PREFERRED — structure derived, color-count identification retains a postulate trace"),
        new Check("Gauge interactions (dynamics)", Status.Hosted,
            "QG60/QG76: gauge theory is COMPATIBLE/HOSTED — the network hosts the 12-generator structure, but the interaction LAGRANGIAN, vertices, and propagators are not derived from Q-events; the coupling VALUES are derived (QG162/163) but not the dynamics"),
        new Check("Interaction vertices", Status.Open,
            "no QG phase derives the specific vertices (γ-e-e, W-u-d, gluon-quark, Higgs Yukawa) as dynamical consequences — this is the missing dynamics content"),
    };

    /// <summary>Status counts.</summary>
    public static IReadOnlyDictionary<Status, int> StatusCounts()
    {
        var dict = Checks().GroupBy(c => c.Status).ToDictionary(g => g.Key, g => g.Count());
        foreach (Status s in Enum.GetValues<Status>())
            if (!dict.ContainsKey(s)) dict[s] = 0;
        return dict;
    }

    /// <summary>The exact missing dynamics.</summary>
    public static string[] MissingDynamics()
        => new[]
        {
            "the gauge interaction LAGRANGIAN / equations of motion (QG60/76 host the structure, not the dynamics)",
            "the INTERACTION VERTICES (γ-e-e, W-u-d, gluon-quark, Higgs Yukawa) — no QG phase derives them",
            "the PROPAGATORS / momentum dependence of the interactions",
            "the SU(3)-color-count identification with the 3-family space (QG79 postulate trace)",
        };

    /// <summary>
    /// Summary: 3 DERIVED (gauge symmetry origin, U(1), SU(2)), 1 PARTIAL (SU(3)), 1 HOSTED (gauge
    /// interactions), 1 OPEN (interaction vertices). The gauge SYMMETRY is derived; the DYNAMICS is not.
    /// </summary>
    public static string Summary()
    {
        var sc = StatusCounts();
        return $"SM dynamics: {sc[Status.Derived]} DERIVED / {sc[Status.Hosted]} HOSTED / "
             + $"{sc[Status.Partial]} PARTIAL / {sc[Status.Open]} OPEN — "
             + "the gauge SYMMETRY (generator structure) is derived (QG161), the interaction DYNAMICS and "
             + "VERTICES remain HOSTED/OPEN";
    }
}
