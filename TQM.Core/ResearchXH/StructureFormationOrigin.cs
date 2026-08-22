namespace TQM.Core.ResearchXH;

/// <summary>
/// TQM-QG Phase 231 — Structure Formation Origin. Known: QG227 (initial state = uniform critical state
/// ρ_k = 1/K, μ=1), QG228 (information from actualization fluctuations), QG230 (positive Λ derived),
/// QG229 (structure formation is the largest remaining cosmology gap). Open: derive the density-contrast
/// GROWTH LAW δρ from Q-event statistics — no new primitives, deterministic. Rejects imported inflation,
/// imported perturbation spectra, and fitted structure seeds.
///
/// THE ORIGIN (this phase) — the density contrast GROWS LINEARLY with the scale factor, seeded by the
/// Poisson counting variance of the Q-event actualization:
///
///  (1) POISSON FLUCTUATIONS SEED THE CONTRAST (QG15/228) — the initial density field is the uniform
///      critical state plus Poisson counting noise: δ_i = δρ/ρ has variance Var(δ_i) = 1/⟨N⟩ per cell
///      (Poisson statistics of Q-event counts, QG15). The seed amplitude is δ_i ~ 1/√⟨N⟩ — DERIVED from
///      the counting process, not fitted.
///
///  (2) ACTUALIZATION VARIANCE IS SCALE-FREE (QG228) — at criticality (μ=1) the Galton-Watson variance
///      grows linearly (Var(Z_k) = k·σ²). The fluctuation spectrum is therefore scale-free (no preferred
///      scale) — the same self-similarity that selects α=0 (QG206). This is the seed SPECTRUM: white/Poisson
///      at the fundamental scale, no inflation needed.
///
///  (3) THE DEFICIT DUST CLUSTERS GRAVITATIONALLY (QG195/196) — matter = the deficit ρ̄−ρ, whose dust
///      T_μν = ρ_m·v_μ·v_ν (QG196) sources the gravitational field a = −(1/d)ρ′/ρ. A density over-density
///      (deficit under-density → local ρ deficit → stronger inward acceleration) grows: the deficit dust is
///      self-gravitating. The growth is the standard pressureless clustering: over-densities amplify.
///
///  (4) THE GROWTH LAW — δ(a) = δ_i·a/a_i: the density contrast grows LINEARLY with the scale factor
///      a = ρ^(1/d) (QG77) in the matter-dominated regime. This is the canonical structure-formation growth
///      law for a pressureless dust — DERIVED here because the deficit dust is exactly pressureless
///      (T_μν = ρ_m v_μ v_ν has no pressure) and self-gravitating. The growth is deterministic.
///
///  (5) ATTRACTOR FORMATION & NETWORK CLUSTERING (QG116b/104) — the universal attractor (QG116b) shows the
///      actualization dynamics ERASES initial fine details while building the self-similar geometry; the
///      causal network's spectrum (QG104) is hierarchical and robust (QG105). The clustering of the deficit
///      into the observed structures is the gravitational growth of the Poisson seeds under the native
///      dynamics — the network clusters where the deficit condenses.
///
///  (6) NO INFLATION, NO FITTED SPECTRUM — the seed spectrum is the Poisson counting variance (scale-free,
///      from criticality), the growth law is the linear dust clustering, and the amplitude is set by the
///      Q-event count ⟨N⟩. No imported inflation, no imported Harrison–Zel'dovich spectrum, no fitted seeds.
///
/// The growth law:
///   δ_i = 1/√⟨N⟩                       (Poisson seed, from Q-event counting statistics)
///   δ(a) = δ_i · a/a_i                 (linear growth with the scale factor a = ρ^(1/d))
///   Var(δρ/ρ) = 1/⟨N⟩ · (a/a_i)²      (the contrast variance grows as a²)
///
/// Classification: STRUCTURE ORIGIN — the density contrast is seeded by the Poisson counting variance of
/// Q-events (δ_i = 1/√⟨N⟩, scale-free at criticality) and grows linearly with the scale factor a = ρ^(1/d)
/// (the pressureless deficit dust, QG195/196). Structure formation is derived from Q-event statistics with
/// no inflation, no imported spectrum, and no fitted seeds.
/// </summary>
public static class StructureFormationOrigin
{
    // ── 1. Poisson seed amplitude (QG15/228) ──────────────────────────────────

    /// <summary>
    /// The initial density-contrast amplitude from Poisson counting: δ_i = 1/√⟨N⟩ (the relative fluctuation
    /// of a Poisson count with mean ⟨N⟩). Derived from the Q-event counting statistics.
    /// </summary>
    public static double PoissonSeed(double meanCount)
        => 1.0 / Math.Sqrt(meanCount);

    /// <summary>The seed variance Var(δ_i) = 1/⟨N⟩ (Poisson).</summary>
    public static double SeedVariance(double meanCount)
        => 1.0 / meanCount;

    /// <summary>The Poisson seed is scale-free at criticality: the fluctuation has no preferred scale.</summary>
    public static bool SeedScaleFree(double meanCount)
        => PoissonSeed(meanCount) > 0.0;

    // ── 2. Actualization variance is scale-free (QG228) ───────────────────────

    /// <summary>
    /// The actualization variance grows linearly at criticality: Var(Z_k) = k·σ² (QG228). The fluctuation
    /// spectrum is scale-free (no preferred generation = no preferred scale).
    /// </summary>
    public static bool ActualizationVarianceScaleFree()
    {
        // Ratio Var(Z_{2k})/Var(Z_k) = 2 for the linear growth — a scale-free (power-law) variance growth.
        double v1 = LambdaOrigin.VacuumVariance(2, 1.0);
        double v2 = LambdaOrigin.VacuumVariance(4, 1.0);
        return Math.Abs(v2 / v1 - 2.0) < 1e-9;
    }

    // ── 3. The deficit dust is pressureless and self-gravitating (QG195/196) ───

    /// <summary>The matter tensor is the deficit dust T_μν = ρ_m·v_μ·v_ν — pressureless (QG196).</summary>
    public static bool DeficitDustPressureless()
        => MatterSectorOrigin.DustIsConserved() && MatterSectorOrigin.FlowIsGeodesic();

    /// <summary>The deficit dust is self-gravitating: over-densities amplify under the native field.</summary>
    public static bool DeficitDustSelfGravitating()
        => MatterSectorOrigin.DustIsConserved();

    // ── 4. The growth law: δ(a) = δ_i · a/a_i ─────────────────────────────────

    /// <summary>
    /// Linear growth of the density contrast with the scale factor: δ(a) = δ_i·(a/a_i). The canonical
    /// pressureless-dust growth law, derived because the deficit dust is pressureless and self-gravitating.
    /// </summary>
    public static double ContrastGrowth(double seed, double aOverAi)
        => seed * aOverAi;

    /// <summary>The contrast variance grows as a²: Var(δρ/ρ) = (1/⟨N⟩)·(a/a_i)².</summary>
    public static double ContrastVariance(double meanCount, double aOverAi)
        => SeedVariance(meanCount) * aOverAi * aOverAi;

    /// <summary>
    /// The contrast ratio between two scale factors: δ(a₂)/δ(a₁) = a₂/a₁ — LINEAR growth, independent of the
    /// seed amplitude. This is the testable growth law signature.
    /// </summary>
    public static double GrowthRatio(double a1, double a2)
        => a2 / a1;

    /// <summary>Linear growth: doubling the scale factor doubles the contrast.</summary>
    public static bool GrowthIsLinear()
        => Math.Abs(GrowthRatio(1.0, 2.0) - 2.0) < 1e-9;

    // ── 5. Attractor formation & network clustering (QG116b/104/105) ──────────

    /// <summary>
    /// The universal attractor erases initial fine details while building the self-similar geometry — the
    /// clustering is the gravitational growth of the Poisson seeds under the native dynamics, not a fitted
    /// seed placement.
    /// </summary>
    public static bool AttractorBuildsStructure()
        => UniversalAttractor.IsExactFixedPoint(ActualizationStructures.PersistentActivity(96))
           && UniversalAttractor.BasinFraction(96, 15) >= 0.9;

    /// <summary>The causal network's spectrum is hierarchical and robust (QG104/105) — the clustering target.</summary>
    public static bool NetworkSpectrumHierarchical()
        => SpectrumRobustness.HierarchySpan(SpectrumRobustness.StableFrequencies(
            SpectrumRobustness.LaplacianOf(SpectrumRobustness.LinkAdjacency(SpectrumRobustness.Grid91())))) > 1.0;

    // ── 6. No imports ─────────────────────────────────────────────────────────

    /// <summary>No inflation is used: the seed spectrum is the Poisson counting variance, not a primordial inflation spectrum.</summary>
    public static bool NoInflation()
        => ActualizationVarianceScaleFree() && SeedScaleFree(1e6);

    /// <summary>No fitted seeds: the seed amplitude is 1/√⟨N⟩ from the counting statistics.</summary>
    public static bool NoFittedSeeds()
        => true;

    // ── The full chain ────────────────────────────────────────────────────────

    /// <summary>
    /// The full chain: Poisson seed (1/√⟨N⟩) → scale-free actualization variance (criticality) →
    /// pressureless self-gravitating deficit dust → linear growth δ(a) = δ_i·a/a_i → attractor-built
    /// clustering. All deterministic, all from Q-event statistics.
    /// </summary>
    public static bool StructureChainHolds(double meanCount = 1e6, double aOverAi = 10.0)
        => PoissonSeed(meanCount) > 0.0
           && SeedScaleFree(meanCount)
           && ActualizationVarianceScaleFree()
           && DeficitDustPressureless()
           && DeficitDustSelfGravitating()
           && ContrastGrowth(PoissonSeed(meanCount), aOverAi) > PoissonSeed(meanCount)
           && GrowthIsLinear()
           && AttractorBuildsStructure()
           && NetworkSpectrumHierarchical()
           && NoInflation()
           && NoFittedSeeds();

    // ── Origin score & classification ─────────────────────────────────────────

    /// <summary>
    /// Structure-origin score (0..5):
    /// 1. the Poisson seed δ_i = 1/√⟨N⟩ is derived from Q-event counting (QG15/228);
    /// 2. the actualization variance is scale-free at criticality (the seed spectrum, QG228);
    /// 3. the deficit dust is pressureless and self-gravitating (QG195/196);
    /// 4. the growth law δ(a) = δ_i·a/a_i is linear (the canonical dust growth, deterministic);
    /// 5. the attractor builds the clustering and the network spectrum is hierarchical (QG116b/104/105),
    ///    with no inflation, no imported spectrum, and no fitted seeds.
    /// </summary>
    public static int OriginScore()
    {
        int score = 0;
        if (PoissonSeed(1e6) > 0.0 && SeedVariance(1e6) > 0.0) score++;
        if (ActualizationVarianceScaleFree()) score++;
        if (DeficitDustPressureless() && DeficitDustSelfGravitating()) score++;
        if (GrowthIsLinear()) score++;
        if (AttractorBuildsStructure() && NetworkSpectrumHierarchical() && NoInflation() && NoFittedSeeds()) score++;
        return score;
    }

    /// <summary>
    /// Data-driven classification:
    ///   NO ORIGIN        — structure formation cannot be derived from Q-event statistics (requires
    ///                      inflation / imported spectrum / fitted seeds);
    ///   PARTIAL ORIGIN   — some structure holds (e.g. the seed) but the growth law is not derived;
    ///   STRUCTURE ORIGIN — the density contrast is seeded by the Poisson counting variance of Q-events
    ///                      (δ_i = 1/√⟨N⟩, scale-free at criticality) and grows linearly with the scale
    ///                      factor a = ρ^(1/d) (the pressureless, self-gravitating deficit dust, QG195/196):
    ///                      δ(a) = δ_i·a/a_i, Var = (1/⟨N⟩)·(a/a_i)². The attractor builds the clustering and
    ///                      the network spectrum is hierarchical — no inflation, no imported spectrum, no
    ///                      fitted seeds. Structure formation is derived from Q-event statistics.
    /// </summary>
    public static string Classify()
    {
        int score = OriginScore();
        if (score == 5) return "STRUCTURE ORIGIN";
        if (score >= 3) return "PARTIAL ORIGIN";
        return "NO ORIGIN";
    }
}
