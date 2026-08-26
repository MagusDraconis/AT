using AT.Core.Temporal;

namespace AT.Core.Quantum;

/// <summary>
/// Generates structured temporal coupling matrices for eigenmode emergence research.
///
/// Each factory method produces a valid symmetric TemporalMatrix representing
/// a specific network topology. The matrices define coupling strengths Kᵢⱼ
/// between oscillator pairs.
/// </summary>
public static class StructuredTemporalMatrixFactory
{
    /// <summary>
    /// Creates an N×N ring lattice where each node connects to k nearest neighbors
    /// (k/2 on each side), with periodic boundary conditions.
    /// Coupling strength = 1 for connected pairs, 0 otherwise.
    /// </summary>
    public static TemporalMatrix CreateRingLattice(int n, int neighbors = 4, int seed = 42)
    {
        ValidateSize(n);
        if (neighbors < 2 || neighbors % 2 != 0)
            throw new ArgumentException("Neighbors must be an even number ≥ 2.", nameof(neighbors));

        var matrix = new TemporalMatrix(n);
        int halfK = neighbors / 2;

        for (int i = 0; i < n; i++)
        {
            for (int d = 1; d <= halfK; d++)
            {
                int j = (i + d) % n;
                matrix[i, j] = 1.0;
                matrix[j, i] = 1.0;
            }
        }

        return matrix;
    }

    /// <summary>
    /// Creates an N×N 2D square lattice with nearest-neighbor (von Neumann)
    /// connectivity and periodic boundary conditions.
    /// N should ideally be a perfect square; otherwise uses floor(sqrt(N))² layout.
    /// </summary>
    public static TemporalMatrix Create2DLattice(int n, int seed = 42)
    {
        ValidateSize(n);
        int side = (int)Math.Floor(Math.Sqrt(n));
        int effectiveN = side * side;

        var matrix = new TemporalMatrix(n);

        for (int idx = 0; idx < effectiveN; idx++)
        {
            int row = idx / side;
            int col = idx % side;

            // Right neighbor
            int rightCol = (col + 1) % side;
            int rightIdx = row * side + rightCol;
            matrix[idx, rightIdx] = 1.0;
            matrix[rightIdx, idx] = 1.0;

            // Down neighbor
            int downRow = (row + 1) % side;
            int downIdx = downRow * side + col;
            matrix[idx, downIdx] = 1.0;
            matrix[downIdx, idx] = 1.0;
        }

        // Remaining nodes (if n is not a perfect square) remain uncoupled.
        return matrix;
    }

    /// <summary>
    /// Creates a Watts-Strogatz small-world network.
    /// Starts with a ring lattice and rewires each edge with probability p,
    /// connecting to a random non-self, non-duplicate target.
    /// </summary>
    public static TemporalMatrix CreateSmallWorld(int n, int neighbors = 4, double rewiringProbability = 0.1, int seed = 42)
    {
        ValidateSize(n);
        if (rewiringProbability < 0 || rewiringProbability > 1)
            throw new ArgumentOutOfRangeException(nameof(rewiringProbability));

        var rng = new Random(seed);
        var matrix = CreateRingLattice(n, neighbors, seed);

        int halfK = neighbors / 2;

        for (int i = 0; i < n; i++)
        {
            for (int d = 1; d <= halfK; d++)
            {
                int j = (i + d) % n;

                if (rng.NextDouble() < rewiringProbability)
                {
                    // Remove old edge.
                    matrix[i, j] = 0.0;
                    matrix[j, i] = 0.0;

                    // Find a new target that is not self and not already connected.
                    int newTarget;
                    int attempts = 0;
                    do
                    {
                        newTarget = rng.Next(n);
                        attempts++;
                    }
                    while ((newTarget == i || Math.Abs(matrix[i, newTarget]) > 0.5) && attempts < 100);

                    if (newTarget != i && Math.Abs(matrix[i, newTarget]) < 0.5)
                    {
                        matrix[i, newTarget] = 1.0;
                        matrix[newTarget, i] = 1.0;
                    }
                    else
                    {
                        // Restore original edge if rewire failed.
                        matrix[i, j] = 1.0;
                        matrix[j, i] = 1.0;
                    }
                }
            }
        }

        return matrix;
    }

    /// <summary>
    /// Creates a Barabási-Albert scale-free network using preferential attachment.
    /// Starts with m0 fully-connected seed nodes, then adds n−m0 nodes each
    /// connecting to m existing nodes with probability proportional to degree.
    /// </summary>
    public static TemporalMatrix CreateScaleFree(int n, int m0 = 3, int m = 2, int seed = 42)
    {
        ValidateSize(n);
        if (m0 < 2) throw new ArgumentOutOfRangeException(nameof(m0), "m0 must be at least 2.");
        if (m < 1 || m > m0) throw new ArgumentOutOfRangeException(nameof(m), $"m must be in [1, {m0}].");

        var rng = new Random(seed);
        var matrix = new TemporalMatrix(n);

        // Seed: m0 nodes fully connected.
        for (int i = 0; i < m0; i++)
            for (int j = i + 1; j < m0; j++)
            {
                matrix[i, j] = 1.0;
                matrix[j, i] = 1.0;
            }

        int[] degrees = new int[n];
        for (int i = 0; i < m0; i++)
            degrees[i] = m0 - 1;

        int totalDegree = m0 * (m0 - 1);

        // Add remaining nodes.
        for (int newNode = m0; newNode < n; newNode++)
        {
            var chosen = new HashSet<int>();

            while (chosen.Count < m)
            {
                // Roulette-wheel selection based on degree.
                int r = rng.Next(totalDegree);
                int cumulative = 0;
                int selected = -1;

                for (int j = 0; j < newNode; j++)
                {
                    cumulative += degrees[j];
                    if (r < cumulative)
                    {
                        selected = j;
                        break;
                    }
                }

                if (selected >= 0 && !chosen.Contains(selected))
                    chosen.Add(selected);
            }

            foreach (int target in chosen)
            {
                matrix[newNode, target] = 1.0;
                matrix[target, newNode] = 1.0;
                degrees[newNode]++;
                degrees[target]++;
                totalDegree += 2;
            }
        }

        return matrix;
    }

    /// <summary>
    /// Creates a clustered network with dense intra-cluster coupling and weak inter-cluster links.
    /// Nodes are partitioned into clusterCount roughly equal groups.
    /// </summary>
    public static TemporalMatrix CreateClustered(int n, int clusterCount = 4, double intraStrength = 1.0, double interStrength = 0.1, int seed = 42)
    {
        ValidateSize(n);
        if (clusterCount < 2 || clusterCount > n)
            throw new ArgumentOutOfRangeException(nameof(clusterCount), $"Cluster count must be in [2, {n}].");

        var rng = new Random(seed);
        var matrix = new TemporalMatrix(n);

        // Assign nodes to clusters.
        int[] clusterIds = new int[n];
        for (int i = 0; i < n; i++)
            clusterIds[i] = i % clusterCount;

        for (int i = 0; i < n; i++)
        {
            for (int j = i + 1; j < n; j++)
            {
                double strength;
                if (clusterIds[i] == clusterIds[j])
                {
                    // Intra-cluster: dense, strong coupling.
                    strength = intraStrength * (0.5 + 0.5 * rng.NextDouble());
                }
                else
                {
                    // Inter-cluster: sparse, weak coupling.
                    if (rng.NextDouble() < 0.1)
                        strength = interStrength * rng.NextDouble();
                    else
                        strength = 0.0;
                }

                matrix[i, j] = strength;
                matrix[j, i] = strength;
            }
        }

        return matrix;
    }

    /// <summary>
    /// Creates a hierarchical network with recursive block structure.
    /// Coupling strength decays with hierarchical distance.
    /// </summary>
    public static TemporalMatrix CreateHierarchical(int n, int levels = 3, double decayFactor = 0.5, int seed = 42)
    {
        ValidateSize(n);
        if (levels < 1) throw new ArgumentOutOfRangeException(nameof(levels), "Levels must be at least 1.");

        var rng = new Random(seed);
        var matrix = new TemporalMatrix(n);

        // Build a binary-tree-like hierarchy.
        int[] levelOf = new int[n];
        int blockSize = 1 << levels;
        for (int i = 0; i < n; i++)
        {
            int block = i / blockSize;
            int posInBlock = i % blockSize;

            // Determine hierarchical level: the smallest sub-block size containing the node.
            int nodeLevel = levels;
            for (int l = levels; l >= 0; l--)
            {
                int subSize = 1 << l;
                if (posInBlock % subSize == 0)
                {
                    nodeLevel = l;
                    break;
                }
            }
            levelOf[i] = nodeLevel;
        }

        for (int i = 0; i < n; i++)
        {
            for (int j = i + 1; j < n; j++)
            {
                // Find the deepest common hierarchical level.
                int commonLevel = 0;
                int blockI = i / blockSize;
                int blockJ = j / blockSize;

                if (blockI == blockJ)
                {
                    // Within same block: find the smallest sub-block containing both.
                    for (int l = levels; l >= 0; l--)
                    {
                        int subSize = 1 << l;
                        if ((i / subSize) == (j / subSize))
                        {
                            commonLevel = l;
                            break;
                        }
                    }
                }

                double strength = Math.Pow(decayFactor, levels - commonLevel) * rng.NextDouble();
                matrix[i, j] = strength;
                matrix[j, i] = strength;
            }
        }

        return matrix;
    }

    /// <summary>
    /// Creates a Gaussian random symmetric matrix (baseline for comparisons).
    /// Entries are drawn from N(0, 1) using Box-Muller.
    /// </summary>
    public static TemporalMatrix CreateGaussianRandom(int n, int seed = 42)
    {
        ValidateSize(n);
        var rng = new Random(seed);
        var matrix = new TemporalMatrix(n);

        for (int i = 0; i < n; i++)
        {
            matrix[i, i] = NextGaussian(rng);
            for (int j = i + 1; j < n; j++)
            {
                double val = NextGaussian(rng);
                matrix[i, j] = val;
                matrix[j, i] = val;
            }
        }

        return matrix;
    }

    private static void ValidateSize(int n)
    {
        if (n <= 1)
            throw new ArgumentOutOfRangeException(nameof(n), "Matrix size must be greater than 1.");
    }

    private static double NextGaussian(Random rng)
    {
        double u1 = 1.0 - rng.NextDouble();
        double u2 = 1.0 - rng.NextDouble();
        return Math.Sqrt(-2.0 * Math.Log(Math.Max(u1, 1e-15))) * Math.Cos(2.0 * Math.PI * u2);
    }
}
