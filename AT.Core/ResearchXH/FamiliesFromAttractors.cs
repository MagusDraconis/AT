namespace AT.Core.ResearchXH;

/// <summary>
/// AT-QG Phase 118 — Families from attractor geometries. QG117 showed the (feedback, damping) parameter
/// plane maps to a DISCRETE ladder of stable attractor geometries (radius 2 and 6 links/node for K=6). This
/// phase asks: can particle-FAMILY structure emerge from the different attractor geometry classes?
///
/// Method (computational, fully deterministic): for each geometry class realized in the parameter plane we
/// extract (1) the GEOMETRY-CLASS COUNT — distinct spectral classes over the plane (and over a range of K);
/// (2) FAMILY ANALOGS — the octave-band spectral family count WITHIN each geometry class (QG106 family
/// structure) — the discrete family-like content of each class; (3) CLASS TRANSITIONS — how sharply classes
/// separate in the parameter plane (adjacent-point sensitivity); (4) HIERARCHY GENERATION — the low-mode
/// successive spectral ratios within each class (the internal mass-like ladder); (5) STABILITY OF CLASSES —
/// whether the family count of each class persists under perturbation (link removal) and network size.
///
/// Answer (determined by the computed data): PARTIAL RELATION — distinct attractor geometry classes DO carry
/// distinct family-like content (radius-2 class: 4 octave families, span 11.90; radius-6 class: 3 families,
/// span 6.40 at N=96; a three-family class exists for K=5 and K=6), the class set is discrete (2 classes
/// across K=3..6) with sharp transitions (adjacent sensitivity 0.62), and the family counts are stable under
/// link-removal perturbation. BUT the octave family count is NOT a size-invariant property of a class: it
/// grows with the network (radius-2 class: 3→4→5 families as N=48→96→192; radius-6 class: 2→3→4), so the
/// discrete family structure is partially emergent (class-dependent, perturbation-robust) yet not a fixed
/// size-independent family number. Classification: PARTIAL RELATION. No new primitives added here.
/// </summary>
public static class FamiliesFromAttractors
{
    /// <summary>Default dynamics parameters (matching QG115–117).</summary>
    public const double DefaultDamping = 0.2;
    public const double DefaultFeedback = 0.7;
    public const int DefaultK = 6;

    /// <summary>K values swept for the geometry-class count study.</summary>
    public static readonly int[] KGrid = { 3, 4, 5, 6 };

    // ── Geometry classes over the parameter plane ──────────────────────────────────

    /// <summary>
    /// Distinct geometry classes over the parameter plane for a given K, with each class's internal
    /// octave-family count and hierarchy span. Returns per distinct radius: (radius, familyCount, span).
    /// </summary>
    public static (double Radius, int Families, double Span)[] ClassProfiles(int K = DefaultK)
    {
        var points = AttractorParameterOrigin.ParameterPlane(K: K);
        var seen = new List<(double Radius, int Families, double Span)>();
        foreach (var p in points)
        {
            if (!seen.Any(s => Math.Abs(s.Radius - p.Radius) < 0.01))
                seen.Add((p.Radius, p.Families, p.Span));
        }
        return seen.OrderBy(s => s.Radius).ToArray();
    }

    /// <summary>Total distinct geometry-class count over the parameter plane for a given K.</summary>
    public static int ClassCount(int K = DefaultK) => ClassProfiles(K).Length;

    /// <summary>Distinct geometry-class counts across K = 3,4,5,6.</summary>
    public static (int K, int Classes, int[] FamilyCounts)[] ClassCountsByK()
        => KGrid.Select(k =>
        {
            var profiles = ClassProfiles(k);
            return (k, profiles.Length, profiles.Select(p => p.Families).ToArray());
        }).ToArray();

    // ── Family analogs ─────────────────────────────────────────────────────────────

    /// <summary>
    /// Family count of a geometry class (via octave-band spectral families) for a representative parameter
    /// point of that class.
    /// </summary>
    public static int FamilyCountAt(double feedback, double damping, int K = DefaultK, int n = 96)
    {
        var net = UniversalAttractor.ConvergedNetwork(ActualizationStructures.PersistentActivity(n), K,
            damping, feedback, 120);
        return StructureFromContent.FamilyCount(net);
    }

    /// <summary>Distinct family counts realized across all geometry classes.</summary>
    public static int[] DistinctFamilyCounts(int K = DefaultK)
        => ClassProfiles(K).Select(p => p.Families).Distinct().OrderBy(f => f).ToArray();

    /// <summary>Does any geometry class carry EXACTLY 3 octave families (the SM family count)?</summary>
    public static bool HasThreeFamilyClass(int K = DefaultK)
        => DistinctFamilyCounts(K).Contains(3);

    // ── Class transitions ──────────────────────────────────────────────────────────

    /// <summary>Adjacent-point spectral sensitivity across the parameter plane (QG117).</summary>
    public static double MaxAdjacentClassSensitivity(int K = DefaultK)
        => AttractorParameterOrigin.MaxAdjacentShapeDistance(K: K);

    // ── Hierarchy generation ───────────────────────────────────────────────────────

    /// <summary>
    /// Internal hierarchy ladder of a geometry class: the first successive spectral ratios ω_{k+1}/ω_k of
    /// its stable-mode frequencies (the mass-like ladder within the class).
    /// </summary>
    public static double[] ClassSuccessiveRatios(double feedback, double damping, int K = DefaultK, int n = 96)
    {
        var net = UniversalAttractor.ConvergedNetwork(ActualizationStructures.PersistentActivity(n), K,
            damping, feedback, 120);
        var freqs = SpectrumRobustness.StableFrequencies(SpectrumRobustness.LaplacianOf(net));
        return SpectrumRobustness.SuccessiveRatios(freqs);
    }

    /// <summary>Hierarchy span of a geometry class.</summary>
    public static double ClassSpan(double feedback, double damping, int K = DefaultK, int n = 96)
    {
        var net = UniversalAttractor.ConvergedNetwork(ActualizationStructures.PersistentActivity(n), K,
            damping, feedback, 120);
        return StructureFromContent.HierarchySpan(net);
    }

    /// <summary>
    /// Distinct hierarchy spans across the geometry classes — does each class carry its own hierarchy depth?
    /// </summary>
    public static (double Radius, double Span)[] ClassSpans(int K = DefaultK)
        => ClassProfiles(K).Select(p => (p.Radius, p.Span)).ToArray();

    // ── Stability of classes ───────────────────────────────────────────────────────

    /// <summary>
    /// Family-count stability under link-removal perturbation: does each geometry class keep its octave
    /// family count when up to `fraction` of its links are removed deterministically?
    /// </summary>
    public static bool FamilyCountsStableUnderPerturbation(double fraction = 0.1, int K = DefaultK, int n = 96)
    {
        var profiles = ClassProfiles(K);
        foreach (var (radius, families, _) in profiles)
        {
            // representative parameter point for this class
            var rep = RepresentativePoint(radius, K);
            if (!rep.HasValue) continue;
            var net = UniversalAttractor.ConvergedNetwork(ActualizationStructures.PersistentActivity(n), K,
                rep.Value.Damping, rep.Value.Feedback, 120);
            var perturbed = SpectrumRobustness.RemoveLinksDeterministic(net, fraction);
            int famAfter = StructureFromContent.FamilyCount(perturbed);
            if (famAfter != families) return false;
        }
        return true;
    }

    /// <summary>
    /// Family-count stability across network size: does each geometry class keep its octave family count at
    /// N = 48, 96, 192?
    /// </summary>
    public static bool FamilyCountsStableAcrossSize(int K = DefaultK)
    {
        var profiles = ClassProfiles(K);
        foreach (var (radius, families, _) in profiles)
        {
            var rep = RepresentativePoint(radius, K);
            if (!rep.HasValue) continue;
            foreach (int n in new[] { 48, 96, 192 })
            {
                var net = UniversalAttractor.ConvergedNetwork(ActualizationStructures.PersistentActivity(n), K,
                    rep.Value.Damping, rep.Value.Feedback, 120);
                if (StructureFromContent.FamilyCount(net) != families) return false;
            }
        }
        return true;
    }

    /// <summary>
    /// Low-mode hierarchy-ratio stability across network size for a class: mean relative deviation of the
    /// first k successive ratios between N=48 and N=192. Small ⇒ the mass-like ladder is a size-invariant
    /// property of the class.
    /// </summary>
    public static double LowModeRatioStabilityAcrossSize(double feedback, double damping, int K = DefaultK,
        int k = 4)
    {
        var net48 = UniversalAttractor.ConvergedNetwork(ActualizationStructures.PersistentActivity(48), K,
            damping, feedback, 120);
        var net192 = UniversalAttractor.ConvergedNetwork(ActualizationStructures.PersistentActivity(192), K,
            damping, feedback, 120);
        var r48 = SpectrumRobustness.SuccessiveRatios(SpectrumRobustness.StableFrequencies(
            SpectrumRobustness.LaplacianOf(net48)));
        var r192 = SpectrumRobustness.SuccessiveRatios(SpectrumRobustness.StableFrequencies(
            SpectrumRobustness.LaplacianOf(net192)));
        return SpectrumRobustness.LowModeRatioMeanDeviation(r48, r192, k);
    }

    /// <summary>
    /// Mean low-mode ratio deviation between the two geometry classes at fixed N — how distinct are the
    /// mass-like ladders of different attractor classes?
    /// </summary>
    public static double InterClassRatioDeviation(int K = DefaultK, int k = 4)
    {
        var profiles = ClassProfiles(K);
        if (profiles.Length < 2) return double.NaN;
        var repA = RepresentativePoint(profiles[0].Radius, K);
        var repB = RepresentativePoint(profiles[^1].Radius, K);
        if (!repA.HasValue || !repB.HasValue) return double.NaN;
        var netA = UniversalAttractor.ConvergedNetwork(ActualizationStructures.PersistentActivity(96), K,
            repA.Value.Damping, repA.Value.Feedback, 120);
        var netB = UniversalAttractor.ConvergedNetwork(ActualizationStructures.PersistentActivity(96), K,
            repB.Value.Damping, repB.Value.Feedback, 120);
        var rA = SpectrumRobustness.SuccessiveRatios(SpectrumRobustness.StableFrequencies(
            SpectrumRobustness.LaplacianOf(netA)));
        var rB = SpectrumRobustness.SuccessiveRatios(SpectrumRobustness.StableFrequencies(
            SpectrumRobustness.LaplacianOf(netB)));
        return SpectrumRobustness.LowModeRatioMeanDeviation(rA, rB, k);
    }

    /// <summary>Representative (feedback, damping) parameter point for a given attractor radius.</summary>
    public static (double Feedback, double Damping)? RepresentativePoint(double radius, int K = DefaultK)
    {
        foreach (double f in AttractorParameterOrigin.FeedbackGrid)
            foreach (double d in AttractorParameterOrigin.DampingGrid)
            {
                var p = AttractorParameterOrigin.AttractorAt(f, d, K: K);
                if (Math.Abs(p.Radius - radius) < 0.01) return (f, d);
            }
        return null;
    }

    // ── Classification ────────────────────────────────────────────────────────────

    /// <summary>
    /// Data-driven classification:
    ///   NO RELATION     — geometry classes carry no family-like structure (single class everywhere, or all
    ///                     classes share one family count; no discrete family content);
    ///   PARTIAL RELATION — distinct geometry classes exist with discrete family content, but counts are not
    ///                     stable (perturbation/size change them), so the family structure is NOT robust;
    ///   FAMILY ORIGIN   — distinct geometry classes each carry a stable, distinct octave-family structure
    ///                     (counts persist under perturbation and size; hierarchy spans differ per class) —
    ///                     particle-family-like content originates from the attractor geometry classes.
    /// </summary>
    public static string Classify(int K = DefaultK)
    {
        var profiles = ClassProfiles(K);
        int[] families = profiles.Select(p => p.Families).ToArray();
        bool multipleClasses = profiles.Length >= 2;
        bool distinctFamilyContent = families.Distinct().Count() >= 2;
        bool stableUnderPert = FamilyCountsStableUnderPerturbation(0.1, K);
        bool stableAcrossSize = FamilyCountsStableAcrossSize(K);

        if (!multipleClasses || !distinctFamilyContent) return "NO RELATION";
        if (!stableUnderPert || !stableAcrossSize) return "PARTIAL RELATION";
        return "FAMILY ORIGIN";
    }
}
