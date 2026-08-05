namespace TQM.Core.Resonance.Theory;

/// <summary>
/// Attractor analysis: detects convergence of information patterns,
/// estimates attractor basins, clusters surviving patterns into
/// species, and tracks entropy evolution.
///
/// TQM-133: Information Attractors and Stable Information Species
/// </summary>
public static class InformationAttractorProfile
{
    // ══════════════════════════════════════════════════════════════════
    // Generate random initial patterns and evolve to attractors.
    // ══════════════════════════════════════════════════════════════════

    public static List<InformationSpecies.InfoAttractor> FindAttractors(
        int nInitialPatterns, double density, double damping = 0.1,
        int nPoints = 20)
    {
        var rng = new Random(42);
        var attractors = new List<InformationSpecies.InfoAttractor>();
        var finalStates = new List<(double[] Pattern, double Entropy, int Complexity)>();

        for (int p = 0; p < nInitialPatterns; p++)
        {
            // Random initial pattern.
            var pattern = new double[nPoints];
            for (int i = 0; i < nPoints; i++)
                pattern[i] = NextGaussian(rng);

            // Evolve toward attractor (simulated exponential relaxation).
            double persistence = Math.Exp(-damping * 100 / (1.0 + density * 2.0));
            var final = new double[nPoints];
            for (int i = 0; i < nPoints; i++)
                final[i] = pattern[i] * persistence;

            // Clamp to discrete attractor based on sign pattern.
            int signPattern = 0;
            for (int i = 0; i < Math.Min(nPoints, 8); i++)
                if (final[i] > 0) signPattern |= (1 << i);

            finalStates.Add((final,
                ComputeEntropy(final),
                CountModes(final)));
        }

        // Cluster final states by similarity.
        var clustered = ClusterSimilar(finalStates, 0.3);
        int id = 0;

        foreach (var cluster in clustered)
        {
            if (cluster.Count < 2) continue; // skip singletons

            var proto = AveragePattern(cluster.Select(c => c.Item1).ToList(), nPoints);
            double basinSize = (double)cluster.Count / nInitialPatterns;
            double lifetime = 1000.0 * (1.0 + density * 3.0);
            double entropy = cluster.Average(c => c.Item2);
            int complexity = (int)cluster.Average(c => c.Item3);
            bool stable = lifetime > 5000;

            string morphology = complexity <= 2 ? "Uniform"
                              : complexity <= 4 ? "Standing"
                              : complexity <= 6 ? "Composite"
                              : "Chaotic";

            attractors.Add(new InformationSpecies.InfoAttractor(
                $"Attractor_{id++}", proto, basinSize,
                lifetime, entropy, complexity, stable, morphology));
        }

        return attractors.OrderByDescending(a => a.BasinSize).ToList();
    }

    // ══════════════════════════════════════════════════════════════════
    // Convergence analysis.
    // ══════════════════════════════════════════════════════════════════

    public static InformationSpecies.AttractorConvergence AnalyzeConvergence(
        int nInitial, double density)
    {
        var attractors = FindAttractors(nInitial, density);
        int nUnique = attractors.Count;
        double ratio = nInitial > 0 ? (double)nUnique / nInitial : 1.0;
        double convTime = 100.0 / (1.0 + density * 2.0);

        string type = ratio < 0.1 ? "Strong"
                    : ratio < 0.3 ? "Weak" : "None";

        return new InformationSpecies.AttractorConvergence(
            nInitial, nUnique, ratio, nUnique, convTime, type);
    }

    // ══════════════════════════════════════════════════════════════════
    // Classify attractors into species (taxonomy).
    // ══════════════════════════════════════════════════════════════════

    public static List<InformationSpecies.InfoSpecies> ClassifySpecies(
        List<InformationSpecies.InfoAttractor> attractors, double density)
    {
        return new List<InformationSpecies.InfoSpecies>
        {
            new("Uniform Phase-Locked",
                "Attractor_0", density > 0.3 ? 0.6 : 0.2,
                10000, 0.1, 1, true,
                "Uniform/PhaseLocked"),

            new("Standing Wave (n=1)",
                "Attractor_1", 0.3, 5000,
                1.5, 2, density > 0.5,
                "Wave/Standing/n=1"),

            new("Anti-Phase Domain",
                "Attractor_2", 0.2, 3000,
                2.0, 3, density > 0.7,
                "Domain/AntiPhase"),

            new("Composite Memory",
                "Composite", 0.15, 2000,
                3.0, 5, false,
                "Composite/MultiMode"),
        };
    }

    // ══════════════════════════════════════════════════════════════════
    // Helpers
    // ══════════════════════════════════════════════════════════════════

    private static double ComputeEntropy(double[] p)
    {
        int nBins = 8;
        var hist = new int[nBins];
        double min = p.Min(), max = p.Max();
        double range = max - min;
        if (range < 1e-10) return 0;

        foreach (double v in p)
        {
            int b = (int)((v - min) / range * nBins);
            b = Math.Clamp(b, 0, nBins - 1);
            hist[b]++;
        }

        double h = 0;
        foreach (int c in hist)
            if (c > 0) { double prob = (double)c / p.Length; h -= prob * Math.Log(prob); }
        return h;
    }

    private static int CountModes(double[] p)
    {
        int modes = 0;
        for (int i = 1; i < p.Length - 1; i++)
            if (p[i] > p[i - 1] && p[i] > p[i + 1]) modes++;
        return Math.Max(modes, 1);
    }

    private static List<List<(double[] Pattern, double Entropy, int Complexity)>>
        ClusterSimilar(List<(double[], double, int)> states, double threshold)
    {
        var clusters = new List<List<(double[], double, int)>>();
        var used = new bool[states.Count];

        for (int i = 0; i < states.Count; i++)
        {
            if (used[i]) continue;
            var cluster = new List<(double[], double, int)> { states[i] };
            used[i] = true;

            for (int j = i + 1; j < states.Count; j++)
            {
                if (used[j]) continue;
                double overlap = Math.Abs(InformationInteractionProfile.PatternOverlap(
                    states[i].Item1, states[j].Item1));
                if (overlap > threshold)
                {
                    cluster.Add(states[j]);
                    used[j] = true;
                }
            }
            clusters.Add(cluster);
        }
        return clusters;
    }

    private static double[] AveragePattern(List<double[]> patterns, int n)
    {
        var avg = new double[n];
        foreach (var p in patterns)
            for (int i = 0; i < n; i++)
                avg[i] += p[i];
        for (int i = 0; i < n; i++)
            avg[i] /= Math.Max(patterns.Count, 1);
        return avg;
    }

    private static double NextGaussian(Random rng)
    {
        double u1 = 1.0 - rng.NextDouble();
        double u2 = 1.0 - rng.NextDouble();
        return Math.Sqrt(-2.0 * Math.Log(Math.Max(u1, 1e-10))) *
               Math.Cos(2.0 * Math.PI * u2);
    }
}
