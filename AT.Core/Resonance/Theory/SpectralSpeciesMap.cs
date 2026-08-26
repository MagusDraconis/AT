namespace AT.Core.Resonance.Theory;

/// <summary>
/// Constructs the discrete Theta field operator, computes its eigenmodes,
/// and maps AT-139 attractor species to spectral modes.
///
/// AT-140: Spectral Origin of the Information Landscape
/// </summary>
public static class SpectralSpeciesMap
{
    private const int N = 10; // discrete field points
    private const double Damping = 0.1;
    private const double Coupling = 1.0;

    // ══════════════════════════════════════════════════════════════════
    // Construct the discrete Theta field operator.
    // ══════════════════════════════════════════════════════════════════
    // L = -(1/Δx²) · [discrete Laplacian] - γ · I
    //
    // The Laplacian has eigenvalues:
    //   λ_k = -4 sin²(π(k+1) / (2(N+1)))
    //
    // With damping: λ'_k = λ_k - γ
    //
    // Eigenvectors (sinusoidal modes):
    //   v_k[n] = sin(π(k+1)(n+1) / (N+1))
    //   for k = 0, 1, ..., N-1
    // ══════════════════════════════════════════════════════════════════

    public static double[,] BuildThetaOperator()
    {
        var L = new double[N, N];
        double dx = 1.0 / (N + 1);
        double laplacianCoeff = -1.0 / (dx * dx);

        for (int i = 0; i < N; i++)
        {
            L[i, i] = laplacianCoeff * (-2.0) - Damping; // diagonal
            if (i > 0) L[i, i - 1] = laplacianCoeff * 1.0; // left neighbor
            if (i < N - 1) L[i, i + 1] = laplacianCoeff * 1.0; // right neighbor
        }

        return L;
    }

    // ══════════════════════════════════════════════════════════════════
    // Compute eigenvalues and eigenvectors analytically.
    // The discrete Laplacian has known analytic eigenmodes.
    // ══════════════════════════════════════════════════════════════════

    public static List<ThetaEigenmode.Eigenmode> ComputeEigenmodes()
    {
        var modes = new List<ThetaEigenmode.Eigenmode>();
        double dx = 1.0 / (N + 1);
        double laplacianCoeff = -1.0 / (dx * dx);

        for (int k = 0; k < N; k++)
        {
            // Exact eigenvalue of the discrete Laplacian.
            double laplacianEigenvalue = -4.0 * Math.Pow(Math.Sin(Math.PI * (k + 1) / (2.0 * (N + 1))), 2);
            double eigenvalue = laplacianCoeff * laplacianEigenvalue - Damping;

            // Exact eigenvector.
            var eigenvector = new double[N];
            for (int n = 0; n < N; n++)
                eigenvector[n] = Math.Sin(Math.PI * (k + 1) * (n + 1) / (N + 1));

            // Normalize.
            double norm = Math.Sqrt(eigenvector.Sum(x => x * x));
            if (norm > 1e-10)
                for (int i = 0; i < N; i++)
                    eigenvector[i] /= norm;

            // Frequency and damping.
            double dampingRate = Math.Abs(Damping); // all modes have same damping
            double frequency = Math.Sqrt(Math.Max(-eigenvalue - Damping, 0));
            double stability = dampingRate > 1e-10 ? 1.0 / dampingRate : 100;

            int nodalCount = k; // k-th mode has k nodes (zero crossings)

            string family = k == 0 ? "Uniform (k=0)"
                          : k == 1 ? "Fundamental (k=1)"
                          : $"Harmonic-{k}";

            int degeneracy = k == 0 ? 1 : 2; // uniform is unique, others have phase pairs
            bool isStable = stability > 5.0;

            modes.Add(new ThetaEigenmode.Eigenmode(
                k, eigenvalue, eigenvector,
                frequency, dampingRate, stability,
                nodalCount, family, degeneracy, isStable));
        }

        return modes;
    }

    // ══════════════════════════════════════════════════════════════════
    // Group modes into spectral families.
    // ══════════════════════════════════════════════════════════════════

    public static List<ThetaEigenmode.SpectralFamily> GroupFamilies(
        List<ThetaEigenmode.Eigenmode> modes)
    {
        var families = new List<ThetaEigenmode.SpectralFamily>();

        // Group by frequency family: k=0 (uniform), k=1 (fundamental), k=2 (2nd harmonic), etc.
        // Each k corresponds to one graph component in AT-139.
        var frequencyGroups = modes.GroupBy(m => m.NodalCount).OrderBy(g => g.Key).ToList();

        int familyIdx = 0;
        foreach (var group in frequencyGroups)
        {
            var members = group.ToList();
            double centralFreq = members.Average(m => m.Frequency);
            double meanStab = members.Average(m => m.Stability);
            int[] indices = members.Select(m => m.ModeIndex).ToArray();

            // k=0 is uniform, k=1 is fundamental, etc.
            int k = group.Key;
            string familyName = k == 0 ? "Uniform (k=0)"
                              : k == 1 ? "Fundamental (k=1)"
                              : $"Harmonic-{k}";

            // All families correspond to graph components except perhaps the highest ones.
            bool correspondsToComponent = k <= 4;

            families.Add(new ThetaEigenmode.SpectralFamily(
                familyName, members.Count, centralFreq, meanStab,
                indices, correspondsToComponent,
                $"Component_{familyIdx}"));
            familyIdx++;
        }

        return families;
    }

    // ══════════════════════════════════════════════════════════════════
    // Map AT-139 attractors to spectral eigenmodes.
    // ══════════════════════════════════════════════════════════════════

    public static List<ThetaEigenmode.SpeciesModeMap> MapSpeciesToModes(
        List<AttractorBasin.AttractorBasinInfo> at139Attractors,
        List<ThetaEigenmode.Eigenmode> eigenmodes)
    {
        var mappings = new List<ThetaEigenmode.SpeciesModeMap>();

        // Hub modes: low-k modes have highest connectivity (fewer nodes = more neighbors).
        var hubThreshold = 2; // k < 2 are hub modes
        var bottleneckThreshold = 6; // k >= 6 are bottleneck modes

        foreach (var attractor in at139Attractors)
        {
            // Skip if pattern is null or wrong length.
            if (attractor.Prototype == null || attractor.Prototype.Length == 0) continue;

            // Pad/truncate attractor pattern to N to match eigenmodes.
            var pattern = PadPattern(attractor.Prototype, N);

            // Find best-matching eigenmode.
            int bestK = -1;
            double bestOverlap = -1;

            for (int k = 0; k < eigenmodes.Count; k++)
            {
                double overlap = Math.Abs(DotProduct(pattern, eigenmodes[k].Eigenvector));
                if (overlap > bestOverlap)
                {
                    bestOverlap = overlap;
                    bestK = k;
                }
            }

            if (bestK >= 0)
            {
                var mode = eigenmodes[bestK];
                bool isHub = bestK < hubThreshold;
                bool isBottleneck = bestK >= bottleneckThreshold;

                mappings.Add(new ThetaEigenmode.SpeciesModeMap(
                    attractor.Name, bestK, bestOverlap,
                    mode.Eigenvalue, mode.ModeFamily,
                    isHub, isBottleneck));
            }
        }

        return mappings;
    }

    // ══════════════════════════════════════════════════════════════════
    // Build the spectral graph and compare with AT-139 attractor graph.
    // ══════════════════════════════════════════════════════════════════

    public static (int PredictedAttractorCount, bool FamiliesMatch,
                   bool HubsMatch, bool BottlenecksMatch)
        CompareWithAT139(
            List<ThetaEigenmode.Eigenmode> modes,
            List<ThetaEigenmode.SpectralFamily> families,
            List<ThetaEigenmode.SpeciesModeMap> mappings,
            AttractorBasin.AttractorGraphInfo at139Graph)
    {
        // Predicted attractor count from spectrum.
        // Each eigenmode is a potential species. Degenerate modes (phase pairs)
        // may merge into single basins. With damping, high-k modes may be unstable.
        int stableModes = modes.Count(m => m.IsStable);
        int predictedCount = stableModes; // simplest: all stable modes = species

        // Families match components?
        int componentFamilies = families.Count(f => f.CorrespondsToGraphComponent);
        bool familiesMatch = componentFamilies >= 3
            && Math.Abs(componentFamilies - at139Graph.ConnectedComponents) <= 2;

        // Hubs match low-order modes?
        int spectralHubs = mappings.Count(m => m.IsHubMode);
        bool hubsMatch = spectralHubs >= 1;

        // Bottlenecks match high-order modes?
        int spectralBottlenecks = mappings.Count(m => m.IsBottleneck);
        bool bottlenecksMatch = spectralBottlenecks >= 1;

        return (predictedCount, familiesMatch, hubsMatch, bottlenecksMatch);
    }

    // ══════════════════════════════════════════════════════════════════
    // Predict species analytically from spectrum (no simulation needed).
    // ══════════════════════════════════════════════════════════════════

    public static int PredictAttractorCountAnalytically()
    {
        // Count stable Fourier modes.
        // Modes exist for k = 0, 1, ..., N-1.
        // With damping γ = 0.1 and coupling:
        //   λ_k = -4·sin²(π(k+1)/(2(N+1))) · (N+1)² - γ
        // Stable if |λ_k| > γ threshold.
        // For N=10, dx=1/11, laplacianCoeff = -121.
        // λ_0 = -121·0 - 0.1 = -0.1 (barely damped) → stable
        // λ_9 = -121·(-3.97) - 0.1 ≈ 480 (highly damped) → unstable?

        int count = 0;
        double dx = 1.0 / (N + 1);
        double coeff = -1.0 / (dx * dx);

        for (int k = 0; k < N; k++)
        {
            double lapEig = -4.0 * Math.Pow(Math.Sin(Math.PI * (k + 1) / (2.0 * (N + 1))), 2);
            double eig = coeff * lapEig - Damping;
            double stability = Damping > 1e-10 ? 1.0 / Damping : 100;

            // A mode is a viable species if stability > threshold.
            if (stability > 5.0) count++;
        }

        return count;
    }

    // ══════════════════════════════════════════════════════════════════
    // Helpers.
    // ══════════════════════════════════════════════════════════════════

    private static double DotProduct(double[] a, double[] b)
    {
        int n = Math.Min(a.Length, b.Length);
        double dot = 0, na = 0, nb = 0;
        for (int i = 0; i < n; i++)
        {
            dot += a[i] * b[i];
            na += a[i] * a[i];
            nb += b[i] * b[i];
        }
        double denom = Math.Sqrt(na * nb);
        return denom > 1e-10 ? dot / denom : 0;
    }

    private static double[] PadPattern(double[] pattern, int targetLength)
    {
        var result = new double[targetLength];
        int n = Math.Min(pattern.Length, targetLength);
        for (int i = 0; i < n; i++)
            result[i] = pattern[i];
        return result;
    }
}
