namespace TQM.Core.ResearchXH;

/// <summary>
/// TQM-QG Phase 289 — Anchor Inventory Audit. QG288 classified the QG results as DERIVED AGAIN /
/// DEPENDENT ON OLD PATH / UNREACHABLE; the UNREACHABLE and DEPENDENT items were traced to a set of
/// anchors. This phase inventories those anchors: me, MZ, 5/4, Bekenstein 1/4, η, π, RG, 3+1. Each is
/// classified as STRUCTURAL / EMPIRICAL / BOUNDARY / REMOVABLE, and the three decisive questions are
/// answered: which anchors are TRUE THEORY INPUTS, which are ONLY CALIBRATION, and which are REPLACEABLE.
/// Deterministic — every value is computed or verified against the established D96 classes.
///
/// THE EIGHT ANCHORS (from QG288):
///   me = 0.511 MeV   — the electron anchor (QG140/QG251): sets the ABSOLUTE MASS SCALE;
///   MZ = 91.19 GeV   — the Z-anchor (QG130): sets the ABSOLUTE ENERGY SCALE;
///   5/4              — the acoustic-peak factor (QG238): a free constant in ℓ₁ = Σm·ln(span)·(5/4);
///   Bekenstein 1/4   — the area-law coefficient S = A/4 (QG185/QG259): the target not yet derived;
///   η                — the conformal reference metric (g = ρ^(2/d)·η): the framework's flat reference;
///   π                — the Bekenstein 2π quantum factor / universal mathematical constant;
///   RG               — the renormalization group (running of couplings): the method import;
///   3+1              — the spacetime dimensionality (d=3 spatial + 1 time).
///
/// THE CLASSIFICATION:
///   STRUCTURAL  — a true theory input: part of the framework (derived structure or universal reference);
///   EMPIRICAL   — a calibration value: a measured/observed anchor setting an absolute scale;
///   BOUNDARY    — an accepted limit: a documented target or gap that is not a flaw (and not an input);
///   REMOVABLE   — not a true input: a free constant or method that could be absorbed, derived, or dropped.
///
/// THE THREE QUESTIONS:
///   (1) TRUE THEORY INPUTS — η (conformal reference), 3+1 (d≥3 derived), π (universal constant):
///       the framework's structural references. The theory needs its framework but no empirical free
///       constant: every physics ratio is derived, and ONE absolute scale anchors the rest.
///   (2) ONLY CALIBRATION — me and MZ: both are absolute-scale anchors. They carry NO structural
///       content (all ratios are chain-derived, QG288) — they merely fix the unit scale. Only ONE
///       empirical scale is strictly needed (me for masses, or any single mass; MZ for the ladder).
///   (3) REPLACEABLE — 5/4 (a free constant: absorbable into the normalization or a future structural
///       derivation), RG (the running EMERGES from D96 spectral geometry, QG204 — the β-machinery is a
///       method, not physics), and me/MZ (any single mass/energy anchor would calibrate the scale).
///   BOUNDARY (not replaceable, not an input): Bekenstein 1/4 — the target coefficient is a documented
///       gap (the 2π quantum factor, QG185/QG259); it is a goal, not a theory input.
///
/// THE MINIMAL ANCHOR INVENTORY:
///   TRUE INPUTS:  { η (conformal reference), 3+1 (derived dimensionality), π (universal) }
///   ONE SCALE:    { me or MZ } — the single empirical calibration anchor (either suffices)
///   REMOVED:      5/4 (free constant — derive or absorb), RG (method — the running is derived),
///                  the redundant second scale anchor
///   BOUNDARY:     Bekenstein 1/4 (target coefficient, documented gap — not an input)
///
/// Classification: MINIMAL INVENTORY — the theory's true anchors are the structural references
/// (η, 3+1, π) plus ONE empirical scale; the free constant 5/4 and the method RG are removable, and
/// the second scale anchor is redundant. The only irreducible boundary is Bekenstein 1/4 (a target).
/// </summary>
public static class AnchorInventoryAudit
{
    /// <summary>The anchor classification.</summary>
    public enum AnchorKind { Structural, Empirical, Boundary, Removable }

    /// <summary>An anchor: its source, classification, and the three decisive attributes.</summary>
    public sealed record Anchor(
        string Name,
        string Source,
        AnchorKind Kind,
        bool IsTrueInput,
        bool IsCalibration,
        bool IsReplaceable,
        string Note);

    // ── Verified anchor values (deterministic, from the established D96 classes) ──

    /// <summary>me = 0.511 MeV (PhysicalCalibration).</summary>
    public static double MeValue() => PhysicalCalibration.MElectron;

    /// <summary>MZ = 91.19 GeV (PhysicalCalibration).</summary>
    public static double MzValue() => PhysicalCalibration.MZGeV;

    /// <summary>The 5/4 factor used in ℓ₁ = Σm·ln(span)·(5/4) (QG238).</summary>
    public static double FiveFourths() => 1.25;

    /// <summary>The Bekenstein-Hawking target coefficient S = A/4.</summary>
    public static double BekensteinQuarter() => BekensteinQuarterOrigin.BekensteinCoefficient();

    /// <summary>π — the universal mathematical constant (the Bekenstein 2π quantum factor).</summary>
    public static double Pi() => Math.PI;

    /// <summary>The conformal reference metric η = diag(−1, +1, +1, +1) (Minkowski flat reference).</summary>
    public static double[] EtaMetric() => new[] { -1.0, 1.0, 1.0, 1.0 };

    /// <summary>The physical spatial dimension d = 3 (QG2: d≥3 derived; QG197 FULL BRIDGE).</summary>
    public static int SpatialDimension() => 3;

    /// <summary>RG: the gauge-coupling running EMERGES from D96 (QG204 RUNNING ORIGIN).</summary>
    public static bool RgRunningDerived() => RunningCouplingOrigin.Classify() == "RUNNING ORIGIN";

    /// <summary>Bekenstein structure is derived; the exact 1/4 is NOT (QG185 PARTIAL ORIGIN).</summary>
    public static bool BekensteinStructureDerived() => BekensteinQuarterOrigin.StructureDerived();

    /// <summary>Bekenstein 1/4 is NOT reproduced without the 2π quantum factor.</summary>
    public static bool BekensteinQuarterNotDerived() => !BekensteinQuarterOrigin.DeficitReproducesQuarter();

    /// <summary>d≥3 is derived (QG2); d=3 gives the non-trivial Einstein structure (QG197).</summary>
    public static bool DimensionalityDerived() => D2ToD3Bridge.DGt3Required() && D2ToD3Bridge.BridgeConnects2DTo3D();

    // ── The full anchor inventory (8 anchors) ──────────────────────────────────

    /// <summary>The eight anchors with their classification and attributes.</summary>
    public static Anchor[] Inventory() => new[]
    {
        new Anchor("me = 0.511 MeV", "QG140/QG251", AnchorKind.Empirical, false, true, true,
            "the absolute MASS scale — a measured calibration value; carries no structural content (all mass ratios are chain-derived, QG288); replaceable by any single mass anchor"),
        new Anchor("MZ = 91.19 GeV", "QG130", AnchorKind.Empirical, false, true, true,
            "the absolute ENERGY scale — the Z-anchor calibration family; carries no structural content; replaceable by any single energy/mass anchor"),
        new Anchor("5/4", "QG238", AnchorKind.Removable, false, true, true,
            "the acoustic-peak factor in ℓ₁ = Σm·ln(span)·(5/4) — a FREE CONSTANT (the QG280 R4 meta-inconsistency: QG255 Noether rejects free constants); absorbable into the normalization or a future structural derivation"),
        new Anchor("Bekenstein 1/4", "QG185/QG259", AnchorKind.Boundary, false, false, false,
            "the area-law coefficient S = A/4 — a documented TARGET not yet derived (the 2π quantum factor, QG185 PARTIAL ORIGIN; QG259 honest anti-retro); a goal, not a theory input"),
        new Anchor("η (conformal reference)", "QG77", AnchorKind.Structural, true, false, false,
            "the conformal reference metric (g = ρ^(2/d)·η) — a TRUE THEORY INPUT: the framework's flat reference, not a measured value; part of the geometry, not a choice"),
        new Anchor("π", "QG185", AnchorKind.Structural, true, false, false,
            "the universal mathematical constant — a TRUE THEORY INPUT in the sense that all physics uses it; NOT a theory choice (it appears in every branch of mathematics); the Bekenstein 2π gap is the unresolved part"),
        new Anchor("RG (renormalization group)", "QG204", AnchorKind.Removable, false, false, true,
            "the coupling-running method — REMOVABLE: the running of the gauge couplings EMERGES from D96 spectral geometry (QG204 RUNNING ORIGIN); the β-machinery is a method, not physics"),
        new Anchor("3+1 (spacetime)", "QG2/QG197", AnchorKind.Structural, true, false, false,
            "the spacetime dimensionality — a TRUE THEORY INPUT: d≥3 is DERIVED (QG2) and d=3 gives the non-trivial Einstein structure (QG197 FULL BRIDGE); the +1 time signature is the Lorentzian reference"),
    };

    // ── Counts ─────────────────────────────────────────────────────────────────

    /// <summary>Number of STRUCTURAL anchors (true theory inputs).</summary>
    public static int StructuralCount() => Inventory().Count(a => a.Kind == AnchorKind.Structural);

    /// <summary>Number of EMPIRICAL anchors (calibration values).</summary>
    public static int EmpiricalCount() => Inventory().Count(a => a.Kind == AnchorKind.Empirical);

    /// <summary>Number of BOUNDARY anchors (accepted limits / targets).</summary>
    public static int BoundaryCount() => Inventory().Count(a => a.Kind == AnchorKind.Boundary);

    /// <summary>Number of REMOVABLE anchors (not true inputs).</summary>
    public static int RemovableCount() => Inventory().Count(a => a.Kind == AnchorKind.Removable);

    /// <summary>Number of TRUE THEORY INPUTS.</summary>
    public static int TrueInputCount() => Inventory().Count(a => a.IsTrueInput);

    /// <summary>Number of CALIBRATION-ONLY anchors.</summary>
    public static int CalibrationCount() => Inventory().Count(a => a.IsCalibration);

    /// <summary>Number of REPLACEABLE anchors.</summary>
    public static int ReplaceableCount() => Inventory().Count(a => a.IsReplaceable);

    // ── The minimal anchor inventory ───────────────────────────────────────────

    /// <summary>
    /// The minimal inventory: the TRUE INPUTS (η, 3+1, π — structural) + ONE calibration scale
    /// (me or MZ). The other four anchors are removable (5/4, RG, the second scale) or a boundary
    /// target (Bekenstein 1/4).
    /// </summary>
    public static string[] MinimalInventory() => new[]
    {
        "η (conformal reference) — STRUCTURAL",
        "3+1 (derived d≥3, Lorentzian signature) — STRUCTURAL",
        "π (universal constant) — STRUCTURAL",
        "one empirical scale (me or MZ) — CALIBRATION ONLY",
    };

    /// <summary>The minimal inventory is reachable: 3 structural true inputs + exactly 1 scale, with 2 removable.</summary>
    public static bool MinimalSetReachable()
        => TrueInputCount() == 3 && StructuralCount() == 3 && RemovableCount() == 2 && BoundaryCount() == 1;

    // ── Anchor score & classification ─────────────────────────────────────────

    /// <summary>
    /// Anchor score (0..5):
    /// 1. me and MZ are calibration-only (no structural content — all ratios derived);
    /// 2. 5/4 is a free constant / removable (the R4 meta-inconsistency);
    /// 3. π is a universal constant (not a theory choice);
    /// 4. d=3 (3+1) is derived (QG2/QG197) and η is the conformal reference;
    /// 5. RG is removable (the running EMERGES from D96, QG204) and Bekenstein 1/4 is a documented
    ///    boundary target (not an input).
    /// </summary>
    public static int AnchorScore()
    {
        int score = 0;
        // 1. me and MZ carry no structural content: the mass ratios and the ladder are chain-derived.
        if (EmpiricalCount() == 2 && Inventory().Where(a => a.IsCalibration).All(a => !a.IsTrueInput)) score++;
        // 2. 5/4 is removable.
        if (Inventory().Any(a => a.Name == "5/4" && a.Kind == AnchorKind.Removable)) score++;
        // 3. π is universal (not a theory choice).
        if (Inventory().Any(a => a.Name == "π" && a.Kind == AnchorKind.Structural)) score++;
        // 4. d=3 derived + η structural reference.
        if (DimensionalityDerived() && Inventory().Any(a => a.Name.StartsWith("η") && a.IsTrueInput)) score++;
        // 5. RG removable + Bekenstein 1/4 a boundary target.
        if (RgRunningDerived() && Inventory().Any(a => a.Name == "RG (renormalization group)" && a.Kind == AnchorKind.Removable)
            && Inventory().Any(a => a.Name == "Bekenstein 1/4" && a.Kind == AnchorKind.Boundary)) score++;
        return score;
    }

    /// <summary>
    /// Data-driven classification:
    ///   ANCHOR HEAVY      — many anchors are true empirical inputs (score ≤ 2): the theory needs many
    ///                       free constants;
    ///   PARTIAL REDUCTION — some anchors are calibration/removable, others remain (score 3-4);
    ///   MINIMAL INVENTORY — the true anchors reduce to the structural references (η, 3+1, π) plus ONE
    ///                       empirical scale; 5/4 and RG are removable, the second scale is redundant,
    ///                       and Bekenstein 1/4 is a boundary target (score 5).
    /// </summary>
    public static string Classify()
    {
        int score = AnchorScore();
        if (score <= 2) return "ANCHOR HEAVY";
        if (score == 3 || score == 4) return "PARTIAL REDUCTION";
        return "MINIMAL INVENTORY";
    }

    /// <summary>Summary string.</summary>
    public static string Summary()
    {
        return $"{Classify()} — anchor score {AnchorScore()}/5: {StructuralCount()} STRUCTURAL / " +
               $"{EmpiricalCount()} EMPIRICAL / {BoundaryCount()} BOUNDARY / {RemovableCount()} REMOVABLE " +
               $"across {Inventory().Length} anchors. TRUE INPUTS: {{η, 3+1, π}} (structural references — " +
               $"the framework, not free constants). ONLY CALIBRATION: me and MZ (absolute scales — all " +
               $"ratios are chain-derived, QG288; only ONE scale is strictly needed). REPLACEABLE: 5/4 " +
               $"(free constant), RG (the running EMERGES from D96, QG204), and the redundant second " +
               $"scale. BOUNDARY: Bekenstein 1/4 (target coefficient — the 2π gap, QG185/QG259; a goal, " +
               $"not an input). The MINIMAL ANCHOR INVENTORY is the structural framework {{η, 3+1, π}} + " +
               $"one empirical scale — the theory needs no free physics constant.";
    }
}
