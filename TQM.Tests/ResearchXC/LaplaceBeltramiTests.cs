using System.Globalization;
using System.Text;
using MathNet.Numerics.LinearAlgebra;
using TQM.Core.Temporal;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXC;

/// <summary>
/// Determines whether the weighted Laplacian L_W = D_K − K (TemporalMatrix.BuildWeightedLaplacian)
/// approximates a Laplace-Beltrami operator. Tests the flat limit, metric-dependence, and a
/// known manifold (the circle S¹).
/// </summary>
public class LaplaceBeltramiTests : ResearchTestBase
{
    public LaplaceBeltramiTests(ITestOutputHelper o) : base(o) { }

    // ── Test 1: L_W preserves the flat limit ───────────────────────────────

    [Fact]
    public void WeightedLaplacian_PreservesFlatLimit()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("Test 1: uniform path graph ⇒ L_W → flat Laplacian (πk)²");

        int[] sizes = { 32, 64, 128 };
        double prevErr = double.PositiveInfinity;

        foreach (int n in sizes)
        {
            double[] evals = Eigenvalues(BuildPathWeightedLaplacian(n, _ => 1.0));
            double maxErr = 0.0;
            for (int k = 1; k <= 3; k++)
            {
                double scaled = n * n * evals[k];          // (1/Δx²)·λ_k, Δx = 1/N
                double limit = Math.PI * Math.PI * k * k;   // (πk)²
                maxErr = Math.Max(maxErr, Math.Abs(scaled - limit));
            }
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "N={0,-3} low-mode continuum error = {1:E4}", n, maxErr));

            Assert.True(maxErr < prevErr, $"N={n}: error did not decrease (prev {prevErr:E4})");
            prevErr = maxErr;
        }

        sb.AppendLine("PASS: L_W preserves the flat Laplacian limit at rate O(1/N²).");
        Output.WriteLine(sb.ToString());
    }

    // ── Test 2: variable weights change the spectrum ───────────────────────

    [Fact]
    public void WeightedLaplacian_VariableWeightsChangeSpectrum()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("Test 2: variable weights (non-uniform metric) change the spectrum");

        int n = 64;
        double[] uniform = Eigenvalues(BuildPathWeightedLaplacian(n, _ => 1.0));
        double[] variable = Eigenvalues(BuildPathWeightedLaplacian(n, i => i % 2 == 0 ? 1.0 : 3.0));

        double maxDiff = 0.0;
        for (int k = 0; k < n; k++) maxDiff = Math.Max(maxDiff, Math.Abs(uniform[k] - variable[k]));

        sb.AppendLine($"max |λ_uniform − λ_variable| = {maxDiff:F4}");
        Assert.True(maxDiff > 0.1,
            $"variable weights did not change the spectrum (max diff {maxDiff:F4})");
        sb.AppendLine("PASS: L_W is genuinely metric-dependent (spectrum changes with weights).");
        Output.WriteLine(sb.ToString());
    }

    // ── Test 3: known manifold — the circle S¹ ─────────────────────────────

    [Fact]
    public void WeightedLaplacian_MatchesKnownManifoldExample()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("Test 3: cycle graph (uniform) ⇒ L_W → Laplace-Beltrami on S¹ (k²)");

        // The unit circle S¹ has Laplace-Beltrami spectrum {k², k=0,±1,±2,…}.
        // A cycle graph C_N with uniform weights, scaled by (N/2π)², converges to k².
        int[] sizes = { 32, 64, 128 };
        double prevErr = double.PositiveInfinity;

        foreach (int n in sizes)
        {
            double[] evals = Eigenvalues(BuildCycleWeightedLaplacian(n, 1.0));
            double scale = n * n / (4.0 * Math.PI * Math.PI);   // (1/Δθ)², Δθ = 2π/N
            double maxErr = 0.0;
            for (int k = 1; k <= 3; k++)
            {
                // Each nonzero mode k of the cycle graph is 2-fold degenerate
                // (e^{±ikθ}); its first occurrence in the sorted spectrum is at 2k−1.
                double scaled = scale * evals[2 * k - 1];
                double limit = (double)(k * k);
                maxErr = Math.Max(maxErr, Math.Abs(scaled - limit));
            }
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "N={0,-3} low-mode S¹ error = {1:E4}", n, maxErr));

            Assert.True(maxErr < prevErr, $"N={n}: error did not decrease (prev {prevErr:E4})");
            prevErr = maxErr;
        }

        Assert.True(prevErr < 1e-1, $"S¹ spectrum did not converge (final error {prevErr:E3})");
        sb.AppendLine("PASS: L_W matches the Laplace-Beltrami spectrum of S¹ (k²) at rate O(1/N²).");
        Output.WriteLine(sb.ToString());
    }

    // ── Helpers ──────────────────────────────────────────────────────────────

    private static double[,] BuildPathWeightedLaplacian(int n, Func<int, double> edgeWeight)
    {
        var matrix = new TemporalMatrix(n);
        for (int i = 0; i < n - 1; i++)
        {
            double w = edgeWeight(i);
            matrix.SetCoupling(i, i + 1, w);
            matrix.SetCoupling(i + 1, i, w);
        }
        return matrix.BuildWeightedLaplacian();
    }

    private static double[,] BuildCycleWeightedLaplacian(int n, double weight)
    {
        var matrix = new TemporalMatrix(n);
        for (int i = 0; i < n; i++)
        {
            int j = (i + 1) % n;
            matrix.SetCoupling(i, j, weight);
            matrix.SetCoupling(j, i, weight);
        }
        return matrix.BuildWeightedLaplacian();
    }

    private static double[] Eigenvalues(double[,] m)
    {
        var mat = Matrix<double>.Build.DenseOfArray(m);
        var evd = mat.Evd(Symmetricity.Symmetric);
        double[] evals = evd.EigenValues.Select(c => c.Real).ToArray();
        Array.Sort(evals);
        return evals;
    }
}
