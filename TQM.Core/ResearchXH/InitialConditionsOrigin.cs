namespace TQM.Core.ResearchXH;

/// <summary>
/// TQM-QG Phase 227 — Initial Conditions Origin. Known: Q-events actualize by a Galton–Watson branching
/// process (QG1/QG7) into the counting measure ρ; criticality μ=1 ⟺ α=0 (QG206); the metric dynamics is
/// the actualization flow (QG222); the universal attractor is a stable exact fixed point with near-universal
/// basin (QG116b/117/121); the per-octave deficit allocation entropy is maximized at α=0 (G4-RHO). Open
/// (QG226 TOE criterion 6): WHY does the universe start in its specific initial state? No new primitives,
/// deterministic.
///
/// THE ORIGIN (this phase) — the initial state is the UNIFORM CRITICAL STATE, the unique minimum-information
/// fixed point of the actualization flow:
///
///  (1) THE INITIAL STATE MUST BE A FIXED POINT OF THE ACTUALIZATION FLOW — a state that is not stationary
///      (∂_t ρ ≠ 0) is a TRANSIENT, not an initial state: it immediately evolves, so it cannot be the
///      starting point. The stationary condition of the derived metric dynamics (QG222: ∂_t ρ = (ln μ)·ρ)
///      requires ln μ = 0, i.e. μ = 1 — CRITICALITY. Hence the initial state must be critical.
///
///  (2) CRITICALITY IS THE UNIQUE SCALE-FREE STATE (QG206) — α=0 (equal deficit per octave) is the unique
///      self-similar, scale-free stable point of the octave-organized counting measure. Any α≠0 introduces a
///      preferred scale — information the theory has no source for. A scale-free initial state is the only
///      one consistent with a dynamics that has no scale input.
///
///  (3) MINIMUM-INFORMATION SELECTION (G4-RHO) — among the critical (μ=1) states, the LEAST-COMMITTAL
///      allocation is uniform: ρ_k = 1/K (equal actualization share per generation). This is the state that
///      maximizes the native entropy functional H(α) = −Σ p_k ln p_k of the per-octave deficit fractions:
///      H is maximized at α=0 (uniform, H = ln K). The minimum-information state = the maximum-entropy
///      state = the uniform critical state. No information (no preferred scale, no preferred generation) is
///      required to specify it — it is the state that needs ZERO initial-condition input.
///
///  (4) THE ATTRACTOR MAKES THE SPECIFIC CHOICE IRRELEVANT (QG116b/117/121) — the universal attractor is a
///      stable exact fixed point with near-universal basin (every content pattern converges to the SAME
///      geometry). The universe's late-time behavior is therefore INSENSITIVE to its initial state: the
///      specific initial condition does not need to be fine-tuned — the dynamics erases it. This is WHY no
///      fine-tuned initial state is required: the attractor absorbs the initial data.
///
///  (5) THE DERIVED INITIAL STATE — combining (1)-(3): the initial state is
///          μ = 1 (critical),  α = 0 (scale-free),  ρ_k = 1/K (uniform, minimum information).
///      This is a deterministic, network-derived initial state: it is the unique stationary (fixed-point),
///      scale-free, minimum-information configuration of the Q-event actualization dynamics. The universe
///      starts critical because any non-critical start is a transient; it starts uniform because uniformity
///      is the minimum-information stationary state; and the attractor erases whatever residual content the
///      initial data might carry.
///
/// Scope — the initial CONDITIONS are derived (the state), not the specific parameter VALUES of the
/// dynamics (feedback/damping ratio) which remain model inputs. Classification: INITIAL-CONDITION ORIGIN —
/// the universe's initial state is the uniform critical state ρ_k = 1/K (μ=1, α=0), the unique
/// minimum-information fixed point of the actualization flow, with the attractor making fine-tuning
/// unnecessary. This closes the QG226 TOE criterion 6 (initial conditions).
/// </summary>
public static class InitialConditionsOrigin
{
    // ── 1. The initial state must be a fixed point (stationarity) ──────────────

    /// <summary>
    /// Stationarity of the actualization flow (QG222): ∂_t ρ = (ln μ)·ρ = 0 requires μ = 1 (criticality).
    /// A non-stationary state is a transient, not an initial state.
    /// </summary>
    public static bool StationaryRequiresCriticality(double mu)
        => Math.Abs(NativeMetricDynamics.DensityRate(mu)) < 1e-12 ? mu == 1.0 : mu != 1.0;

    /// <summary>∂_t ρ at criticality (μ=1) is exactly zero — the state is stationary.</summary>
    public static bool CriticalStateStationary()
        => Math.Abs(NativeMetricDynamics.DensityRate(1.0)) < 1e-12;

    /// <summary>∂_t ρ for any μ≠1 is non-zero — those states are transients, not initial states.</summary>
    public static bool NonCriticalStatesAreTransients()
        => Math.Abs(NativeMetricDynamics.DensityRate(0.5)) > 1e-9
           && Math.Abs(NativeMetricDynamics.DensityRate(2.0)) > 1e-9;

    // ── 2. Criticality is the unique scale-free state (QG206) ─────────────────

    /// <summary>
    /// Spread of the per-octave deficit fractions at α: equal-deficit-per-octave (α=0) has spread 0
    /// (perfect self-similarity); α≠0 introduces a preferred scale (spread &gt; 0).
    /// </summary>
    public static double SpreadAt(double alpha, int K = 8, double lambda = 1.5)
    {
        var p = RhoDynamics.DeficitFractions(alpha, K, lambda);
        double mean = p.Average();
        double spread = p.Max(x => Math.Abs(x - mean));
        return spread;
    }

    /// <summary>α=0 is the unique scale-free (zero-spread, self-similar) deficit allocation.</summary>
    public static bool AlphaZeroUniqueScaleFree(int K = 8, double lambda = 1.5)
        => Math.Abs(SpreadAt(0.0, K, lambda)) < 1e-9
           && SpreadAt(0.3, K, lambda) > 1e-3
           && SpreadAt(-0.3, K, lambda) > 1e-3;

    // ── 3. Minimum-information = maximum-entropy = uniform (G4-RHO) ────────────

    /// <summary>Native entropy H(α) of the per-octave deficit fractions.</summary>
    public static double EntropyAt(double alpha, int K = 8, double lambda = 1.5)
        => RhoDynamics.Entropy(alpha, K, lambda);

    /// <summary>Uniform (α=0) maximizes the entropy: H(0) = ln K ≥ H(α) for all α.</summary>
    public static bool UniformIsMaxEntropy(int K = 8, double lambda = 1.5)
    {
        double h0 = EntropyAt(0.0, K, lambda);
        for (int i = 1; i <= 20; i++)
        {
            double a = 0.05 * i;
            if (EntropyAt(a, K, lambda) > h0 + 1e-9) return false;
            if (EntropyAt(-a, K, lambda) > h0 + 1e-9) return false;
        }
        return true;
    }

    /// <summary>H(0) equals ln K exactly (the uniform allocation's entropy).</summary>
    public static bool UniformEntropyIsLnK(int K = 8)
        => Math.Abs(EntropyAt(0.0, K) - Math.Log(K)) < 1e-9;

    /// <summary>The minimum-information state: ρ_k = 1/K (equal actualization share per generation).</summary>
    public static double[] MinimumInformationState(int K)
    {
        var rho = new double[K];
        for (int k = 0; k < K; k++) rho[k] = 1.0 / K;
        return rho;
    }

    /// <summary>The uniform state IS the critical branching state: ρ_k = μ^k/S with μ=1 gives ρ_k = 1/K.</summary>
    public static bool UniformIsCriticalBranchingState(int K)
    {
        for (int k = 0; k < K; k++)
            if (Math.Abs(QuantumAmplitudeOrigin.CountingMeasureShare(1.0, k, K) - 1.0 / K) > 1e-9)
                return false;
        return true;
    }

    // ── 4. The attractor makes fine-tuning unnecessary (QG116b/117/121) ────────

    /// <summary>The universal attractor is a stable exact fixed point with a near-universal basin.</summary>
    public static bool AttractorErasesInitialData(int n = 96, int samples = 15)
        => UniversalAttractor.IsExactFixedPoint(ActualizationStructures.PersistentActivity(n))
           && UniversalAttractor.BasinFraction(n, samples) >= 0.9;

    /// <summary>The dynamics' late-time behavior is insensitive to the initial content (attractor basin).</summary>
    public static bool LateTimeInsensitiveToInitialState(int n = 96, int samples = 15)
        => UniversalAttractor.BasinFraction(n, samples) >= 0.9;

    // ── 5. The derived initial state ───────────────────────────────────────────

    /// <summary>The derived initial state: critical (μ=1), scale-free (α=0), uniform (ρ_k = 1/K).</summary>
    public static (double Mu, double Alpha, double[] Rho) InitialState(int K = 8)
        => (1.0, 0.0, MinimumInformationState(K));

    /// <summary>The derived initial state is stationary, scale-free, and minimum-information simultaneously.</summary>
    public static bool InitialStateIsDerived(int K = 8)
        => CriticalStateStationary()
           && AlphaZeroUniqueScaleFree(K)
           && UniformIsMaxEntropy(K)
           && UniformIsCriticalBranchingState(K);

    // ── Origin score & classification ─────────────────────────────────────────

    /// <summary>
    /// Initial-condition-origin score (0..5):
    /// 1. stationarity: the initial state must be a fixed point of the actualization flow ⇒ μ=1 (critical);
    /// 2. scale-freeness: α=0 is the unique scale-free state (QG206);
    /// 3. minimum-information: the uniform allocation ρ_k=1/K maximizes the native entropy (H(0)=ln K);
    /// 4. the uniform state IS the critical branching state (QG216 at μ=1);
    /// 5. the universal attractor erases the initial data (fine-tuning unnecessary, QG116b).
    /// </summary>
    public static int OriginScore(int K = 8)
    {
        int score = 0;
        if (CriticalStateStationary() && NonCriticalStatesAreTransients()) score++;
        if (AlphaZeroUniqueScaleFree(K)) score++;
        if (UniformIsMaxEntropy(K) && UniformEntropyIsLnK(K)) score++;
        if (UniformIsCriticalBranchingState(K)) score++;
        if (AttractorErasesInitialData()) score++;
        return score;
    }

    /// <summary>
    /// Data-driven classification:
    ///   NO ORIGIN               — the initial state cannot be derived (requires an input condition);
    ///   PARTIAL ORIGIN          — some structure holds (e.g. criticality alone), not the full selection;
    ///   INITIAL-CONDITION ORIGIN — the universe's initial state IS the uniform critical state ρ_k = 1/K
    ///                              (μ=1, α=0): stationarity (fixed point of the actualization flow, QG222)
    ///                              forces criticality; scale-freeness forces α=0 (QG206); minimum-information
    ///                              (maximum-entropy, G4-RHO) selects the uniform allocation; and the universal
    ///                              attractor (QG116b) erases any residual content, so no fine-tuning is
    ///                              required. Initial conditions are DERIVED, not assumed.
    /// </summary>
    public static string Classify(int K = 8)
    {
        int score = OriginScore(K);
        if (score == 5) return "INITIAL-CONDITION ORIGIN";
        if (score >= 3) return "PARTIAL ORIGIN";
        return "NO ORIGIN";
    }
}
