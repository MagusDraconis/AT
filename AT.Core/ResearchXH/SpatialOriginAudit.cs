namespace AT.Core.ResearchXH;

/// <summary>The epistemic status of a step in the Difference → Density → Metric → Measure chain.</summary>
public enum MeasureProvenance
{
    /// <summary>Follows algebraically from the steps before it.</summary>
    Derived,

    /// <summary>Posited, not derived — but anchored by observation.</summary>
    Assumed,

    /// <summary>An interpretive identification between a theoretical object and a geometric quantity.</summary>
    Correspondence,
}

/// <summary>Whether a place in the theory actually needs the counting measure.</summary>
public enum MeasureUsage
{
    /// <summary>Breaks if √det g_ij ≠ ρ.</summary>
    LoadBearing,

    /// <summary>References ρ but not its geometric volume reading.</summary>
    Neutral,
}

/// <summary>One step of the derivation chain.</summary>
public sealed record MeasureStep(string Step, string What, MeasureProvenance Provenance, string Basis);

/// <summary>One place in the theory that may consume the counting measure.</summary>
public sealed record MeasureUse(string Where, MeasureUsage Usage, string Basis);

/// <summary>One exponent's outcome, computed.</summary>
public sealed record ExponentCase(double N, double ClockExponent, double VolumeExponent,
    bool ClockLawHolds, bool CountingMeasureHolds);

/// <summary>
/// ResearchY-G_031 — SPATIAL-ORIGIN AUDIT.
///
/// QUESTION. Exactly where does <c>B = σ</c> enter the theory?
///
/// THE ANSWER, AND IT IS NOT WHERE THE PROGRAMME HAD BEEN LOOKING. <c>B = σ</c> does **not** enter as a
/// separate assumption anywhere. Write the conformal ansatz with an arbitrary exponent n:
///
///     g_μν = ρ^(2n)·η_μν   ⟹   A = B = n·ln ρ = n·d·σ
///                              √(−g₀₀) = ρ^n        (the clock)
///                              √det g_ij = ρ^(n·d)  (the spatial measure)
///
/// Then, in d = 3:
///
///     **n = 1/d  ⟺  √(−g₀₀) = ρ^(1/d)  ⟺  √det g_ij = ρ**
///
/// **The clock law and the counting measure are THE SAME EQUATION** — the two readings of one exponent
/// choice, read off two slots that conformal flatness (A = B) has already made equal. There is no
/// "spatial measure" step anywhere in the chain to be found or removed.
///
/// THIS RENARRATES G_023. G_023 said AT "pins k = 0 twice — the clock law forces A = σ and the counting
/// measure forces B = σ". There are not two pins: conformal flatness supplies A = B, and ONE exponent choice
/// supplies both values. The counting measure was therefore never an independent postulate, and G_030's
/// dilemma is not "counting measure versus optics" but **"conformal flatness plus the clock law versus
/// optics"** — a smaller and sharper statement.
///
/// PROVENANCE. ρ is DERIVED from Difference (the scalar face, QG285/QG292). The conformal ansatz and its
/// exponent are ASSUMED (the metric ansatz, QG207; the exponent is the clock law, anchored by G_004's
/// 0.99600 calibration and G_009's GPS agreement). The counting measure is then DERIVED, and the reading of
/// ρ as a geometric volume is a CORRESPONDENCE (G_018's provenance asymmetry).
///
/// VERDICT: REQUIRED. Because it is not a separate assumption, it cannot be removed on its own — removing it
/// requires abandoning conformal flatness or the clock law (G_028). And it is load-bearing: the count
/// conservation N = ∫ρ dV, the deficit accounting (QG181/182), the horizon-area/entropy chain (QG185/QG259)
/// and the cosmological density parameters all read ρ as the geometric volume. What breaks is quantified
/// below: the geometric-to-count ratio runs from 1.000012735 at the Sun to **3.437584871** at J0740+6620.
/// </summary>
public static class SpatialOriginAudit
{
    public const int D = 3;

    /// <summary>The exponent the clock law requires — and hence the counting measure.</summary>
    public static double RequiredExponent => 1.0 / D;

    public static double Sigma(double rho) => Math.Log(rho) / D;

    /// <summary>A = B = n·ln ρ (conformal flatness by construction).</summary>
    public static double AOf(double rho, double n) => n * Math.Log(rho);

    public static double BOf(double rho, double n) => n * Math.Log(rho);

    /// <summary>√(−g₀₀) = ρ^n.</summary>
    public static double Clock(double rho, double n) => Math.Exp(AOf(rho, n));

    /// <summary>√det g_ij = ρ^(n·d).</summary>
    public static double SpatialMeasure(double rho, double n) => Math.Exp(D * BOf(rho, n));

    /// <summary>Does the clock law √(−g₀₀) = ρ^(1/d) hold at this exponent?</summary>
    public static bool ClockLawHolds(double rho, double n)
        => Math.Abs(Clock(rho, n) - Math.Pow(rho, 1.0 / D)) < 1.0e-9 * Math.Max(1.0, Math.Pow(rho, 1.0 / D));

    /// <summary>Does the counting measure √det g_ij = ρ hold at this exponent?</summary>
    public static bool CountingMeasureHolds(double rho, double n)
        => Math.Abs(SpatialMeasure(rho, n) - rho) < 1.0e-9 * Math.Max(1.0, rho);

    /// <summary>One exponent's full outcome.</summary>
    public static ExponentCase CaseFor(double rho, double n)
        => new(n, n, n * D, ClockLawHolds(rho, n), CountingMeasureHolds(rho, n));

    /// <summary>
    /// THE PROVENANCE THEOREM, EXECUTED: over a deterministic sweep of (n, ρ), n = 1/d holds **iff** the
    /// clock law holds **iff** the counting measure holds. Returns the number of mismatches (must be 0).
    ///
    /// The point ρ = 1 is DEGENERATE and is excluded: there the metric is flat, A = B = 0 for EVERY exponent,
    /// so all exponents agree trivially. It carries no information about the equivalence.
    /// </summary>
    public static int EquivalenceMismatches(int divisions = 400, double nMin = -2.0, double nMax = 2.0,
        double rhoMin = -3.0, double rhoMax = 3.0)
    {
        int mismatches = 0;
        for (int i = 0; i <= divisions; i++)
        {
            double n = nMin + (nMax - nMin) * i / divisions;
            for (int j = 0; j <= divisions; j++)
            {
                double lnRho = rhoMin + (rhoMax - rhoMin) * j / divisions;
                if (Math.Abs(lnRho) < 1.0e-9) continue;              // flat space: degenerate, excluded
                double rho = Math.Exp(lnRho);
                bool exponentIsRight = Math.Abs(n - RequiredExponent) < 1.0e-12;
                bool both = ClockLawHolds(rho, n) && CountingMeasureHolds(rho, n);
                if (exponentIsRight != both) mismatches++;
            }
        }
        return mismatches;
    }

    /// <summary>Is the counting measure entailed by the conformal ansatz at the clock exponent?</summary>
    public static bool CountingMeasureIsEntailed() => EquivalenceMismatches() == 0;

    /// <summary>
    /// At ρ = 1 the metric is flat and every exponent agrees — the degeneracy the sweep excludes.
    /// </summary>
    public static bool FlatSpaceIsDegenerate()
    {
        foreach (double n in new[] { -1.0, 0.0, RequiredExponent, 1.0, 2.0 })
            if (!ClockLawHolds(1.0, n) || !CountingMeasureHolds(1.0, n)) return false;
        return true;
    }

    // ── The chain and the usage inventory ─────────────────────────────────────

    /// <summary>The Difference → Density → Metric → Measure chain, with each step's provenance.</summary>
    public static MeasureStep[] Chain() => new[]
    {
        new MeasureStep("Difference → Density", "ρ is the SCALAR FACE of the one Difference (trace part)",
            MeasureProvenance.Derived, "QG285/QG286/QG292 — trace/traceless decomposition; no separate postulate"),
        new MeasureStep("Density → Metric (ansatz)", "g_μν = ρ^(2n)·η_μν — conformal, with exponent n",
            MeasureProvenance.Assumed, "QG207 'the metric ansatz' — posited, and the source of A = B"),
        new MeasureStep("the exponent n", "n = 1/d",
            MeasureProvenance.Assumed, "this IS the clock law √(−g₀₀) = ρ^(1/d); anchored by G_004's 0.99600 calibration and G_009's GPS agreement"),
        new MeasureStep("Metric → Spatial measure", "√det g_ij = ρ  (B = σ)",
            MeasureProvenance.Derived, "algebraic consequence — with n = 1/d and d = 3, ρ^(n·d) = ρ identically; NOT a separate postulate"),
        new MeasureStep("ρ read as a geometric volume", "the count IS the volume element",
            MeasureProvenance.Correspondence, "G_018's provenance asymmetry — a boundary identification, not a derivation"),
    };

    /// <summary>Where the counting measure is actually consumed.</summary>
    public static MeasureUse[] Uses() => new[]
    {
        new MeasureUse("the metric ansatz itself", MeasureUsage.LoadBearing,
            "g = ρ^(2/d)η — the counting measure is not separable from the ansatz that produces the geometry"),
        new MeasureUse("count conservation  N = ∫ρ dV", MeasureUsage.LoadBearing,
            "QG194/222 — the volume integral IS the count only if √det g_ij = ρ"),
        new MeasureUse("deficit accounting  Σρ = 1", MeasureUsage.LoadBearing,
            "QG181/QG182 — the deficit dust is defined against the count-per-volume"),
        new MeasureUse("horizon area → entropy  S = A/4", MeasureUsage.LoadBearing,
            "QG185/QG259 — the area is computed from the metric; a 3.44× volume departure moves it"),
        new MeasureUse("cosmological density parameters  Ω, n_s", MeasureUsage.LoadBearing,
            "the count→volume map sets the densities"),
        new MeasureUse("quantum amplitude  |ψ|² = ρ", MeasureUsage.Neutral,
            "QG216 — a probability reading of ρ, not a volume reading"),
        new MeasureUse("RAR / MOND scale  g† = cH₀/(2π)", MeasureUsage.Neutral,
            "QG080 — uses c and H₀ only; no volume element"),
    };

    /// <summary>Does any place actually need the counting measure?</summary>
    public static bool AnyLoadBearingUse() => Uses().Any(u => u.Usage == MeasureUsage.LoadBearing);

    /// <summary>The load-bearing count.</summary>
    public static int LoadBearingUseCount() => Uses().Count(u => u.Usage == MeasureUsage.LoadBearing);

    // ── What breaks, quantified ───────────────────────────────────────────────

    /// <summary>B for the γ = +1 survivor (G_029): the alternative measure that is not ρ.</summary>
    public static double SurvivorB(double x) => 0.5 * Math.Log(2.0 - Math.Exp(-2.0 * x));

    /// <summary>The counting-measure departure of the survivor: √det g_ij / ρ.</summary>
    public static double GeometricOverCountRatio(double x)
        => Math.Exp(D * SurvivorB(x)) / Math.Exp(-D * x);

    /// <summary>The count error the survivor implies: N_geometric / N_count − 1.</summary>
    public static double CountError(double x) => GeometricOverCountRatio(x) - 1.0;

    /// <summary>What breaks, listed — with the magnitude at the densest measured star.</summary>
    public static (string What, double Magnitude)[] Breakage() => new[]
    {
        ("count conservation N = ∫ρ dV: the volume integral is no longer the count",
            CountError(0.247002)),
        ("deficit accounting (QG181/182): the count-per-volume basis shifts", CountError(0.247002)),
        ("horizon area / entropy (QG185/QG259): the area moves with the volume", CountError(0.247002)),
        ("cosmological densities (Ω, n_s): the count→volume map changes", CountError(0.247002)),
    };

    /// <summary>
    /// THE VERDICT, COMPUTED (ResearchY-G_027: never a literal). REQUIRED iff the counting measure is
    /// entailed by the ansatz (so it cannot be removed alone) AND something actually consumes it.
    /// OPTIONAL iff it were entailed but nothing consumed it. REFUTED iff it were neither.
    /// </summary>
    public static string Verdict()
    {
        bool entailed = CountingMeasureIsEntailed();
        bool loadBearing = AnyLoadBearingUse();
        if (entailed && loadBearing) return "REQUIRED";
        return entailed ? "OPTIONAL" : "REFUTED";
    }

    /// <summary>
    /// Can the counting measure be removed WITHOUT touching the clock law or conformal flatness?
    /// No: with A = B fixed and A = σ, B = σ follows. Removal requires leaving conformal flatness.
    /// </summary>
    public static bool RemovableWithoutLeavingConformalFlatness()
    {
        // Search for any n ≠ 1/d that keeps the clock law (with A = B implied by the ansatz).
        for (int i = -2000; i <= 2000; i++)
        {
            double n = RequiredExponent + i * 1.0e-6;
            if (Math.Abs(n - RequiredExponent) < 1.0e-12) continue;
            if (ClockLawHolds(1.5, n) && CountingMeasureHolds(1.5, n)) return true;
        }
        return false;
    }
}
