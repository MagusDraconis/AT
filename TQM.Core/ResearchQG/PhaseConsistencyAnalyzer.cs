using System.Globalization;
using System.Text;

namespace TQM.Core.ResearchQG;

/// <summary>
/// Internal-consistency audit of Phases 148-155 (flavor/gauge/multiplicity/contingency line).
/// Searches for contradictions between the phases' classifications, identifies the strongest
/// unresolved assumption, and assigns a confidence score.
/// </summary>
public static class PhaseConsistencyAnalyzer
{
    /// <summary>Phase-by-phase classification of the key objects.</summary>
    public static (string Phase, string Object, string Classification)[] Timeline() => new[]
    {
        ("148", "Koide Q=2/3", "CONTINGENT (not derived/emergent/selected)"),
        ("149", "SU(3)", "CONTINGENT (conditional on n=3)"),
        ("149", "U(1) / SU(2)", "DERIVED / EMERGENT"),
        ("150", "internal N=3 (generations, color)", "SELECTED (derived-lower ∩ empirical-upper)"),
        ("151", "N≥4 / upper bound N≤3", "CONTINGENT (empirical)"),
        ("152", "generations=3, color=3", "CONTINGENT (via empirical upper bound)"),
        ("152", "3 log-normal classes", "INDEPENDENT ensembles"),
        ("153", "3 log-normal classes", "independence UNTESTABLE (underdetermined)"),
        ("154", "Koide Q=2/3", "REAL hidden structure, CONTINGENT origin"),
        ("155", "neutrino-Koide", "FALSIFIED (charged-lepton-specific)"),
    };

    /// <summary>The strongest contradiction (flip of the internal 3 classification).</summary>
    public static string StrongestContradiction =>
        "The internal multiplicity N=3 (generations=3, color=3) was classified SELECTED in Phase 150 " +
        "(derived-lower ∩ empirical-upper) but reclassified CONTINGENT in Phases 151-152, because Phase 151 " +
        "showed the upper bound N≤3 is empirical (contingent). The phases do not explicitly reconcile the " +
        "flip: 'selected' emphasizes the derived lower bound + unique intersection; 'contingent' emphasizes " +
        "that the upper bound pinning the value to EXACTLY 3 is empirical. Both are partially right, but the " +
        "label for the same object reversed without a stated reconciliation.";

    /// <summary>The strongest unresolved assumption.</summary>
    public static string StrongestUnresolvedAssumption =>
        "The binary structure/content dichotomy is insufficient. 'Contingent' conflates two distinct things: " +
        "(a) 'the value is not derivable from the primitives' (contingent ORIGIN), and (b) 'the value is a " +
        "random draw with no structure' (COINCIDENCE). Phase 148 used 'contingent' for (a); Phase 154 showed " +
        "Koide is NOT (b) — it is a REAL hidden structure (BF≈3e4 vs coincidence) with a contingent origin. " +
        "The framework lacks a clean third category 'real structure with contingent origin', so the single word " +
        "'contingent' spans two incompatible meanings.";

    /// <summary>Confidence score (0..1) in the overall classification, with per-item confidences.</summary>
    public static (string Item, double Confidence)[] ConfidenceBreakdown() => new[]
    {
        ("U(1) derived", 0.95),
        ("spatial 3 derived", 0.85),
        ("N≥3 derived (CP theorem)", 0.90),
        ("N≤3 contingent (empirical)", 0.70),
        ("Koide real (BF vs coincidence)", 0.90),
        ("Koide origin contingent", 0.70),
        ("neutrino-Koide falsified", 0.90),
        ("3 classes independent", 0.55), // underdetermined by one universe
    };

    public static double OverallConfidence() =>
        ConfidenceBreakdown().Average(c => c.Confidence);
}
