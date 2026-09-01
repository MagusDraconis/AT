using System.Globalization;
using System.Text;
using AT.Core.ResearchXD;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXD;

public class AT_XD002_PredictionRiskMatrix : ResearchTestBase
{
    public AT_XD002_PredictionRiskMatrix(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void XD002_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchXD-002 Prediction Risk Matrix");

        var predictions = PredictionRiskAnalyzer.ScorePredictions();

        // 1. Risk matrix
        Sec(sb, "Prediction Risk Matrix");
        sb.AppendLine(PredictionRiskAnalyzer.RiskMatrix(predictions));

        // 2. The kill shot
        Sec(sb, "The Single Critical Kill Shot");
        sb.AppendLine(PredictionRiskAnalyzer.TheKillShot());

        // 3. Risk summary
        Sec(sb, "Risk Summary — What Dies If Each Prediction Fails");
        sb.AppendLine("  Prediction falsified     AT sections killed");
        sb.AppendLine("  " + new string('-', 55));
        sb.AppendLine("  w(z) = -1                X046, X062, Λ emergence, XB entire");
        sb.AppendLine("  a₀ ≠ f(H₀)               X063, correlation gravity chain");
        sb.AppendLine("  Inverted ν ordering       X060 (Model A), neutrino physics");
        sb.AppendLine("  WIMP DM detected          X064, defect DM identity");
        sb.AppendLine("  α constant (no log-norm)  XB002-005, abundance law");
        sb.AppendLine("  M² ≠ ⟨k⟩                 XC002-005, final parameter link");
        sb.AppendLine();
        sb.AppendLine("  AT is MODULAR: killing one prediction doesn't kill all.");
        sb.AppendLine("  The strongest kill: w(z) = -1 (Λ emergence + abundance).");
        sb.AppendLine("  A weaker kill: inverted ν ordering (only neutrino sector).");
        sb.AppendLine();

        // 4. Final
        string classification = "D: Clear Experimental Kill-Shot Identified (w(z) via Euclid)";

        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  ResearchXD-002 COMPLETE.");
        sb.AppendLine($"  Classification: {classification}");
        sb.AppendLine($"  #1 KILL SHOT: w(z) measurement via Euclid (by 2030).");
        sb.AppendLine($"  Falsifying this KILLS the Λ emergence chain.");
        sb.AppendLine($"  AT is falsifiable, predictive, zero-parameter.");
        sb.AppendLine(new string('=', 100));
        Output.WriteLine(sb.ToString());
    }

    private static void Sec(StringBuilder sb, string t)
    { sb.AppendLine(t); sb.AppendLine(new string('-', t.Length)); }
}
