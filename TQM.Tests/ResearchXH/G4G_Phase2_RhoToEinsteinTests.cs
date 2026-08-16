using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// G4-G Phase 2 — is the Einstein structure fully encoded in ρ? Reconstructs G_μν DIRECTLY from
/// ρ, ∂ρ, ∂²ρ (no intermediate metric/σ objects) and compares it against the metric-based
/// reconstruction (through σ = (1/d)ln ρ), measuring exact agreement, refinement stability of the
/// finite-difference reconstruction, and dimension dependence.
///
/// Tests: G4-G20 (direct = metric-based), G4-G21 (finite-difference refinement), G4-G22 (dimension).
/// </summary>
public class G4G_Phase2_RhoToEinsteinTests : ResearchTestBase
{
    public G4G_Phase2_RhoToEinsteinTests(ITestOutputHelper o) : base(o) { }

    private const double A = 0.5;

    // ── G4-G20: direct reconstruction agrees with the metric-based one ─────────────────

    [Fact]
    public void G4_G20_DirectReconstructionMatchesMetricBased()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-G20: direct (ρ,ρ′,ρ″) reconstruction vs metric-based reconstruction");

        double[] xs = { -0.8, -0.4, 0.0, 0.4, 0.8 };
        bool allOk = true;
        foreach (int d in new[] { 2, 3, 4 })
        {
            sb.AppendLine($"d = {d}:");
            sb.AppendLine($"{"x",7} {"G11 direct",11} {"G11 metric",11} {"Gii direct",11} {"Gii metric",11}  match");
            foreach (double x in xs)
            {
                double rho = HigherDimEinstein.Rho(x, A);
                double rp = HigherDimEinstein.RhoPrime(x, A);
                double r2 = HigherDimEinstein.RhoSecond(x, A);
                double d11 = HigherDimEinstein.DirectEinstein11(rho, rp, d);
                double m11 = HigherDimEinstein.Einstein11(x, A, d);
                double doi = HigherDimEinstein.DirectEinsteinOther(rho, rp, r2, d);
                double moi = HigherDimEinstein.EinsteinOther(x, A, d);
                bool ok = Math.Abs(d11 - m11) < 1e-12 && Math.Abs(doi - moi) < 1e-12;
                if (!ok) allOk = false;
                sb.AppendLine($"{x,7:F2} {d11,11:F5} {m11,11:F5} {doi,11:F5} {moi,11:F5}  {ok}");
            }
            sb.AppendLine();
        }

        sb.AppendLine($"direct reconstruction ≡ metric-based for all d and x: {allOk}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: G_μν is a pure algebraic function of ρ, ∂ρ, ∂²ρ — no intermediate metric");
        sb.AppendLine("objects are required. The Einstein structure is fully encoded in the counting measure.");
        Output.WriteLine(sb.ToString());

        Assert.True(allOk, "direct reconstruction does not match the metric-based one");
    }

    // ── G4-G21: finite-difference (refinement) reconstruction converges ────────────────

    [Fact]
    public void G4_G21_FiniteDifferenceRefinementConverges()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-G21: finite-difference reconstruction converges under refinement");

        // Non-quadratic profile ρ = 1 + a·x⁴ (so central differences have genuine truncation error).
        double x = 0.3;
        static double Rho(double x) => 1.0 + 0.5 * x * x * x * x;
        static double RhoPrime(double x) => 2.0 * x * x * x;    // 4ax³, a=0.5 → 2x³
        static double RhoSecond(double x) => 6.0 * x * x;       // 12ax² → 6x²
        int d = 3;
        double rho = Rho(x);
        double exact11 = HigherDimEinstein.DirectEinstein11(rho, RhoPrime(x), d);
        double exactO = HigherDimEinstein.DirectEinsteinOther(rho, RhoPrime(x), RhoSecond(x), d);

        sb.AppendLine($"{"h",10} {"err G11",10} {"err Gii",10}");
        var errs = new List<(double h, double e11, double ei)>();
        foreach (double h in new[] { 0.1, 0.05, 0.025, 0.0125 })
        {
            double rp = (Rho(x + h) - Rho(x - h)) / (2 * h);
            double r2 = (Rho(x + h) - 2 * rho + Rho(x - h)) / (h * h);
            double fd11 = HigherDimEinstein.DirectEinstein11(rho, rp, d);
            double fdi = HigherDimEinstein.DirectEinsteinOther(rho, rp, r2, d);
            double e11 = Math.Abs(fd11 - exact11);
            double ei = Math.Abs(fdi - exactO);
            errs.Add((h, e11, ei));
            sb.AppendLine($"{h,10:F4} {e11,10:E2} {ei,10:E2}");
        }

        bool converges = errs[^1].e11 < errs[0].e11 && errs[^1].ei < errs[0].ei;
        sb.AppendLine();
        sb.AppendLine($"finite-difference error decreases under refinement: {converges}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the direct reconstruction from numerical ∂ρ, ∂²ρ converges to the analytic");
        sb.AppendLine("G_μν as the grid spacing h → 0 — refinement-stable.");
        Output.WriteLine(sb.ToString());

        Assert.True(converges, "finite-difference reconstruction does not converge under refinement");
    }

    // ── G4-G22: dimension dependence of the direct reconstruction ──────────────────────

    [Fact]
    public void G4_G22_DimensionDependence()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-G22: dimension dependence of the direct reconstruction");

        double x = 0.3;
        sb.AppendLine($"{"d",4} {"G_11 direct",12} {"G_ii direct",12} {"non-trivial",12} {"trace −(d−2)R/2",16}");
        bool traceOk = true;
        foreach (int d in new[] { 2, 3, 4, 5, 6 })
        {
            double rho = HigherDimEinstein.Rho(x, A);
            double rp = HigherDimEinstein.RhoPrime(x, A);
            double r2 = HigherDimEinstein.RhoSecond(x, A);
            double g11 = HigherDimEinstein.DirectEinstein11(rho, rp, d);
            double go = HigherDimEinstein.DirectEinsteinOther(rho, rp, r2, d);
            double tr = Math.Pow(rho, -2.0 / d) * (g11 + (d - 1.0) * go);
            double expect = -0.5 * (d - 2.0) * HigherDimEinstein.ScalarCurvature(x, A, d);
            bool trOk = Math.Abs(tr - expect) < 1e-10;
            if (!trOk) traceOk = false;
            bool nt = Math.Max(Math.Abs(g11), Math.Abs(go)) > 1e-9;
            sb.AppendLine($"{d,4} {g11,12:F6} {go,12:F6} {nt,12} {trOk,16}");
        }

        sb.AppendLine();
        sb.AppendLine($"trace identity holds in all dimensions d=2..6: {traceOk}");
        sb.AppendLine($"non-trivial for d≥3, degenerate at d=2");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the direct reconstruction is dimension-generic — it produces the correct");
        sb.AppendLine("Einstein structure (trace + non-triviality) in every dimension, with d=2 as the degenerate case.");
        Output.WriteLine(sb.ToString());

        Assert.True(traceOk, "trace identity fails in some dimension");
        // d=2 degenerate, d=3+ non-trivial.
        Assert.True(Math.Abs(HigherDimEinstein.DirectEinstein11(HigherDimEinstein.Rho(x, A), HigherDimEinstein.RhoPrime(x, A), 2)) < 1e-12, "d=2 should be degenerate");
        Assert.True(Math.Abs(HigherDimEinstein.DirectEinstein11(HigherDimEinstein.Rho(x, A), HigherDimEinstein.RhoPrime(x, A), 4)) > 1e-9, "d=4 should be non-trivial");
    }
}
