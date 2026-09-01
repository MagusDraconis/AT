using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 248 — Final SM Dynamics Closure Audit. Re-check the ten SM-dynamics components after
/// QG242-247 and determine whether Standard-Model dynamics is now complete. Audit only.
/// </summary>
public class ATQG_Phase248_FinalSmDynamicsClosureAuditTests : ResearchTestBase
{
    public ATQG_Phase248_FinalSmDynamicsClosureAuditTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG2480_TenComponents()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2480: the ten SM-dynamics components");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - Reviews QG242/243/244/246/247 (the SM-dynamics derivation arc).");
        sb.AppendLine("  - Each component is classified DERIVED / PARTIAL / BOUNDARY / OPEN / HOSTED.");
        sb.AppendLine();

        sb.AppendLine("THE TEN COMPONENTS:");
        foreach (var c in FinalSmDynamicsClosureAudit.Components())
        {
            sb.AppendLine($"  {c.Name}: {c.Status}");
            sb.AppendLine($"      {c.Evidence}");
        }
        sb.AppendLine();
        sb.AppendLine($"By status: {string.Join(", ", FinalSmDynamicsClosureAudit.StatusCounts().Select(kv => $"{kv.Key}={kv.Value}"))}");

        Output.WriteLine(sb.ToString());

        Assert.Equal(10, FinalSmDynamicsClosureAudit.Components().Length);
        var sc = FinalSmDynamicsClosureAudit.StatusCounts();
        Assert.Equal(8, sc[FinalSmDynamicsClosureAudit.Status.Derived]);
        Assert.Equal(1, sc[FinalSmDynamicsClosureAudit.Status.Partial]);
        Assert.Equal(1, sc[FinalSmDynamicsClosureAudit.Status.Boundary]);
        Assert.Equal(0, sc[FinalSmDynamicsClosureAudit.Status.Open]);
        Assert.Equal(0, sc[FinalSmDynamicsClosureAudit.Status.Hosted]);
    }

    [Fact]
    public void ATQG2481_RemainingItems()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2481: the exact remaining (non-derived) items");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - The remaining items are the propagator machinery (framework-completeness) and the");
        sb.AppendLine("    SU(3) color-count identification (QG79 postulate trace).");
        sb.AppendLine();

        sb.AppendLine("REMAINING ITEMS:");
        foreach (var m in FinalSmDynamicsClosureAudit.RemainingItems())
            sb.AppendLine($"  • {m}");

        Output.WriteLine(sb.ToString());

        Assert.Equal(2, FinalSmDynamicsClosureAudit.RemainingItems().Length);
        Assert.Contains("Propagators", string.Join(" ", FinalSmDynamicsClosureAudit.RemainingItems()));
        Assert.Contains("SU(3)", string.Join(" ", FinalSmDynamicsClosureAudit.RemainingItems()));
    }

    [Fact]
    public void ATQG2482_Summary()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2482: summary — SM DYNAMICS COMPLETE");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - SM dynamics is complete iff no component is OPEN or HOSTED and at most one is");
        sb.AppendLine("    PARTIAL (a documented framework-completeness item).");
        sb.AppendLine();

        sb.AppendLine($"SUMMARY: {FinalSmDynamicsClosureAudit.Summary()}");
        sb.AppendLine($"SM dynamics complete? {FinalSmDynamicsClosureAudit.SmDynamicsComplete()}");
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - Gauge dynamics (symmetry, equations, Lagrangian, vertices) DERIVED (QG243/244);");
        sb.AppendLine("    the Higgs sector (field, potential, SSB) DERIVED (QG246);");
        sb.AppendLine("  - the Yukawa sector (interaction, mass mechanism) DERIVED (QG247);");
        sb.AppendLine("  - the only PARTIAL is the propagator machinery (framework-completeness, not a");
        sb.AppendLine("    physics gap); the only BOUNDARY is the SU(3) color-count (QG79 postulate trace).");
        sb.AppendLine("  - No OPEN and no HOSTED component remains — the QG242-245 gap list is closed.");

        Output.WriteLine(sb.ToString());

        Assert.True(FinalSmDynamicsClosureAudit.SmDynamicsComplete(), "SM dynamics must be complete (no OPEN/HOSTED)");
        Assert.Contains("SM DYNAMICS COMPLETE", FinalSmDynamicsClosureAudit.Summary());
    }
}
