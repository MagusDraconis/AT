using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 8 — the dimension landscape. Profiles d=1..20 across eight native criteria and classifies each
/// dimension as FORBIDDEN / ALLOWED / PREFERRED, producing a phase space of dimensions.
///
/// Tests: ATQG80 (phase-space table), ATQG81 (viability categories), ATQG82 (landscape summary).
/// </summary>
public class ATQG_Phase8_DimensionLandscapeTests : ResearchTestBase
{
    public ATQG_Phase8_DimensionLandscapeTests(ITestOutputHelper o) : base(o) { }

    // ── ATQG80: the phase-space table ──────────────────────────────────────────────

    [Fact]
    public void ATQG80_PhaseSpaceTable()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG80: the dimension phase space (d=1..20)");

        sb.AppendLine($"{"d",4} {"richness",9} {"graviton",9} {"Weyl",9} {"gravity 1/d",11} {"rot v²",8} {"frozen",8} {"class",11}");
        for (int d = 1; d <= 20; d++)
        {
            var p = DimensionLandscape.Profile(d);
            sb.AppendLine($"{d,4} {p.richness,9:F0} {p.graviton,9:F0} {p.weyl,9:F0} {p.deficitGravity,11:F3} {p.rotationCurve,8:F3} {p.frozenFraction,8:F3} {DimensionLandscape.Classify(d),11}");
        }

        // Classification checks: d≤2 FORBIDDEN, d=3 PREFERRED, d≥4 ALLOWED.
        bool forbidden = DimensionLandscape.Classify(1) == "FORBIDDEN" && DimensionLandscape.Classify(2) == "FORBIDDEN";
        bool preferred = DimensionLandscape.Classify(3) == "PREFERRED" && DimensionLandscape.Classify(4) != "PREFERRED";
        bool allowed = DimensionLandscape.Classify(4) == "ALLOWED" && DimensionLandscape.Classify(20) == "ALLOWED";

        sb.AppendLine();
        sb.AppendLine($"FORBIDDEN (d≤2): {forbidden};  PREFERRED (d=3 only): {preferred};  ALLOWED (d≥4): {allowed}");
        Output.WriteLine(sb.ToString());

        Assert.True(forbidden, "d=1,2 should be FORBIDDEN");
        Assert.True(preferred, "only d=3 should be PREFERRED");
        Assert.True(allowed, "d≥4 should be ALLOWED");
    }

    // ── ATQG81: viability categories (pathological / efficient / minimal-dynamical) ─

    [Fact]
    public void ATQG81_ViabilityCategories()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG81: viability categories across the landscape");

        // Pathological: d≤2 has no gravity (Einstein degenerate).
        bool pathological = !DimensionLandscape.HasGravity(1) && !DimensionLandscape.HasGravity(2);

        // Conformal-complete (Weyl=0): d=2 (D=3) — but it is FORBIDDEN (no gravity).
        var p2 = DimensionLandscape.Profile(2);
        bool conformalCompleteForbidden = DimensionLandscape.ConformalComplete(2) && p2.frozenFraction == 0.0 && p2.graviton == 0.0 && !DimensionLandscape.HasGravity(2);

        // Minimal dynamical: d=3 (3+1) has the fewest non-zero graviton modes (2).
        var p3 = DimensionLandscape.Profile(3);
        bool minimalDynamical = p3.graviton == 2.0 && p3.graviton < DimensionLandscape.Profile(4).graviton;

        // Inefficient (frozen): d≥4 has frozen fraction > 0.9 by d=20 (most metric d.o.f. frozen).
        var p4 = DimensionLandscape.Profile(4);
        var p20 = DimensionLandscape.Profile(20);
        bool inefficient = p4.frozenFraction > p3.frozenFraction && p20.frozenFraction > 0.9;

        // Deficit gravity and rotation curves are defined for all d≥3 (gravity 1/d > 0, rotation |s|/d > 0).
        bool deficitDefined = p3.deficitGravity > 0.0 && p20.deficitGravity > 0.0;

        sb.AppendLine($"pathological (d≤2, no gravity): {pathological}");
        sb.AppendLine($"conformal-complete d=2 but FORBIDDEN (no gravity): {conformalCompleteForbidden}");
        sb.AppendLine($"minimal-dynamical (d=3, 2 graviton modes): {minimalDynamical}");
        sb.AppendLine($"inefficient (d≥4, frozen fraction → 1): {inefficient}");
        sb.AppendLine($"deficit gravity + rotation defined for all d≥3: {deficitDefined}");

        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the landscape separates into pathological (d≤2), the conformal-complete-but-forbidden");
        sb.AppendLine("d=2, the minimal-dynamical d=3 (3+1), and increasingly inefficient (d≥4) dimensions.");
        Output.WriteLine(sb.ToString());

        Assert.True(pathological, "d≤2 should be pathological (no gravity)");
        Assert.True(conformalCompleteForbidden, "d=2 should be conformal-complete but forbidden");
        Assert.True(minimalDynamical, "d=3 should be minimal dynamical");
        Assert.True(inefficient && deficitDefined, "d≥4 should be inefficient; gravity defined");
    }

    // ── ATQG82: landscape summary ───────────────────────────────────────────────────

    [Fact]
    public void ATQG82_LandscapeSummary()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG82: the dimension landscape — summary");

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
        sb.AppendLine($"  PREFERRED  (d=3, 3+1 minimal dynamical): {preferred}");
        sb.AppendLine($"  ALLOWED    (d≥4, frozen fraction grows): {allowed}");
        sb.AppendLine();
        sb.AppendLine("LANDSCAPE CONCLUSION:");
        sb.AppendLine("  • d=1,2: FORBIDDEN — Einstein tensor identically zero (no gravity).");
        sb.AppendLine("  • d=3:   PREFERRED — first non-trivial gravity AND minimal PROPAGATING gravity (2 graviton");
        sb.AppendLine("           polarizations, 10 Weyl components) — the unique 3+1 minimal dynamical gravity.");
        sb.AppendLine("  • d≥4:   ALLOWED — gravity exists but the conformal-flatness assumption freezes an ever-growing");
        sb.AppendLine("           fraction of the metric (frozen fraction → 1), making them increasingly 'inefficient'.");
        sb.AppendLine();
        sb.AppendLine("The phase space has a unique minimal-dynamical point (d=3 = 3+1), with all d≥4 viable-but-inefficient");
        sb.AppendLine("and d≤2 forbidden (the conformal-complete d=2 is forbidden by the absence of gravity).");
        Output.WriteLine(sb.ToString());

        Assert.True(forbidden == 2 && preferred == 1 && allowed == 17,
            "d=1..20 should give 2 forbidden, 1 preferred, 17 allowed");
    }
}
