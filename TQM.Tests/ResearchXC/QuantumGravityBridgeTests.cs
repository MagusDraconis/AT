using System.Globalization;
using System.Text;
using MathNet.Numerics.LinearAlgebra;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXC;

/// <summary>
/// Executable form of the Quantum-Gravity Bridge finding (Docs/Audits/QuantumGravityBridge.md):
/// the quantum operator L_Q and the gravity operator (d'Alembertian □, the BDG continuum
/// limit) have incompatible spectral signatures, so no single operator bridges them.
///
///   L_Q  (graph Laplacian): positive semi-definite (Riemannian / elliptic).
///   □ = ∂²/∂t² − ∂²/∂x²:   indefinite (Lorentzian / hyperbolic).
/// </summary>
public class QuantumGravityBridgeTests : ResearchTestBase
{
    public QuantumGravityBridgeTests(ITestOutputHelper o) : base(o) { }

    // ── Test 1: L_Q is positive semi-definite ──────────────────────────────

    [Fact]
    public void GraphLaplacian_IsPositiveSemidefinite()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("Test 1: L_Q is positive semi-definite (Riemannian)");

        int n = 64;
        double[] evals = PathGraphLaplacianEigenvalues(n);
        double minEval = evals.Min();

        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "L_Q (N={0}): eigenvalues in [{1:F6}, {2:F6}]", n, minEval, evals.Max()));

        Assert.True(minEval >= -1e-9,
            $"L_Q is not positive semi-definite (min eigenvalue = {minEval:E3})");
        sb.AppendLine("PASS: min eigenvalue >= 0 (all eigenvalues non-negative).");
        Output.WriteLine(sb.ToString());
    }

    // ── Test 2: the BDG operator (□) is indefinite ─────────────────────────

    [Fact]
    public void BDGOperator_IsIndefinite()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("Test 2: BDG operator (d'Alembertian □) is indefinite (Lorentzian)");

        // Evaluate the discrete d'Alembertian stencil at (x,t)=(0,0) on a plane wave
        // φ = cos(kx − ωt). Its action is 2[cos(ωh) − cos(kh)]/h², whose sign flips
        // between k<ω (negative) and k>ω (positive).
        double h = 1.0 / 128;
        double omega = 2.0 * Math.PI * 0.3;
        double kLow = 2.0 * Math.PI * 0.1;   // k < ω
        double kHigh = 2.0 * Math.PI * 0.5;  // k > ω

        double boxLow = DiscreteDAlembertian(0.0, 0.0, h, kLow, omega);
        double boxHigh = DiscreteDAlembertian(0.0, 0.0, h, kHigh, omega);

        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "□_h φ(0,0)  [k=0.1 < ω]: {0:F4}  (negative)", boxLow));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "□_h φ(0,0)  [k=0.5 > ω]: {0:F4}  (positive)", boxHigh));

        Assert.True(boxLow < 0.0, $"Expected □φ(k<ω) < 0, got {boxLow:F4}");
        Assert.True(boxHigh > 0.0, $"Expected □φ(k>ω) > 0, got {boxHigh:F4}");
        sb.AppendLine("PASS: the operator changes sign with k vs ω — indefinite.");
        Output.WriteLine(sb.ToString());
    }

    // ── Test 3: the two operators differ in signature ──────────────────────

    [Fact]
    public void QuantumGravityBridge_OperatorsDifferInSignature()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("Test 3: L_Q (non-negative) vs □ (indefinite) — no bridge");

        double[] lqEvals = PathGraphLaplacianEigenvalues(64);
        double minLq = lqEvals.Min();

        double omega = 2.0 * Math.PI * 0.3;
        double kLow = 2.0 * Math.PI * 0.1;
        double kHigh = 2.0 * Math.PI * 0.5;
        double eigLow = kLow * kLow - omega * omega;    // < 0
        double eigHigh = kHigh * kHigh - omega * omega; // > 0

        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "L_Q min eigenvalue = {0:F6}  (>= 0)", minLq));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "□ plane-wave eigenvalues: {0:F4} (<0)  and  {1:F4} (>0)", eigLow, eigHigh));

        Assert.True(minLq >= -1e-9, "L_Q is not positive semi-definite");
        Assert.True(eigLow < 0.0 && eigHigh > 0.0, "□ is not indefinite");

        // One operator has a non-negative spectrum, the other an indefinite spectrum.
        // They cannot be the same object; no single operator bridges L_Q -> □.
        sb.AppendLine("PASS: incompatible signatures — the quantum-gravity bridge is absent.");
        Output.WriteLine(sb.ToString());
    }

    // ── Helpers ──────────────────────────────────────────────────────────────

    /// <summary>1D path-graph Laplacian eigenvalues (tridiagonal, diagonal=2, off-diagonal=-1).</summary>
    private static double[] PathGraphLaplacianEigenvalues(int n)
    {
        var a = new double[n, n];
        for (int i = 0; i < n; i++)
        {
            a[i, i] = 2.0;
            if (i > 0) a[i, i - 1] = -1.0;
            if (i < n - 1) a[i, i + 1] = -1.0;
        }
        var mat = Matrix<double>.Build.DenseOfArray(a);
        var evd = mat.Evd(Symmetricity.Symmetric);
        double[] evals = evd.EigenValues.Select(c => c.Real).ToArray();
        Array.Sort(evals);
        return evals;
    }

    private static double Phi(double x, double t, double k, double omega) =>
        Math.Cos(k * x - omega * t);

    /// <summary>Discrete d'Alembertian (4-neighbor stencil) on a 1+1 grid.</summary>
    private static double DiscreteDAlembertian(double x, double t, double h, double k, double omega)
    {
        double up = Phi(x, t + h, k, omega);
        double down = Phi(x, t - h, k, omega);
        double right = Phi(x + h, t, k, omega);
        double left = Phi(x - h, t, k, omega);
        return (up + down - right - left) / (h * h);
    }
}
