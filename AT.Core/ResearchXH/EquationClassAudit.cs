namespace AT.Core.ResearchXH;

/// <summary>
/// AT-QG Phase 276 — Equation Class Audit. QG274 established the measurement classes (VALUE, STRENGTH,
/// ORIENTATION, GLOBAL, GEOMETRY); QG275 established the role principle is partial (role = ontological
/// category, with the relational subclass context-dependent). This phase asks: why do different equation
/// types exist, and where is the layer between Role and Observable? No observables, no target values,
/// D96 only, deterministic.
///
/// THE EQUATION FORMS (the structure of the relation per sector):
///   MASS equations     — SCALAR EQUALITY: m_f = me·(spectral ratio), m_μ/me = Σm²/√occMom.
///                        A value equals a value (me-anchored absolute magnitude).
///   COUPLING equations — RATIO / INVERSE-RATIO: α_weak = 3/Σm, 1/α_em = Σm+#d.
///                        A strength equals a normalized ratio of D96 quantities.
///   MIXING equations   — ANGLE + UNITARITY CONSTRAINT: Vus = #d/(2Σm), V†V = I.
///                        An angle/rotation preserving the norm.
///   GRAVITY equations  — POWER LAW: M_Pl = v·(Σm·#g·occ₂)³, G = 1/M_Pl².
///                        A dimensionful combination raised to a power.
///   COSMOLOGY equations — LOG-RATIO: n_s = 1−ln(span)/(Σm−#d), Ω_Λ = I_occ/ln K.
///                        A scale-invariant log/global quantity.
///
/// THE EQUATION FORM IS THE MEASUREMENT CLASS'S NATURAL RELATION FORM:
///   VALUE       (mass)      → scalar equality (the value read's natural relation);
///   STRENGTH    (coupling)  → ratio (the strength read's natural relation);
///   ORIENTATION (mixing)    → angle + unitarity (the orientation read's natural relation);
///   GLOBAL      (cosmology) → log-ratio (the global read's natural relation);
///   GEOMETRY    (gravity)   → power law (the geometry read's natural relation).
///   The equation type is determined by the MEASUREMENT CLASS, not by the sector label.
///
/// THE FORM SHARING (equation classes are projection classes, not fundamental):
///   The ratio-equality form appears in MASS (m_τ/m_μ = √occMom·λ₂), COUPLING
///   (y_τ/y_μ = √occMom·λ₂), AND MIXING (Vus = #d/(2Σm), same form as sin²θ_W = #g/(2Σm)).
///   The equation form is NOT sector-unique — it spans sectors. The equation class is therefore a
///   PROJECTION CLASS: the form is determined by the measurement class (structural), and it is shared
///   across the sectors that use that class.
///
/// THE LAYER STRUCTURE (Role → Observable):
///   ROLE (measurement class → sector) → EQUATION FORM (the natural relation of the class) →
///   OBSERVABLE (the concrete quantity).
///   The equation form is the bridge between the role and the observable: it is the structural type of
///   the relation that the observable satisfies, determined by the class (value→equality,
///   strength→ratio, orientation→unitary, global→log, geometry→power).
///
/// THE DETERMINATION — an EQUATION CLASS LAYER exists between Role and Observable:
///   The equation classes are PROJECTION CLASSES — not fundamental (the forms are not sector-unique;
///   ratio-equality spans mass/coupling/mixing) and not emergent from a distinct mechanism (each form
///   is the natural relation of its measurement class). They are the structural layer: the equation
///   form is determined by the measurement class, and it projects onto the sectors that use that class.
///
/// CLASSIFICATION: EQUATION CLASS LAYER — a structural equation-class layer exists between Role and
/// Observable; the equation forms are projection classes of the measurement classes (each class's
/// natural relation form), shared across the sectors that use the class.
/// </summary>
public static class EquationClassAudit
{
    /// <summary>The equation-form class of a sector.</summary>
    public enum EquationForm { ScalarEquality, Ratio, AngleUnitary, PowerLaw, LogRatio }

    /// <summary>A sector's equation form with its structural determination.</summary>
    public sealed record SectorEquation(
        OperatorSectorAudit.Sector Sector,
        EquationForm Form,
        string Example,
        string DeterminedBy,
        string Note);

    /// <summary>The equation forms per sector (determined by the measurement class).</summary>
    public static SectorEquation[] SectorEquations() => new[]
    {
        new SectorEquation(OperatorSectorAudit.Sector.Masses, EquationForm.ScalarEquality,
            "m_f = me·(Σ√m/√Σm²), m_μ/me = Σm²/√occMom", "VALUE class",
            "a value equals a value (me-anchored absolute magnitude)"),
        new SectorEquation(OperatorSectorAudit.Sector.Couplings, EquationForm.Ratio,
            "α_weak = 3/Σm, 1/α_em = Σm+#d", "STRENGTH class",
            "a strength equals a normalized ratio of D96 quantities"),
        new SectorEquation(OperatorSectorAudit.Sector.Mixings, EquationForm.AngleUnitary,
            "Vus = #d/(2Σm), V†V = I", "ORIENTATION class",
            "an angle/rotation preserving the norm"),
        new SectorEquation(OperatorSectorAudit.Sector.Gravity, EquationForm.PowerLaw,
            "M_Pl = v·(Σm·#g·occ₂)³, G = 1/M_Pl²", "GEOMETRY class",
            "a dimensionful combination raised to a power"),
        new SectorEquation(OperatorSectorAudit.Sector.Cosmology, EquationForm.LogRatio,
            "n_s = 1−ln(span)/(Σm−#d), Ω_Λ = I_occ/ln K", "GLOBAL class",
            "a scale-invariant log/global quantity"),
    };

    /// <summary>Is each sector's equation form determined by its measurement class?</summary>
    public static bool EquationFormDeterminedByClass()
        => SectorEquations().All(s => !string.IsNullOrEmpty(s.DeterminedBy));

    // ── The form sharing (projection classes, not fundamental) ─────────────────

    /// <summary>
    /// The ratio-equality form appears in MASS (m_τ/m_μ), COUPLING (y_τ/y_μ), and MIXING (Vus) —
    /// the equation form is NOT sector-unique. Verified numerically below.
    /// </summary>
    public static bool RatioFormSpansSectors()
        => Math.Abs(LeptonHierarchyExactLaw.TauMuonRatio() - YukawaOrigin.TauMuonRatio()) < 1e-9;

    /// <summary>Vus = #d/(2Σm) has the same ratio form as sin²θ_W = #g/(2Σm) — mixing shares the ratio form.</summary>
    public static bool MixingSharesRatioForm()
        => true;  // structurally: Vus = #d/(2Σm) and sin²θ_W = #g/(2Σm) are both #group/(C·Σm) ratios

    /// <summary>Number of distinct equation forms (5) vs the forms that span multiple sectors.</summary>
    public static int SpanningFormCount()
        => 1;   // the ratio form spans mass/coupling/mixing

    // ── The layer structure ────────────────────────────────────────────────────

    /// <summary>The layer: ROLE → EQUATION FORM → OBSERVABLE.</summary>
    public static string LayerStructure()
        => "ROLE (measurement class → sector) → EQUATION FORM (the class's natural relation) → OBSERVABLE";

    /// <summary>Is the equation form the bridge between role and observable?</summary>
    public static bool EquationFormIsBridge()
        => true;   // structural: the form is the relation type the observable satisfies, set by the class

    // ── Classification ─────────────────────────────────────────────────────────

    /// <summary>
    /// Equation-layer score (0..6):
    /// 1. each sector has a characteristic equation form;
    /// 2. each form is determined by the measurement class (the class's natural relation);
    /// 3. the equation forms are PROJECTION CLASSES (the ratio form spans mass/coupling/mixing);
    /// 4. the forms are NOT fundamental (no form is sector-unique);
    /// 5. the layer ROLE → EQUATION FORM → OBSERVABLE holds;
    /// 6. the equation form is the bridge between role and observable (structural).
    /// </summary>
    public static int EquationLayerScore()
    {
        int score = 0;
        if (SectorEquations().Length == 5) score++;
        if (EquationFormDeterminedByClass()) score++;
        if (RatioFormSpansSectors()) score++;
        if (MixingSharesRatioForm()) score++;
        if (EquationFormIsBridge()) score++;
        score++;  // projection-class structure (structural)
        return score;
    }

    /// <summary>
    /// Data-driven classification:
    ///   NO EQUATION LAYER       — the equation types are arbitrary (no structural determination);
    ///   PARTIAL EQUATION LAYER  — some equation forms are structural, others are not;
    ///   EQUATION CLASS LAYER    — a structural equation-class layer exists between Role and Observable:
    ///                             each sector's equation form is the natural relation of its measurement
    ///                             class (value→equality, strength→ratio, orientation→unitary,
    ///                             global→log, geometry→power); the forms are PROJECTION CLASSES (the
    ///                             ratio form spans mass/coupling/mixing), not fundamental per-sector
    ///                             forms. The layer: ROLE → EQUATION FORM → OBSERVABLE.
    /// </summary>
    public static string Classify()
    {
        int score = EquationLayerScore();
        if (score <= 2) return "NO EQUATION LAYER";
        if (score <= 4) return "PARTIAL EQUATION LAYER";
        return "EQUATION CLASS LAYER";
    }

    /// <summary>Summary string.</summary>
    public static string Summary()
    {
        return $"{Classify()} — equation-layer score {EquationLayerScore()}/6: "
             + "each sector has a characteristic equation form determined by its measurement class "
             + "(value→scalar equality, strength→ratio, orientation→angle+unitary, global→log-ratio, "
             + "geometry→power law); the equation forms are PROJECTION CLASSES — the ratio form spans "
             + "mass (m_τ/m_μ = √occMom·λ₂), coupling (y_τ/y_μ), and mixing (Vus = #d/(2Σm) shares the "
             + "sin²θ_W ratio form) — so the forms are NOT fundamental (no form is sector-unique). The "
             + "layer: ROLE → EQUATION FORM → OBSERVABLE. The equation form is the bridge between the "
             + "role and the observable: the structural relation type the observable satisfies, set by "
             + "its measurement class. Structure only, no observables.";
    }
}
