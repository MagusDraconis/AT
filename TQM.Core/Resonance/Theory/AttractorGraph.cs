namespace TQM.Core.Resonance.Theory;

/// <summary>
/// Maps the global topology of the Theta information attractor landscape
/// using potential-based gradient descent on pattern vectors.
///
/// TQM-139: Information Attractor Landscape Topology
/// </summary>
public static class AttractorGraph
{
    private const int PatternDim = 10;
    private const double GradientStep = 0.05;
    private const int MaxSteps = 200;
    private const double ConvergenceTolerance = 1e-4;
    private const double BasinSimilarityThreshold = 0.75;

    // Explicit attractor centers — these create the multi-minimum landscape.
    private static readonly double[][] AttractorCenters = GenerateAttractorCenters();

    private static double[][] GenerateAttractorCenters()
    {
        var centers = new List<double[]>();
        // Create ~20 attractor centers at diverse Fourier mode configurations.
        for (int k = 0; k <= 4; k++)        // frequency modes 0-4
        for (int phase = 0; phase < 5; phase++) // phase offsets
        {
            var p = new double[PatternDim];
            double ph = phase * Math.PI * 2 / 5;
            double amp = 1.0 + (k % 3) * 0.5;
            for (int i = 0; i < PatternDim; i++)
            {
                double angle = 2 * Math.PI * k * i / PatternDim + ph;
                p[i] = amp * Math.Sin(angle);
                if (k > 0) p[i] += 0.3 * Math.Cos(angle * (k + 1));
            }
            centers.Add(p);
        }
        return centers.ToArray();
    }

    // ══════════════════════════════════════════════════════════════════
    // The effective information potential V(p) — multi-minimum landscape.
    // ══════════════════════════════════════════════════════════════════
    // V(p) = Σ w_k · exp(-||p - center_k||² / (2·σ²))
    //       + α · smoothness(p) + β · roughness(p)
    //
    // Each attractor center is a Gaussian well.
    // Smoothness favors low-frequency patterns.
    // Roughness penalizes noise.
    // ══════════════════════════════════════════════════════════════════

    private static double EffectivePotential(double[] p)
    {
        // Attraction to nearest centers (negative = deeper).
        double nearestDist = double.MaxValue;
        foreach (var center in AttractorCenters)
        {
            double d2 = 0;
            for (int i = 0; i < PatternDim; i++)
                d2 += (p[i] - center[i]) * (p[i] - center[i]);
            if (d2 < nearestDist) nearestDist = d2;
        }

        // Multi-scale potential: nearest-center well + smoothness + roughness.
        double sigma2 = 2.0;
        double wellDepth = -2.0 * Math.Exp(-nearestDist / (2 * sigma2));

        double smooth = Smoothness(p);
        double rough = Roughness(p);

        return wellDepth - 0.5 * smooth + 0.3 * rough;
    }

    private static double[] ComputeGradient(double[] p)
    {
        var grad = new double[PatternDim];
        double h = 0.001;
        double v0 = EffectivePotential(p);

        for (int i = 0; i < PatternDim; i++)
        {
            p[i] += h;
            double vp = EffectivePotential(p);
            p[i] -= h;
            grad[i] = (vp - v0) / h;
        }
        return grad;
    }

    // ══════════════════════════════════════════════════════════════════
    // Landscape component functions.
    // ══════════════════════════════════════════════════════════════════

    /// <summary>
    /// Smoothness: energy concentration in low Fourier modes.
    /// Higher smoothness = lower potential = more stable.
    /// </summary>
    private static double Smoothness(double[] p)
    {
        int n = p.Length;
        double total = p.Sum(x => x * x);
        if (total < 1e-10) return 0;

        double lowEnergy = 0;
        for (int k = 0; k <= 2; k++)
        {
            double re = 0, im = 0;
            for (int i = 0; i < n; i++)
            {
                double angle = 2 * Math.PI * k * i / n;
                re += p[i] * Math.Cos(angle);
                im += p[i] * Math.Sin(angle);
            }
            lowEnergy += (re * re + im * im) / (n * n);
        }
        return lowEnergy / Math.Max(total / n, 0.01);
    }

    /// <summary>
    /// Roughness: high-frequency energy fraction. Penalizes noisy patterns.
    /// </summary>
    private static double Roughness(double[] p)
    {
        int n = p.Length;
        double total = p.Sum(x => x * x);
        if (total < 1e-10) return 0;

        double highFreq = 0;
        for (int i = 1; i < n; i++)
            highFreq += (p[i] - p[i - 1]) * (p[i] - p[i - 1]);

        return highFreq / Math.Max(total * 10, 0.01);
    }

    // ══════════════════════════════════════════════════════════════════
    // Generate random initial conditions.
    // ══════════════════════════════════════════════════════════════════

    public static List<double[]> GenerateInitialConditions(
        int count, string type = "random", int? seed = null)
    {
        var rng = seed.HasValue ? new Random(seed.Value) : new Random(42);
        var ics = new List<double[]>();

        for (int i = 0; i < count; i++)
        {
            var p = new double[PatternDim];
            switch (type)
            {
                case "low_entropy":
                    // Mostly uniform with small noise.
                    for (int j = 0; j < PatternDim; j++)
                        p[j] = 1.0 + NextGaussian(rng) * 0.1;
                    break;
                case "high_entropy":
                    // Completely random.
                    for (int j = 0; j < PatternDim; j++)
                        p[j] = NextGaussian(rng) * 2.0;
                    break;
                case "structured":
                    // Sinusoidal patterns.
                    double freq = 0.5 + rng.NextDouble() * 3;
                    double phase = rng.NextDouble() * 2 * Math.PI;
                    double amp = 0.5 + rng.NextDouble() * 1.5;
                    for (int j = 0; j < PatternDim; j++)
                        p[j] = amp * Math.Sin(freq * 2 * Math.PI * j / PatternDim + phase)
                             + NextGaussian(rng) * 0.2;
                    break;
                case "mixed":
                default:
                    // Mix of structured and random.
                    if (rng.NextDouble() < 0.5)
                    {
                        for (int j = 0; j < PatternDim; j++)
                            p[j] = NextGaussian(rng) * 2.0;
                    }
                    else
                    {
                        double f = 0.5 + rng.NextDouble() * 3;
                        double ph = rng.NextDouble() * 2 * Math.PI;
                        double a = 0.5 + rng.NextDouble() * 1.5;
                        for (int j = 0; j < PatternDim; j++)
                            p[j] = a * Math.Sin(f * 2 * Math.PI * j / PatternDim + ph)
                                 + NextGaussian(rng) * 0.3;
                    }
                    break;
            }
            ics.Add(p);
        }
        return ics;
    }

    // ══════════════════════════════════════════════════════════════════
    // Gradient descent to find the nearest local minimum.
    // ══════════════════════════════════════════════════════════════════

    public static (double[] FinalPattern, double FinalPotential, bool Converged, int Steps)
        GradientDescent(double[] initial)
    {
        var p = (double[])initial.Clone();
        double prevV = EffectivePotential(p);

        for (int step = 0; step < MaxSteps; step++)
        {
            var grad = ComputeGradient(p);
            double gradNorm = Math.Sqrt(grad.Sum(g => g * g));

            if (gradNorm < ConvergenceTolerance)
            {
                double finalV = EffectivePotential(p);
                return (p, finalV, true, step);
            }

            // Gradient descent step with adaptive step size.
            double stepSize = GradientStep / (1.0 + gradNorm * 0.1);
            for (int i = 0; i < PatternDim; i++)
                p[i] -= stepSize * grad[i];

            double currentV = EffectivePotential(p);
            if (Math.Abs(currentV - prevV) < ConvergenceTolerance * 0.1)
            {
                return (p, currentV, true, step);
            }
            prevV = currentV;
        }

        double v = EffectivePotential(p);
        return (p, v, false, MaxSteps);
    }

    // ══════════════════════════════════════════════════════════════════
    // Map the complete attractor landscape.
    // ══════════════════════════════════════════════════════════════════

    public static (List<AttractorBasin.AttractorBasinInfo> Basins,
                   List<AttractorBasin.AttractorTransition> Transitions,
                   int TotalICs, int Converged)
        MapLandscape(int nInitialConditions = 1000, int? seed = null)
    {
        var rng = seed.HasValue ? new Random(seed.Value) : new Random(42);

        // Generate ICs of different types.
        var ics = new List<double[]>();
        ics.AddRange(GenerateInitialConditions(nInitialConditions / 4, "mixed", rng.Next()));
        ics.AddRange(GenerateInitialConditions(nInitialConditions / 4, "low_entropy", rng.Next()));
        ics.AddRange(GenerateInitialConditions(nInitialConditions / 4, "high_entropy", rng.Next()));
        ics.AddRange(GenerateInitialConditions(nInitialConditions / 4, "structured", rng.Next()));

        // Gradient descent all ICs.
        var finalStates = new List<(double[] Pattern, double Potential)>();
        int converged = 0;

        foreach (var ic in ics)
        {
            var (final, potential, conv, _) = GradientDescent(ic);
            if (conv)
            {
                finalStates.Add((final, potential));
                converged++;
            }
        }

        // Cluster final states into attractor basins.
        var clusters = ClusterBasins(finalStates, BasinSimilarityThreshold);

        // Build basin records.
        var basins = new List<AttractorBasin.AttractorBasinInfo>();
        int basinId = 0;
        foreach (var cluster in clusters)
        {
            if (cluster.Count < 3) continue; // too small to be a real attractor

            var proto = AveragePattern(cluster.Select(c => c.Pattern).ToList());
            double volume = (double)cluster.Count / converged;
            double stability = 500 + cluster.Count * 2; // proxy
            double fitness = Smoothness(proto);
            double complexity = CountZeroCrossings(proto);
            double energy = proto.Sum(x => x * x);
            double depth = cluster.Average(c => c.Potential);

            string symClass = complexity < 1 ? "Uniform"
                            : complexity < 3 ? "Odd"
                            : complexity < 5 ? "Even"
                            : complexity < 7 ? "Mixed" : "Complex";

            string name = basinId < 4
                ? new[] { "A", "B", "C", "D" }[basinId]
                : $"N{basinId - 3}";

            basins.Add(new AttractorBasin.AttractorBasinInfo(
                name, proto, volume, stability, fitness,
                complexity, energy, depth, 0, symClass));
            basinId++;
        }

        // Compute connectivity (edges between nearby basins).
        for (int i = 0; i < basins.Count; i++)
        {
            int connections = 0;
            for (int j = 0; j < basins.Count; j++)
            {
                if (i == j) continue;
                double sim = Math.Abs(InformationInteractionProfile.PatternOverlap(
                    basins[i].Prototype, basins[j].Prototype));
                if (sim > 0.3) connections++;
            }
            basins[i] = basins[i] with { Connectivity = connections };
        }

        // Build transition edges.
        var transitions = new List<AttractorBasin.AttractorTransition>();
        for (int i = 0; i < basins.Count; i++)
        for (int j = 0; j < basins.Count; j++)
        {
            if (i == j) continue;
            double sim = Math.Abs(InformationInteractionProfile.PatternOverlap(
                basins[i].Prototype, basins[j].Prototype));
            if (sim > 0.3)
            {
                double distance = EuclideanDistance(basins[i].Prototype, basins[j].Prototype);
                double barrier = (basins[i].PotentialDepth + basins[j].PotentialDepth) / 2
                    + distance * 0.5;
                double prob = Math.Exp(-barrier * 2) * sim;
                bool bidirectional = Math.Abs(basins[i].PotentialDepth - basins[j].PotentialDepth) < 0.5;

                transitions.Add(new AttractorBasin.AttractorTransition(
                    basins[i].Name, basins[j].Name,
                    prob, barrier, distance, bidirectional));
            }
        }

        return (basins, transitions, ics.Count, converged);
    }

    // ══════════════════════════════════════════════════════════════════
    // Compute graph topology metrics.
    // ══════════════════════════════════════════════════════════════════

    public static AttractorBasin.AttractorGraphInfo ComputeTopology(
        List<AttractorBasin.AttractorBasinInfo> basins,
        List<AttractorBasin.AttractorTransition> transitions)
    {
        int n = basins.Count;
        int e = transitions.Count;
        double meanConn = n > 0 ? basins.Average(b => b.Connectivity) : 0;
        double density = n > 1 ? (double)e / (n * (n - 1)) : 0;

        // Connected components (flood fill).
        var adj = new Dictionary<string, HashSet<string>>();
        foreach (var b in basins) adj[b.Name] = new HashSet<string>();
        foreach (var t in transitions)
        {
            adj[t.FromAttractor].Add(t.ToAttractor);
            adj[t.ToAttractor].Add(t.FromAttractor);
        }

        var visited = new HashSet<string>();
        int components = 0;
        foreach (string name in adj.Keys)
        {
            if (visited.Contains(name)) continue;
            components++;
            var queue = new Queue<string>();
            queue.Enqueue(name);
            visited.Add(name);
            while (queue.Count > 0)
            {
                string cur = queue.Dequeue();
                foreach (string nb in adj[cur])
                    if (!visited.Contains(nb)) { visited.Add(nb); queue.Enqueue(nb); }
            }
        }

        bool fullyConnected = components <= 1 && density > 0.3;

        // Diameter (longest shortest path via BFS from each node).
        int diameter = 0;
        foreach (string start in adj.Keys)
        {
            var dist = new Dictionary<string, int>();
            var q = new Queue<string>();
            dist[start] = 0; q.Enqueue(start);
            while (q.Count > 0)
            {
                string cur = q.Dequeue();
                int d = dist[cur];
                if (d > diameter) diameter = d;
                foreach (string nb in adj[cur])
                    if (!dist.ContainsKey(nb)) { dist[nb] = d + 1; q.Enqueue(nb); }
            }
        }

        // Clustering coefficient.
        double clustering = 0;
        foreach (string node in adj.Keys)
        {
            var neighbors = adj[node].ToList();
            int k = neighbors.Count;
            if (k < 2) continue;
            int edgesBetween = 0;
            for (int i = 0; i < k; i++)
            for (int j = i + 1; j < k; j++)
                if (adj[neighbors[i]].Contains(neighbors[j])) edgesBetween++;
            clustering += (double)edgesBetween / (k * (k - 1) / 2);
        }
        clustering = adj.Count > 0 ? clustering / adj.Count : 0;

        // Central hubs.
        int hubCount = basins.Count(b => b.Connectivity > 2 * meanConn && meanConn > 0);

        // Bottlenecks (nodes whose removal increases components).
        var bottlenecks = new List<string>();
        foreach (string node in adj.Keys.ToList())
        {
            var tempVisited = new HashSet<string> { node };
            string start = adj.Keys.FirstOrDefault(k => k != node);
            if (start == null) continue;
            var q2 = new Queue<string>(); q2.Enqueue(start); tempVisited.Add(start);
            while (q2.Count > 0)
            {
                string cur = q2.Dequeue();
                foreach (string nb in adj[cur])
                    if (!tempVisited.Contains(nb) && nb != node)
                    { tempVisited.Add(nb); q2.Enqueue(nb); }
            }
            if (tempVisited.Count < adj.Count - 1) // removal increased fragmentation
                bottlenecks.Add(node);
        }

        string topology;
        if (clustering > 0.5 && diameter < n / 2) topology = "Small-World";
        else if (hubCount > 0 && clustering > 0.2) topology = "Hub-and-Spoke";
        else if (clustering > 0.3 && diameter > n / 2) topology = "Hierarchical";
        else if (density > 0.4) topology = "Lattice";
        else if (hubCount > 0 || components > 1) topology = "Modular";
        else topology = "Random";

        return new AttractorBasin.AttractorGraphInfo(
            basins, transitions, n, e, meanConn, density,
            components, fullyConnected, diameter, clustering,
            topology, hubCount, bottlenecks);
    }

    // ══════════════════════════════════════════════════════════════════
    // 1D potential slice along principal axis.
    // ══════════════════════════════════════════════════════════════════

    public static double[] ComputePotentialSlice(int nPoints = 100)
    {
        // Principal axis: interpolate between min and max potential attractors.
        var slice = new double[nPoints];
        for (int i = 0; i < nPoints; i++)
        {
            // Variable sinusoidal pattern along the axis.
            double t = (double)i / nPoints;
            var p = new double[PatternDim];
            for (int j = 0; j < PatternDim; j++)
                p[j] = Math.Sin(t * 4 * Math.PI + j * 0.5) + 0.5 * Math.Cos(t * 8 * Math.PI + j * 0.3);
            slice[i] = EffectivePotential(p);
        }
        return slice;
    }

    // ══════════════════════════════════════════════════════════════════
    // Helpers.
    // ══════════════════════════════════════════════════════════════════

    private static List<List<(double[] Pattern, double Potential)>> ClusterBasins(
        List<(double[] Pattern, double Potential)> states, double threshold)
    {
        var clusters = new List<List<(double[], double)>>();
        var used = new bool[states.Count];

        for (int i = 0; i < states.Count; i++)
        {
            if (used[i]) continue;
            var cluster = new List<(double[], double)> { states[i] };
            used[i] = true;

            for (int j = i + 1; j < states.Count; j++)
            {
                if (used[j]) continue;
                double sim = Math.Abs(InformationInteractionProfile.PatternOverlap(
                    states[i].Pattern, states[j].Pattern));
                if (sim > threshold)
                {
                    cluster.Add(states[j]);
                    used[j] = true;
                }
            }
            clusters.Add(cluster);
        }
        return clusters.OrderByDescending(c => c.Count).ToList();
    }

    private static double[] AveragePattern(List<double[]> patterns)
    {
        int n = patterns.FirstOrDefault()?.Length ?? PatternDim;
        var avg = new double[n];
        foreach (var p in patterns)
            for (int i = 0; i < n; i++)
                avg[i] += p[i];
        for (int i = 0; i < n; i++)
            avg[i] /= Math.Max(patterns.Count, 1);
        return avg;
    }

    private static int CountZeroCrossings(double[] p)
    {
        int zc = 0;
        for (int i = 1; i < p.Length; i++)
            if (p[i] * p[i - 1] < 0) zc++;
        return zc;
    }

    private static double EuclideanDistance(double[] a, double[] b)
    {
        int n = Math.Min(a.Length, b.Length);
        double sum = 0;
        for (int i = 0; i < n; i++) sum += (a[i] - b[i]) * (a[i] - b[i]);
        return Math.Sqrt(sum);
    }

    private static double NextGaussian(Random rng)
    {
        double u1 = 1.0 - rng.NextDouble();
        double u2 = 1.0 - rng.NextDouble();
        return Math.Sqrt(-2.0 * Math.Log(Math.Max(u1, 1e-10))) *
               Math.Cos(2.0 * Math.PI * u2);
    }
}
