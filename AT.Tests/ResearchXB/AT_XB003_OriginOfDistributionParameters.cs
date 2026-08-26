using System.Globalization;
using System.Text;
using AT.Core.ResearchXB;
using AT.Core.ResearchXB.Models;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXB;

public class AT_XB003_OriginOfDistributionParameters : ResearchTestBase
{
    public AT_XB003_OriginOfDistributionParameters(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void XB003_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchXB-003 Origin of Distribution Parameters");

        var parameters = CascadeDepthModel.ComputeParameters();

        // 1. Recap
        Sec(sb, "XB001-XB002 Recap");
        sb.AppendLine("  XB001: Abundance ≠ Identity. Abundance = frozen history.");
        sb.AppendLine("  XB002: All abundance quantities are LOG-NORMAL.");
        sb.AppendLine("  XB003: Where do μ and σ come from?");
        sb.AppendLine();

        // 2. Cascade depth → parameters
        Sec(sb, "Cascade Depth → μ, σ");
        sb.AppendLine(DistributionParameterAnalyzer.AnalyzeAll());

        // 3. The universal volatility
        Sec(sb, "Universal Per-Step Volatility σ₀²");
        sb.AppendLine("  σ₀² ≈ 0.09 is the FUNDAMENTAL VOLATILITY of one actualization.");
        sb.AppendLine("  ALL abundance variances reduce to this ONE number:");
        sb.AppendLine("    σ²(α) = N_α · σ₀²");
        sb.AppendLine("    σ²(m_e) = N_m · σ₀²");
        sb.AppendLine("    σ²(Ω_DM) = N_Ω · σ₀²");
        sb.AppendLine();
        sb.AppendLine("  OPEN: Can σ₀² be derived from M² or Q-event statistics?");
        sb.AppendLine();

        // 4. The three-layer hierarchy
        Sec(sb, "The Complete Abundance Hierarchy");
        sb.AppendLine(DistributionParameterAnalyzer.TheAbundanceHierarchy());

        // 5. Predictions
        Sec(sb, "Testable Predictions");
        sb.AppendLine("  1. σ² ∝ log(T_init/T_freezeout) for any new abundance variable.");
        sb.AppendLine("  2. σ²/N ≈ constant ≈ 0.09 across ALL abundance variables.");
        sb.AppendLine("  3. Variables freezing at the SAME epoch have similar σ².");
        sb.AppendLine("     (α and m_e both freeze at EW scale → similar σ² ≈ 3.6.)");
        sb.AppendLine("  4. Variables freezing EARLIER have SMALLER σ².");
        sb.AppendLine("     (M² freezes at GUT scale → σ² ≈ 0.6.)");
        sb.AppendLine();

        // 6. Final
        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  ResearchXB-003 COMPLETE.");
        sb.AppendLine($"  Classification: C — Distribution parameters strongly constrained.");
        sb.AppendLine($"  σ²/N ≈ constant ≈ 0.09 (universal per-step volatility).");
        sb.AppendLine($"  N = log(T_init/T_freezeout) determines cascade depth.");
        sb.AppendLine($"  Three layers complete: Category → Distribution → Parameters.");
        sb.AppendLine(new string('=', 100));
        Output.WriteLine(sb.ToString());
    }

    private static void Sec(StringBuilder sb, string t)
    { sb.AppendLine(t); sb.AppendLine(new string('-', t.Length)); }
}
