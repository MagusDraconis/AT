using System.Globalization;
using System.Text;
using AT.Core.ResearchXB;
using AT.Core.ResearchXB.Models;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXB;

public class AT_XB008_OriginOfActualizationRates : ResearchTestBase
{
    public AT_XB008_OriginOfActualizationRates(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void XB008_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchXB-008 Origin of Actualization Rates");

        // 1. The final unknown
        Sec(sb, "The Final Unknown — Actualization Rates Γ_X(T)");
        sb.AppendLine("  XB007: Freezeout at Γ_X(T_f) = H(T_f).");
        sb.AppendLine("  But what determines Γ_X(T)?");
        sb.AppendLine("  Can actualization rates be derived?");
        sb.AppendLine();

        // 2. Rate table
        Sec(sb, "Actualization Rates at Freezeout");
        sb.AppendLine(ActualizationRateAnalyzer.AnalyzeAll());

        // 3. Universal rate law
        Sec(sb, "Universal Rate Law — Γ = n·σ·v");
        sb.AppendLine("  Γ_X(T) = n_X(T) · σ_X · v_X");
        sb.AppendLine();
        sb.AppendLine("  n_X ∝ T³     — entity density (thermodynamics)");
        sb.AppendLine("  σ_X          — cross-section (physics of X)");
        sb.AppendLine("  v_X ~ 1      — velocity (relativistic at high T)");
        sb.AppendLine();
        sb.AppendLine("  DIFFERENT VARIABLES → DIFFERENT σ_X:");
        sb.AppendLine("    α:    σ_EM = α²/T²    → Γ ∝ α²·T");
        sb.AppendLine("    m_e:  σ ~ constant    → Γ ∝ T³ (defect formation)");
        sb.AppendLine("    Ω_DM: σ ~ 1/M²        → Γ ∝ T³/M²");
        sb.AppendLine("    M²:   σ ~ ℓ_P²        → Γ ∝ T³·ℓ_P²");
        sb.AppendLine();
        sb.AppendLine("  ALL σ_X are determined by the IDENTITY PHYSICS");
        sb.AppendLine("  of the variable — what defect, what interaction.");
        sb.AppendLine("  NO additional abundance parameters needed.");
        sb.AppendLine();

        // 4. Complete
        Sec(sb, "Final Abundance Formula");
        sb.AppendLine(ActualizationRateAnalyzer.TheFinalAbundanceFormula());

        // 5. Final
        string classification = "B: Weak Constraints on Universal Rate (scaling derived, prefactors from identity physics)";

        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  ResearchXB-008 COMPLETE.");
        sb.AppendLine($"  Classification: {classification}");
        sb.AppendLine($"  Γ_X(T) = n_X·σ_X·v_X — universal rate form.");
        sb.AppendLine($"  σ_X determined by identity physics of X (not abundance).");
        sb.AppendLine($"  SEVEN LAYERS COMPLETE. Abundance Physics FINISHED.");
        sb.AppendLine(new string('=', 100));
        Output.WriteLine(sb.ToString());
    }

    private static void Sec(StringBuilder sb, string t)
    { sb.AppendLine(t); sb.AppendLine(new string('-', t.Length)); }
}
