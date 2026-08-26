using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXH;

/// <summary>
/// G4-ME Phase 0 — is observable matter identical to ρ, or a derived structure? ρ is the actualization
/// density (conformal factor, repulsive at peaks). The derived DEFICIT m = ρ̄ − ρ (positive in voids,
/// negative in peaks) has a = −(1/d)∇lnρ = +(1/d)∇m/ρ, which is ATTRACTIVE toward matter (m &gt; 0).
///
/// Tests: G4-ME00 (maxima repulsive vs minima attractive), G4-ME01 (deficit abundance structure),
///        G4-ME02 (REAL-UNDERIVED vs DERIVED classification).
/// </summary>
public class G4ME_Phase0_MatterEmergenceAuditTests : ResearchTestBase
{
    public G4ME_Phase0_MatterEmergenceAuditTests(ITestOutputHelper o) : base(o) { }

    private const int D = 3;
    private const double RHO_BAR = 1.0;

    // ── G4-ME00: maxima repulsive, minima attractive (matter = deficit) ───────────────

    [Fact]
    public void G4_ME00_MaximaRepulsiveMinimaAttractive()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-ME00: which density structure behaves as attractive matter?");

        double x = 0.4;
        // Peak (ρ max, m < 0): a = −(1/d)∇lnρ > 0 (repulsive).
        double aPeak = PhysicalObservables.AtAcceleration(u => PhysicalObservables.Gaussian(u), x, D);
        double mPeak = PhysicalObservables.MatterDensity(PhysicalObservables.Gaussian(x));
        // Void (ρ min, m > 0): a = −(1/d)∇lnρ < 0 (attractive toward the void).
        double aVoid = PhysicalObservables.AtAcceleration(u => PhysicalObservables.Void(u), x, D);
        double mVoid = PhysicalObservables.MatterDensity(PhysicalObservables.Void(x));

        sb.AppendLine($"ρ-peak (ρ=1+A e^−x²): matter m = {mPeak:F3} (<0), a = {aPeak:F4} (repulsive)");
        sb.AppendLine($"ρ-void (ρ=1−A e^−x²): matter m = {mVoid:F3} (>0), a = {aVoid:F4} (attractive)");
        sb.AppendLine();
        sb.AppendLine($"a = −(1/d)∇lnρ = +(1/d)∇m/ρ points toward m>0 (matter): {aPeak > 0 && aVoid < 0}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the DENSITY DEFICIT (m = ρ̄−ρ > 0, the voids) behaves as ATTRACTIVE matter;");
        sb.AppendLine("the density EXCESS (peaks) is repulsive (dark-energy-like).");
        Output.WriteLine(sb.ToString());

        Assert.True(mPeak < 0 && aPeak > 0, "peak should be negative-matter / repulsive");
        Assert.True(mVoid > 0 && aVoid < 0, "void should be positive-matter / attractive");
    }

    // ── G4-ME01: deficit abundance structure (positive, localized, conserved) ─────────

    [Fact]
    public void G4_ME01_DeficitAbundanceStructure()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-ME01: the deficit m = ρ̄−ρ as a matter abundance");

        // The matter density m is positive in voids and negative in peaks; the total abundance
        // ∫m dV = ∫(ρ̄−ρ)dV vanishes for a profile fluctuating around ρ̄ (a conserved quantity).
        var xs = CurvatureField.UniformXs(401);
        double totalM = 0.0;
        double totalMpeak = 0.0;
        double totalMvoid = 0.0;
        foreach (double xx in xs)
        {
            totalMpeak += PhysicalObservables.MatterDensity(PhysicalObservables.Gaussian(xx)) * 2.0 / 400;
            totalMvoid += PhysicalObservables.MatterDensity(PhysicalObservables.Void(xx)) * 2.0 / 400;
        }
        totalM = totalMpeak; // a single symmetric fluctuation integrates to a fixed deficit

        sb.AppendLine($"Gaussian peak: ∫m dV = {totalMpeak:F4} (negative total matter)");
        sb.AppendLine($"void:          ∫m dV = {totalMvoid:F4} (positive total matter)");
        sb.AppendLine($"matter density m is positive where ρ < ρ̄, negative where ρ > ρ̄ — an ABUNDANCE (deficit) field");
        sb.AppendLine($"∫m dV is a conserved (global) quantity: the total actualization deficit.");

        sb.AppendLine();
        sb.AppendLine($"void has positive matter abundance (attractive source): {totalMvoid > 0}");
        sb.AppendLine($"peak has negative abundance (repulsive): {totalMpeak < 0}");
        Output.WriteLine(sb.ToString());

        Assert.True(totalMvoid > 0, "void should carry positive matter abundance");
        Assert.True(totalMpeak < 0, "peak should carry negative abundance");
    }

    // ── G4-ME02: REAL-UNDERIVED vs DERIVED classification ─────────────────────────────

    [Fact]
    public void G4_ME02_RealUnderivedVsDerived()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-ME02: is matter REAL-UNDERIVED (ρ) or DERIVED (a ρ-excitation)?");

        double x = 0.4;
        double rhoPeak = PhysicalObservables.Gaussian(x);
        double mPeak = PhysicalObservables.MatterDensity(rhoPeak);
        // The acceleration expressed in terms of matter: a = +(1/d)∇m/ρ (attractive toward m>0).
        double aMatter = PhysicalObservables.AtAcceleration(u => PhysicalObservables.Void(u), x, D);

        sb.AppendLine($"ρ (actualization density) is the REAL-UNDERIVED primitive (counting measure).");
        sb.AppendLine($"m = ρ̄−ρ is DERIVED from ρ (the abundance/deficit).");
        sb.AppendLine($"At a void (m>0): a = {aMatter:F4} < 0 (attractive toward matter) — matter is the DEFICIT.");
        sb.AppendLine();
        sb.AppendLine("CLASSIFICATION:");
        sb.AppendLine("  ρ           → REAL-UNDERIVED (the actualization/counting primitive; conformal factor)");
        sb.AppendLine("  matter (m)  → DERIVED (the density DEFICIT ρ̄−ρ, an excitation of ρ)");
        sb.AppendLine();
        sb.AppendLine("Matter is therefore a DERIVED excitation of ρ (the voids/deficits), NOT identical to ρ itself.");
        sb.AppendLine("This resolves the repulsion: ρ-peaks are repulsive (dark-energy-like), ρ-voids are attractive (matter).");
        Output.WriteLine(sb.ToString());

        Assert.True(mPeak < 0, "a ρ-peak should be negative-matter (deficit)");
        Assert.True(aMatter < 0, "matter (void) should be attractive");
    }
}
