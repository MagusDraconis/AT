using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 42 — final TRM decomposition. Classifies every TRM component and computes the percentage derived
/// from AT. Categories: DERIVED / PARTIAL / IMPORTED / NEW PRIMITIVE.
///
/// Tests: ATQG420 (classification table), ATQG421 (percentage), ATQG422 (decomposition summary).
/// </summary>
public class ATQG_Phase42_FinalTRMAuditTests : ResearchTestBase
{
    public ATQG_Phase42_FinalTRMAuditTests(ITestOutputHelper o) : base(o) { }

    // ── ATQG420: classification table ────────────────────────────────────────────────

    [Fact]
    public void ATQG420_ClassificationTable()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG420: classify every TRM component");

        int derived = 0, partial = 0, imported = 0, primitive = 0;
        foreach (var c in FinalTRMAudit.Components)
        {
            string cls = FinalTRMAudit.Classify(c);
            sb.AppendLine($"{c,-22} -> {cls}");
            switch (cls)
            {
                case "DERIVED": derived++; break;
                case "PARTIAL": partial++; break;
                case "IMPORTED": imported++; break;
                case "NEW PRIMITIVE": primitive++; break;
            }
        }

        sb.AppendLine();
        sb.AppendLine($"DERIVED       : {derived}");
        sb.AppendLine($"PARTIAL       : {partial}");
        sb.AppendLine($"IMPORTED      : {imported}");
        sb.AppendLine($"NEW PRIMITIVE : {primitive}");
        Output.WriteLine(sb.ToString());

        Assert.Equal(2, derived);
        Assert.Equal(1, partial);
        Assert.Equal(2, imported);
        Assert.Equal(1, primitive);
    }

    // ── ATQG421: percentage derived ───────────────────────────────────────────────────

    [Fact]
    public void ATQG421_PercentageDerived()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG421: what percentage of TRM is derived from AT?");

        int full = FinalTRMAudit.FullyDerivedCount();
        int partial = FinalTRMAudit.PartialCount();
        double pct = FinalTRMAudit.DerivedPercentage();

        sb.AppendLine($"fully DERIVED components: {full}/6");
        sb.AppendLine($"PARTIAL components:       {partial}/6");
        sb.AppendLine($"derived score (DERIVED + 0.5·PARTIAL): {pct:F2}%");
        sb.AppendLine();
        sb.AppendLine($"fully DERIVED fraction: {full / 6.0 * 100.0:F2}%");
        sb.AppendLine($"DERIVED + PARTIAL fraction: {(full + partial) / 6.0 * 100.0:F2}%");
        Output.WriteLine(sb.ToString());

        Assert.Equal(2, full);
        Assert.Equal(1, partial);
        Assert.Equal(41.666666666666664, pct, 3);
    }

    // ── ATQG422: decomposition summary ────────────────────────────────────────────────

    [Fact]
    public void ATQG422_DecompositionSummary()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG422: the final TRM decomposition");

        sb.AppendLine("WHAT IS DERIVED FROM AT:");
        sb.AppendLine("  • saturation core (Poisson Q-event counting)  → DERIVED");
        sb.AppendLine("  • redshift (g_00 = −ρ^(2/d))                  → DERIVED");
        sb.AppendLine("  • Schwarzschild recovery                       → PARTIAL (scalar g_00 yes; γ=+1 needs ψ)");
        sb.AppendLine();
        sb.AppendLine("WHAT IS NOT DERIVED:");
        sb.AppendLine("  • rotation-curve term √(g_N·a0)               → IMPORTED (MOND ansatz)");
        sb.AppendLine("  • temporal propagation (n = e^Φ)              → IMPORTED (refractive medium)");
        sb.AppendLine("  • ψ sector (spin-2)                           → NEW PRIMITIVE");
        sb.AppendLine();
        sb.AppendLine("SUMMARY: 2/6 TRM components are fully DERIVED; 1 is PARTIAL; the remaining 3 need an imported rule or the");
        sb.AppendLine("new ψ primitive. The scalar (saturation + redshift) core is AT-derived; the lensing/GW/rotation-curve");
        sb.AppendLine("phenomenology is not.");
        Output.WriteLine(sb.ToString());

        Assert.Equal("DERIVED", FinalTRMAudit.Classify("saturation-core"));
        Assert.Equal("DERIVED", FinalTRMAudit.Classify("redshift"));
        Assert.Equal("PARTIAL", FinalTRMAudit.Classify("schwarzschild-recovery"));
        Assert.Equal("NEW PRIMITIVE", FinalTRMAudit.Classify("psi-sector"));
    }
}
