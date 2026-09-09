namespace AT.Core.ResearchT;

/// <summary>
/// Result of a bounded-innovation (Darwinian) evolution run over a spectral landscape.
/// </summary>
public sealed record InnovationResult(
    string Model,
    int LandscapeSize,          // A = number of distinct non-zero modes (potential species)
    int FinalSpecies,           // species above the extinction threshold at saturation
    int ExtinctionEvents,       // cumulative alive→dead crossings over the run
    int ColonizationEvents,     // cumulative dead→alive crossings over the run
    double Turnover,            // (extinction + colonization) / steps — per-step turnover
    int SaturationTime,         // first step of a flat plateau window (−1 = never saturates)
    double FinalEntropy,        // Shannon entropy (nats) of the equilibrium distribution
    double FinalDiversity,      // exp(H) = effective number of species
    double FinalDominance,      // max x_i at the end
    int[] Survivors,            // sorted mode indices with x_i > threshold at saturation
    string Verdict);            // BOUNDED / UNBOUNDED / CONDITIONAL

/// <summary>
/// Bounded Innovation Audit. Tests whether Darwinian evolution on a spectral landscape
/// produces a FINITE, saturated species count, or whether mutation-driven "innovation"
/// keeps the diversity growing.
///
/// Model — replicator–mutator dynamics with resource (crowding) constraint:
///   · landscape  = the distinct non-zero eigenspaces of the graph Laplacian, ordered by
///                  eigenvalue ascending. Each mode k is a potential species.
///   · fitness    w_k = m_k / λ_k (resource ∝ multiplicity, cost ∝ eigenvalue), the same
///                  model-free score as T_006.
///   · selection  x_k grows in proportion to w_k.
///   · crowding   (resource constraint / carrying capacity) density-dependent fitness
///                  f_k = w_k / (1 + β·x_k): a species that fills its niche suppresses its
///                  own growth, so it cannot take all resources.
///   · mutation   each step, a fraction μ of offspring of mode k appear at neighbouring
///                  modes k±1 (a ring in eigenvalue order) — innovation/colonization.
///   · extinction species with abundance x_k ≤ ε are treated as extinct for measurement.
///
/// Update (deterministic, mass-conserving):
///   y_k = (1−μ) x_k f_k + (μ/2) x_{k−1} f_{k−1} + (μ/2) x_{k+1} f_{k+1},  x ← y / Σy.
/// With positive mutation μ &gt; 0 and crowding β &gt; 0 the map has a unique interior fixed
/// point, so the species count saturates: boundedness is DERIVED, the saturation value is
/// EMERGENT (it depends on μ, β, and the landscape). Deterministic throughout.
/// </summary>
public static class BoundedInnovationAnalyzer
{
    public const double DefaultMutationRate = 0.01;
    public const double DefaultCrowding = 1.0;
    public const double DefaultExtinctionThreshold = 1e-6;
    public const int DefaultSteps = 20000;
    public const int PlateauWindow = 500;

    public static InnovationResult Run(
        string model,
        double[,] adjacency,
        int steps = DefaultSteps,
        double mutationRate = DefaultMutationRate,
        double crowding = DefaultCrowding)
    {
        var (distinct, mult) = AttractorDominanceAnalyzer.Eigenspaces(adjacency);
        return Evolve(model, distinct, mult, steps, mutationRate, crowding);
    }

    public static InnovationResult RunSpectrum(
        string model,
        double[] spectrum,
        int steps = DefaultSteps,
        double mutationRate = DefaultMutationRate,
        double crowding = DefaultCrowding)
    {
        var sorted = spectrum.OrderBy(x => x).ToArray();
        var (distinct, mult) = AttractorDominanceAnalyzer.GroupSpectrum(sorted);
        return Evolve(model, distinct, mult, steps, mutationRate, crowding);
    }

    private static InnovationResult Evolve(
        string model,
        double[] distinct,
        int[] mult,
        int steps,
        double mutationRate,
        double crowding)
    {
        // Non-zero modes only — the uniform (zero) mode is the trivial, structureless attractor.
        var ids = new List<int>();
        for (int i = 0; i < distinct.Length; i++)
            if (distinct[i] > 1e-9) ids.Add(i);

        int A = ids.Count;
        if (A == 0)
            return new InnovationResult(model, 0, 0, 0, 0, 0.0, -1, 0.0, 0.0, 0.0,
                Array.Empty<int>(), "BOUNDED");

        var w = new double[A];
        for (int k = 0; k < A; k++)
            w[k] = mult[ids[k]] / distinct[ids[k]];   // fitness = multiplicity / eigenvalue

        double mu = mutationRate;
        double beta = crowding;
        double eps = DefaultExtinctionThreshold;

        var x = new double[A];
        for (int k = 0; k < A; k++) x[k] = 1.0 / A;   // uniform initial distribution

        var alive = new bool[A];
        var prevAlive = new bool[A];
        for (int k = 0; k < A; k++) alive[k] = x[k] > eps;

        int extinctions = 0, colonizations = 0;
        int saturationTime = -1;
        var plateau = new Queue<int>();

        var f = new double[A];
        var y = new double[A];

        for (int t = 1; t <= steps; t++)
        {
            // Density-dependent fitness and its normalizer Z = Σ_k x_k f_k.
            double z = 0.0;
            for (int k = 0; k < A; k++)
            {
                f[k] = w[k] / (1.0 + beta * x[k]);
                z += x[k] * f[k];
            }

            // Ring mutation (nearest-neighbour in eigenvalue order). For A == 2 the two
            // neighbours coincide, giving total mutation μ to the single other mode.
            for (int k = 0; k < A; k++)
            {
                double s = (1.0 - mu) * x[k] * f[k];
                if (A > 1)
                {
                    int left = (k - 1 + A) % A;
                    int right = (k + 1) % A;
                    s += (mu / 2.0) * x[left] * f[left];
                    s += (mu / 2.0) * x[right] * f[right];
                }
                else
                {
                    // A == 1: a single mode has nowhere to mutate, so the mutation term is a
                    // self-loop that conserves mass (x stays at its normalized value 1.0).
                    s += mu * x[k] * f[k];
                }
                y[k] = s;
            }

            for (int k = 0; k < A; k++) x[k] = y[k] / z;

            // Extinction / colonization accounting.
            for (int k = 0; k < A; k++) prevAlive[k] = alive[k];
            for (int k = 0; k < A; k++)
            {
                alive[k] = x[k] > eps;
                if (alive[k] && !prevAlive[k]) colonizations++;
                if (!alive[k] && prevAlive[k]) extinctions++;
            }

            // Saturation: first step of a run of `PlateauWindow` flat species-count values.
            int speciesNow = alive.Count(a => a);
            plateau.Enqueue(speciesNow);
            if (plateau.Count > PlateauWindow) plateau.Dequeue();
            if (saturationTime < 0 && plateau.Count == PlateauWindow &&
                plateau.All(c => c == speciesNow))
                saturationTime = t - PlateauWindow + 1;
        }

        double h = 0.0;
        foreach (double xx in x)
            if (xx > 1e-300) h -= xx * Math.Log(xx);

        double finalDominance = x.Max();
        int finalSpecies = alive.Count(a => a);
        double turnover = steps > 0 ? (double)(extinctions + colonizations) / steps : 0.0;
        int[] survivors = Enumerable.Range(0, A).Where(k => x[k] > eps).ToArray();

        string verdict = saturationTime >= 0 ? "BOUNDED" : "UNBOUNDED";

        return new InnovationResult(model, A, finalSpecies, extinctions, colonizations,
            turnover, saturationTime, h, Math.Exp(h), finalDominance, survivors, verdict);
    }

    /// <summary>
    /// Open-landscape control: a deterministic niche-filling process where a species count
    /// grows at a fixed innovation rate per step, optionally capped by a resource carrying
    /// capacity. Without a cap the count grows linearly (UNBOUNDED); with a finite cap it
    /// saturates (BOUNDED) — the resource constraint is what bounds innovation.
    /// </summary>
    public static int[] OpenLandscapeSpeciesCount(int steps, double innovationRate, double? carryingCapacity)
    {
        var counts = new int[steps + 1];
        double s = 1.0;
        counts[0] = (int)Math.Round(s);
        for (int t = 1; t <= steps; t++)
        {
            s += innovationRate;
            if (carryingCapacity.HasValue)
                s = Math.Min(s, carryingCapacity.Value);
            counts[t] = (int)Math.Round(s);
        }
        return counts;
    }

    /// <summary>Verdict for the open-landscape control: UNBOUNDED (no cap), BOUNDED (cap reached), CONDITIONAL (growing but below cap at horizon).</summary>
    public static string ClassifyOpenLandscape(int finalCount, double unboundedCount, double? carryingCapacity)
    {
        if (carryingCapacity is null)
            return finalCount > 1 ? "UNBOUNDED" : "BOUNDED";
        if (finalCount >= carryingCapacity.Value)
            return "BOUNDED";
        return "CONDITIONAL";
    }
}
