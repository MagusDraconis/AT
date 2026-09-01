using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 118 — Families from attractor geometries. QG117 showed dynamic parameters produce discrete
/// geometry classes. This phase asks: can particle-FAMILY structure emerge from the different attractor
/// geometry classes? Investigates geometry-class count, family analogs (octave families within each class),
/// class transitions, hierarchy generation, and class stability. Classify: NO RELATION / PARTIAL RELATION /
/// FAMILY ORIGIN.
///
/// Tests: ATQG1180 (geometry-class count + family analogs), ATQG1181 (class transitions + hierarchy
/// generation), ATQG1182 (stability of classes + classification).
/// </summary>
public class ATQG_Phase118_FamiliesFromAttractorsTests : ResearchTestBase
{
    public ATQG_Phase118_FamiliesFromAttractorsTests(ITestOutputHelper o) : base(o) { }

    // ── ATQG1180: geometry-class count + family analogs ───────────────────────────

    [Fact]
    public void ATQG1180_GeometryClassCountAndFamilyAnalogs()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1180: geometry-class count and family analogs within each class");

        var profiles = FamiliesFromAttractors.ClassProfiles();
        var byK = FamiliesFromAttractors.ClassCountsByK();
        int[] distinctFams = FamiliesFromAttractors.DistinctFamilyCounts();
        bool has3 = FamiliesFromAttractors.HasThreeFamilyClass();

        sb.AppendLine("GEOMETRY CLASSES (K=6, parameter plane):");
        foreach (var (r, f, s) in profiles)
            sb.AppendLine($"  radius {r:F1}: families={f}, span={s:F2}");
        sb.AppendLine();
        sb.AppendLine("CLASS COUNTS ACROSS K:");
        foreach (var (k, c, fams) in byK)
            sb.AppendLine($"  K={k}: classes={c}, family counts=[{string.Join(",", fams)}]");
        sb.AppendLine();
        sb.AppendLine($"  distinct family counts (K=6): [{string.Join(",", distinctFams)}]");
        sb.AppendLine($"  a three-family geometry class exists (K=6): {has3}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the parameter plane yields a DISCRETE set of geometry classes (2 for every K),");
        sb.AppendLine("and the classes carry DISTINCT internal family content (4 vs 3 octave families at K=6).");
        sb.AppendLine("A three-family class — the SM count — is realized for K=5 and K=6.");
        Output.WriteLine(sb.ToString());

        Assert.True(profiles.Length >= 2, "multiple distinct geometry classes exist");
        Assert.True(distinctFams.Length >= 2, "geometry classes carry distinct family counts");
        Assert.True(has3, "a three-family geometry class exists (SM family analog)");
    }

    // ── ATQG1181: class transitions + hierarchy generation ───────────────────────

    [Fact]
    public void ATQG1181_ClassTransitionsAndHierarchyGeneration()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1181: class transitions and internal hierarchy generation");

        double sensitivity = FamiliesFromAttractors.MaxAdjacentClassSensitivity();
        var spans = FamiliesFromAttractors.ClassSpans();

        sb.AppendLine("CLASS TRANSITIONS:");
        sb.AppendLine($"  max adjacent-point spectral sensitivity: {sensitivity:F4}");
        sb.AppendLine();
        sb.AppendLine("HIERARCHY GENERATION (distinct hierarchy depth per class):");
        foreach (var (r, s) in spans)
            sb.AppendLine($"  radius {r:F1}: hierarchy span {s:F2}");
        sb.AppendLine();
        foreach (var (r, f, s) in FamiliesFromAttractors.ClassProfiles())
        {
            var rep = FamiliesFromAttractors.RepresentativePoint(r);
            double stab = FamiliesFromAttractors.LowModeRatioStabilityAcrossSize(rep.Value.Feedback, rep.Value.Damping);
            double[] ratios = FamiliesFromAttractors.ClassSuccessiveRatios(rep.Value.Feedback, rep.Value.Damping);
            sb.AppendLine($"  radius {r:F1}: low-mode ratios [{string.Join(", ", ratios.Take(4).Select(x => x.ToString("F2")))}] "
                + $"(size-stability {stab:F4})");
        }
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: classes are sharply separated (sensitivity 0.62) and EACH class generates its");
        sb.AppendLine("own hierarchy (distinct spans, size-stable low-mode ladders) — discrete classes with");
        sb.AppendLine("internal mass-like structure.");
        Output.WriteLine(sb.ToString());

        Assert.True(sensitivity > 0.3, "geometry classes are sharply separated");
        Assert.True(spans.Select(x => x.Span).Distinct().Count() >= 2, "classes carry distinct hierarchy depths");
    }

    // ── ATQG1182: stability of classes + classification ──────────────────────────

    [Fact]
    public void ATQG1182_ClassStabilityAndClassification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1182: class stability → NO RELATION / PARTIAL RELATION / FAMILY ORIGIN");

        bool pertStable = FamiliesFromAttractors.FamilyCountsStableUnderPerturbation(0.1);
        bool sizeStable = FamiliesFromAttractors.FamilyCountsStableAcrossSize();
        string cls = FamiliesFromAttractors.Classify();
        double interClass = FamiliesFromAttractors.InterClassRatioDeviation();

        sb.AppendLine("CLASS STABILITY:");
        sb.AppendLine($"  family counts stable under 10% link-removal perturbation: {pertStable}");
        sb.AppendLine($"  family counts stable across N=48/96/192: {sizeStable}");
        sb.AppendLine($"  inter-class low-mode ratio deviation: {interClass:F4}");
        sb.AppendLine();
        sb.AppendLine($"CLASSIFICATION: {cls}");
        sb.AppendLine();
        sb.AppendLine("  • NOT NO RELATION: geometry classes DO carry distinct, perturbation-robust family");
        sb.AppendLine("    content (4 vs 3 octave families at K=6; three-family class at K=5,6).");
        sb.AppendLine("  • NOT FAMILY ORIGIN: the octave family count is NOT a size-invariant property — it");
        sb.AppendLine("    grows with the network (3→4→5 as N=48→96→192), so the discrete family number is not");
        sb.AppendLine("    a fixed emergent constant of the class.");
        sb.AppendLine("  • PARTIAL RELATION: distinct stable geometry classes with class-dependent family");
        sb.AppendLine("    structure partially emerge, but a size-independent discrete family spectrum is not");
        sb.AppendLine("    achieved.");
        Output.WriteLine(sb.ToString());

        Assert.Equal("PARTIAL RELATION", cls);
        Assert.True(pertStable, "family counts are robust under perturbation");
        Assert.False(sizeStable, "family counts are NOT size-invariant (grow with N)");
    }
}
