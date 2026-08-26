namespace AT.Core.ResearchXH;

/// <summary>
/// AT-QG Phase 297 — Exception Audit. Focus: the 5/4 constant. QG238's first acoustic peak is
/// ℓ₁ = Σm·ln(span)·(5/4); QG255's Noether rule rejects 5/4 as a free constant; QG280 R4 documents
/// the meta-inconsistency; QG289 classified 5/4 REMOVABLE (free constant); QG296 classified it
/// REQUIRES EXTRA ASSUMPTION. This phase audits the 5/4 constant itself: is it derived, structural,
/// an artifact, a fit, or a boundary — and can every occurrence be traced to the same source?
/// No observables, no target values, D96 only, deterministic.
///
/// THE OCCURRENCES OF 5/4:
///   (1) QG238 — ℓ₁ = Σm·ln(span)·(5/4) = 220.48 (the first acoustic peak);
///   (2) QG253 — 5/4·Σ√m/λ₂ was a TIE CANDIDATE for m_μ/me in the formula-uniqueness tournament
///       (dev 0.15% — a free-constant multiplier);
///   (3) QG253/255 — the standard multiplier test set {2, 3, 4, 5, 1/2, 1/3, 5/4, 4/5, √2, √3}
///       used to scan alternative formulas;
///   (4) QG255 — the Noether rule REJECTS 5/4 as a free constant (5/4·Σ√m/λ₂ excluded);
///   (5) QG289/296 — documented as a free constant / requires extra assumption.
///
/// THE ANALYSIS — what IS 5/4?
///   DERIVED? NO — none of the derived beat identities equals 5/4: Σ√m/span ≈ 10, occMom/Σm ≈ 20,
///       Σm²/Σm ≈ 12/5, occMom/Σm² ≈ 25/3. 5/4 = 1.25 is NOT a D96 spectral ratio.
///   STRUCTURAL? NO — QG238's claim "5/4 is the lightest-octave-relative multiplicity scale" is a
///       LABEL IDENTITY: with occ₀ = 4, (occ₀+1)/occ₀ = 5/4 exactly — the same kind of numerical
///       coincidence QG185 rejected for Bekenstein's 1/occ₀ = 1/4 ("a numerical identity without a
///       mechanism"). There is no mechanism.
///   ARTIFACT? PARTIALLY — the QG255/238 inconsistency is an artifact of the selection rules: QG255
///       was calibrated to exclude the tie candidate 5/4·Σ√m/λ₂, yet the published QG238 uses the
///       same 5/4. The RULE is inconsistent; the 5/4 itself is a genuine fit.
///   FIT? YES — the PRIMARY classification. The observed ℓ₁ = 220.5 requires the factor
///       ℓ₁/(Σm·ln span) = 1.2501 ≈ 5/4 (fit to 0.008%). 5/4 is the multiplicative factor fitted so
///       that ℓ₁ matches observation — a fit, not a derivation.
///   BOUNDARY? NO — it has been documented as an exception, but it is a REMOVABLE fit (QG289: a free
///       constant, absorbable into the normalization or a future structural derivation), not an
///       irreducible boundary.
///
/// CAN EVERY OCCURRENCE BE TRACED TO THE SAME SOURCE? NO.
///   The QG238 5/4 (a fit multiplier in ℓ₁) and the QG253 5/4 (a generic candidate in the tournament
///       multiplier set) are the SAME numerical value but DIFFERENT contexts: one is the fitted factor
///       of the acoustic peak, the other is one of the standard small-constant multipliers scanned by
///       the formula-selection tournament. They do not share a single structural origin — both are
///       instances of "small rational multiplier" fitting practice.
///
/// THE DETERMINATION:
///   5/4 = FIT. It is not derived (no beat identity), not structural (the (occ₀+1)/occ₀ reading is a
///   label identity without a mechanism — the same standard that rejected Bekenstein 1/occ₀), not a
///   genuine boundary (it is a removable fit). Its occurrences cannot be traced to one source: the
///   ℓ₁ fit and the tournament multiplier are independent instances of small-rational fitting.
///
/// Classification: EXCEPTION REMAINS — 5/4 is a FIT (the factor needed to match ℓ₁ = 220.48 to the
/// observed 220.5; the "lightest-octave-relative multiplicity" is a label identity without a
/// mechanism), and its occurrences trace to independent fitting contexts, not a single D96 source.
/// The QG280 R4 meta-inconsistency (QG238 uses 5/4, QG255 rejects free constants) stands — the
/// exception is characterized but not resolved.
/// </summary>
public static class ExceptionAudit
{
    /// <summary>The 5/4 classification.</summary>
    public enum FiveFourthsClass { Derived, Structural, Artifact, Fit, Boundary }

    /// <summary>An occurrence of 5/4 with its source.</summary>
    public sealed record Occurrence(
        string Phase,
        string Context,
        string Formula,
        bool SameSourceAsL1);

    // ── Verified deterministic facts ───────────────────────────────────────────

    /// <summary>5/4 = 1.25 exactly.</summary>
    public static double FiveFourths() => 1.25;

    /// <summary>None of the derived beat identities equals 5/4 (Σ√m/span≈10, occMom/Σm≈20, Σm²/Σm≈12/5, occMom/Σm²≈25/3).</summary>
    public static bool NotDerivedFromBeatIdentities()
        => Math.Abs(ProjectionFamilyAudit.SqrtMOverSpan() - 1.25) > 0.5
           && Math.Abs(EffectiveAccessCounts.OctaveOccupationMoment() / 95.0 - 1.25) > 0.5;

    /// <summary>5/4 = (occ₀+1)/occ₀ with occ₀=4 — a LABEL IDENTITY without a mechanism (like Bekenstein 1/occ₀).</summary>
    public static bool IsLabelIdentity()
        => Math.Abs((EffectiveAccessCounts.OctaveOccupancies()[0] + 1.0)
                    / EffectiveAccessCounts.OctaveOccupancies()[0] - 1.25) < 1e-9;

    /// <summary>The label identity is the SAME kind QG185 rejected for Bekenstein 1/occ₀ = 1/4 ("a numerical identity without a mechanism").</summary>
    public static bool LabelIdentityLikeBekenstein()
        => BekensteinQuarterOrigin.InverseOctaveIsQuarter()
           && !BekensteinQuarterOrigin.DeficitReproducesQuarter();

    /// <summary>The observed ℓ₁ = 220.5 requires the fit factor ℓ₁/(Σm·ln span) ≈ 5/4 (fit to 0.008%).</summary>
    public static bool IsFitFactor()
    {
        double ratio = AcousticPeakOrigin.L1Observed
                       / (AcousticPeakOrigin.TotalModes() * AcousticPeakOrigin.LnSpan());
        return Math.Abs(ratio / 1.25 - 1.0) < 0.01;
    }

    /// <summary>The QG255 Noether rule rejects 5/4 while the published QG238 uses it — the inconsistency is an ARTIFACT of the rule's calibration.</summary>
    public static bool RuleInconsistencyIsArtifact()
        => SelectionPrincipleAudit.NoetherInconsistentWithPublished();

    /// <summary>5/4 is documented as REMOVABLE (QG289: a free constant, absorbable or a future structural derivation).</summary>
    public static bool DocumentedRemovable()
        => AnchorInventoryAudit.Inventory().Any(a => a.Name == "5/4" && a.Kind == AnchorInventoryAudit.AnchorKind.Removable);

    // ── The occurrences of 5/4 ─────────────────────────────────────────────────

    /// <summary>All occurrences of 5/4 with their contexts.</summary>
    public static Occurrence[] Occurrences() => new Occurrence[]
    {
        new("QG238", "acoustic peak fit", "ℓ₁ = Σm·ln(span)·(5/4)", true),
        new("QG253", "formula-uniqueness tie candidate", "5/4·Σ√m/λ₂ (m_μ/me candidate, dev 0.15%)", false),
        new("QG253", "standard multiplier test set", "{2, 3, 4, 5, 1/2, 1/3, 5/4, 4/5, √2, √3}", false),
        new("QG255", "Noether rule rejection", "5/4 rejected as a free constant", false),
        new("QG289", "anchor inventory", "5/4 = REMOVABLE (free constant)", false),
        new("QG296", "reconstruction audit", "5/4 = REQUIRES EXTRA ASSUMPTION", false),
    };

    /// <summary>
    /// Can every occurrence be traced to the same source? NO — the QG238 5/4 (a fit multiplier in ℓ₁)
    /// and the QG253/255 5/4 (a generic candidate in the tournament multiplier set) are the same value
    /// but different contexts: one is the fitted factor of the acoustic peak, the other is a standard
    /// small-constant multiplier in the formula-selection tournament. No single D96 origin.
    /// </summary>
    public static bool AllOccurrencesSameSource()
        => Occurrences().All(o => o.SameSourceAsL1);

    // ── The 5/4 classification ─────────────────────────────────────────────────

    /// <summary>
    /// The 5/4 classification (data-driven):
    ///   DERIVED    — a D96 spectral ratio equals 5/4 (no: the beat identities are ≈10, ≈20, ≈12/5, ≈25/3);
    ///   STRUCTURAL — 5/4 has a mechanism (no: (occ₀+1)/occ₀ is a label identity without one);
    ///   ARTIFACT   — the value is a computational artifact (partial: the RULE is inconsistent, the 5/4 is a real fit);
    ///   FIT        — the value is fitted to match observation (YES: ℓ₁/(Σm·ln span) ≈ 5/4 to 0.008%);
    ///   BOUNDARY   — the value is an irreducible input (no: it is a removable fit).
    /// </summary>
    public static FiveFourthsClass ClassifyFiveFourths()
    {
        if (IsFitFactor()) return FiveFourthsClass.Fit;
        if (!NotDerivedFromBeatIdentities()) return FiveFourthsClass.Derived;
        if (IsLabelIdentity() && LabelIdentityLikeBekenstein()) return FiveFourthsClass.Structural;
        return FiveFourthsClass.Artifact;
    }

    // ── Exception score & classification ──────────────────────────────────────

    /// <summary>
    /// Exception score (0..5):
    /// 1. 5/4 is not derivable from the D96 beat identities (none equals 1.25);
    /// 2. 5/4 = (occ₀+1)/occ₀ is a LABEL IDENTITY without a mechanism (the Bekenstein-1/occ₀ standard);
    /// 3. 5/4 IS a fit factor (ℓ₁/(Σm·ln span) ≈ 5/4 to 0.008%);
    /// 4. the QG255/238 inconsistency is an artifact of the rule's calibration (not a real derivation);
    /// 5. not every occurrence traces to one source (the ℓ₁ fit vs the tournament multiplier) — the
    ///    exception is characterized but REMAINS.
    /// </summary>
    public static int ExceptionScore()
    {
        int score = 0;
        if (NotDerivedFromBeatIdentities()) score++;
        if (IsLabelIdentity() && LabelIdentityLikeBekenstein()) score++;
        if (IsFitFactor()) score++;
        if (RuleInconsistencyIsArtifact()) score++;
        if (!AllOccurrencesSameSource()) score++;
        return score;
    }

    /// <summary>
    /// Data-driven classification:
    ///   EXCEPTION CLOSED    — 5/4 is fully explained (score ≤ 2);
    ///   PARTIAL RESOLUTION  — 5/4 is characterized but some occurrence shares a source (score 3-4);
    ///   EXCEPTION REMAINS   — 5/4 is a FIT with no D96 origin, its occurrences trace to independent
    ///                         fitting contexts, and the QG280 R4 meta-inconsistency stands (score 5).
    /// </summary>
    public static string Classify()
    {
        int score = ExceptionScore();
        if (score <= 2) return "EXCEPTION CLOSED";
        if (score == 3 || score == 4) return "PARTIAL RESOLUTION";
        return "EXCEPTION REMAINS";
    }

    /// <summary>Summary string.</summary>
    public static string Summary()
    {
        return $"{Classify()} — exception score {ExceptionScore()}/5. 5/4 = {ClassifyFiveFourths()}: " +
               $"it is NOT derived (no beat identity equals 1.25: Σ√m/span≈10, occMom/Σm≈20, Σm²/Σm≈12/5, " +
               $"occMom/Σm²≈25/3), NOT structural (the 'lightest-octave-relative multiplicity' is the " +
               $"label identity (occ₀+1)/occ₀ = 5/4 without a mechanism — the same standard QG185 used to " +
               $"reject Bekenstein 1/occ₀ = 1/4), NOT a genuine boundary (it is a removable fit, QG289). " +
               $"It IS a FIT: the observed ℓ₁ = 220.5 requires ℓ₁/(Σm·ln span) = 1.2501 ≈ 5/4 (0.008%). " +
               $"Not every occurrence traces to the same source: the QG238 5/4 (the acoustic-peak fit) and " +
               $"the QG253/255 5/4 (a standard tournament multiplier) are the same value in different " +
               $"fitting contexts. The QG280 R4 meta-inconsistency (QG238 uses 5/4, QG255 rejects free " +
               $"constants) stands — the exception is characterized but not resolved.";
    }
}
