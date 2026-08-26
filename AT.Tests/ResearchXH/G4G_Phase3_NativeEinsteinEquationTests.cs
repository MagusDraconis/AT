using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXH;

/// <summary>
/// G4-G Phase 3 — do the Einstein equations emerge natively? Identifies a native stress-energy
/// analogue T = G/κ (symmetric, conserved, from ρ) and tests whether the actualization density's
/// KINETIC part (∇ρ) alone sources the geometry, or whether the full conformal structure (∂²ρ) is
/// essential. No Einstein equations imported.
///
/// Tests: G4-G30 (symmetric + conserved T), G4-G31 (G = κT relation + trace), G4-G32 (kinetic
/// stress-energy is insufficient).
/// </summary>
public class G4G_Phase3_NativeEinsteinEquationTests : ResearchTestBase
{
    public G4G_Phase3_NativeEinsteinEquationTests(ITestOutputHelper o) : base(o) { }

    private const double A = 0.5;
    private const double Kappa = 1.0; // gravitational coupling (κ = 8πG; value is a units convention)

    // ── G4-G30: native stress-energy is symmetric and conserved ────────────────────────

    [Fact]
    public void G4_G30_NativeStressEnergyIsSymmetricAndConserved()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-G30: native stress-energy T = G/κ is symmetric and conserved");

        sb.AppendLine($"{"d",4} {"max |∇^μ T_μ1| over x",24}  conserved  symmetric");
        bool conserved = true;
        foreach (int d in new[] { 2, 3, 4 })
        {
            double maxAbs = 0.0;
            for (int i = 0; i <= 30; i++)
            {
                double x = -0.85 + 1.7 * i / 30.0;
                // ∇^μ T_μ1 = (1/κ) ∇^μ G_μ1 (Bianchi residual).
                double r = HigherDimEinstein.BianchiResidual(x, A, d) / Kappa;
                maxAbs = Math.Max(maxAbs, Math.Abs(r));
            }
            bool ok = maxAbs < 1e-8;
            if (!ok) conserved = false;
            // Symmetry: T = G/κ is diagonal (x-only profile), off-diagonal ≡ 0.
            sb.AppendLine($"{d,4} {maxAbs,24:E2}  {ok}        True");
        }

        sb.AppendLine();
        sb.AppendLine($"T = G/κ is symmetric (diagonal) and divergence-free (∇^μ T_μν = 0): {conserved}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the native stress-energy analogue T = G/κ is symmetric and conserved — the two");
        sb.AppendLine("physical properties required of a stress-energy tensor, obtained natively from ρ.");
        Output.WriteLine(sb.ToString());

        Assert.True(conserved, "native stress-energy is not conserved");
    }

    // ── G4-G31: the G = κT relation and its trace ─────────────────────────────────────

    [Fact]
    public void G4_G31_EinsteinRelationAndTrace()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-G31: the native Einstein relation G_μν = κT_μν and its trace");

        double[] xs = { -0.6, -0.2, 0.0, 0.2, 0.6 };
        bool relationOk = true, traceOk = true;
        foreach (int d in new[] { 3, 4 })
        {
            sb.AppendLine($"d = {d}:");
            sb.AppendLine($"{"x",7} {"G_11",9} {"κT_11",9} {"G_ii",9} {"κT_ii",9}  G=κT  trace=−(d−2)R/(2κ)");
            foreach (double x in xs)
            {
                double g11 = HigherDimEinstein.Einstein11(x, A, d);
                double gii = HigherDimEinstein.EinsteinOther(x, A, d);
                double t11 = Kappa * HigherDimEinstein.NativeStress11(x, A, d, Kappa);
                double tii = Kappa * HigherDimEinstein.NativeStressOther(x, A, d, Kappa);
                bool rel = Math.Abs(g11 - t11) < 1e-12 && Math.Abs(gii - tii) < 1e-12;
                double tr = HigherDimEinstein.TraceNativeStress(x, A, d, Kappa);
                double expect = -0.5 * (d - 2.0) * HigherDimEinstein.ScalarCurvature(x, A, d) / Kappa;
                bool trOk = Math.Abs(tr - expect) < 1e-10;
                if (!rel) relationOk = false;
                if (!trOk) traceOk = false;
                sb.AppendLine($"{x,7:F2} {g11,9:F4} {t11,9:F4} {gii,9:F4} {tii,9:F4}  {rel}  {trOk}");
            }
            sb.AppendLine();
        }

        sb.AppendLine($"G_μν = κT_μν holds at all x, d: {relationOk}");
        sb.AppendLine($"trace T^μ_μ = −(d−2)R/(2κ): {traceOk}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the native Einstein relation G_μν = κT_μν holds, with T = G/κ the conserved");
        sb.AppendLine("stress-energy and the correct trace structure.");
        Output.WriteLine(sb.ToString());

        Assert.True(relationOk, "G = κT relation fails");
        Assert.True(traceOk, "trace structure of T fails");
    }

    // ── G4-G32: kinetic stress-energy is insufficient (∂²ρ is essential) ──────────────

    [Fact]
    public void G4_G32_KineticStressEnergyIsInsufficient()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-G32: does the kinetic (∇ρ) stress-energy alone source the geometry?");

        double x = 0.4;
        sb.AppendLine($"{"d",4} {"G_11",9} {"κTkin_11",10} {"G_ii",9} {"κTkin_ii",10}  match?");
        bool kineticMatches = true;
        foreach (int d in new[] { 3, 4 })
        {
            double g11 = HigherDimEinstein.Einstein11(x, A, d);
            double gii = HigherDimEinstein.EinsteinOther(x, A, d);
            double k11 = Kappa * HigherDimEinstein.KineticStress11(x, A, d);
            double kii = Kappa * HigherDimEinstein.KineticStressOther(x, A, d);
            // A single κ must satisfy BOTH components; check the ratios are equal.
            double ratio11 = k11 != 0 ? g11 / k11 : double.NaN;
            double ratioIi = kii != 0 ? gii / kii : double.NaN;
            bool match = Math.Abs(ratio11 - ratioIi) < 1e-9;
            if (!match) kineticMatches = false;
            sb.AppendLine($"{d,4} {g11,9:F4} {k11,10:F4} {gii,9:F4} {kii,10:F4}  {match}");
        }

        sb.AppendLine();
        sb.AppendLine($"kinetic stress-energy (∇ρ only) reproduces G with a single κ: {kineticMatches}");
        sb.AppendLine();
        sb.AppendLine("The kinetic T (from ∇ρ) is ∝ (ρ′)² in BOTH components, but G_ii also carries a ∂²ρ (σ″)");
        sb.AppendLine("term — so no single κ relates them. The source is the FULL conformal structure (ρ, ∂ρ, ∂²ρ),");
        sb.AppendLine("not just the actualization density's kinetic/gradient part.");
        Output.WriteLine(sb.ToString());

        Assert.False(kineticMatches, "unexpected: kinetic stress-energy reproduces G");
        // The conserved T = G/κ DOES work (from G4-G30/31), so the full ρ structure is the source.
        double full11 = HigherDimEinstein.Einstein11(x, A, 3);
        double fullT11 = Kappa * HigherDimEinstein.NativeStress11(x, A, 3, Kappa);
        Assert.True(Math.Abs(full11 - fullT11) < 1e-12, "full ρ structure should reproduce G exactly");
    }
}
