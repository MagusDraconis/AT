namespace AT.Core.Resonance.Theory;

/// <summary>
/// Generates nonlinear combinations of Theta eigenmodes, clusters the results
/// into composite species, and maps AT-139 attractors to their composite origins.
///
/// AT-141: Nonlinear Mode Composition and Species Emergence
/// </summary>
public static class SpeciesCompositionMap
{
    private const int N = 10;
    private const double CompositeSimilarityThreshold = 0.90;

    // ══════════════════════════════════════════════════════════════════
    // Get the eigenmode basis from AT-140.
    // ══════════════════════════════════════════════════════════════════

    private static List<double[]> GetEigenmodeBasis()
    {
        var modes = SpectralSpeciesMap.ComputeEigenmodes();
        return modes.Select(m => m.Eigenvector).ToList();
    }

    // ══════════════════════════════════════════════════════════════════
    // Generate all composite modes: single, pairs (linear + nonlinear), triples.
    // ══════════════════════════════════════════════════════════════════

    public static List<CompositeMode.CompositeModeInfo> GenerateComposites(
        int maxModes = 10, int? seed = null)
    {
        var rng = seed.HasValue ? new Random(seed.Value) : new Random(42);
        var basis = GetEigenmodeBasis();
        int K = Math.Min(maxModes, basis.Count);
        var composites = new List<CompositeMode.CompositeModeInfo>();
        int id = 0;

        // ── Single modes (pure eigenmodes) ──
        for (int i = 0; i < K; i++)
        {
            double energy = basis[i].Sum(x => x * x);
            int complexity = CountZeroCrossings(basis[i]);

            composites.Add(new CompositeMode.CompositeModeInfo(
                $"M{i}", (double[])basis[i].Clone(),
                new[] { i }, new[] { 1.0 }, false,
                10.0, complexity, energy));
        }

        // ── Linear pair combinations ──
        double[] coeffs = { 0.5, 1.0, 1.5 };
        for (int i = 0; i < K; i++)
        for (int j = i + 1; j < K; j++)
        foreach (double ca in coeffs)
        foreach (double cb in coeffs.Take(2))
        {
            if (Math.Abs(ca) < 0.1 && Math.Abs(cb) < 0.1) continue;
            var pattern = new double[N];
            for (int n = 0; n < N; n++)
                pattern[n] = ca * basis[i][n] + cb * basis[j][n];

            double norm = Math.Sqrt(pattern.Sum(x => x * x));
            if (norm > 1e-10 && norm < 10.0)
            {
                for (int n = 0; n < N; n++) pattern[n] /= norm;
                double energy = pattern.Sum(x => x * x);
                int complexity = CountZeroCrossings(pattern);

                composites.Add(new CompositeMode.CompositeModeInfo(
                    $"C{id++}", pattern,
                    new[] { i, j }, new[] { ca, cb }, false,
                    EstimateStability(i, j, false), complexity, energy));
            }
        }

        // ── Nonlinear pair combinations (with product term) ──
        double[] nlCoeffs = { 0.3, 0.5, 0.7 };
        for (int i = 0; i < K; i++)
        for (int j = i + 1; j < K; j++)
        foreach (double ca in nlCoeffs)
        foreach (double cb in nlCoeffs)
        foreach (double cc in nlCoeffs)
        {
            var pattern = new double[N];
            for (int n = 0; n < N; n++)
                pattern[n] = ca * basis[i][n] + cb * basis[j][n]
                           + cc * basis[i][n] * basis[j][n];

            double norm = Math.Sqrt(pattern.Sum(x => x * x));
            if (norm > 1e-10 && norm < 10.0)
            {
                for (int n = 0; n < N; n++) pattern[n] /= norm;
                double energy = pattern.Sum(x => x * x);
                int complexity = CountZeroCrossings(pattern);

                composites.Add(new CompositeMode.CompositeModeInfo(
                    $"N{id++}", pattern,
                    new[] { i, j }, new[] { ca, cb, cc }, true,
                    EstimateStability(i, j, true), complexity, energy));
            }
        }

        // ── Triple combinations ──
        for (int i = 0; i < K; i++)
        for (int j = i + 1; j < K; j++)
        for (int k = j + 1; k < K; k++)
        {
            if (k - i > 4) continue; // only nearby modes couple significantly
            var pattern = new double[N];
            double ca = 0.4 + rng.NextDouble() * 0.3;
            double cb = 0.4 + rng.NextDouble() * 0.3;
            double cc = 0.4 + rng.NextDouble() * 0.3;
            for (int n = 0; n < N; n++)
                pattern[n] = ca * basis[i][n] + cb * basis[j][n] + cc * basis[k][n];

            double norm = Math.Sqrt(pattern.Sum(x => x * x));
            if (norm > 1e-10 && norm < 10.0)
            {
                for (int n = 0; n < N; n++) pattern[n] /= norm;
                double energy = pattern.Sum(x => x * x);
                int complexity = CountZeroCrossings(pattern);

                composites.Add(new CompositeMode.CompositeModeInfo(
                    $"T{id++}", pattern,
                    new[] { i, j, k }, new[] { ca, cb, cc }, false,
                    EstimateStability(i, k, false) * 0.7, complexity, energy));
            }
        }

        return composites;
    }

    // ══════════════════════════════════════════════════════════════════
    // Cluster composites into species.
    // ══════════════════════════════════════════════════════════════════

    public static List<CompositeMode.CompositeModeInfo> ClusterComposites(
        List<CompositeMode.CompositeModeInfo> composites)
    {
        var clustered = new List<CompositeMode.CompositeModeInfo>();
        var used = new bool[composites.Count];

        for (int i = 0; i < composites.Count; i++)
        {
            if (used[i]) continue;
            var cluster = new List<CompositeMode.CompositeModeInfo> { composites[i] };
            used[i] = true;

            for (int j = i + 1; j < composites.Count; j++)
            {
                if (used[j]) continue;
                double sim = Math.Abs(DotProduct(composites[i].Pattern, composites[j].Pattern));
                if (sim > CompositeSimilarityThreshold)
                {
                    cluster.Add(composites[j]);
                    used[j] = true;
                }
            }

            // Use the most stable composite as the representative.
            var best = cluster.OrderByDescending(c => c.Stability).First();
            clustered.Add(best);
        }

        return clustered
            .OrderByDescending(c => c.Stability)
            .ThenBy(c => c.Complexity)
            .ToList();
    }

    // ══════════════════════════════════════════════════════════════════
    // Map AT-139 attractors to composite modes.
    // ══════════════════════════════════════════════════════════════════

    public static List<CompositeMode.SpeciesComposition> MapToComposites(
        List<AttractorBasin.AttractorBasinInfo> at139Species,
        List<CompositeMode.CompositeModeInfo> composites)
    {
        var mappings = new List<CompositeMode.SpeciesComposition>();

        foreach (var species in at139Species)
        {
            if (species.Prototype == null) continue;

            var pattern = PadPattern(species.Prototype, N);

            // Find best-matching composite.
            int bestIdx = -1;
            double bestOverlap = -1;

            for (int c = 0; c < composites.Count; c++)
            {
                double overlap = Math.Abs(DotProduct(pattern, composites[c].Pattern));
                if (overlap > bestOverlap)
                {
                    bestOverlap = overlap;
                    bestIdx = c;
                }
            }

            if (bestIdx >= 0)
            {
                var comp = composites[bestIdx];
                int modeCount = comp.SourceModes.Length;

                string compType = modeCount == 1 ? "Pure"
                                : modeCount == 2 && !comp.HasNonlinearTerm ? "Linear-Pair"
                                : modeCount == 2 && comp.HasNonlinearTerm ? "Nonlinear-Pair"
                                : "Triple";

                mappings.Add(new CompositeMode.SpeciesComposition(
                    species.Name, comp.SourceModes, bestOverlap,
                    modeCount == 1, modeCount == 2 && !comp.HasNonlinearTerm,
                    modeCount == 2 && comp.HasNonlinearTerm, modeCount >= 3,
                    compType));
            }
        }

        return mappings;
    }

    // ══════════════════════════════════════════════════════════════════
    // Compute mode coupling matrix C_ij.
    // ══════════════════════════════════════════════════════════════════

    public static CompositeMode.ModeCouplingMatrix ComputeCouplingMatrix(
        List<CompositeMode.CompositeModeInfo> composites, int modeCount)
    {
        int K = modeCount;
        var linearC = new double[K, K];
        var nonlinearC = new double[K, K];

        // Count how many stable composites involve each pair.
        foreach (var comp in composites)
        {
            if (comp.SourceModes.Length != 2) continue;
            int i = comp.SourceModes[0];
            int j = comp.SourceModes[1];
            if (i >= K || j >= K) continue;

            if (comp.HasNonlinearTerm)
                nonlinearC[i, j] += comp.Stability * 0.1;
            else
                linearC[i, j] += comp.Stability * 0.1;
        }

        // Find strongest pairs.
        var pairs = new List<(int i, int j, double strength)>();
        for (int i = 0; i < K; i++)
        for (int j = i + 1; j < K; j++)
        {
            double total = linearC[i, j] + nonlinearC[i, j];
            if (total > 0.01)
                pairs.Add((i, j, total));
        }

        var strongest = pairs.OrderByDescending(p => p.strength).Take(10)
            .Select(p => (p.i, p.j)).ToArray();
        int sigCount = pairs.Count(p => p.strength > 0.05);

        return new CompositeMode.ModeCouplingMatrix(
            K, linearC, nonlinearC, strongest, sigCount);
    }

    // ══════════════════════════════════════════════════════════════════
    // Helpers.
    // ══════════════════════════════════════════════════════════════════

    private static double DotProduct(double[] a, double[] b)
    {
        int n = Math.Min(a.Length, b.Length);
        double dot = 0, na = 0, nb = 0;
        for (int i = 0; i < n; i++) { dot += a[i] * b[i]; na += a[i] * a[i]; nb += b[i] * b[i]; }
        double denom = Math.Sqrt(na * nb);
        return denom > 1e-10 ? dot / denom : 0;
    }

    private static int CountZeroCrossings(double[] p)
    {
        int zc = 0;
        for (int i = 1; i < p.Length; i++)
            if (p[i] * p[i - 1] < 0) zc++;
        return zc;
    }

    private static double EstimateStability(int i, int j, bool nonlinear)
    {
        // Nearby modes couple more strongly → more stable composites.
        double separation = Math.Abs(i - j);
        double baseStability = 10.0 / (1.0 + separation * 0.5);
        if (nonlinear) baseStability *= 0.7; // nonlinear modes less stable
        return baseStability;
    }

    private static double[] PadPattern(double[] pattern, int targetLength)
    {
        var result = new double[targetLength];
        int n = Math.Min(pattern.Length, targetLength);
        for (int i = 0; i < n; i++) result[i] = pattern[i];
        return result;
    }
}
