using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXH;

/// <summary>
/// G4-RHO Phase 0 — dynamical origin of ρ. Most of the gravity program now follows from ρ; here we ask what
/// determines ρ itself, testing abundance laws, actualization dynamics, conservation principles, attractor
/// behavior, and scale-free solutions — and whether α=0 (the log hierarchy) arises naturally.
///
/// Tests: G4-RHO00 (scale-free continuum), G4-RHO01 (conservation rejects the log), G4-RHO02 (scale-free
///        field + classification).
/// </summary>
public class G4RHO_Phase0_DynamicalOriginTests : ResearchTestBase
{
    public G4RHO_Phase0_DynamicalOriginTests(ITestOutputHelper o) : base(o) { }

    private const int D = 3;

    private static double A(Func<double, double> rho, double r) => RhoDynamics.Acceleration3D(rho, r, D);
    private static double V2(Func<double, double> rho, double r) => RhoDynamics.RotationCurve(rho, r, D);

    // ── G4-RHO00: scale-free solutions form a continuum ──────────────────────────────

    [Fact]
    public void G4_RHO00_ScaleFreeContinuum()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-RHO00: scale-free (self-similar) actualization → a continuum of power laws");

        sb.AppendLine($"{"s",6} {"a(3)",12} {"v²(3)",10} {"v²(9)",10} {"flat",8} {"sign",12}");
        bool allFlat = true;
        bool signFlips = false;
        double aNeg = 0, aPos = 0;
        foreach (double s in new[] { -1.0, -0.5, 0.5, 1.0 })
        {
            double a3 = A(u => RhoDynamics.ScaleFreeDensity(u, s), 3.0);
            double v3 = V2(u => RhoDynamics.ScaleFreeDensity(u, s), 3.0);
            double v9 = V2(u => RhoDynamics.ScaleFreeDensity(u, s), 9.0);
            bool flat = Math.Abs(v3 - v9) < 1e-9;
            if (!flat) allFlat = false;
            if (s < 0) aNeg = a3;
            if (s > 0) aPos = a3;
            sb.AppendLine($"{s,6:F1} {a3,12:F6} {v3,10:F5} {v9,10:F5} {flat,8} {(a3 < 0 ? "attractive" : "repulsive"),12}");
        }
        signFlips = aNeg > 0 && aPos < 0;

        sb.AppendLine();
        sb.AppendLine($"all scale-free densities give a FLAT rotation curve (v² = |s|/d = const): {allFlat}");
        sb.AppendLine($"field sign flips at s=0 (s<0 repulsive, s>0 attractive): {signFlips}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: self-similarity (no preferred scale) yields a CONTINUUM ρ ∝ r^s, every member flat,");
        sb.AppendLine("but with sign set by s. Scale-free-ness alone does NOT select a unique profile.");
        Output.WriteLine(sb.ToString());

        Assert.True(allFlat, "all scale-free densities should give flat rotation curves");
        Assert.True(signFlips, "field sign should flip between s<0 and s>0");
    }

    // ── G4-RHO01: conservation rejects the log, selects a repulsive power law ────────

    [Fact]
    public void G4_RHO01_ConservationRejectsLog()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-RHO01: flux conservation selects a power law and REJECTS the log density");

        // Steady-state actualization: conserved flux F = ρ·v·r^(d−1) = const (v = const, β=0).
        // ρ ∝ r^(−(d−1)) = r^−2 conserves F; the log density ρ = ρ̄ + c·ln r does NOT.
        sb.AppendLine($"{"r",5} {"F(ρ∝r⁻²)",12} {"F(log)",12}");
        double fPow1 = 0, fPow4 = 0, fLog1 = 0, fLog4 = 0;
        foreach (double r in new[] { 1.0, 2.0, 3.0, 4.0 })
        {
            double fp = RhoDynamics.Flux(u => RhoDynamics.ScaleFreeDensity(u, -2.0), r);
            double fl = RhoDynamics.Flux(u => RhoDynamics.LogDensity(u, 0.4), r);
            if (r == 1.0) { fPow1 = fp; fLog1 = fl; }
            if (r == 4.0) { fPow4 = fp; fLog4 = fl; }
            sb.AppendLine($"{r,5:F1} {fp,12:F6} {fl,12:F6}");
        }

        double aPow = A(u => RhoDynamics.ScaleFreeDensity(u, -2.0), 3.0);
        double aLog = A(u => RhoDynamics.LogDensity(u, 0.4), 3.0);

        bool powerLawConserves = Math.Abs(fPow4 - fPow1) < 1e-9;
        bool logNotConserved = fLog4 > 10.0 * fLog1;
        bool powerLawRepulsive = aPow > 0;
        bool logAttractive = aLog < 0;

        sb.AppendLine();
        sb.AppendLine($"ρ ∝ r⁻² conserves the flux (F const): {powerLawConserves}  → a = {aPow:F4} (repulsive)");
        sb.AppendLine($"log density does NOT conserve flux (F grows 24×): {logNotConserved}  → a = {aLog:F4} (attractive)");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the raw actualization flux, if conserved, selects ρ ∝ r⁻² — a REPULSIVE power law.");
        sb.AppendLine("The attractive log-deficit density (α=0) is NOT a steady state of flux conservation.");
        Output.WriteLine(sb.ToString());

        Assert.True(powerLawConserves, "ρ ∝ r⁻² should conserve the flux");
        Assert.True(logNotConserved, "log density should fail flux conservation");
        Assert.True(powerLawRepulsive, "conserved-flux power law should be repulsive");
        Assert.True(logAttractive, "log density should be attractive");
    }

    // ── G4-RHO02: scale-free field + classification ──────────────────────────────────

    [Fact]
    public void G4_RHO02_ScaleFreeFieldClassification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-RHO02: the scale-free field a ∝ 1/r, and the classification of α=0");

        // The flat rotation curve v² = r|a| = const ⟺ a ∝ 1/r ⟺ a(λr) = a(r)/λ (scale-free field). It is
        // satisfied by EVERY power-law density ρ ∝ r^s, so scale-invariance gives flatness but not uniqueness.
        sb.AppendLine($"{"s",6} {"a(3)",12} {"a(9)",12} {"a(9)/a(3)",12}");
        bool allScaleFree = true;
        foreach (double s in new[] { -1.0, 0.5, 1.0 })
        {
            double a3 = A(u => RhoDynamics.ScaleFreeDensity(u, s), 3.0);
            double a9 = A(u => RhoDynamics.ScaleFreeDensity(u, s), 9.0);
            double ratio = a9 / a3;
            if (Math.Abs(ratio - 1.0 / 3.0) > 1e-6) allScaleFree = false;
            sb.AppendLine($"{s,6:F1} {a3,12:F6} {a9,12:F6} {ratio,12:F4}");
        }

        sb.AppendLine();
        sb.AppendLine($"every power-law density gives the scale-free field a ∝ 1/r (a(9)/a(3)=1/3): {allScaleFree}");
        sb.AppendLine();
        sb.AppendLine("CLASSIFICATION: PREFERRED (α=0), NOT DERIVED from dynamics.");
        sb.AppendLine("  • Self-similarity → continuum of power laws (all flat, sign set by s) — no unique profile.");
        sb.AppendLine("  • Conservation → ρ ∝ r⁻² (repulsive) — the WRONG sector; rejects the log.");
        sb.AppendLine("  • The flat, ATTRACTIVE rotation curve (matter) requires the deficit m = ρ̄−ρ, whose unique");
        sb.AppendLine("    scale-invariant form is α=0 (log). This is a SYMMETRY selection, not a dynamical attractor.");
        sb.AppendLine("  • The dynamical origin of ρ — why the actualization takes the deficit (attractive) form rather");
        sb.AppendLine("    than the conserved raw flux (repulsive) — remains OPEN.");
        Output.WriteLine(sb.ToString());

        Assert.True(allScaleFree, "all power-law densities should give the scale-free field a ∝ 1/r");
    }
}
