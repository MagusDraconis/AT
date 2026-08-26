using System.Globalization;
using System.Text;
using MathNet.Numerics.LinearAlgebra;
using AT.Core.Temporal;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXC;

/// <summary>
/// Determines whether a curved-space Schrödinger equation can be constructed from the
/// verified weighted Laplacian L_W = D_K − K (TemporalMatrix.BuildWeightedLaplacian).
/// The curved Schrödinger is i·∂ψ/∂t = L_W ψ; the flat case is L_W = L_Q (uniform weights).
/// </summary>
public class CurvedSchrodingerTests : ResearchTestBase
{
    public CurvedSchrodingerTests(ITestOutputHelper o) : base(o) { }

    // ── Test 1: L_W defines a curved (metric-dependent) operator ───────────

    [Fact]
    public void WeightedLaplacian_DefinesCurvedOperator()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("Test 1: L_W defines a curved (metric-dependent) operator");

        int n = 32;
        double[,] flat = BuildPathWeightedLaplacian(n, _ => 1.0);
        double[,] curved = BuildPathWeightedLaplacian(n, i => i % 2 == 0 ? 1.0 : 3.0);

        double asym = MaxAsymmetry(curved);
        double minEig = MinEigenvalue(curved);
        double[] flatEigs = Eigenvalues(flat), curvedEigs = Eigenvalues(curved);
        double maxDiff = 0;
        for (int k = 0; k < n; k++) maxDiff = Math.Max(maxDiff, Math.Abs(flatEigs[k] - curvedEigs[k]));

        sb.AppendLine($"L_W asymmetry = {asym:E3}, min eigenvalue = {minEig:F4}");
        sb.AppendLine($"max |λ_flat − λ_curved| = {maxDiff:F4}");

        Assert.True(asym < 1e-12, "L_W is not symmetric (not a valid Hamiltonian)");
        Assert.True(minEig >= -1e-9, "L_W is not positive semi-definite");
        Assert.True(maxDiff > 0.1, "non-uniform weights do not change the spectrum (not curved)");
        sb.AppendLine("PASS: L_W is a symmetric, positive-definite, metric-dependent operator.");
        Output.WriteLine(sb.ToString());
    }

    // ── Test 2: the curved operator reduces to the flat Schrödinger ────────

    [Fact]
    public void CurvedOperator_ReducesToFlatSchrodinger()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("Test 2: uniform weights ⇒ i∂ψ/∂t = L_W ψ reduces to flat i∂ψ/∂t = L_Q ψ");

        int n = 16;
        double[,] lw = BuildPathWeightedLaplacian(n, _ => 1.0);
        double[,] lq = UnweightedPathLaplacian(n);
        double maxDiff = MaxAbsDiff(lw, lq);

        sb.AppendLine($"max |L_W − L_Q| = {maxDiff:E3}");
        Assert.True(maxDiff < 1e-12, "uniform-weight L_W does not equal the flat L_Q");
        sb.AppendLine("PASS: uniform weights ⇒ curved Schrödinger = flat Schrödinger (i∂ψ/∂t = L_Q ψ).");
        Output.WriteLine(sb.ToString());
    }

    // ── Test 3: the curved Schrödinger equation conserves the norm ─────────

    [Fact]
    public void CurvedSchrodinger_ConservesNorm()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("Test 3: i∂ψ/∂t = L_W ψ conserves ||ψ||² (unitary evolution)");

        int n = 24;
        double[,] lw = BuildPathWeightedLaplacian(n, i => i % 2 == 0 ? 1.0 : 3.0);

        // Self-adjointness: symmetric generator + real eigenvalues ⇒ unitary propagator.
        double asym = MaxAsymmetry(lw);
        var mat = Matrix<double>.Build.DenseOfArray(lw);
        var evd = mat.Evd(Symmetricity.Symmetric);
        double[] lambdas = evd.EigenValues.Select(c => c.Real).ToArray();
        double maxImag = evd.EigenValues.Select(c => Math.Abs(c.Imaginary)).Max();
        var v = evd.EigenVectors; // Matrix<Complex>

        sb.AppendLine($"L_W asymmetry = {asym:E3}, max |Im(λ_k)| = {maxImag:E3}");

        // Direct dynamical check: evolve a unit state via the exact propagator
        // ψ(t) = V diag(e^{−iλ_k t}) V^T ψ(0), and verify ||ψ(t)||² = 1.
        var rng = new Random(7);
        double[] psi0 = Enumerable.Range(0, n).Select(_ => rng.NextDouble() - 0.5).ToArray();
        double norm0 = Math.Sqrt(psi0.Sum(p => p * p));
        for (int j = 0; j < n; j++) psi0[j] /= norm0; // ||ψ(0)|| = 1

        double[] c = new double[n]; // c = V^T ψ(0)
        for (int k = 0; k < n; k++)
            for (int j = 0; j < n; j++) c[k] += v[j, k] * psi0[j];

        double maxNormErr = 0;
        foreach (double t in new[] { 0.1, 0.5, 1.0, 2.0 })
        {
            double norm2 = 0;
            for (int j = 0; j < n; j++)
            {
                double re = 0, im = 0;
                for (int k = 0; k < n; k++)
                {
                    re += v[j, k] * Math.Cos(lambdas[k] * t) * c[k];
                    im -= v[j, k] * Math.Sin(lambdas[k] * t) * c[k];
                }
                norm2 += re * re + im * im;
            }
            double err = Math.Abs(norm2 - 1.0);
            maxNormErr = Math.Max(maxNormErr, err);
            sb.AppendLine($"t={t:F1}: ||ψ(t)||² = {norm2:F10} (err {err:E3})");
        }

        Assert.True(asym < 1e-12, "L_W is not self-adjoint");
        Assert.True(maxImag < 1e-9, "L_W has non-real eigenvalues (non-unitary)");
        Assert.True(maxNormErr < 1e-9, $"norm not conserved (max error {maxNormErr:E3})");
        sb.AppendLine("PASS: the curved Schrödinger equation conserves the norm (unitary).");
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

    private static double MaxAbsDiff(double[,] a, double[,] b)
    {
        int n = a.GetLength(0);
        double d = 0;
        for (int i = 0; i < n; i++)
            for (int j = 0; j < n; j++) d = Math.Max(d, Math.Abs(a[i, j] - b[i, j]));
        return d;
    }

    private static double MinEigenvalue(double[,] m)
    {
        var mat = Matrix<double>.Build.DenseOfArray(m);
        return mat.Evd(Symmetricity.Symmetric).EigenValues.Select(c => c.Real).Min();
    }

    private static double[] Eigenvalues(double[,] m)
    {
        var mat = Matrix<double>.Build.DenseOfArray(m);
        double[] evals = mat.Evd(Symmetricity.Symmetric).EigenValues.Select(c => c.Real).ToArray();
        Array.Sort(evals);
        return evals;
    }
}
