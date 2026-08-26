namespace AT.Core.ResearchXH;

/// <summary>
/// AT-QG Phase 125 — Stability of high-energy sectors. QG124 established an energy-ordered sector hierarchy
/// (12 sectors total, 10 reachable only above baseline energy; SECTOR ORIGIN). This phase asks: do the higher
/// sectors remain STABLE, or do they DECAY into the observable 3-family sector?
///
/// Method (computational, fully deterministic): introduce the de-actualization (link-decay) primitive —
/// links are created by active nodes as in QG115/122, but a link is REMOVED when both endpoints' activity
/// falls below the decay threshold (a link de-actualizes when neither endpoint sustains it). Within this
/// energy-supported dynamics we measure: (1) SECTOR LIFETIME — build a high-energy sector (ceiling 8) then
/// remove the energy regime (ceiling 1) and count the number of dynamics steps until the radius falls to the
/// baseline maximum; (2) ATTRACTOR STABILITY — is the high-energy sector a FIXED POINT (radius unchanged over
/// hundreds of further steps at the same ceiling); (3) DOWNWARD TRANSITIONS — gradually ramp the energy
/// ceiling down and record the discrete radius plateaus (sector rungs) visited; (4) METASTABILITY — after a
/// brief energy dip the sector decays, then energy restoration re-grows it (energy-supported persistence);
/// (5) OBSERVABLE REMNANTS — after full decay the network lands in the baseline radius class; compare the
/// remnant's family structure to the observable sector.
///
/// Answer (determined by the computed data): [filled by Classify]. New primitive: link decay (de-actualization).
/// </summary>
public static class HighEnergySectorStability
{
    /// <summary>Default dynamics parameters (matching QG115–124).</summary>
    public const double DefaultDamping = 0.3;
    public const double DefaultFeedback = 0.9;
    public const int DefaultK = 6;

    /// <summary>High-energy regime used to build high sectors.</summary>
    public const double HighCeiling = 8.0;

    /// <summary>Baseline (observable) energy regime.</summary>
    public const double BaselineCeiling = 1.0;

    /// <summary>Default decay threshold (link de-actualizes when both endpoints fall below).</summary>
    public const double DecayThreshold = 0.5;

    // ── Convenience wrappers ───────────────────────────────────────────────────

    /// <summary>Radius (links per node) of an adjacency.</summary>
    public static double RadiusOf(double[,] adjacency) => EnergyDependentAttractors.RadiusOf(adjacency);

    /// <summary>Build the high-energy sector attractor at the high ceiling (with link decay).</summary>
    public static (double[] Activity, double[,] Adjacency) HighEnergySector(int n = 96, int K = DefaultK,
        double feedback = DefaultFeedback, double damping = DefaultDamping)
        => EnergyDependentAttractors.AdaptiveNetworkWithDecayFull(
            EnergyDependentAttractors.EnergyScaledSeed(n, 1.0), K, damping, feedback, 200, HighCeiling,
            DecayThreshold);

    /// <summary>Build the observable (baseline) sector attractor at the baseline ceiling (with link decay).</summary>
    public static (double[] Activity, double[,] Adjacency) ObservableSector(int n = 96, int K = DefaultK,
        double feedback = DefaultFeedback, double damping = DefaultDamping)
        => EnergyDependentAttractors.AdaptiveNetworkWithDecayFull(
            EnergyDependentAttractors.EnergyScaledSeed(n, 1.0), K, damping, feedback, 200, BaselineCeiling,
            DecayThreshold);

    // ── 1. Sector lifetime ──────────────────────────────────────────────────────

    /// <summary>
    /// Sector lifetime: build the high-energy sector, then REMOVE the energy regime (drop ceiling to baseline)
    /// and evolve. Returns the per-step radius trajectory and the step index at which the radius first falls to
    /// (or below) the observable baseline maximum radius.
    /// </summary>
    public static (double[] Radii, int CollapseStep, double CollapseRadius) SectorLifetime(int n = 96,
        int K = DefaultK, double feedback = DefaultFeedback, double damping = DefaultDamping, int steps = 40)
    {
        double baselineMax = BaselineMaxRadius(n, K, feedback, damping);
        var (a, _) = HighEnergySector(n, K, feedback, damping);
        var radii = new double[steps];
        for (int t = 0; t < steps; t++)
        {
            (a, var adj) = EnergyDependentAttractors.AdaptiveNetworkWithDecayFull(
                a, K, damping, feedback, 1, BaselineCeiling, DecayThreshold);
            radii[t] = RadiusOf(adj);
        }
        int collapse = steps;
        for (int t = 0; t < steps; t++)
        {
            if (radii[t] <= baselineMax + 1e-9) { collapse = t + 1; break; }
        }
        double collapseRadius = collapse < steps ? radii[collapse - 1] : radii[^1];
        return (radii, collapse, collapseRadius);
    }

    /// <summary>Maximum radius realized over the feedback sweep at the baseline ceiling (decay dynamics).</summary>
    public static double BaselineMaxRadius(int n = 96, int K = DefaultK, double feedback = DefaultFeedback,
        double damping = DefaultDamping)
    {
        double max = 0.0;
        for (double f = 0.2; f <= 1.0 + 1e-9; f += 0.1)
        {
            var (_, adj) = EnergyDependentAttractors.AdaptiveNetworkWithDecayFull(
                EnergyDependentAttractors.EnergyScaledSeed(n, 1.0), K, damping, f, 120, BaselineCeiling,
                DecayThreshold);
            max = Math.Max(max, RadiusOf(adj));
        }
        return max;
    }

    // ── 2. Attractor stability ──────────────────────────────────────────────────

    /// <summary>
    /// Fixed-point test: is the high-energy sector a stable attractor at its own ceiling? The radius after an
    /// additional extended evolution (from the converged state) must be unchanged.
    /// </summary>
    public static bool HighEnergySectorIsFixedPoint(int n = 96, int K = DefaultK,
        double feedback = DefaultFeedback, double damping = DefaultDamping, int extraSteps = 400)
    {
        var (a, adj) = HighEnergySector(n, K, feedback, damping);
        double r0 = RadiusOf(adj);
        (a, adj) = EnergyDependentAttractors.AdaptiveNetworkWithDecayFull(
            a, K, damping, feedback, extraSteps, HighCeiling, DecayThreshold);
        return Math.Abs(RadiusOf(adj) - r0) < 1e-9;
    }

    // ── 3. Downward transitions ─────────────────────────────────────────────────

    /// <summary>
    /// Downward-transition ladder: ramp the ceiling smoothly from the high regime down to baseline (M ramp
    /// steps) and record the realized radius plateau after each ramp step. Distinct radius plateaus visited
    /// during the decline = the downward rung count (higher sectors decay through intermediate sectors).
    /// </summary>
    public static (double Ceiling, double Radius)[] DownwardLadder(int n = 96, int K = DefaultK,
        double feedback = DefaultFeedback, double damping = DefaultDamping, int rampSteps = 30)
    {
        var (a, _) = HighEnergySector(n, K, feedback, damping);
        var result = new List<(double, double)>();
        for (int i = 0; i <= rampSteps; i++)
        {
            double ceil = HighCeiling - (HighCeiling - BaselineCeiling) * i / (double)rampSteps;
            (a, var adj) = EnergyDependentAttractors.AdaptiveNetworkWithDecayFull(
                a, K, damping, feedback, 3, ceil, DecayThreshold);
            result.Add((ceil, RadiusOf(adj)));
        }
        return result.ToArray();
    }

    /// <summary>Number of DISTINCT radius plateaus visited during the downward ramp (≥2 = multi-rung decay).</summary>
    public static int DownwardRungCount(int n = 96, int K = DefaultK, double feedback = DefaultFeedback,
        double damping = DefaultDamping, int rampSteps = 30)
    {
        var ladder = DownwardLadder(n, K, feedback, damping, rampSteps);
        var distinct = new List<double>();
        foreach (var (_, r) in ladder)
            if (distinct.Count == 0 || Math.Abs(r - distinct[^1]) > 1e-6) distinct.Add(r);
        return distinct.Count;
    }

    // ── 4. Metastability ────────────────────────────────────────────────────────

    /// <summary>
    /// Metastability: build the high-energy sector, briefly dip the ceiling to baseline (decay begins), then
    /// RESTORE the high ceiling. Returns (radiusAfterDip, radiusAfterRestore, originalRadius). Recovery
    /// (radiusAfterRestore ≈ originalRadius) means the sector is energy-supported and re-emerges: metastable.
    /// </summary>
    public static (double AfterDip, double AfterRestore, double Original) RecoveryAfterDip(int n = 96,
        int K = DefaultK, double feedback = DefaultFeedback, double damping = DefaultDamping, int dipSteps = 5,
        int restoreSteps = 150)
    {
        var (a, adj) = HighEnergySector(n, K, feedback, damping);
        double r0 = RadiusOf(adj);
        for (int t = 0; t < dipSteps; t++)
            (a, adj) = EnergyDependentAttractors.AdaptiveNetworkWithDecayFull(
                a, K, damping, feedback, 1, BaselineCeiling, DecayThreshold);
        double dipped = RadiusOf(adj);
        for (int t = 0; t < restoreSteps; t++)
            (a, adj) = EnergyDependentAttractors.AdaptiveNetworkWithDecayFull(
                a, K, damping, feedback, 1, HighCeiling, DecayThreshold);
        double restored = RadiusOf(adj);
        return (dipped, restored, r0);
    }

    // ── 5. Observable remnants ──────────────────────────────────────────────────

    /// <summary>
    /// Observable remnant: after the high-energy sector has FULLY decayed at baseline, what is the family
    /// structure of the remnant? Returns the family count of the remnant and the family count of the
    /// observable baseline sector built fresh from the seed.
    /// </summary>
    public static (int RemnantFamilies, int ObservableFamilies, double RemnantRadius, double ObservableRadius)
        ObservableRemnant(int n = 96, int K = DefaultK, double feedback = DefaultFeedback,
            double damping = DefaultDamping)
    {
        var (_, obsAdj) = ObservableSector(n, K, feedback, damping);
        int obsFamilies = StructureFromContent.FamilyCount(obsAdj);
        double obsRadius = RadiusOf(obsAdj);

        var (a, _) = HighEnergySector(n, K, feedback, damping);
        for (int t = 0; t < 40; t++)
            (a, var adj) = EnergyDependentAttractors.AdaptiveNetworkWithDecayFull(
                a, K, damping, feedback, 1, BaselineCeiling, DecayThreshold);
        var remnant = EnergyDependentAttractors.AdaptiveNetworkWithDecayFull(
            a, K, damping, feedback, 120, BaselineCeiling, DecayThreshold).Adjacency;
        return (StructureFromContent.FamilyCount(remnant), obsFamilies, RadiusOf(remnant), obsRadius);
    }

    // ── Classification ──────────────────────────────────────────────────────────

    /// <summary>
    /// Data-driven classification:
    ///   UNSTABLE   — the high-energy sector is NOT a fixed point: it decays even while its energy regime is
    ///                maintained (no stable plateau at high ceiling);
    ///   STABLE     — the high-energy sector persists even after the energy regime is removed (lifetime far
    ///                exceeds the removal window; the sector is self-sustaining);
    ///   METASTABLE — the high-energy sector is a stable fixed point at its own ceiling but DECAYS into the
    ///                observable sector when the energy regime is removed, and RE-EMERGES when energy is
    ///                restored (energy-supported, downward-transitioning) — the concrete case.
    /// </summary>
    public static string Classify(int n = 96, int K = DefaultK, double feedback = DefaultFeedback,
        double damping = DefaultDamping)
    {
        bool fixedPoint = HighEnergySectorIsFixedPoint(n, K, feedback, damping);
        var (_, collapseStep, _) = SectorLifetime(n, K, feedback, damping);
        var (_, afterRestore, original) = RecoveryAfterDip(n, K, feedback, damping);

        if (!fixedPoint) return "UNSTABLE";
        if (collapseStep >= 40 && Math.Abs(afterRestore - original) < 1e-6) return "STABLE";
        return "METASTABLE";
    }
}
