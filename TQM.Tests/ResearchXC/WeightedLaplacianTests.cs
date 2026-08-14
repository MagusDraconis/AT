using System.Globalization;
using System.Text;
using MathNet.Numerics.LinearAlgebra;
using TQM.Core.Temporal;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXC;

/// <summary>
/// Verifies the weighted graph Laplacian L_W = D_K − K, built from the existing spatial
/// coupling matrix K_ij (TemporalMatrix). Properties checked: symmetry, zero row-sum,
/// positive semi-definiteness, and reduction to the unweighted Laplacian when K is binary.
/// </summary>
public class WeightedLaplacianTests : ResearchTestBase
{
    public WeightedLaplacianTests(ITestOutputHelper o) : base(o) { }

    // ── Test 1: symmetry ────────────────────────────────────────────────────

    [Fact]
    public void WeightedLaplacian_IsSymmetric()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("Test 1: L_W is symmetric");

        var lw = BuildSpatialWeightedLaplacian(seed: 42, n: 8);
        double maxAsym = MaxAsymmetry(lw);

        sb.AppendLine($"max |L_W[i,j] − L_W[j,i]| = {maxAsym:E3}");
        Assert.True(maxAsym < 1e-12, $"L_W is not symmetric (max asymmetry {maxAsym:E3})");
        sb.AppendLine("PASS: L_W is symmetric.");
        Output.WriteLine(sb.ToString());
    }

    // ── Test 2: zero row sum ────────────────────────────────────────────────

    [Fact]
    public void WeightedLaplacian_HasZeroRowSum()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("Test 2: L_W has zero row sum");

        var lw = BuildSpatialWeightedLaplacian(seed: 43, n: 8);
        double maxRowSum = MaxAbsRowSum(lw);

        sb.AppendLine($"max |Σ_j L_W[i,j]| = {maxRowSum:E3}");
        Assert.True(maxRowSum < 1e-9, $"L_W row sums are not zero (max {maxRowSum:E3})");
        sb.AppendLine("PASS: L_W has zero row sum (constant vector is in the kernel).");
        Output.WriteLine(sb.ToString());
    }

    // ── Test 3: positive semi-definite ──────────────────────────────────────

    [Fact]
    public void WeightedLaplacian_IsPositiveSemidefinite()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("Test 3: L_W is positive semi-definite");

        var lw = BuildSpatialWeightedLaplacian(seed: 44, n: 8);
        double minEig = MinEigenvalue(lw);

        sb.AppendLine($"min eigenvalue of L_W = {minEig:F6}");
        Assert.True(minEig >= -1e-9, $"L_W is not positive semi-definite (min {minEig:E3})");
        sb.AppendLine("PASS: L_W is positive semi-definite.");
        Output.WriteLine(sb.ToString());
    }

    // ── Test 4: reduces to the unweighted Laplacian ─────────────────────────

    [Fact]
    public void WeightedLaplacian_ReducesToUnweighted()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("Test 4: binary coupling ⇒ L_W = L_Q (unweighted Laplacian)");

        int n = 6;
        var matrix = new TemporalMatrix(n);
        for (int i = 0; i < n - 1; i++) // path graph: unit coupling on edges
        {
            matrix.SetCoupling(i, i + 1, 1.0);
            matrix.SetCoupling(i + 1, i, 1.0);
        }

        var lw = matrix.BuildWeightedLaplacian();
        var lq = UnweightedPathLaplacian(n);

        double maxDiff = MaxAbsDiff(lw, lq);
        sb.AppendLine($"max |L_W − L_Q| = {maxDiff:E3}");

        Assert.True(maxDiff < 1e-12, $"binary coupling does not reduce to L_Q (max diff {maxDiff:E3})");
        sb.AppendLine("PASS: binary coupling ⇒ L_W = L_Q.");
        Output.WriteLine(sb.ToString());
    }

    // ── Helpers ──────────────────────────────────────────────────────────────

    /// <summary>Builds L_W from a spatial coupling matrix over random positions (deterministic).</summary>
    private static double[,] BuildSpatialWeightedLaplacian(int seed, int n)
    {
        var rng = new Random(seed);
        var network = new TemporalNetwork(n);
        for (int i = 0; i < n; i++)
        {
            network.AddNode(new TemporalNode(i, rng.NextDouble() * 2 * Math.PI, 1.0)
            {
                X = rng.NextDouble(),
                Y = rng.NextDouble()
            });
        }
        network.Matrix.FillSpatialCoupling(network.Nodes, k: 1.0, lambda: 0.5, normalize: false);
        return network.Matrix.BuildWeightedLaplacian();
    }

    private static double[,] UnweightedPathLaplacian(int n)
    {
        var l = new double[n, n];
        for (int i = 0; i < n; i++)
        {
            int degree = 0;
            if (i > 0) { l[i, i - 1] = -1; degree++; }
            if (i < n - 1) { l[i, i + 1] = -1; degree++; }
            l[i, i] = degree;
        }
        return l;
    }

    private static double MaxAsymmetry(double[,] m)
    {
        int n = m.GetLength(0);
        double d = 0;
        for (int i = 0; i < n; i++)
            for (int j = i + 1; j < n; j++) d = Math.Max(d, Math.Abs(m[i, j] - m[j, i]));
        return d;
    }

    private static double MaxAbsRowSum(double[,] m)
    {
        int n = m.GetLength(0);
        double d = 0;
        for (int i = 0; i < n; i++)
        {
            double s = 0;
            for (int j = 0; j < n; j++) s += m[i, j];
            d = Math.Max(d, Math.Abs(s));
        }
        return d;
    }

    private static double MinEigenvalue(double[,] m)
    {
        var mat = Matrix<double>.Build.DenseOfArray(m);
        var evd = mat.Evd(Symmetricity.Symmetric);
        return evd.EigenValues.Select(c => c.Real).Min();
    }

    private static double MaxAbsDiff(double[,] a, double[,] b)
    {
        int n = a.GetLength(0);
        double d = 0;
        for (int i = 0; i < n; i++)
            for (int j = 0; j < n; j++) d = Math.Max(d, Math.Abs(a[i, j] - b[i, j]));
        return d;
    }
}
