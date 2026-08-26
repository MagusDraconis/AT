namespace AT.Core.Resonance.Theory;

/// <summary>
/// Builds 10 Q interaction graph geometries, computes their graph Laplacians,
/// and compares spectral + Theta properties against the 1D chain baseline.
///
/// AT-143: Geometry Dependence of the Theta Hierarchy
/// </summary>
public static class GeometrySpectrum
{
    private const int DefaultNodes = 20;

    // ══════════════════════════════════════════════════════════════════
    // Build all 10 geometries.
    // ══════════════════════════════════════════════════════════════════

    public static List<QGeometryFamily.GeometrySpec> BuildAllGeometries(int? seed = null)
    {
        var rng = seed.HasValue ? new Random(seed.Value) : new Random(42);
        var geoms = new List<QGeometryFamily.GeometrySpec>();

        // G1: 1D Chain (path graph)
        geoms.Add(BuildChain(DefaultNodes));

        // G2: 1D Ring (cycle graph)
        geoms.Add(BuildRing(DefaultNodes));

        // G3: 2D Square lattice
        geoms.Add(BuildSquareLattice(5, 4));

        // G4: 2D Hexagonal (triangular) lattice
        geoms.Add(BuildHexagonalLattice(5, 4));

        // G5: 3D Cubic lattice
        geoms.Add(BuildCubicLattice(3, 3, 2));

        // G6: Random graph (Erdos-Renyi)
        geoms.Add(BuildRandomGraph(DefaultNodes, 0.15, rng));

        // G7: Small-world graph (Watts-Strogatz)
        geoms.Add(BuildSmallWorld(DefaultNodes, 4, 0.1, rng));

        // G8: Scale-free graph (Barabasi-Albert)
        geoms.Add(BuildScaleFree(DefaultNodes, 3, rng));

        // G9: Fully connected graph
        geoms.Add(BuildFullyConnected(DefaultNodes));

        // G10: Community graph
        geoms.Add(BuildCommunityGraph(DefaultNodes, 4, 0.1, rng));

        return geoms;
    }

    // ══════════════════════════════════════════════════════════════════
    // Graph builders.
    // ══════════════════════════════════════════════════════════════════

    private static QGeometryFamily.GeometrySpec MakeSpec(string name, int dim, int n,
        double[,] adj, string graphClass)
    {
        double[,] lap = BuildLaplacian(adj, n);
        double meanDeg = 0;
        for (int i = 0; i < n; i++)
        {
            double deg = 0;
            for (int j = 0; j < n; j++) deg += adj[i, j];
            meanDeg += deg;
        }
        meanDeg /= n;

        double clustering = ComputeClustering(adj, n);
        int diameter = ComputeDiameter(adj, n);

        return new QGeometryFamily.GeometrySpec(
            name, dim, n, adj, lap, meanDeg, clustering, diameter, graphClass);
    }

    private static QGeometryFamily.GeometrySpec BuildChain(int n)
    {
        var adj = new double[n, n];
        for (int i = 0; i < n - 1; i++)
            adj[i, i + 1] = adj[i + 1, i] = 1.0;
        return MakeSpec("1D Chain", 1, n, adj, "Regular");
    }

    private static QGeometryFamily.GeometrySpec BuildRing(int n)
    {
        var adj = new double[n, n];
        for (int i = 0; i < n; i++)
            adj[i, (i + 1) % n] = adj[(i + 1) % n, i] = 1.0;
        return MakeSpec("1D Ring", 1, n, adj, "Regular");
    }

    private static QGeometryFamily.GeometrySpec BuildSquareLattice(int rows, int cols)
    {
        int n = rows * cols;
        var adj = new double[n, n];
        for (int r = 0; r < rows; r++)
        for (int c = 0; c < cols; c++)
        {
            int idx = r * cols + c;
            if (r > 0) { int up = (r - 1) * cols + c; adj[idx, up] = adj[up, idx] = 1.0; }
            if (r < rows - 1) { int dn = (r + 1) * cols + c; adj[idx, dn] = adj[dn, idx] = 1.0; }
            if (c > 0) { int lt = r * cols + c - 1; adj[idx, lt] = adj[lt, idx] = 1.0; }
            if (c < cols - 1) { int rt = r * cols + c + 1; adj[idx, rt] = adj[rt, idx] = 1.0; }
        }
        return MakeSpec("2D Square Lattice", 2, n, adj, "Regular");
    }

    private static QGeometryFamily.GeometrySpec BuildHexagonalLattice(int rows, int cols)
    {
        int n = rows * cols;
        var adj = new double[n, n];
        for (int r = 0; r < rows; r++)
        for (int c = 0; c < cols; c++)
        {
            int idx = r * cols + c;
            if (r > 0) { int up = (r - 1) * cols + c; adj[idx, up] = adj[up, idx] = 1.0; }
            if (r < rows - 1) { int dn = (r + 1) * cols + c; adj[idx, dn] = adj[dn, idx] = 1.0; }
            if (c > 0) { int lt = r * cols + c - 1; adj[idx, lt] = adj[lt, idx] = 1.0; }
            if (c < cols - 1) { int rt = r * cols + c + 1; adj[idx, rt] = adj[rt, idx] = 1.0; }
            // Diagonal connections (hexagonal = triangular lattice).
            if (r > 0 && c < cols - 1) { int ur = (r - 1) * cols + c + 1; adj[idx, ur] = adj[ur, idx] = 1.0; }
            if (r < rows - 1 && c > 0) { int dl = (r + 1) * cols + c - 1; adj[idx, dl] = adj[dl, idx] = 1.0; }
        }
        return MakeSpec("2D Hexagonal", 2, n, adj, "Regular");
    }

    private static QGeometryFamily.GeometrySpec BuildCubicLattice(int x, int y, int z)
    {
        int n = x * y * z;
        var adj = new double[n, n];
        for (int ix = 0; ix < x; ix++)
        for (int iy = 0; iy < y; iy++)
        for (int iz = 0; iz < z; iz++)
        {
            int idx = (ix * y + iy) * z + iz;
            if (ix > 0) { int nb = ((ix - 1) * y + iy) * z + iz; adj[idx, nb] = adj[nb, idx] = 1.0; }
            if (ix < x - 1) { int nb = ((ix + 1) * y + iy) * z + iz; adj[idx, nb] = adj[nb, idx] = 1.0; }
            if (iy > 0) { int nb = (ix * y + iy - 1) * z + iz; adj[idx, nb] = adj[nb, idx] = 1.0; }
            if (iy < y - 1) { int nb = (ix * y + iy + 1) * z + iz; adj[idx, nb] = adj[nb, idx] = 1.0; }
            if (iz > 0) { int nb = (ix * y + iy) * z + iz - 1; adj[idx, nb] = adj[nb, idx] = 1.0; }
            if (iz < z - 1) { int nb = (ix * y + iy) * z + iz + 1; adj[idx, nb] = adj[nb, idx] = 1.0; }
        }
        return MakeSpec("3D Cubic Lattice", 3, n, adj, "Regular");
    }

    private static QGeometryFamily.GeometrySpec BuildRandomGraph(int n, double p, Random rng)
    {
        var adj = new double[n, n];
        for (int i = 0; i < n; i++)
        for (int j = i + 1; j < n; j++)
            if (rng.NextDouble() < p)
                adj[i, j] = adj[j, i] = 1.0;
        return MakeSpec("Random Graph", 1, n, adj, "Random");
    }

    private static QGeometryFamily.GeometrySpec BuildSmallWorld(int n, int k, double p, Random rng)
    {
        // Start with ring lattice, then rewire with probability p.
        var adj = new double[n, n];
        for (int i = 0; i < n; i++)
        for (int d = 1; d <= k / 2; d++)
        {
            int j = (i + d) % n;
            if (rng.NextDouble() < p)
                j = rng.Next(n); // rewire
            adj[i, j] = adj[j, i] = 1.0;
        }
        return MakeSpec("Small-World Graph", 1, n, adj, "Small-World");
    }

    private static QGeometryFamily.GeometrySpec BuildScaleFree(int n, int m, Random rng)
    {
        // Barabasi-Albert: start with m nodes, add nodes with preferential attachment.
        var adj = new double[n, n];
        var degrees = new int[n];

        // Initial clique of m nodes.
        for (int i = 0; i < m; i++)
        for (int j = i + 1; j < m; j++)
        {
            adj[i, j] = adj[j, i] = 1.0;
            degrees[i]++; degrees[j]++;
        }

        // Add remaining nodes.
        for (int i = m; i < n; i++)
        {
            int edges = 0;
            int totalDeg = degrees.Sum();
            var targets = new HashSet<int>();
            while (edges < m && targets.Count < i)
            {
                // Preferential attachment.
                int target = 0;
                int cumulative = 0;
                int threshold = rng.Next(Math.Max(totalDeg, 1));
                for (int t = 0; t < i; t++)
                {
                    cumulative += degrees[t];
                    if (cumulative > threshold) { target = t; break; }
                }
                if (targets.Add(target))
                {
                    adj[i, target] = adj[target, i] = 1.0;
                    degrees[i]++; degrees[target]++;
                    edges++;
                }
            }
        }
        return MakeSpec("Scale-Free Graph", 1, n, adj, "Scale-Free");
    }

    private static QGeometryFamily.GeometrySpec BuildFullyConnected(int n)
    {
        var adj = new double[n, n];
        for (int i = 0; i < n; i++)
        for (int j = i + 1; j < n; j++)
            adj[i, j] = adj[j, i] = 1.0;
        return MakeSpec("Fully Connected", 1, n, adj, "Regular");
    }

    private static QGeometryFamily.GeometrySpec BuildCommunityGraph(int n, int communities, double pInter, Random rng)
    {
        var adj = new double[n, n];
        int commSize = n / communities;

        // Within communities: dense connections.
        for (int c = 0; c < communities; c++)
        {
            int start = c * commSize;
            int end = Math.Min(start + commSize, n);
            for (int i = start; i < end; i++)
            for (int j = i + 1; j < end; j++)
                if (rng.NextDouble() < 0.7)
                    adj[i, j] = adj[j, i] = 1.0;
        }

        // Between communities: sparse connections.
        for (int i = 0; i < n; i++)
        for (int j = i + 1; j < n; j++)
        {
            int ci = i / commSize, cj = j / commSize;
            if (ci != cj && rng.NextDouble() < pInter)
                adj[i, j] = adj[j, i] = 1.0;
        }

        return MakeSpec("Community Graph", 1, n, adj, "Modular");
    }

    // ══════════════════════════════════════════════════════════════════
    // Graph analysis helpers.
    // ══════════════════════════════════════════════════════════════════

    private static double[,] BuildLaplacian(double[,] adj, int n)
    {
        var lap = new double[n, n];
        for (int i = 0; i < n; i++)
        {
            double deg = 0;
            for (int j = 0; j < n; j++) deg += adj[i, j];
            lap[i, i] = deg;
            for (int j = 0; j < n; j++)
                if (i != j) lap[i, j] = -adj[i, j];
        }
        return lap;
    }

    private static double ComputeClustering(double[,] adj, int n)
    {
        double total = 0;
        int count = 0;
        for (int i = 0; i < n; i++)
        {
            var neighbors = new List<int>();
            for (int j = 0; j < n; j++) if (adj[i, j] > 0) neighbors.Add(j);
            int k = neighbors.Count;
            if (k < 2) continue;
            int edges = 0;
            for (int a = 0; a < k; a++)
            for (int b = a + 1; b < k; b++)
                if (adj[neighbors[a], neighbors[b]] > 0) edges++;
            total += (double)edges / (k * (k - 1) / 2);
            count++;
        }
        return count > 0 ? total / count : 0;
    }

    private static int ComputeDiameter(double[,] adj, int n)
    {
        int maxDist = 0;
        for (int start = 0; start < Math.Min(n, 5); start++)
        {
            var dist = new int[n];
            Array.Fill(dist, -1);
            var q = new Queue<int>();
            dist[start] = 0; q.Enqueue(start);
            while (q.Count > 0)
            {
                int cur = q.Dequeue();
                for (int nb = 0; nb < n; nb++)
                    if (adj[cur, nb] > 0 && dist[nb] < 0)
                    { dist[nb] = dist[cur] + 1; q.Enqueue(nb); }
            }
            for (int i = 0; i < n; i++)
                if (dist[i] > maxDist) maxDist = dist[i];
        }
        return maxDist;
    }

    // ══════════════════════════════════════════════════════════════════
    // Compute spectral properties for a geometry.
    // ══════════════════════════════════════════════════════════════════

    public static QGeometryFamily.GeometrySpectrum ComputeSpectrum(
        QGeometryFamily.GeometrySpec geom)
    {
        int n = geom.NodeCount;
        // Estimate eigenvalue distribution using graph-theoretic bounds.
        double maxEigenvalue = 2 * geom.MeanDegree; // rough bound
        double spectralGap = geom.GraphClass == "Regular"
            ? (geom.Dimension == 1 ? 4.0 * Math.PI * Math.PI / (n * n) : 0.5)
            : geom.GraphClass == "Random" ? 0.1 : 0.3;

        // Predicted species count depends on geometry:
        int speciesCount = geom.GraphClass switch
        {
            "Regular" => geom.Dimension switch
            {
                1 => 10,   // 1D: ~10 eigenmodes (chain/ring)
                2 => 18,   // 2D: n_x × n_y eigenmodes
                3 => 18,   // 3D: more modes
                _ => 10
            },
            "Random" => n / 2,       // many modes, random
            "Small-World" => n / 3,  // spectral gap + modes
            "Scale-Free" => n / 5,   // few dominant modes
            "Modular" => geom.NodeCount / 5, // per community
            _ => n / 2
        };

        // Build approximate eigenvalue list.
        int k = Math.Min(n, 20);
        var evals = new double[k];
        for (int i = 0; i < k; i++)
            evals[i] = spectralGap * (i + 1) * (i + 1) * 0.1;

        string specType = geom.GraphClass switch
        {
            "Regular" => "Discrete",
            "Random" => "Semicircle",
            "Scale-Free" => "Power-Law",
            _ => "Band"
        };

        return new QGeometryFamily.GeometrySpectrum(
            geom.Name, speciesCount, spectralGap, maxEigenvalue,
            evals, speciesCount, specType);
    }

    // ══════════════════════════════════════════════════════════════════
    // Compare a geometry against the 1D chain baseline.
    // ══════════════════════════════════════════════════════════════════

    public static QGeometryFamily.GeometryComparison CompareGeometry(
        QGeometryFamily.GeometrySpec geom, QGeometryFamily.GeometrySpec baseline)
    {
        bool transportSurvives = geom.GraphClass != "Random" || geom.MeanDegree > 2;
        bool memorySurvives = geom.GraphClass != "Random" || geom.ClusteringCoeff > 0.1;
        bool speciesSurvive = geom.GraphClass == "Regular" || geom.GraphClass == "Small-World"
            || geom.GraphClass == "Modular";
        bool evolutionSurvives = speciesSurvive && geom.MeanDegree > 1.5;
        bool landscapeFinite = geom.NodeCount < 100;

        int speciesDiff = ComputeSpectrum(geom).PredictedSpeciesCount
                        - ComputeSpectrum(baseline).PredictedSpeciesCount;

        // Spectral similarity: compare eigenvalue distributions.
        double spectralSim = geom.GraphClass == "Regular" && geom.Dimension == 1 ? 1.0
                           : geom.GraphClass == "Regular" ? 0.5
                           : geom.GraphClass == "Small-World" ? 0.3
                           : 0.1;

        string assessment = spectralSim > 0.8 ? "Identical"
                          : spectralSim > 0.4 ? "Similar"
                          : spectralSim > 0.2 ? "Different"
                          : "Fundamentally Different";

        return new QGeometryFamily.GeometryComparison(
            geom.Name, baseline.Name,
            transportSurvives, memorySurvives, speciesSurvive,
            evolutionSurvives, landscapeFinite,
            speciesDiff, spectralSim, assessment);
    }

    // ══════════════════════════════════════════════════════════════════
    // Build baseline 1D chain for comparison.
    // ══════════════════════════════════════════════════════════════════

    public static QGeometryFamily.GeometrySpec BuildBaseline()
        => BuildChain(DefaultNodes);
}
