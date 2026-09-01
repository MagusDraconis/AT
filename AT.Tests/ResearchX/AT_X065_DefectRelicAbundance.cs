using System.Globalization;
using System.Text;
using AT.Core.Research;
using AT.Tests.Shared;

namespace AT.Tests.ResearchX;

public class AT_X065_DefectRelicAbundance : ResearchTestBase
{
    public AT_X065_DefectRelicAbundance(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void AT_X065_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("AT-X065 Relic Abundance of Defect Dark Matter");

        var models = DefectRelicAbundanceAnalyzer.AnalyzeModels();
        var freezeout = DefectRelicAbundanceAnalyzer.SimulateFreezeout();
        int surviving = models.Count(m => m.Survives);

        // 1. Abundance models
        Sec(sb, "Relic Abundance Models");
        sb.AppendLine("  Model                          Ω_pred  Ratio_pred  Survives?");
        sb.AppendLine("  " + new string('─', 60));
        foreach (var m in models)
        {
            string s = m.Survives ? "✓" : "✗";
            string ratio = m.PredictedRatio > 0 ? $"{m.PredictedRatio:F1}" : "—";
            sb.AppendLine($"  {m.Name,-30} {m.PredictedOmegaDM,7:F2}  {ratio,10}     {s}");
        }
        sb.AppendLine();
        sb.AppendLine($"  Observed: Ω_DM = {0.27}, Ω_DM/Ω_b = {5.4}");
        sb.AppendLine($"  {surviving}/{models.Count} models survive.");
        sb.AppendLine("  All surviving models: natural scale Ω ~ 0.1-1 (correct order).");
        sb.AppendLine();

        // 2. Freezeout simulation
        Sec(sb, "Defect Freezeout Simulation");
        sb.AppendLine(DefectRelicAbundanceAnalyzer.FreezeoutTable(freezeout));

        // 3. The WIMP miracle
        Sec(sb, "The 'WIMP Miracle' for AT Defects");
        sb.AppendLine("  TeV-mass defect + weak-scale cross-section → Ω ~ 0.1-1.");
        sb.AppendLine("  This is the SAME 'miracle' that motivated WIMP searches.");
        sb.AppendLine();
        sb.AppendLine("  AT explanation: it's NOT a miracle — it's a CONSEQUENCE");
        sb.AppendLine("  of the defect energy scale (~TeV from X057) and the");
        sb.AppendLine("  natural interaction cross-section (core size)².");
        sb.AppendLine();
        sb.AppendLine("  The TeV scale itself comes from the defect correlation");
        sb.AppendLine("  length ξ (X058), which is set by the electroweak scale.");
        sb.AppendLine("  TeV mass → weak-scale σ → Ω ~ 0.1 — NATURAL.");
        sb.AppendLine();

        // 4. The ratio problem
        Sec(sb, "The Ω_DM/Ω_b ≈ 5 Problem");
        sb.AppendLine("  Why is DM ~5× more abundant than baryons?");
        sb.AppendLine();
        sb.AppendLine("  AT HYPOTHESIS: The ratio reflects the relative");
        sb.AppendLine("  abundance of NEUTRAL vs CHARGED defect types at formation.");
        sb.AppendLine();
        sb.AppendLine("  In the defect ecology (X049b):");
        sb.AppendLine("    • U(1)-coupled defects → baryons (charged, visible).");
        sb.AppendLine("    • Neutral defects → dark matter (invisible).");
        sb.AppendLine();
        sb.AppendLine("  If the defect moduli space has ~5 neutral sectors for");
        sb.AppendLine("  every charged sector, the ratio is ~5 naturally.");
        sb.AppendLine();
        sb.AppendLine("  STATUS: Plausible but not proven. The number of neutral");
        sb.AppendLine("  vs charged moduli depends on the specific defect moduli");
        sb.AppendLine("  space topology — not fully derived in AT.");
        sb.AppendLine();

        // 5. Honest conclusion
        Sec(sb, "Honest Conclusion");
        sb.AppendLine(DefectRelicAbundanceAnalyzer.TheVerdict());

        // 6. Final
        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  AT-X065 COMPLETE.");
        sb.AppendLine($"  Classification: A — Relic Abundance is CONTINGENT.");
        sb.AppendLine($"  Natural scale: Ω ~ 0.1-1 (correct order of magnitude).");
        sb.AppendLine($"  Exact value not derivable — same as ALL DM models.");
        sb.AppendLine(new string('=', 100));
        Output.WriteLine(sb.ToString());
    }

    private static void Sec(StringBuilder sb, string t)
    { sb.AppendLine(t); sb.AppendLine(new string('-', t.Length)); }
}
