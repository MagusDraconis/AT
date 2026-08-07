using System.Globalization;

namespace TQM.Core.Research;

/// <summary>
/// Reconstructs geometry from Q-event correlations.
/// TQM-X041b: Metric Emergence from Q-Event Correlations
/// </summary>
public static class CorrelationGeometryAnalyzer
{
    private static readonly Random Rng = new(42); // deterministic

    /// <summary>
    /// Generate Q-events on a known graph and compute pairwise correlations.
    /// </summary>
    public static (double[,] correlations, double[,] trueDistances, int[] eventVertices)
        GenerateEvents(int numVertices, int eventsPerVertex, string graphType)
    {
        // Build adjacency based on graph type
        double[,] trueDist = BuildTrueDistances(numVertices, graphType);

        // Place events: each vertex gets eventsPerVertex events
        int totalEvents = numVertices * eventsPerVertex;
        int[] eventVertices = new int[totalEvents];
        for (int v = 0; v < numVertices; v++)
            for (int e = 0; e < eventsPerVertex; e++)
                eventVertices[v * eventsPerVertex + e] = v;

        // Compute correlation: C(i,j) = exp(-d(i,j)/L) * (1 + noise)
        // where d is graph distance, L is correlation length
        double L = numVertices / 3.0; // correlation length
        double[,] correlations = new double[totalEvents, totalEvents];
        double[,] trueDistances = new double[totalEvents, totalEvents];

        for (int i = 0; i < totalEvents; i++)
        {
            for (int j = 0; j < totalEvents; j++)
            {
                int vi = eventVertices[i];
                int vj = eventVertices[j];
                double d = trueDist[vi, vj];
                trueDistances[i, j] = d;
                double noise = 1.0 + 0.05 * (Rng.NextDouble() - 0.5);
                correlations[i, j] = Math.Exp(-d / L) * noise;
                correlations[i, j] = Math.Min(1.0, Math.Max(0.0, correlations[i, j]));
            }
        }

        return (correlations, trueDistances, eventVertices);
    }

    private static double[,] BuildTrueDistances(int n, string graphType)
    {
        double[,] dist = new double[n, n];

        switch (graphType)
        {
            case "1D_Line":
                for (int i = 0; i < n; i++)
                    for (int j = 0; j < n; j++)
                        dist[i, j] = Math.Abs(i - j);
                break;

            case "1D_Circle":
                for (int i = 0; i < n; i++)
                    for (int j = 0; j < n; j++)
                        dist[i, j] = Math.Min(Math.Abs(i - j), n - Math.Abs(i - j));
                break;

            case "2D_Grid":
                int side = (int)Math.Sqrt(n);
                for (int i = 0; i < n; i++)
                {
                    (int xi, int yi) = (i % side, i / side);
                    for (int j = 0; j < n; j++)
                    {
                        (int xj, int yj) = (j % side, j / side);
                        dist[i, j] = Math.Sqrt((xi - xj) * (xi - xj) + (yi - yj) * (yi - yj));
                    }
                }
                break;

            case "3D_Cube":
                int s = (int)Math.Cbrt(n);
                for (int i = 0; i < n; i++)
                {
                    (int xi, int yi, int zi) = (i % s, (i / s) % s, i / (s * s));
                    for (int j = 0; j < n; j++)
                    {
                        (int xj, int yj, int zj) = (j % s, (j / s) % s, j / (s * s));
                        double dx = xi - xj, dy = yi - yj, dz = zi - zj;
                        dist[i, j] = Math.Sqrt(dx * dx + dy * dy + dz * dz);
                    }
                }
                break;

            case "Random_Geometric":
                // Place vertices randomly in 2D unit square, compute Euclidean distances
                double[] xs = new double[n], ys = new double[n];
                for (int i = 0; i < n; i++)
                { xs[i] = Rng.NextDouble(); ys[i] = Rng.NextDouble(); }

                for (int i = 0; i < n; i++)
                    for (int j = 0; j < n; j++)
                        dist[i, j] = Math.Sqrt((xs[i] - xs[j]) * (xs[i] - xs[j])
                                             + (ys[i] - ys[j]) * (ys[i] - ys[j]));
                break;

            default:
                for (int i = 0; i < n; i++)
                    for (int j = 0; j < n; j++)
                        dist[i, j] = Math.Abs(i - j);
                break;
        }

        return dist;
    }

    /// <summary>
    /// Reconstruct distances from correlations: d_ij = -L * log(C_ij).
    /// </summary>
    public static double[,] ReconstructDistances(double[,] correlations, double L)
    {
        int n = correlations.GetLength(0);
        double[,] reconstructed = new double[n, n];
        for (int i = 0; i < n; i++)
            for (int j = 0; j < n; j++)
            {
                double c = Math.Max(correlations[i, j], 0.001); // clip to avoid log(0)
                reconstructed[i, j] = -L * Math.Log(c);
            }
        return reconstructed;
    }

    /// <summary>
    /// Estimate dimension from correlation scaling: N(r) ∝ r^d.
    /// </summary>
    public static double EstimateDimension(double[,] distances, int eventCount)
    {
        // Count pairs within distance r for various r
        int samples = 20;
        double maxDist = 0;
        for (int i = 0; i < eventCount; i++)
            for (int j = i + 1; j < eventCount; j++)
                if (distances[i, j] > maxDist)
                    maxDist = distances[i, j];

        double[] logR = new double[samples];
        double[] logN = new double[samples];

        for (int k = 0; k < samples; k++)
        {
            double r = maxDist * (k + 1) / samples;
            int count = 0;
            for (int i = 0; i < eventCount; i++)
                for (int j = i + 1; j < eventCount; j++)
                    if (distances[i, j] <= r)
                        count++;

            logR[k] = Math.Log(r);
            logN[k] = Math.Log(Math.Max(count, 1));
        }

        // Linear regression: log N = d * log r + const
        double sumX = 0, sumY = 0, sumXY = 0, sumX2 = 0;
        for (int k = 0; k < samples; k++)
        {
            sumX += logR[k]; sumY += logN[k];
            sumXY += logR[k] * logN[k];
            sumX2 += logR[k] * logR[k];
        }
        double d = (samples * sumXY - sumX * sumY) / (samples * sumX2 - sumX * sumX);
        return d;
    }

    /// <summary>
    /// Compute Spearman rank correlation between true and reconstructed distances.
    /// </summary>
    public static double RankCorrelation(double[,] trueD, double[,] reconD, int n)
    {
        // Flatten upper triangle to arrays
        int pairs = n * (n - 1) / 2;
        double[] tv = new double[pairs], rv = new double[pairs];
        int idx = 0;
        for (int i = 0; i < n; i++)
            for (int j = i + 1; j < n; j++)
            { tv[idx] = trueD[i, j]; rv[idx] = reconD[i, j]; idx++; }

        // Rank both arrays
        int[] tRank = Rank(tv), rRank = Rank(rv);

        // Spearman
        double sumD2 = 0;
        for (int i = 0; i < pairs; i++)
        {
            double d = tRank[i] - rRank[i];
            sumD2 += d * d;
        }
        return 1.0 - (6.0 * sumD2) / (pairs * (pairs * pairs - 1.0));
    }

    private static int[] Rank(double[] values)
    {
        int n = values.Length;
        int[] ranks = new int[n];
        var indexed = values.Select((v, i) => (v, i)).OrderBy(x => x.v).ToArray();
        for (int i = 0; i < n; i++)
            ranks[indexed[i].i] = i + 1;
        return ranks;
    }

    public static List<CorrelationGeometryMetrics.CorrelationResult> RunAllTests()
    {
        var results = new List<CorrelationGeometryMetrics.CorrelationResult>();

        var configs = new[]
        {
            ("1D_Line", 1), ("1D_Circle", 1), ("2D_Grid", 2), ("3D_Cube", 3), ("Random_Geometric", 2)
        };

        foreach (var (graphType, actualDim) in configs)
        {
            int vertices = graphType switch
            {
                "1D_Line" => 50, "1D_Circle" => 50, "2D_Grid" => 49, "3D_Cube" => 27, _ => 30
            };
            int eventsPerVertex = 3;
            int totalEvents = vertices * eventsPerVertex;

            var (corr, trueDist, _) = GenerateEvents(vertices, eventsPerVertex, graphType);
            double L = vertices / 3.0;
            double[,] reconDist = ReconstructDistances(corr, L);

            double dimEst = EstimateDimension(reconDist, totalEvents);
            double rankCorr = RankCorrelation(trueDist, reconDist, totalEvents);

            // Mean relative error
            double mre = 0; int count = 0;
            for (int i = 0; i < totalEvents; i++)
                for (int j = i + 1; j < totalEvents; j++)
                {
                    if (trueDist[i, j] > 0.01)
                    {
                        mre += Math.Abs(reconDist[i, j] - trueDist[i, j]) / trueDist[i, j];
                        count++;
                    }
                }
            mre /= count;

            string notes = dimEst >= actualDim * 0.7 && dimEst <= actualDim * 1.4
                ? $"Dimension estimate {dimEst:F2} ≈ actual {actualDim}. Good recovery."
                : $"Dimension mismatch: estimated {dimEst:F2}, actual {actualDim}.";

            results.Add(new CorrelationGeometryMetrics.CorrelationResult(
                graphType, totalEvents, dimEst, actualDim, rankCorr, mre, notes));
        }

        return results;
    }

    public static string AnalyzeResults(List<CorrelationGeometryMetrics.CorrelationResult> results)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("CORRELATION GEOMETRY RECONSTRUCTION");
        sb.AppendLine();
        sb.AppendLine("  Graph           Events  Est.Dim  True.Dim  Rank.Corr  MRE     Verdict");
        sb.AppendLine("  " + new string('─', 80));
        foreach (var r in results)
        {
            string verdict = r.DistanceCorrelation > 0.9 ? "✓ RECOVERED"
                : r.DistanceCorrelation > 0.7 ? "~ PARTIAL"
                : "✗ FAILED";
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  {0,-15} {1,6}  {2,7:F2}  {3,8}  {4,9:F3}  {5,6:F3}  {6}",
                r.GraphType, r.EventCount, r.DimensionEstimate,
                r.ActualDimension, r.DistanceCorrelation,
                r.MetricReconstructionError, verdict));
        }
        return sb.ToString();
    }
}
