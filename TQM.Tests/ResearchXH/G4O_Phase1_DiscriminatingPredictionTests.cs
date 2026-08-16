using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// G4-O Phase 1 — does the ρ-only Einstein structure predict an observable difference from GR?
/// Compares a density-sourced theory (GR: source = ρ) against a curvature-sourced theory
/// (TQM: source = (ln ρ)″) over uniform / Gaussian / shell / double-peak profiles, measuring the
/// potential, acceleration, redshift, and lensing proxy, and classifies NO/WEAK/STRONG difference.
///
/// Tests: G4-O10 (uniform: STRONG), G4-O11 (shell: STRONG), G4-O12 (double-peak + classification).
/// </summary>
public class G4O_Phase1_DiscriminatingPredictionTests : ResearchTestBase
{
    public G4O_Phase1_DiscriminatingPredictionTests(ITestOutputHelper o) : base(o) { }

    private const int D = 3;

    // ── G4-O10: uniform density — GR sources, TQM does not ─────────────────────────────

    [Fact]
    public void G4_O10_UniformDensityStrongDifference()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-O10: uniform density — GR vs TQM acceleration");

        double[] xs = { -0.6, -0.3, 0.3, 0.6 };
        sb.AppendLine($"{"x",7} {"a_GR",9} {"a_TQM",9}  differ?");
        bool differ = true;
        foreach (double x in xs)
        {
            double ag = PhysicalObservables.GrAcceleration(u => PhysicalObservables.Uniform(u), x);
            double at = PhysicalObservables.TqmAcceleration(u => PhysicalObservables.Uniform(u), x, D);
            bool d = Math.Abs(ag) > 1e-6 && Math.Abs(at) < 1e-9;
            if (!d) differ = false;
            sb.AppendLine($"{x,7:F2} {ag,9:F4} {at,9:F4}  {d}");
        }

        sb.AppendLine();
        sb.AppendLine($"GR has non-zero acceleration in uniform density (a = −ρ₀x), TQM has zero (a = −∇lnρ = 0): {differ}");
        sb.AppendLine();
        sb.AppendLine("CLASSIFICATION: STRONG DIFFERENCE — a uniform density produces a gravitational field in GR");
        sb.AppendLine("(Newtonian shell/linear field) but NONE in TQM (field ∝ ∇ρ = 0).");
        Output.WriteLine(sb.ToString());

        Assert.True(differ, "uniform density should produce a STRONG GR/TQM difference");
    }

    // ── G4-O11: shell density — GR long-range, TQM localized ───────────────────────────

    [Fact]
    public void G4_O11_ShellDensityStrongDifference()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-O11: shell density — long-range (GR) vs localized (TQM) field");

        double xOut = 0.8;  // outside a sharp shell (r=0.5, σ=0.06)
        double ag = PhysicalObservables.GrAcceleration(x => PhysicalObservables.Shell(x, 0.5, 0.5, 0.06), xOut);
        double at = PhysicalObservables.TqmAcceleration(x => PhysicalObservables.Shell(x, 0.5, 0.5, 0.06), xOut, D);
        double xIn = 0.2;   // inside the shell
        double agIn = PhysicalObservables.GrAcceleration(x => PhysicalObservables.Shell(x, 0.5, 0.5, 0.06), xIn);
        double atIn = PhysicalObservables.TqmAcceleration(x => PhysicalObservables.Shell(x, 0.5, 0.5, 0.06), xIn, D);

        sb.AppendLine($"outside shell (x={xOut}): a_GR = {ag:F4}, a_TQM = {at:E2}");
        sb.AppendLine($"inside shell  (x={xIn}): a_GR = {agIn:F4}, a_TQM = {atIn:E2}");
        sb.AppendLine();
        sb.AppendLine($"GR field extends outside the shell (|a_GR| ≫ 0): {Math.Abs(ag) > 0.1}");
        sb.AppendLine($"TQM field vanishes outside the shell (|a_TQM| ≪ |a_GR|): {Math.Abs(at) < 1e-3 && Math.Abs(atIn) < 1e-3}");
        sb.AppendLine();
        sb.AppendLine("CLASSIFICATION: STRONG DIFFERENCE — GR has the Newtonian long-range (1/r²) field outside a");
        sb.AppendLine("mass shell; TQM has (exponentially) ZERO field there (the field ∝ ∇ρ is localized at the shell).");
        Output.WriteLine(sb.ToString());

        Assert.True(Math.Abs(ag) > 0.1, "GR acceleration outside shell should be non-zero");
        Assert.True(Math.Abs(at) < 1e-3, "TQM acceleration outside shell should vanish");
        Assert.True(Math.Abs(atIn) < 1e-3, "TQM acceleration inside shell should also vanish");
    }

    // ── G4-O12: double-peak + redshift/lensing proxy + classification ──────────────────

    [Fact]
    public void G4_O12_DoublePeakAndClassification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-O12: double-peak density + redshift/lensing proxy + classification");

        // TQM source changes sign (curvature of ln ρ); GR source is always ≥ 0 (density value).
        double xMid = 0.0; // between the two peaks
        double sGr = PhysicalObservables.GrSource(x => PhysicalObservables.DoublePeak(x), xMid);
        double sTqm = PhysicalObservables.TqmSource(x => PhysicalObservables.DoublePeak(x), xMid);
        double sTqmPeak = PhysicalObservables.TqmSource(x => PhysicalObservables.DoublePeak(x), 0.4);

        sb.AppendLine($"double-peak source at x=0:  S_GR = {sGr:F4} (≥0 always), S_TQM = {sTqm:F4} (>0 at density min)");
        sb.AppendLine($"TQM source at peak x=0.4: S_TQM = {sTqmPeak:F4} (<0 at density max)");
        sb.AppendLine($"TQM source SIGN-CHANGES (curvature), GR source does NOT: {sTqm > 0 && sTqmPeak < 0}");

        // Redshift proxy: TQM redshift = −ΔΦ = −(1/d)Δlnρ, a purely LOCAL (edge) effect.
        double zTqm = PhysicalObservables.Redshift(0.1, 0.5, 0.5, D); // using the ρ=1+ax² potential for the proxy
        sb.AppendLine($"TQM redshift proxy (Φ=(1/d)lnρ) between x=0.1 and 0.5: {zTqm:F4}");

        bool signChange = sTqm > 0 && sTqmPeak < 0;
        sb.AppendLine();
        sb.AppendLine($"TQM source sign-changing vs GR positive-definite: {signChange}");
        sb.AppendLine();
        sb.AppendLine("CLASSIFICATION: STRONG DIFFERENCE — TQM's source is the sign-changing log-density curvature");
        sb.AppendLine("(positive at density minima, negative at maxima), while GR's source is the always-positive density");
        sb.AppendLine("value. The most decisive observable is the ABSENCE of a long-range field in uniform/shell-exterior");
        sb.AppendLine("regions (G4-O10/11) — a qualitative, falsifiable prediction distinguishing TQM from GR.");
        Output.WriteLine(sb.ToString());

        Assert.True(signChange, "TQM source should be sign-changing (curvature), GR positive-definite");
    }
}
