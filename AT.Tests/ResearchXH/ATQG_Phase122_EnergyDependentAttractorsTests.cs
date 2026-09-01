using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 122 — Energy dependence of attractor classes. QG89 derived energy = actualization rate
/// (Q-event activity); QG117 showed the parameter plane maps to a discrete ladder of attractor classes.
/// This phase asks: can HIGHER actualization-energy regimes generate NEW attractor classes not accessible in
/// the current parameter range? Investigates energy scaling, actualization-rate regimes, attractor phase
/// transitions, family-count evolution, and high-energy classes. Classify: NO EFFECT / PARTIAL EFFECT /
/// NEW CLASSES.
///
/// Tests: ATQG1220 (energy scaling + actualization-rate regimes), ATQG1221 (phase transitions +
/// family-count evolution), ATQG1222 (high-energy classes + classification).
/// </summary>
public class ATQG_Phase122_EnergyDependentAttractorsTests : ResearchTestBase
{
    public ATQG_Phase122_EnergyDependentAttractorsTests(ITestOutputHelper o) : base(o) { }

    // ── ATQG1220: energy scaling + actualization-rate regimes ─────────────────────

    [Fact]
    public void ATQG1220_EnergyScalingAndRateRegimes()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1220: seed energy scaling and activity-ceiling (rate) regimes");

        var scaling = EnergyDependentAttractors.RadiusVsEnergyScale();
        bool responds = EnergyDependentAttractors.RadiusRespondsToEnergyScale();

        sb.AppendLine("RADIUS VS SEED ENERGY SCALE (baseline ceiling 1.0):");
        foreach (var (e, r) in scaling)
            sb.AppendLine($"  E={e:F2}: radius={r:F2}");
        sb.AppendLine($"  radius responds to energy scale: {responds}");
        sb.AppendLine();
        sb.AppendLine("RADIUS LADDER BY ENERGY CEILING (actualization-rate regime):");
        foreach (var (c, radii) in EnergyDependentAttractors.LadderByCeiling())
            sb.AppendLine($"  ceiling={c:F1}: [{string.Join(", ", radii.Select(r => r.ToString("F2")))}]");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: raising the seed energy scale grows the attractor radius (0 → 22 as E goes");
        sb.AppendLine("0.25 → 8), and raising the activity ceiling — the energy regime — extends the radius");
        sb.AppendLine("ladder from {2, 6} (baseline) to radii as large as 19.67 (ceiling 4+). Energy controls");
        sb.AppendLine("which attractor classes are accessible.");
        Output.WriteLine(sb.ToString());

        Assert.True(responds, "attractor radius responds to seed energy scale");
        // ladder extends well beyond the baseline cap (K = 6) at high ceilings
        double baselineMax = EnergyDependentAttractors.BaselineMaxRadius();
        double highMax = EnergyDependentAttractors.MaxRadiusAtCeiling(4.0);
        Assert.True(highMax > baselineMax + 0.5, "high-ceiling ladder extends beyond the baseline cap");
    }

    // ── ATQG1221: phase transitions + family-count evolution ─────────────────────

    [Fact]
    public void ATQG1221_PhaseTransitionsAndFamilyEvolution()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1221: attractor phase transitions and family-count evolution with energy");

        bool classesGrow = EnergyDependentAttractors.SpectralClassesGrowWithEnergy();

        sb.AppendLine("ATTRACTOR PHASE TRANSITIONS (distinct spectral classes vs energy ceiling):");
        foreach (double c in EnergyDependentAttractors.EnergyCeilings)
            sb.AppendLine($"  ceiling={c:F1}: {EnergyDependentAttractors.SpectralClassCount(c)} spectral classes");
        sb.AppendLine($"  class count grows with energy: {classesGrow}");
        sb.AppendLine();
        sb.AppendLine("FAMILY-COUNT EVOLUTION (f=0.7, d=0.3):");
        foreach (var (c, fams, span, r) in EnergyDependentAttractors.FamilyEvolution())
            sb.AppendLine($"  ceiling={c:F1}: radius={r:F2}, families={fams}, span={span:F2}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the number of accessible attractor phases GROWS sharply with the energy regime");
        sb.AppendLine("(2 classes at ceiling 1 → 8 at ceiling 4). The octave-family count COMPRESSES at high");
        sb.AppendLine("energy (3 → 2 families; span 6.40 → 2.98) — higher energy merges family structure while");
        sb.AppendLine("opening new geometry classes.");
        Output.WriteLine(sb.ToString());

        Assert.True(classesGrow, "spectral class count grows with energy ceiling");
        // family structure compresses at high energy (span shrinks)
        var evolution = EnergyDependentAttractors.FamilyEvolution();
        Assert.True(evolution[^1].Span < evolution[0].Span, "hierarchy span compresses at high energy");
    }

    // ── ATQG1222: high-energy classes + classification ───────────────────────────

    [Fact]
    public void ATQG1222_HighEnergyClassesAndClassification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1222: high-energy classes → NO EFFECT / PARTIAL EFFECT / NEW CLASSES");

        bool newClasses = EnergyDependentAttractors.HighEnergyClassesExist();
        double baselineMax = EnergyDependentAttractors.BaselineMaxRadius();
        double highMax = EnergyDependentAttractors.MaxRadiusAtCeiling(4.0);
        string cls = EnergyDependentAttractors.Classify();

        sb.AppendLine("HIGH-ENERGY CLASSES:");
        sb.AppendLine($"  baseline max radius (ceiling 1.0): {baselineMax:F2}");
        sb.AppendLine($"  high-energy max radius (ceiling 4.0): {highMax:F2}");
        sb.AppendLine($"  classes exist beyond the baseline range: {newClasses}");
        sb.AppendLine();
        sb.AppendLine($"CLASSIFICATION: {cls}");
        sb.AppendLine();
        sb.AppendLine("  • NOT NO EFFECT: energy strongly controls the attractor (radius 0→22 with seed energy;");
        sb.AppendLine("    spectral class count 2→8 across ceilings).");
        sb.AppendLine("  • NEW CLASSES: higher actualization-energy regimes OPEN attractor classes unreachable");
        sb.AppendLine("    in the baseline regime — the radius ladder extends to 19.67 (vs the K=6 cap), so");
        sb.AppendLine("    classes above the current range genuinely exist. (Family count compresses 3→2, so");
        sb.AppendLine("    new geometry classes come with MERGED family structure.)");
        Output.WriteLine(sb.ToString());

        Assert.Equal("NEW CLASSES", cls);
        Assert.True(newClasses, "classes exist beyond the baseline parameter range");
        Assert.True(highMax > baselineMax + 0.5, "high-energy ladder extends beyond the baseline cap");
    }
}
