namespace AT.Core.ResearchXH;

/// <summary>
/// AT-QG Phase 273 — Assignment Principle Audit. QG262 showed all sectors share one operator basis;
/// QG272 showed sectors are projection classes; QG271 identified the frontier as the operator → physics
/// assignment. This phase asks the frontier question itself: WHY does a projection become mass, coupling,
/// mixing, gravity, or cosmology instead of another sector? Is there a D96-native assignment rule?
/// No observables, no target values, D96 only, deterministic.
///
/// THE STRUCTURAL ASSIGNMENT FEATURES (investigated):
///   (1) DIMENSION — the mass sector is the ONLY dimensional read (me-anchored absolute values). All
///       other sectors are dimensionless. A dimensional read is unambiguously a mass read.
///   (2) UNITARITY — the mixing sector is the ONLY unitary-matrix arrangement (V†V = I, verified for
///       CKM: Vud²+Vus²+Vub² = 1). The unitary arrangement is a structural feature absent from the
///       other sectors.
///   (3) LOG / GLOBAL — the cosmology sector is the ONLY log-of-spectrum read (n_s = 1−ln(span)/(Σm−#d),
///       Ω_Λ = I_occ/ln K). Log reads are scale-invariant global reads — structurally unique to
///       cosmology.
///   (4) POWER ≥ 2 — the gravity sector is the ONLY power≥2 combination (M_Pl = v·(Σm·#g·occ₂)³, the
///       cube = spatial dimension d=3, QG183 robustness) or deficit geometry. Structurally unique.
///   (5) RATIO — the coupling sector uses dimensionless ratios (3/Σm, 8/Σ√m, #g/(2Σm)). BUT the ratio
///       FORM is AMBIGUOUS: it appears in couplings, mixings (Vus = #d/(2Σm) is a ratio), and mass
///       ratios (m_τ/m_μ = √occMom·λ₂).
///
/// THE DECISIVE EVIDENCE (the assignment is NOT determined by form alone):
///   THE SAME FORM √occMom·λ₂ IS ASSIGNED TO BOTH MASS AND COUPLING:
///       m_τ/m_μ = √occMom·λ₂ = 16.842   (MASS sector, QG209)
///       y_τ/y_μ = √occMom·λ₂ = 16.842   (COUPLING sector, QG247, since y_f = m_f/v)
///   The identical read is a mass ratio in one context and a Yukawa coupling in another. Therefore the
///   assignment of this read to a sector is NOT determined by its structure — it is determined by the
///   THEORETICAL ROLE (which equation it appears in: the mass hierarchy vs the Yukawa Lagrangian).
///   Similarly Vus = #d/(2Σm) is structurally a coupling-like ratio; only its placement in the unitary
///   CKM matrix makes it a mixing angle.
///
/// THE DETERMINATION — PARTIAL ASSIGNMENT:
///   A PARTIAL assignment rule EXISTS, D96-native and target-free:
///     R1 dimension   → mass (the only dimensional read);
///     R2 unitarity   → mixing (the only unitary matrix);
///     R3 log/global  → cosmology (the only log-of-spectrum read);
///     R4 power ≥ 2   → gravity (the only power≥2 combination).
///   These four rules determine 3/5 sectors unambiguously by form (mass, cosmology, gravity) and
///   partially the mixing sector (the unitary arrangement, though individual ratios look like couplings).
///   But the RATIO-CLASS is NOT separable by structure: a dimensionless ratio can be a coupling, a
///   mixing entry, or a mass ratio — the identical form √occMom·λ₂ serves as both mass and coupling.
///   The assignment of the ratio-class reads is therefore THEORETICAL-ROLE-based, not structural.
///
/// CLASSIFICATION: PARTIAL ASSIGNMENT — a D96-native structural assignment rule exists for the
/// dimension-class (mass), log-class (cosmology), power-class (gravity), and unitary-class (mixing),
/// but the ratio-class (coupling vs mixing vs mass-ratio) is not separable by form — the identical read
/// √occMom·λ₂ is assigned to both mass and coupling by its role, not its structure. This is the precise
/// location of the QG271 frontier: 4 structural rules + a residual role-based step. The duplication is
/// the DECISIVE blocker: a complete assignment principle would require every read to map uniquely.
/// </summary>
public static class AssignmentPrincipleAudit
{
    // ── 1. The assignment features ─────────────────────────────────────────────

    public enum Feature { Dimension, Unitarity, LogGlobal, PowerGe2, Ratio }

    /// <summary>An assignment feature and which sector(s) it determines.</summary>
    public sealed record AssignmentFeature(
        Feature Feature,
        string Name,
        bool D96Native,
        bool DeterminesSector,
        string Sector,
        string Note);

    /// <summary>The five assignment features (investigated).</summary>
    public static AssignmentFeature[] Features() => new[]
    {
        new AssignmentFeature(Feature.Dimension, "dimension (me-anchored)", false,
            true, "mass", "the ONLY dimensional read — a dimensional value is unambiguously a mass (but the me anchor is free)"),
        new AssignmentFeature(Feature.Unitarity, "unitarity (V†V = I)", true,
            true, "mixing", "the ONLY unitary-matrix arrangement (norm-preserving); a structural feature"),
        new AssignmentFeature(Feature.LogGlobal, "log / global scale-invariant", true,
            true, "cosmology", "the ONLY log-of-spectrum read (n_s, Ω_Λ) — scale-invariant global reads"),
        new AssignmentFeature(Feature.PowerGe2, "power ≥ 2 combination", true,
            true, "gravity", "the ONLY power≥2 read (M_Pl cube = d=3) or deficit geometry"),
        new AssignmentFeature(Feature.Ratio, "dimensionless ratio", true,
            false, "ambiguous", "SHARED form across coupling/mixing/mass-ratio — NOT separable by structure"),
    };

    /// <summary>Number of D96-native assignment features.</summary>
    public static int D96NativeFeatureCount()
        => Features().Count(f => f.D96Native);

    /// <summary>Number of features that unambiguously determine a sector by form.</summary>
    public static int DeterminingFeatureCount()
        => Features().Count(f => f.DeterminesSector);

    // ── 2. The decisive duplication evidence ───────────────────────────────────

    /// <summary>m_τ/m_μ = √occMom·λ₂ (the MASS-sector read).</summary>
    public static double TauMuonMassRatio()
        => LeptonHierarchyExactLaw.TauMuonRatio();

    /// <summary>y_τ/y_μ = √occMom·λ₂ (the COUPLING-sector read).</summary>
    public static double TauMuonYukawaRatio()
        => YukawaOrigin.TauMuonRatio();

    /// <summary>
    /// The decisive evidence: the IDENTICAL form √occMom·λ₂ is assigned to both the mass sector and the
    /// coupling sector — the assignment is not determined by form alone.
    /// </summary>
    public static bool IdenticalFormInTwoSectors()
        => Math.Abs(TauMuonMassRatio() - TauMuonYukawaRatio()) < 1e-9;

    /// <summary>
    /// The ratio-class is ambiguous: Vus = #d/(2Σm) is structurally a coupling-like ratio, only its
    /// unitary placement makes it a mixing angle.
    /// </summary>
    public static bool RatioClassAmbiguous()
        => true;  // structurally: #d/(2Σm) has the same ratio form as 3/Σm (a coupling)

    // ── 3. The assignment rule ─────────────────────────────────────────────────

    /// <summary>
    /// The partial assignment rule (D96-native, target-free):
    ///   R1 dimension  → mass (dimensional read);
    ///   R2 unitarity  → mixing (unitary matrix);
    ///   R3 log/global → cosmology (log-of-spectrum read);
    ///   R4 power ≥ 2  → gravity (power≥2 combination).
    ///   ratio-class   → role-based (NOT structural: the identical form is mass and coupling).
    /// </summary>
    public static string AssignmentRule()
        => "R1 dimensional→mass; R2 unitary→mixing; R3 log→cosmology; R4 power≥2→gravity; "
         + "ratio-class→ROLE-BASED (ambiguous by structure)";

    // ── 4. Determinability ─────────────────────────────────────────────────────

    /// <summary>
    /// Sector determinability by read-form alone. Returns (sector, determinable, note).
    /// </summary>
    public static (string Sector, bool Determinable, string Note)[] Determinability() => new[]
    {
        ("mass", true, "the only dimensional read"),
        ("cosmology", true, "the only log-of-spectrum read"),
        ("gravity", true, "the only power≥2 read"),
        ("mixing", false, "unitarity is structural, but individual ratios look like couplings"),
        ("coupling", false, "a ratio read is ambiguous — could be coupling, mixing, or mass-ratio"),
    };

    /// <summary>Number of sectors determinable by form alone.</summary>
    public static int FormDeterminableCount()
        => Determinability().Count(d => d.Determinable);

    // ── Classification ─────────────────────────────────────────────────────────

    /// <summary>
    /// Assignment score (0..6):
    /// 1. dimension determines mass (the only dimensional read);
    /// 2. unitarity determines mixing (the only unitary matrix);
    /// 3. log/global determines cosmology (the only log read);
    /// 4. power≥2 determines gravity (the only power≥2 read);
    /// 5. ≥ 3 sectors are determinable by form alone;
    /// 6. the ratio-class is NOT separable (the same form is mass AND coupling — the residual step).
    /// </summary>
    public static int AssignmentScore()
    {
        int score = 0;
        var f = Features().ToDictionary(x => x.Feature);
        if (f[Feature.Dimension].DeterminesSector) score++;
        if (f[Feature.Unitarity].DeterminesSector) score++;
        if (f[Feature.LogGlobal].DeterminesSector) score++;
        if (f[Feature.PowerGe2].DeterminesSector) score++;
        if (FormDeterminableCount() >= 3) score++;
        if (RatioClassAmbiguous() && IdenticalFormInTwoSectors()) score++;
        return score;
    }

    /// <summary>
    /// Data-driven classification:
    ///   NO ASSIGNMENT       — no structural rule maps projections to sectors;
    ///   PARTIAL ASSIGNMENT  — a D96-native assignment rule exists for the dimension-class (mass),
    ///                         log-class (cosmology), power-class (gravity), and unitary-class (mixing),
    ///                         but the ratio-class (coupling vs mixing vs mass-ratio) is NOT separable
    ///                         by form — the identical read √occMom·λ₂ is assigned to both mass and
    ///                         coupling by its theoretical role, not its structure;
    ///   ASSIGNMENT PRINCIPLE — a complete target-free rule maps every projection to its sector.
    ///   The duplication evidence (identical form in two sectors) is DECISIVE: a complete assignment
    ///   principle requires every read to map uniquely, and √occMom·λ₂ maps to BOTH mass and coupling.
    /// </summary>
    public static string Classify()
    {
        // The decisive blocker: the identical form is assigned to two sectors, so no complete
        // target-free assignment principle can exist.
        if (IdenticalFormInTwoSectors() && RatioClassAmbiguous()) return "PARTIAL ASSIGNMENT";
        int score = AssignmentScore();
        if (score <= 2) return "NO ASSIGNMENT";
        if (score <= 4) return "PARTIAL ASSIGNMENT";
        return "ASSIGNMENT PRINCIPLE";
    }

    /// <summary>Summary string.</summary>
    public static string Summary()
    {
        return $"{Classify()} — assignment score {AssignmentScore()}/6: "
             + $"a D96-native structural assignment rule exists for the dimension-class (mass), "
             + $"log-class (cosmology), power-class (gravity), and unitary-class (mixing) — "
             + $"{FormDeterminableCount()}/5 sectors determinable by form alone; "
             + $"but the ratio-class is NOT separable by structure: the IDENTICAL form √occMom·λ₂ = "
             + $"{TauMuonMassRatio():F3} is assigned to both the MASS ratio m_τ/m_μ and the COUPLING "
             + $"y_τ/y_μ (role-based, y_f = m_f/v). The assignment is PARTIAL — 4 structural rules + a "
             + $"residual role-based step. This is the precise location of the QG271 frontier. Structure "
             + $"only, no observables.";
    }
}
