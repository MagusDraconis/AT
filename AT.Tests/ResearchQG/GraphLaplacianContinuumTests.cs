using System.Globalization;
using System.Text;
using MathNet.Numerics.LinearAlgebra;
using AT.Tests.Shared;

namespace AT.Tests.ResearchQG;

/// <summary>
/// Verifies the continuum limit L_Q -> flat Laplacian (the discrete second-difference
/// operator on a 1D chain), per Docs/Theory/04_Q_Networks_and_Laplacian.md.
/// The path-graph Laplacian (tridiagonal, diagonal=2, off-diagonal=-1) has the exact
/// spectrum lambda_k = (1/dx^2)[2 - 2 cos(pi k/(N+1))], k = 1..N, dx = 1/(N+1),
/// converging to (pi k)^2 (the eigenvalues of -d^2/dx^2 on [0,1]) as N -> infinity.
/// </summary>
public class GraphLaplacianContinuumTests : ResearchTestBase
{
    public GraphLaplacianContinuumTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void GraphLaplacian_ConvergesToFlatLaplacian()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

        int[] sizes = { 32, 64, 128, 256 };
        double tol = 1e-6;                       // numerical EVD vs closed form
        int lowModes = 3;                        // k = 1..lowModes for the continuum-rate check

        var sb = new StringBuilder();
        PrintHeader("L_Q -> Flat Laplacian Continuum Verification");

        double prevContinuumErr = double.PositiveInfinity;

        foreach (int n in sizes)
        {
            double dx = 1.0 / (n + 1.0);
            double[] numerical = ScaledLaplacianEigenvalues(n, dx);

            // 1. Relative error against the closed-form discrete spectrum.
            double maxRelErr = 0.0;
            for (int k = 0; k < n; k++)
            {
                double expected = (1.0 / (dx * dx)) * (2.0 - 2.0 * Math.Cos(Math.PI * (k + 1) / (n + 1.0)));
                double relErr = Math.Abs(numerical[k] - expected) / Math.Max(1.0, Math.Abs(expected));
                maxRelErr = Math.Max(maxRelErr, relErr);
            }

            // 2. Convergence rate: deviation of the low modes from the continuum (pi k)^2.
            double continuumErr = 0.0;
            for (int k = 1; k <= lowModes; k++)
            {
                double discrete = (1.0 / (dx * dx)) * (2.0 - 2.0 * Math.Cos(Math.PI * k / (n + 1.0)));
                double limit = Math.PI * Math.PI * k * k;
                continuumErr = Math.Max(continuumErr, Math.Abs(discrete - limit));
            }

            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "N={0,-4} dx=1/{1,-4} maxRelErr={2:E3}  lowModeContinuumErr={3:E4}",
                n, n + 1, maxRelErr, continuumErr));

            // Assertions: relative error within tolerance; error decreases with N.
            Assert.True(maxRelErr < tol,
                $"N={n}: max relative error {maxRelErr:E3} exceeds tolerance {tol}");
            Assert.True(continuumErr < prevContinuumErr,
                $"N={n}: continuum error {continuumErr:E4} did not decrease (prev {prevContinuumErr:E4})");
            prevContinuumErr = continuumErr;
        }

        sb.AppendLine();
        sb.AppendLine("Verified: L_Q spectrum = (1/dx^2)[2 - 2cos(pi k/(N+1))] -> (pi k)^2 as N -> infinity.");
        Output.WriteLine(sb.ToString());
    }

    /// <summary>
    /// Builds the 1D path-graph Laplacian L_Q (tridiagonal, diagonal=2, off-diagonal=-1),
    /// computes its eigenvalues numerically (symmetric EVD), sorts them ascending, and
    /// returns them scaled by 1/dx^2 (the continuum operator).
    /// </summary>
    private static double[] ScaledLaplacianEigenvalues(int n, double dx)
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

        double scale = 1.0 / (dx * dx);
        for (int i = 0; i < n; i++) evals[i] *= scale;
        return evals;
    }
}
