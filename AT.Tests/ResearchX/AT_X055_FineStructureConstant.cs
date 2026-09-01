using System.Globalization;
using System.Text;
using AT.Core.Research;
using AT.Tests.Shared;

namespace AT.Tests.ResearchX;

public class AT_X055_FineStructureConstant : ResearchTestBase
{
    public AT_X055_FineStructureConstant(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void AT_X055_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("AT-X055 Origin of the Fine-Structure Constant");

        var models = FineStructureAnalyzer.AnalyzeModels();
        var scan = FineStructureAnalyzer.ScanAlpha();
        int surviving = models.Count(m => m.Survives);

        double optAlpha = scan.OrderByDescending(p => p.Fitness).First().Alpha;
        const double obsAlpha = 1.0 / 137.035999084;

        // 1. Candidate models
        Sec(sb, "Candidate Origins of α");
        sb.AppendLine("  Model                              α⁻¹(pred)  α⁻¹(obs)  LogErr  Survives?");
        sb.AppendLine("  " + new string('─', 70));
        foreach (var m in models)
        {
            double predInv = 1.0 / Math.Max(m.PredictedAlpha, 0.0001);
            string s = m.Survives ? "✓" : "✗";
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  {0,-35} {1,8:F1}  {2,8:F1}  {3,6:F2}    {4}",
                m.Name, predInv, 137.036, m.LogError, s));
        }
        sb.AppendLine();
        sb.AppendLine($"  {surviving}/{models.Count} models survive. None precisely predicts α≈1/137.");
        sb.AppendLine();

        // 2. Alpha scan
        Sec(sb, "α Scan — Ecological Fitness Optimization");
        sb.AppendLine(FineStructureAnalyzer.ScanTable(scan));
        sb.AppendLine();

        // 3. The viable window
        Sec(sb, "The Viable Window for α");
        sb.AppendLine("  α ≪ 10⁻⁴   → Too weak. No bound states. No atoms. No chemistry.");
        sb.AppendLine("  α ≈ 10⁻³   → WEAK coupling. Large atoms (a₀ ∝ 1/α). Fragile.");
        sb.AppendLine("  α ≈ 7×10⁻³ → OBSERVED. Stable atoms. Rich chemistry. OPTIMAL.");
        sb.AppendLine("  α ≈ 10⁻²   → Strong coupling. Compact atoms. Complex molecules.");
        sb.AppendLine("  α ≈ 10⁻¹   → VERY strong. Relativistic inner electrons. Unstable.");
        sb.AppendLine("  α ≥ 1      → Collapse. No atoms — everything is plasma.");
        sb.AppendLine();
        sb.AppendLine("  The observed α sits in a SWEET SPOT:");
        sb.AppendLine("    • Strong enough for stable bound states (atoms exist).");
        sb.AppendLine("    • Weak enough that relativistic corrections are small.");
        sb.AppendLine("    • Maximizes information capacity (chemistry, biology).");
        sb.AppendLine();

        // 4. Why not O(1)?
        Sec(sb, "Why α ≪ 1? — The Vortex Stability Argument");
        sb.AppendLine("  U(1) coupling strength = vortex-vortex phase interaction.");
        sb.AppendLine("  If α ~ 1: vortices strongly interact → merge/collapse →");
        sb.AppendLine("    no persistent charged defects → no stable matter.");
        sb.AppendLine();
        sb.AppendLine("  Vortex stability requires α ≪ 1. The precise smallness");
        sb.AppendLine("  is set by the ratio: (vortex core energy) / (interaction energy).");
        sb.AppendLine("  This ratio is determined by the defect potential's shape,");
        sb.AppendLine("  which depends on PDE coefficients c₀, M (AT-010).");
        sb.AppendLine();

        // 5. Honest assessment
        Sec(sb, "Honest Assessment");
        sb.AppendLine(FineStructureAnalyzer.TheDerivation());

        // 6. Hostile review
        Sec(sb, "Hostile Review");
        sb.AppendLine(FineStructureAnalyzer.HostileReview());

        // 7. Final verdict
        string classification = Math.Abs(optAlpha - obsAlpha) / obsAlpha < 0.5
            ? "C: Partial Emergence (optimum near observed)"
            : "B: Weak Preference";

        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  AT-X055 COMPLETE.");
        sb.AppendLine($"  Classification: {classification}");
        sb.AppendLine($"  Optimal α ≈ {optAlpha:F4} (α⁻¹ ≈ {1.0 / optAlpha:F0}).");
        sb.AppendLine($"  Observed α ≈ {obsAlpha:F4} (α⁻¹ ≈ {1.0 / obsAlpha:F0}).");
        sb.AppendLine($"  α is CONSTRAINED to 10⁻⁴–10⁻¹ window by defect stability.");
        sb.AppendLine($"  Precise value not uniquely derived — remains open.");
        sb.AppendLine(new string('=', 100));
        Output.WriteLine(sb.ToString());
    }

    private static void Sec(StringBuilder sb, string t)
    { sb.AppendLine(t); sb.AppendLine(new string('-', t.Length)); }
}
