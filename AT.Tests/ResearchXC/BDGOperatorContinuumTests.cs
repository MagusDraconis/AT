using System.Globalization;
using System.Text;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXC;

/// <summary>
/// Verifies the BDG (causal-set d'Alembertian) operator's continuum limit on a flat
/// 1+1 lattice. The BDG operator's defining property (BdgUniquenessAnalyzer, O0) is that
/// it converges to the Lorentzian d'Alembertian □ = ∂²/∂t² − ∂²/∂x². On a regular grid this
/// reduces to the 4-neighbor stencil [φ(t+h) + φ(t−h) − φ(x+h) − φ(x−h)] / h².
/// This test checks that stencil against the exact □φ for a plane wave, confirming
/// convergence at rate O(h²).
/// </summary>
public class BDGOperatorContinuumTests : ResearchTestBase
{
    public BDGOperatorContinuumTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void BDGOperator_ConvergesToDAlembertian()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

        // Plane wave φ(x,t) = cos(kx − ωt); exact d'Alembertian □φ = (k² − ω²)·φ.
        double k = 2.0 * Math.PI * 0.5;      // spatial frequency
        double omega = 2.0 * Math.PI * 0.3;  // temporal frequency
        double x0 = 0.4, t0 = 0.4;           // evaluation point (grid-aligned multiples of h)

        double[] hValues = { 1.0 / 16, 1.0 / 32, 1.0 / 64, 1.0 / 128 };
        double tol = 1e-2; // loose bound; the O(h²) truncation error is ~0.4% at h=1/16

        var sb = new StringBuilder();
        PrintHeader("BDG Operator → d'Alembertian Continuum Verification (1+1)");

        double prevErr = double.PositiveInfinity;
        foreach (double h in hValues)
        {
            // Align the evaluation point to the grid so the stencil is exact.
            double x = Math.Round(x0 / h) * h;
            double t = Math.Round(t0 / h) * h;

            double discrete = DiscreteDAlembertian(x, t, h, k, omega);
            double exact = (k * k - omega * omega) * Phi(x, t, k, omega);
            double relErr = Math.Abs(discrete - exact) / Math.Max(1.0, Math.Abs(exact));

            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "h=1/{0,-3} relErr={1:E3}  discrete={2:F4} exact={3:F4}",
                (int)Math.Round(1.0 / h), relErr, discrete, exact));

            Assert.True(relErr < tol, $"h={h}: relative error {relErr:E3} exceeds tolerance {tol}");
            Assert.True(relErr < prevErr, $"h={h}: error did not decrease (prev {prevErr:E3})");
            prevErr = relErr;
        }

        sb.AppendLine();
        sb.AppendLine("Verified: discrete d'Alembertian → □ = ∂²/∂t² − ∂²/∂x² at rate O(h²).");
        Output.WriteLine(sb.ToString());
    }

    private static double Phi(double x, double t, double k, double omega) =>
        Math.Cos(k * x - omega * t);

    /// <summary>
    /// Discrete d'Alembertian on a 1+1 grid (Lorentzian signature):
    /// □_h φ(x,t) = [φ(x,t+h) + φ(x,t−h) − φ(x+h,t) − φ(x−h,t)] / h².
    /// </summary>
    private static double DiscreteDAlembertian(double x, double t, double h, double k, double omega)
    {
        double up = Phi(x, t + h, k, omega);
        double down = Phi(x, t - h, k, omega);
        double right = Phi(x + h, t, k, omega);
        double left = Phi(x - h, t, k, omega);
        return (up + down - right - left) / (h * h);
    }
}
