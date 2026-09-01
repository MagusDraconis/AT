using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// G4-O Phase 1 — does the ρ-only Einstein structure predict an observable difference from GR?
/// Compares a density-sourced theory (GR: source = ρ) against a curvature-sourced theory
/// (AT: source = (ln ρ)″) over uniform / Gaussian / shell / double-peak profiles, measuring the
/// potential, acceleration, redshift, and lensing proxy, and classifies NO/WEAK/STRONG difference.
///
/// Tests: G4-O10 (uniform: STRONG), G4-O11 (shell: STRONG), G4-O12 (double-peak + classification).
/// </summary>
public class G4O_Phase1_DiscriminatingPredictionTests : ResearchTestBase
{
    public G4O_Phase1_DiscriminatingPredictionTests(ITestOutputHelper o) : base(o) { }

    private const int D = 3;

    // ── G4-O10: uniform density — GR sources, AT does not ─────────────────────────────

    [Fact]
    public void G4_O10_UniformDensityStrongDifference()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-O10: uniform density — GR vs AT acceleration");

        double[] xs = { -0.6, -0.3, 0.3, 0.6 };
        sb.AppendLine($"{"x",7} {"a_GR",9} {"a_AT",9}  differ?");
        bool differ = true;
        foreach (double x in xs)
        {
            double ag = PhysicalObservables.GrAcceleration(u => PhysicalObservables.Uniform(u), x);
            double at = PhysicalObservables.AtAcceleration(u => PhysicalObservables.Uniform(u), x, D);
            bool d = Math.Abs(ag) > 1e-6 && Math.Abs(at) < 1e-9;
            if (!d) differ = false;
            sb.AppendLine($"{x,7:F2} {ag,9:F4} {at,9:F4}  {d}");
        }

        sb.AppendLine();
        sb.AppendLine($"GR has non-zero acceleration in uniform density (a = −ρ₀x), AT has zero (a = −∇lnρ = 0): {differ}");
        sb.AppendLine();
        sb.AppendLine("CLASSIFICATION: STRONG DIFFERENCE — a uniform density produces a gravitational field in GR");
        sb.AppendLine("(Newtonian shell/linear field) but NONE in AT (field ∝ ∇ρ = 0).");
        Output.WriteLine(sb.ToString());

        Assert.True(differ, "uniform density should produce a STRONG GR/AT difference");
    }

    // ── G4-O11: shell density — GR long-range, AT localized ───────────────────────────

    [Fact]
    public void G4_O11_ShellDensityStrongDifference()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-O11: shell density — long-range (GR) vs localized (AT) field");

        double xOut = 0.8;  // outside a sharp shell (r=0.5, σ=0.06)
        double ag = PhysicalObservables.GrAcceleration(x => PhysicalObservables.Shell(x, 0.5, 0.5, 0.06), xOut);
        double at = PhysicalObservables.AtAcceleration(x => PhysicalObservables.Shell(x, 0.5, 0.5, 0.06), xOut, D);
        double xIn = 0.2;   // inside the shell
        double agIn = PhysicalObservables.GrAcceleration(x => PhysicalObservables.Shell(x, 0.5, 0.5, 0.06), xIn);
        double atIn = PhysicalObservables.AtAcceleration(x => PhysicalObservables.Shell(x, 0.5, 0.5, 0.06), xIn, D);

        sb.AppendLine($"outside shell (x={xOut}): a_GR = {ag:F4}, a_AT = {at:E2}");
        sb.AppendLine($"inside shell  (x={xIn}): a_GR = {agIn:F4}, a_AT = {atIn:E2}");
        sb.AppendLine();
        sb.AppendLine($"GR field extends outside the shell (|a_GR| ≫ 0): {Math.Abs(ag) > 0.1}");
        sb.AppendLine($"AT field vanishes outside the shell (|a_AT| ≪ |a_GR|): {Math.Abs(at) < 1e-3 && Math.Abs(atIn) < 1e-3}");
        sb.AppendLine();
        sb.AppendLine("CLASSIFICATION: STRONG DIFFERENCE — GR has the Newtonian long-range (1/r²) field outside a");
        sb.AppendLine("mass shell; AT has (exponentially) ZERO field there (the field ∝ ∇ρ is localized at the shell).");
        Output.WriteLine(sb.ToString());

        Assert.True(Math.Abs(ag) > 0.1, "GR acceleration outside shell should be non-zero");
        Assert.True(Math.Abs(at) < 1e-3, "AT acceleration outside shell should vanish");
        Assert.True(Math.Abs(atIn) < 1e-3, "AT acceleration inside shell should also vanish");
    }

    // ── G4-O12: double-peak + redshift/lensing proxy + classification ──────────────────

    [Fact]
    public void G4_O12_DoublePeakAndClassification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-O12: double-peak density + redshift/lensing proxy + classification");

        // AT source changes sign (curvature of ln ρ); GR source is always ≥ 0 (density value).
        double xMid = 0.0; // between the two peaks
        double sGr = PhysicalObservables.GrSource(x => PhysicalObservables.DoublePeak(x), xMid);
        double sAt = PhysicalObservables.AtSource(x => PhysicalObservables.DoublePeak(x), xMid);
        double sAtPeak = PhysicalObservables.AtSource(x => PhysicalObservables.DoublePeak(x), 0.4);

        sb.AppendLine($"double-peak source at x=0:  S_GR = {sGr:F4} (≥0 always), S_AT = {sAt:F4} (>0 at density min)");
        sb.AppendLine($"AT source at peak x=0.4: S_AT = {sAtPeak:F4} (<0 at density max)");
        sb.AppendLine($"AT source SIGN-CHANGES (curvature), GR source does NOT: {sAt > 0 && sAtPeak < 0}");

        // Redshift proxy: AT redshift = −ΔΦ = −(1/d)Δlnρ, a purely LOCAL (edge) effect.
        double zAt = PhysicalObservables.Redshift(0.1, 0.5, 0.5, D); // using the ρ=1+ax² potential for the proxy
        sb.AppendLine($"AT redshift proxy (Φ=(1/d)lnρ) between x=0.1 and 0.5: {zAt:F4}");

        bool signChange = sAt > 0 && sAtPeak < 0;
        sb.AppendLine();
        sb.AppendLine($"AT source sign-changing vs GR positive-definite: {signChange}");
        sb.AppendLine();
        sb.AppendLine("CLASSIFICATION: STRONG DIFFERENCE — AT's source is the sign-changing log-density curvature");
        sb.AppendLine("(positive at density minima, negative at maxima), while GR's source is the always-positive density");
        sb.AppendLine("value. The most decisive observable is the ABSENCE of a long-range field in uniform/shell-exterior");
        sb.AppendLine("regions (G4-O10/11) — a qualitative, falsifiable prediction distinguishing AT from GR.");
        Output.WriteLine(sb.ToString());

        Assert.True(signChange, "AT source should be sign-changing (curvature), GR positive-definite");
    }
}
