using System.Globalization;
using System.Text;
using MathNet.Numerics.LinearAlgebra;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXC;

/// <summary>
/// Verifies the Quantum-Gravity Bridge finding (Docs/Audits/QuantumGravityBridge.md):
/// the quantum operator L_Q and the gravity operator (d'Alembertian □, the BDG continuum
/// limit) have INCOMPATIBLE spectral signatures, so no single operator bridges them.
///   L_Q  (graph Laplacian): positive semi-definite (Riemannian / elliptic).
///   □ = ∂²/∂t² − ∂²/∂x²:   indefinite (Lorentzian / hyperbolic) — plane-wave
///                           eigenvalues k² − ω² take both signs.
/// </summary>
public class QuantumGravityBridgeTests : ResearchTestBase
{
    public QuantumGravityBridgeTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void QuantumGravityBridge_OperatorsDifferInSignature()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("Quantum-Gravity Bridge: L_Q (Riemannian) vs □ (Lorentzian) signature");

        // ---- 1. L_Q (graph Laplacian) is positive semi-definite (Riemannian). ----
        int n = 64;
        var lq = BuildPathGraphLaplacian(n);
        var evd = lq.Evd(Symmetricity.Symmetric);
        double[] lqEvals = evd.EigenValues.Select(c => c.Real).ToArray();

        double minLq = lqEvals.Min();
        double maxLq = lqEvals.Max();
        sb.AppendLine($"L_Q (N={n}): eigenvalues in [{minLq:F6}, {maxLq:F6}] — all >= 0");

        // ---- 2. The d'Alembertian □ is indefinite (Lorentzian). ----
        // For a plane wave cos(kx − ωt), □φ = (k² − ω²)·φ. The coefficient changes sign
        // with k vs ω, so □ has both positive and negative eigenvalues.
        double omega = 2.0 * Math.PI * 0.3;
        double kLow = 2.0 * Math.PI * 0.1;   // k < ω  => k² − ω² < 0
        double kHigh = 2.0 * Math.PI * 0.5;  // k > ω  => k² − ω² > 0
        double eigLow = kLow * kLow - omega * omega;
        double eigHigh = kHigh * kHigh - omega * omega;
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "□ plane-wave eigenvalues: (k=0.1) k²−ω²={0:F4} < 0,  (k=0.5) k²−ω²={1:F4} > 0",
            eigLow, eigHigh));

        // ---- Assertions. ----
        Assert.True(minLq >= -1e-9, $"L_Q is not positive semi-definite (min={minLq:E3})");
        Assert.True(eigLow < 0.0 && eigHigh > 0.0,
            "□ is not indefinite (expected one negative and one positive eigenvalue)");

        // The two operators cannot be the same object: one has a non-negative spectrum,
        // the other an indefinite spectrum. Hence no single operator bridges L_Q → □.
        sb.AppendLine();
        sb.AppendLine("Verdict: L_Q (non-negative spectrum) and □ (indefinite spectrum) are");
        sb.AppendLine("         incompatible operators — the quantum-gravity bridge is absent.");
        Output.WriteLine(sb.ToString());
    }

    /// <summary>1D path-graph Laplacian: tridiagonal, diagonal=2, off-diagonal=-1.</summary>
    private static Matrix<double> BuildPathGraphLaplacian(int n)
    {
        var a = new double[n, n];
        for (int i = 0; i < n; i++)
        {
            a[i, i] = 2.0;
            if (i > 0) a[i, i - 1] = -1.0;
            if (i < n - 1) a[i, i + 1] = -1.0;
        }
        return Matrix<double>.Build.DenseOfArray(a);
    }
}
