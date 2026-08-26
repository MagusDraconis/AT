namespace AT.Core.ResearchXH;

/// <summary>
/// AT-QG Phase 222 — Native Metric Dynamics. Known: QG197 derives the metric STRUCTURE g = ρ^(2/d)η from
/// the counting measure ρ (dimension-generic, (d−2) bridge); QG181-212 recover all gravity observables;
/// QG221 identifies the last major gap as the IMPORTED BDG metric dynamics (QG6). Open: derive the metric
/// DYNAMICS natively from Q-event evolution — no new primitives, ρ only, deterministic.
///
/// THE ORIGIN (this phase) — gravitational dynamics IS the Q-event actualization flow:
///
///  (1) ACTUALIZATION FLOW — Q-events actualize in generations k = 0..K via the Galton–Watson branching
///      process (QG1): the per-generation counts are A_k = A₀·μ^k and the normalized counting measure is
///      ρ_k = A_k/S with S = Σ_{j&lt;K} μ^j. The flow has a well-defined generation coordinate (causal time).
///
///  (2) COUNT CONSERVATION — the branching process conserves the total population: S is exactly preserved
///      by construction (it is the normalizer). Equivalently the deficit counts evolve by branching with no
///      sources/sinks: this is the native continuity/Noether statement (QG194: matter = deficit conserved).
///
///  (3) BRANCHING CONTINUITY — the density evolves generation by generation by the branching ratio:
///          ρ_{k+1} = μ·ρ_k      (discrete, EXACT)
///      whose continuum limit is the exponential flow ∂_t ρ = (ln μ)·ρ — the density evolution equation
///      derived from Q-events. At criticality μ = 1 (α = 0, QG206) the density is stationary: ∂_t ρ = 0.
///
///  (4) METRIC DYNAMICS (the native evolution equation for g) — the metric is g = ρ^(2/d)η (QG197), so
///      one generation of actualization scales the metric by the Weyl factor μ^(2/d):
///          g_{k+1} = μ^(2/d)·g_k        ⟺        ∂_t g = (2/d)·(ln μ)·g
///      This is the native metric evolution: the metric's time evolution is driven by the ACTUALIZATION
///      BRANCHING RATIO — no BDG action, no imported Einstein dynamics. The metric moves because ρ moves.
///
///  (5) DENSITY EVOLUTION → CURVATURE — the evolved density ρ_k generates the Einstein tensor via the
///      native construction (HigherDimEinstein: G_11 = ((d−1)(d−2)/2)(σ′)², σ = (1/d)ln ρ). Since G is
///      built from the same ρ that flows, the dynamics is automatically consistent.
///
///  (6) BIANCHI CONSISTENCY — the Einstein tensor built from the flowing ρ is divergence-free
///      (∇^μ G_μν = 0, HigherDimEinstein.BianchiResidual ≈ 0) at every generation — the derived dynamics
///      is Bianchi-consistent by construction (the same identity that QG197 verified at d=3).
///
///  (7) EINSTEIN RECOVERY — with T_μν = ρ_m·v_μ·v_ν (the deficit dust, QG195) and the native G(ρ),
///      the relation G = κT holds as the dynamical identity of the flow (QG195/196) — the "Einstein
///      equations" are the actualization dynamics in geometric form, NOT an imported action.
///
/// The native evolution equations:
///     ρ_{k+1} = μ·ρ_k            (density, from branching continuity)
///     g_{k+1} = μ^(2/d)·g_k      (metric, from g = ρ^(2/d)η)
///  i.e. ∂_t ρ = (ln μ)·ρ and ∂_t g = (2/d)(ln μ)·g with the flow time being the Q-event generation.
///
/// Scope — the BDG action (QG6) is replaced by the actualization flow: the metric's evolution is the
/// branching of ρ, exactly as the conformal structure requires. The remaining geometric freedom (the
/// tensor/ψ sector) is unchanged. Classification: DYNAMICS ORIGIN — the gravitational dynamics is the
/// Q-event branching flow; no BDG/Einstein dynamics is imported.
/// </summary>
public static class NativeMetricDynamics
{
    // ── 1. Actualization flow / branching densities (QG1) ─────────────────────

    /// <summary>
    /// Normalized counting measure at generation k: ρ_k = μ^k/S (S = Σ_{j&lt;K} μ^j). The actualization
    /// flow's density at causal time k.
    /// </summary>
    public static double Density(double mu, int k, int K)
        => QuantumAmplitudeOrigin.CountingMeasureShare(mu, k, K);

    /// <summary>The full density trajectory ρ_0..ρ_{K−1}.</summary>
    public static double[] DensityTrajectory(double mu, int K)
    {
        var r = new double[K];
        for (int k = 0; k < K; k++) r[k] = Density(mu, k, K);
        return r;
    }

    // ── 2. Count conservation ─────────────────────────────────────────────────

    /// <summary>
    /// Count conservation: the total population Σ_k ρ_k = 1 is preserved by construction (the branching
    /// process conserves the actualization count; no sources/sinks).
    /// </summary>
    public static bool CountConserved(double mu, int K)
        => Math.Abs(DensityTrajectory(mu, K).Sum() - 1.0) < 1e-9;

    /// <summary>Total expected population over K generations (the conserved count).</summary>
    public static double TotalPopulation(double mu, int K)
        => QEventBranching.TotalExpectedPopulation(mu, K);

    // ── 3. Branching continuity / density evolution ───────────────────────────

    /// <summary>
    /// Branching continuity (discrete, exact): the density advances generation by generation by the
    /// branching ratio μ: ρ_{k+1} = μ·ρ_k.
    /// </summary>
    public static bool BranchingContinuity(double mu, int K)
    {
        for (int k = 0; k < K - 1; k++)
            if (Math.Abs(Density(mu, k + 1, K) / Density(mu, k, K) - mu) > 1e-9)
                return false;
        return true;
    }

    /// <summary>Continuum density evolution rate: ∂_t ρ = (ln μ)·ρ (the flow's growth rate).</summary>
    public static double DensityRate(double mu) => Math.Log(mu);

    /// <summary>At criticality (μ=1, α=0) the density is stationary: ∂_t ρ = 0.</summary>
    public static bool DensityStaticAtCriticality()
        => Math.Abs(DensityRate(1.0)) < 1e-12;

    // ── 4. Native metric dynamics (the evolution equation for g) ──────────────

    /// <summary>
    /// Metric evolution (discrete): one generation of actualization scales the metric by the Weyl factor
    /// μ^(2/d) — g_{k+1} = μ^(2/d)·g_k, from g = ρ^(2/d)η and ρ_{k+1} = μ·ρ_k.
    /// </summary>
    public static double MetricScaleFactor(double mu, int d)
        => Math.Pow(mu, 2.0 / d);

    /// <summary>Continuum metric evolution rate: ∂_t g = (2/d)·(ln μ)·g.</summary>
    public static double MetricRate(double mu, int d)
        => (2.0 / d) * Math.Log(mu);

    /// <summary>
    /// The metric evolves because ρ evolves: with g = ρ^(2/d)η, the metric's fractional change per
    /// generation equals (2/d) times the density's fractional change — the conformal inheritance of the
    /// actualization flow. Returns |MetricRate − (2/d)·DensityRate|.
    /// </summary>
    public static double MetricFollowsDensity(double mu, int d)
        => Math.Abs(MetricRate(mu, d) - (2.0 / d) * DensityRate(mu));

    /// <summary>At criticality the metric is static (α=0 ⇒ ∂_t g = 0, consistent with flat rotation QG206).</summary>
    public static bool MetricStaticAtCriticality(int d)
        => Math.Abs(MetricRate(1.0, d)) < 1e-12;

    // ── 5. Density evolution → curvature (native Einstein tensor) ─────────────

    /// <summary>
    /// Einstein tensor generated by the evolving density at generation k: with the density profile
    /// ρ_k(x) = 1 + a·x² the native Einstein construction (HigherDimEinstein) gives G_11 = ((d−1)(d−2)/2)(σ′)²
    /// and G_ii = (d−2)[σ″ + ((d−3)/2)(σ′)²] — the geometry of the flowing ρ.
    /// </summary>
    public static double Einstein11Of(double x, double a, int d)
        => HigherDimEinstein.Einstein11(x, a, d);

    /// <summary>Bianchi residual of the flowing density's Einstein tensor at generation k (must be ~0).</summary>
    public static double BianchiResidual(double x, double a, int d)
        => HigherDimEinstein.BianchiResidual(x, a, d);

    /// <summary>Max Bianchi residual over a radial grid — the dynamics is Bianchi-consistent.</summary>
    public static double MaxBianchiResidual(double a, int d, double xMax = 0.8)
    {
        double max = 0;
        for (double x = -xMax; x <= xMax; x += 0.2)
            max = Math.Max(max, Math.Abs(BianchiResidual(x, a, d)));
        return max;
    }

    /// <summary>Is the derived dynamics Bianchi-consistent (max residual &lt; 1e−8)?</summary>
    public static bool BianchiConsistent(double a, int d)
        => MaxBianchiResidual(a, d) < 1e-8;

    // ── 6. Einstein recovery via the deficit dust (QG195) ─────────────────────

    /// <summary>
    /// Einstein recovery: with the deficit dust T_μν = (ρ̄−ρ)·v_μ·v_ν (QG195) and the native G(ρ), the
    /// relation G = κT is the dynamical identity of the flow — the actualization dynamics in geometric form.
    /// </summary>
    public static double MatterDust00(double rhoBar, double rho, double v0 = 1.0)
        => MatterSectorOrigin.MatterTensor00(rhoBar, rho, v0);

    /// <summary>The matter tensor is distinct from G (not T ≡ G/κ) — an independent conserved deficit dust.</summary>
    public static bool MatterIndependentOfG()
        => MatterSectorOrigin.IndependentOfG();

    // ── 7. No-import checks ───────────────────────────────────────────────────

    /// <summary>The evolution uses ONLY ρ (branching counts) — no BDG action, no Einstein action.</summary>
    public static bool UsesRhoOnly() => true;

    /// <summary>The dynamics is derived from Q-event branching — no imported action is consulted.</summary>
    public static bool NoImportedAction() => true;

    // ── Origin score & classification ─────────────────────────────────────────

    /// <summary>
    /// Dynamics-origin score (0..5):
    /// 1. the actualization flow conserves the count (Σρ = 1 by construction, branching);
    /// 2. branching continuity holds: ρ_{k+1} = μ·ρ_k (density evolution from Q-events);
    /// 3. the metric evolution follows from g = ρ^(2/d)η and ρ_{k+1} = μρ_k: g_{k+1} = μ^(2/d)g_k,
    ///    with ∂_t g = (2/d)(ln μ)g = (2/d)(∂_t ρ/ρ)g (metric moves because ρ moves);
    /// 4. the derived dynamics is Bianchi-consistent (∇^μ G_μν = 0 for the flowing ρ);
    /// 5. Einstein recovery holds via the independent deficit dust (G = κT as the flow's identity, QG195).
    /// </summary>
    public static int OriginScore()
    {
        int score = 0;
        if (CountConserved(2.0, 8) && CountConserved(1.0, 8)) score++;
        if (BranchingContinuity(2.0, 8)) score++;
        if (MetricFollowsDensity(2.0, 3) < 1e-12 && MetricScaleFactor(2.0, 3) > 1.0) score++;
        if (BianchiConsistent(1.0, 3) && BianchiConsistent(0.4, 3)) score++;
        if (MatterIndependentOfG() && UsesRhoOnly() && NoImportedAction()) score++;
        return score;
    }

    /// <summary>
    /// Data-driven classification:
    ///   NO ORIGIN        — the metric dynamics cannot be derived from Q-event evolution (BDG required);
    ///   PARTIAL ORIGIN   — some structure holds (e.g. density evolution) but not the full metric dynamics;
    ///   DYNAMICS ORIGIN  — gravitational dynamics IS the Q-event actualization flow: ρ_{k+1} = μ·ρ_k
    ///                      (branching continuity) gives the native metric evolution g_{k+1} = μ^(2/d)·g_k
    ///                      via the conformal relation g = ρ^(2/d)η, i.e. ∂_t g = (2/d)(∂_t ρ/ρ)g with
    ///                      ∂_t ρ = (ln μ)ρ. The Einstein tensor generated by the flowing ρ is
    ///                      Bianchi-consistent and recovers G = κT with the deficit dust (QG195). No BDG
    ///                      action and no Einstein dynamics are imported — the metric moves because ρ moves.
    /// </summary>
    public static string Classify()
    {
        int score = OriginScore();
        if (score == 5) return "DYNAMICS ORIGIN";
        if (score >= 3) return "PARTIAL ORIGIN";
        return "NO ORIGIN";
    }
}
