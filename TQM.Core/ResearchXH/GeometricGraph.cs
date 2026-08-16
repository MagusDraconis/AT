namespace TQM.Core.ResearchXH;

/// <summary>
/// A deterministic, unweighted, undirected graph representing a 2-dimensional
/// constant-curvature geometry (flat, spherical, hyperbolic). G4 Phase 0: determine
/// whether curvature information is already encoded in graph spectra.
/// </summary>
public sealed record GeometricGraph(
    string Name,
    int Dimension,
    double ScalarCurvature,
    double[,] Adjacency,
    double[]? Density = null)
{
    public int VertexCount => Adjacency.GetLength(0);

    /// <summary>Per-vertex event density (actualization rate / counting measure), or uniform 1 if absent.</summary>
    public double[] VertexDensity()
        => Density ?? Enumerable.Repeat(1.0, VertexCount).ToArray();

    public int[] Degrees()
    {
        int n = VertexCount;
        var deg = new int[n];
        for (int i = 0; i < n; i++)
            for (int j = 0; j < n; j++)
                if (Adjacency[i, j] != 0.0)
                    deg[i]++;
        return deg;
    }

    public double MeanDegree()
    {
        int[] d = Degrees();
        return d.Average();
    }

    /// <summary>Unnormalized combinatorial graph Laplacian L = D − A.</summary>
    public double[,] UnnormalizedLaplacian()
    {
        int n = VertexCount;
        var l = new double[n, n];
        for (int i = 0; i < n; i++)
        {
            double degree = 0.0;
            for (int j = 0; j < n; j++)
                if (i != j) degree += Adjacency[i, j];
            l[i, i] = degree;
            for (int j = 0; j < n; j++)
                if (i != j) l[i, j] = -Adjacency[i, j];
        }
        return l;
    }

    /// <summary>Symmetric normalized Laplacian L_sym = I − D^(−1/2) A D^(−1/2).</summary>
    public double[,] NormalizedSymmetricLaplacian()
    {
        int n = VertexCount;
        int[] deg = Degrees();
        var l = new double[n, n];
        for (int i = 0; i < n; i++)
        {
            l[i, i] = 1.0; // A_ii = 0 (no self-loops) => diagonal of I − D^-1/2 A D^-1/2 is 1
            for (int j = i + 1; j < n; j++)
            {
                if (Adjacency[i, j] == 0.0) continue;
                double di = Math.Sqrt(deg[i]);
                double dj = Math.Sqrt(deg[j]);
                if (di > 0.0 && dj > 0.0)
                {
                    double w = -1.0 / (di * dj);
                    l[i, j] = w;
                    l[j, i] = w;
                }
            }
        }
        return l;
    }

    public bool IsConnected() => GraphFactory.Connected(Adjacency, VertexCount);
}
