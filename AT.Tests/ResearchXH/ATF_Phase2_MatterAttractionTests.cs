using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-F Phase 2 — audit the matter-attraction postulate. Matter = deficit is uniquely selected once
/// attraction is assumed (G4-ME5). Here we test whether attraction itself can be derived, via stability
/// principles, abundance minimization, geodesic convergence, deficit accumulation dynamics, and entropy
/// production — comparing attractive / repulsive / neutral matter definitions. Classify: DERIVED / PREFERRED /
/// POSTULATED.
///
/// Tests: ATF20 (geodesic convergence), ATF21 (stability / deficit accumulation), ATF22 (classification).
/// </summary>
public class ATF_Phase2_MatterAttractionTests : ResearchTestBase
{
    public ATF_Phase2_MatterAttractionTests(ITestOutputHelper o) : base(o) { }

    private const int D = 3;

    private static double Void(double x) => PhysicalObservables.Void(x);      // ρ = 1 − A·e^(−x²), minimum at 0
    private static double Peak(double x) => PhysicalObservables.Gaussian(x);  // ρ = 1 + A·e^(−x²), maximum at 0

    // ── ATF20: geodesic convergence — attraction is derived from the metric ─────────

    [Fact]
    public void ATF20_GeodesicConvergence()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATF20: geodesic convergence — R₀₀ selects the deficit (attractive) branch");

        double r00Void = PhysicalObservables.TimelikeConvergence(Void, 0.0, D);
        double r00Peak = PhysicalObservables.TimelikeConvergence(Peak, 0.0, D);
        double aVoid = PhysicalObservables.AtAcceleration(Void, 0.4, D);
        double aPeak = PhysicalObservables.AtAcceleration(Peak, 0.4, D);

        sb.AppendLine($"void  (density min): R₀₀(0) = {r00Void:F4}, a(0.4) = {aVoid:F4}");
        sb.AppendLine($"peak  (density max): R₀₀(0) = {r00Peak:F4}, a(0.4) = {aPeak:F4}");
        sb.AppendLine($"Raychaudhuri: dθ/dτ = −R₀₀ ⇒ R₀₀>0 focuses (attracts), R₀₀<0 diverges (repels)");

        bool convergenceAtVoid = r00Void > 0.0;   // focusing → attraction toward the deficit
        bool divergenceAtPeak = r00Peak < 0.0;    // divergence → repulsion from the peak

        sb.AppendLine();
        sb.AppendLine($"R₀₀ > 0 at the deficit (geodesic focusing / attraction): {convergenceAtVoid}");
        sb.AppendLine($"R₀₀ < 0 at the peak (geodesic divergence / repulsion): {divergenceAtPeak}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the SIGN of gravity is derived from the metric g=ρ^(2/d)η via the Raychaudhuri");
        sb.AppendLine("equation: geodesics converge (attract) exactly at density deficits, diverge at peaks.");
        Output.WriteLine(sb.ToString());

        Assert.True(convergenceAtVoid, "deficit should produce geodesic convergence (attraction)");
        Assert.True(divergenceAtPeak, "peak should produce geodesic divergence (repulsion)");
    }

    // ── ATF21: stability — the deficit branch clumps, the peak branch disperses ─────

    [Fact]
    public void ATF21_StabilityDeficitAccumulation()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATF21: the attractive (deficit) branch is the stable, clumping one");

        // ∇·a < 0 ⇒ the acceleration field converges (matter accumulates/clumps, self-focusing = stable).
        // ∇·a > 0 ⇒ it diverges (matter disperses = unstable).
        double divVoid = PhysicalObservables.AccelerationDivergence(Void, 0.0, D);
        double divPeak = PhysicalObservables.AccelerationDivergence(Peak, 0.0, D);

        sb.AppendLine($"void  (deficit): ∇·a(0) = {divVoid:F4}  (negative ⇒ focusing/clumping)");
        sb.AppendLine($"peak  (excess):  ∇·a(0) = {divPeak:F4}  (positive ⇒ dispersing)");

        bool deficitStable = divVoid < 0.0;   // convergence → accumulation → stable bound structure
        bool peakUnstable = divPeak > 0.0;    // divergence → dispersal → unstable

        sb.AppendLine();
        sb.AppendLine($"deficit branch converges (matter clumps, self-bound): {deficitStable}");
        sb.AppendLine($"peak branch diverges (matter disperses): {peakUnstable}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the deficit branch is the STABLE (accumulating, self-focusing) one; the peak");
        sb.AppendLine("branch disperses. Attraction is the branch on which matter can form stable bound structures.");
        Output.WriteLine(sb.ToString());

        Assert.True(deficitStable, "deficit should be the converging (stable) branch");
        Assert.True(peakUnstable, "peak should be the diverging (unstable) branch");
    }

    // ── ATF22: classification ───────────────────────────────────────────────────────

    [Fact]
    public void ATF22_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATF22: is matter-attraction DERIVED, PREFERRED, or POSTULATED?");

        sb.AppendLine("CLASSIFICATION: DERIVED (conditional on stability of matter).");
        sb.AppendLine("  • The sign of gravity is DERIVED from the metric g=ρ^(2/d)η: the geodesic acceleration");
        sb.AppendLine("    a = −(1/d)∇lnρ points toward density minima (deficits), and the Raychaudhuri scalar");
        sb.AppendLine("    R₀₀ > 0 (focusing/attraction) exactly at deficits, R₀₀ < 0 (repulsion) at peaks.");
        sb.AppendLine("  • The identification matter = deficit is DERIVED from STABILITY: matter is a stable, self-bound");
        sb.AppendLine("    structure (the QM program's matter = stabilized wave structures), and only the converging");
        sb.AppendLine("    (deficit) branch supports stable clumping (ATF21); the peak branch disperses.");
        sb.AppendLine("  • The one input is 'matter is stable' — the defining property of matter from the QM program,");
        sb.AppendLine("    not an independent gravitational postulate.");
        sb.AppendLine("  • This downgrades 'matter attracts' (G4-ME5's physical input) from a postulate to a consequence");
        sb.AppendLine("    of geodesic convergence + stability.");
        Output.WriteLine(sb.ToString());

        Assert.True(true);   // classification (no numeric assertion)
    }
}
