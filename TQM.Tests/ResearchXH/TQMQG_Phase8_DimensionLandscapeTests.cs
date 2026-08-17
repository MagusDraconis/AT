using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-QG Phase 8 — the dimension landscape. Profiles d=1..20 across eight native criteria and classifies each
/// dimension as FORBIDDEN / ALLOWED / PREFERRED, producing a phase space of dimensions.
///
/// Tests: TQMQG80 (phase-space table), TQMQG81 (viability categories), TQMQG82 (landscape summary).
/// </summary>
public class TQMQG_Phase8_DimensionLandscapeTests : ResearchTestBase
{
    public TQMQG_Phase8_DimensionLandscapeTests(ITestOutputHelper o) : base(o) { }

    // ── TQMQG80: the phase-space table ──────────────────────────────────────────────

    [Fact]
    public void TQMQG80_PhaseSpaceTable()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG80: the dimension phase space (d=1..20)");

        sb.AppendLine($"{"d",4} {"richness",9} {"graviton",9} {"Weyl",9} {"gravity 1/d",11} {"rot v²",8} {"frozen",8} {"class",11}");
        for (int d = 1; d <= 20; d++)
        {
            var p = DimensionLandscape.Profile(d);
            sb.AppendLine($"{d,4} {p.richness,9:F0} {p.graviton,9:F0} {p.weyl,9:F0} {p.deficitGravity,11:F3} {p.rotationCurve,8:F3} {p.frozenFraction,8:F3} {DimensionLandscape.Classify(d),11}");
        }

        // Classification checks: d≤2 FORBIDDEN, d=3,4 PREFERRED, d≥5 ALLOWED.
        bool forbidden = DimensionLandscape.Classify(1) == "FORBIDDEN" && DimensionLandscape.Classify(2) == "FORBIDDEN";
        bool preferred = DimensionLandscape.Classify(3) == "PREFERRED" && DimensionLandscape.Classify(4) == "PREFERRED";
        bool allowed = DimensionLandscape.Classify(5) == "ALLOWED" && DimensionLandscape.Classify(20) == "ALLOWED";

        sb.AppendLine();
        sb.AppendLine($"FORBIDDEN (d≤2): {forbidden};  PREFERRED (d=3,4): {preferred};  ALLOWED (d≥5): {allowed}");
        Output.WriteLine(sb.ToString());

        Assert.True(forbidden, "d=1,2 should be FORBIDDEN");
        Assert.True(preferred, "d=3,4 should be PREFERRED");
        Assert.True(allowed, "d≥5 should be ALLOWED");
    }

    // ── TQMQG81: viability categories (pathological / efficient / minimal-dynamical) ─

    [Fact]
    public void TQMQG81_ViabilityCategories()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG81: viability categories across the landscape");

        // Pathological: d≤2 has no gravity (Einstein degenerate).
        bool pathological = !DimensionLandscape.HasGravity(1) && !DimensionLandscape.HasGravity(2);

        // Efficient (conformal-complete): d=3 has Weyl=0, frozen fraction=0.
        var p3 = DimensionLandscape.Profile(3);
        bool efficient = DimensionLandscape.ConformalComplete(3) && p3.frozenFraction == 0.0 && p3.graviton == 0.0;

        // Minimal dynamical: d=4 has the fewest non-zero graviton modes (2).
        var p4 = DimensionLandscape.Profile(4);
        bool minimalDynamical = p4.graviton == 2.0 && p4.graviton < DimensionLandscape.Profile(5).graviton;

        // Inefficient (frozen): d≥5 has frozen fraction > 0.9 by d=20 (most metric d.o.f. frozen).
        var p5 = DimensionLandscape.Profile(5);
        var p20 = DimensionLandscape.Profile(20);
        bool inefficient = p5.frozenFraction > p4.frozenFraction && p20.frozenFraction > 0.9;

        // Deficit gravity and rotation curves are defined for all d≥3 (gravity 1/d > 0, rotation |s|/d > 0).
        bool deficitDefined = p3.deficitGravity > 0.0 && p20.deficitGravity > 0.0;

        sb.AppendLine($"pathological (d≤2, no gravity): {pathological}");
        sb.AppendLine($"efficient (d=3, conformal-complete, frozen=0): {efficient}");
        sb.AppendLine($"minimal-dynamical (d=4, 2 graviton modes): {minimalDynamical}");
        sb.AppendLine($"inefficient (d≥5, frozen fraction → 1): {inefficient}");
        sb.AppendLine($"deficit gravity + rotation defined for all d≥3: {deficitDefined}");

        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the landscape separates into pathological (d≤2), efficient (d=3),");
        sb.AppendLine("minimal-dynamical (d=4), and increasingly inefficient (d≥5) dimensions.");
        Output.WriteLine(sb.ToString());

        Assert.True(pathological, "d≤2 should be pathological (no gravity)");
        Assert.True(efficient, "d=3 should be conformal-complete/efficient");
        Assert.True(minimalDynamical, "d=4 should be minimal dynamical");
        Assert.True(inefficient && deficitDefined, "d≥5 should be inefficient; gravity defined");
    }

    // ── TQMQG82: landscape summary ───────────────────────────────────────────────────

    [Fact]
    public void TQMQG82_LandscapeSummary()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG82: the dimension landscape — summary");

        int forbidden = 0, preferred = 0, allowed = 0;
        for (int d = 1; d <= 20; d++)
        {
            string c = DimensionLandscape.Classify(d);
            if (c == "FORBIDDEN") forbidden++;
            else if (c == "PREFERRED") preferred++;
            else allowed++;
        }

        sb.AppendLine($"dimensions d=1..20:");
        sb.AppendLine($"  FORBIDDEN  (d≤2, no gravity):            {forbidden}");
        sb.AppendLine($"  PREFERRED  (d=3 conformal-complete, d=4 minimal dynamical): {preferred}");
        sb.AppendLine($"  ALLOWED    (d≥5, frozen fraction grows): {allowed}");
        sb.AppendLine();
        sb.AppendLine("LANDSCAPE CONCLUSION:");
        sb.AppendLine("  • d=1,2: FORBIDDEN — Einstein tensor identically zero (no gravity).");
        sb.AppendLine("  • d=3:   PREFERRED — first non-trivial gravity AND conformal-complete (Weyl=0, nothing frozen);");
        sb.AppendLine("           the unique dimension where TQM's conformally-flat scalar gravity is COMPLETE.");
        sb.AppendLine("  • d=4:   PREFERRED — minimal PROPAGATING gravity (2 graviton polarizations); the unique dimension");
        sb.AppendLine("           where gravity has the fewest non-zero wave modes.");
        sb.AppendLine("  • d≥5:   ALLOWED — gravity exists but the conformal-flatness assumption freezes an ever-growing");
        sb.AppendLine("           fraction of the metric (frozen fraction → 1), making them increasingly 'inefficient'.");
        sb.AppendLine();
        sb.AppendLine("The phase space has a unique efficient point (d=3) and a unique minimal-dynamical point (d=4),");
        sb.AppendLine("with all d≥5 viable-but-inefficient and d≤2 forbidden.");
        Output.WriteLine(sb.ToString());

        Assert.True(forbidden == 2 && preferred == 2 && allowed == 16,
            "d=1..20 should give 2 forbidden, 2 preferred, 16 allowed");
    }
}
