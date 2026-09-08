namespace AT.Core.ResearchT;

/// <summary>Result of Darwinian replicator competition over the attractor set.</summary>
public sealed record CompetitionResult(
    string Model,
    int AttractorCount,          // non-zero eigenspaces (attractors) in competition
    double InitialDominance,     // largest multiplicity fraction BEFORE competition
    double FinalDominance,       // D = max(p) after the replicator run
    double EffectiveAttractors,  // N_eff = exp(H) after the run
    double ExtinctionFraction,   // fraction of attractors with p < 1e-6
    double HHI,                  // Σ p_i² (concentration, 1 = monopoly)
    int TimeToDominance,         // steps to first reach D ≥ 0.9 (0 = already, −1 = never)
    double FitnessRatio);        // w_max / w_2nd

/// <summary>
/// Darwinian dominance via replicator dynamics over the attractor (eigenspace) set.
///
/// Each attractor i is an eigenspace of the graph Laplacian with eigenvalue λ_i and
/// multiplicity m_i. Fitness is scored MODEL-FREE (no AT assumptions):
///   w_i = r_i / c_i,  r_i = m_i (resource acquisition ∝ basin size), c_i = λ_i
///   (maintenance cost ∝ mode energy). So w_i = m_i / λ_i.
/// Replicator: p_i(t+1) = p_i(t)·w_i / Σ_j p_j(t)·w_j, from uniform p_i(0) = 1/A.
/// The zero (uniform) mode is excluded as the trivial structureless attractor.
/// Deterministic throughout.
/// </summary>
public static class DarwinianDominanceAnalyzer
{
    public const double DominanceThreshold = 0.9;
    public const int DefaultSteps = 2000;

    public static CompetitionResult RunCompetition(string model, double[,] adjacency, int steps = DefaultSteps)
    {
        var (distinct, mult) = AttractorDominanceAnalyzer.Eigenspaces(adjacency);
        return Compete(model, distinct, mult, steps);
    }

    public static CompetitionResult RunCompetitionSpectrum(string model, double[] spectrum, int steps = DefaultSteps)
    {
        var (distinct, mult) = AttractorDominanceAnalyzer.GroupSpectrum(spectrum.OrderBy(x => x).ToArray());
        return Compete(model, distinct, mult, steps);
    }

    private static CompetitionResult Compete(string model, double[] distinct, int[] mult, int steps)
    {
        int n = mult.Sum();
        // Non-zero (structured) eigenspaces only — the uniform mode is the trivial attractor.
        var idx = new List<int>();
        for (int i = 0; i < distinct.Length; i++)
            if (distinct[i] > 1e-9) idx.Add(i);

        int A = idx.Count;
        if (A == 0)
            return new CompetitionResult(model, 0, 0, 0, 0, 0, 0, -1, 0);

        var w = new double[A];
        for (int k = 0; k < A; k++)
            w[k] = mult[idx[k]] / distinct[idx[k]];   // fitness = multiplicity / eigenvalue

        double initialD = idx.Max(i => (double)mult[i]) / n;   // largest basin before competition

        var p = Enumerable.Repeat(1.0 / A, A).ToArray();
        int timeToDominance = -1;
        for (int t = 1; t <= steps; t++)
        {
            double z = 0.0;
            for (int i = 0; i < A; i++) z += p[i] * w[i];
            for (int i = 0; i < A; i++) p[i] = p[i] * w[i] / z;

            if (timeToDominance < 0 && p.Max() >= DominanceThreshold)
                timeToDominance = t;
        }

        double D = p.Max();
        double H = 0.0;
        foreach (double x in p)
            if (x > 1e-300) H -= x * Math.Log(x);
        double neff = Math.Exp(H);
        double extinct = (double)p.Count(x => x < 1e-6) / A;
        double hhi = p.Sum(x => x * x);

        double wMax = w.Max();
        double wSecond = A > 1 ? w.OrderByDescending(x => x).Skip(1).First() : 0.0;
        double ratio = A > 1 && wSecond > 0 ? wMax / wSecond : double.PositiveInfinity;

        return new CompetitionResult(model, A, initialD, D, neff, extinct, hhi, timeToDominance, ratio);
    }
}
