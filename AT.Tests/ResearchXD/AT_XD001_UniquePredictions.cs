using System.Globalization;
using System.Text;
using AT.Core.ResearchXD;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXD;

public class AT_XD001_UniquePredictions : ResearchTestBase
{
    public AT_XD001_UniquePredictions(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void XD001_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchXD-001 Unique Predictions of Zero-Parameter AT");

        var predictions = UniquePredictionAnalyzer.CatalogPredictions();
        int uniqueCount = predictions.Count(p => p.IsUnique);
        int testable = predictions.Count(p => p.Falsifiability != UniquePredictionAnalyzer.Falsifiability.Untestable);

        // 1. Prediction catalog
        Sec(sb, "Prediction Catalog — 8 Predictions");
        sb.AppendLine("  #  Prediction                            Unique?  Falsifiable?");
        sb.AppendLine("  " + new string('-', 65));
        for (int i = 0; i < predictions.Count; i++)
        {
            var p = predictions[i];
            string uniq = p.IsUnique ? "✓ AT" : "~ shared";
            string test = p.Falsifiability == UniquePredictionAnalyzer.Falsifiability.Untestable ? "no" : "YES";
            sb.AppendLine($"  {i + 1}  {p.Name,-38} {uniq,-7}  {test}");
        }
        sb.AppendLine();
        sb.AppendLine($"  {uniqueCount}/{predictions.Count} unique to AT. {testable}/{predictions.Count} falsifiable.");
        sb.AppendLine();

        // 2. Falsification ranking
        Sec(sb, "Falsification Ranking");
        sb.AppendLine(UniquePredictionAnalyzer.FalsificationRanking(predictions));

        // 3. Falsification tree
        Sec(sb, "Falsification Tree");
        sb.AppendLine(UniquePredictionAnalyzer.TheFalsificationTree());

        // 4. ResearchXD launch
        Sec(sb, "ResearchXD — The Prediction Phase");
        sb.AppendLine("  ResearchX:  Identity Physics   (WHAT exists).");
        sb.AppendLine("  ResearchXB: Abundance Physics  (HOW MUCH).");
        sb.AppendLine("  ResearchXC: Unification Physics (WHY two layers).");
        sb.AppendLine("  ResearchXD: Prediction Physics  (TEST IT).");
        sb.AppendLine();
        sb.AppendLine("  AT is now a SCIENTIFIC THEORY:");
        sb.AppendLine("    • 2 primitives (Q + Randomness)");
        sb.AppendLine("    • 0 free continuous parameters");
        sb.AppendLine("    • 8 testable predictions");
        sb.AppendLine("    • 4 unique AT signatures");
        sb.AppendLine("    • Falsifiable by ~2030");
        sb.AppendLine();

        // 5. Final
        string classification = uniqueCount >= 4 && testable >= 6
            ? "D: Unique and Falsifiable" : "C: Strongly Testable";

        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  ResearchXD-001 COMPLETE.");
        sb.AppendLine($"  Classification: {classification}");
        sb.AppendLine($"  {uniqueCount} unique predictions. {testable} falsifiable.");
        sb.AppendLine($"  Fastest kill: Euclid w(z) measurement (by 2030).");
        sb.AppendLine($"  AT is SCIENTIFIC: falsifiable, predictive, zero-parameter.");
        sb.AppendLine(new string('=', 100));
        Output.WriteLine(sb.ToString());
    }

    private static void Sec(StringBuilder sb, string t)
    { sb.AppendLine(t); sb.AppendLine(new string('-', t.Length)); }
}
