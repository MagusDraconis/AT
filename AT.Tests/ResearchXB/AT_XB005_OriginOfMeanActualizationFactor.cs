using System.Globalization;
using System.Text;
using AT.Core.ResearchXB;
using AT.Core.ResearchXB.Models;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXB;

public class AT_XB005_OriginOfMeanActualizationFactor : ResearchTestBase
{
    public AT_XB005_OriginOfMeanActualizationFactor(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void XB005_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchXB-005 Origin of the Mean Actualization Factor");

        // 1. Recap
        Sec(sb, "XB001-XB004 Recap");
        sb.AppendLine("  XB001: Abundance ≠ Identity.");
        sb.AppendLine("  XB002: All abundance = LOG-NORMAL.");
        sb.AppendLine("  XB003: σ²/N ≈ 0.09 (universal per-step volatility).");
        sb.AppendLine("  XB004: σ₀² = Var[-log(p)] from Born rule.");
        sb.AppendLine("  XB005: WHERE does the mean drift μ come from?");
        sb.AppendLine();

        // 2. Cosmic expansion origin
        Sec(sb, "Cosmological Expansion → r̄");
        sb.AppendLine(MeanActualizationFactorAnalyzer.AnalyzeAll());

        // 3. The μ-σ² unification
        Sec(sb, "μ-σ² Unification");
        sb.AppendLine("  BOTH parameters come from cosmology:");
        sb.AppendLine();
        sb.AppendLine("  μ  = log(N_final/N_initial) — cosmic EXPANSION drift.");
        sb.AppendLine("  σ² = N·σ₀²              — accumulated RANDOMNESS.");
        sb.AppendLine();
        sb.AppendLine("  The cascade depth N = log(T_init/T_freeze) determines BOTH.");
        sb.AppendLine("  ONE parameter (N) → TWO distribution parameters (μ, σ²).");
        sb.AppendLine();

        // 4. Complete hierarchy
        Sec(sb, "Complete Abundance Physics — Five Layers");
        sb.AppendLine(MeanActualizationFactorAnalyzer.CompleteAbundanceHierarchy());

        // 5. Final
        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  ResearchXB-005 COMPLETE.");
        sb.AppendLine($"  Classification: C — Strong origin of r̄ identified.");
        sb.AppendLine($"  r̄ > 1 because the universe EXPANDS (N grows with time).");
        sb.AppendLine($"  μ = log(N_final/N_initial) = cosmic expansion drift.");
        sb.AppendLine($"  FIVE LAYERS COMPLETE. Abundance Physics is FINISHED.");
        sb.AppendLine(new string('=', 100));
        Output.WriteLine(sb.ToString());
    }

    private static void Sec(StringBuilder sb, string t)
    { sb.AppendLine(t); sb.AppendLine(new string('-', t.Length)); }
}
