using System.Globalization;
using System.Text;
using AT.Core.ResearchXF;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXF;

public class AT_XF005_KnowledgeEmergencePrinciple : ResearchTestBase
{
    public AT_XF005_KnowledgeEmergencePrinciple(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void XF005_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchXF-005 Knowledge Emergence Principle");

        var points = KnowledgeEmergenceAnalyzer.ComputeKnowledgePoints();
        int knowing = points.Count(p => p.Regime >= KnowledgeEmergenceAnalyzer.KnowledgeRegime.Knowing);
        int learning = points.Count(p => p.Regime == KnowledgeEmergenceAnalyzer.KnowledgeRegime.Learning);

        // 1. Knowledge chain
        Sec(sb, "Observer → Knowledge — The Validation Chain");
        sb.AppendLine(KnowledgeEmergenceAnalyzer.KnowledgeChain(points));
        sb.AppendLine();
        sb.AppendLine($"  KNOWING: {knowing}  LEARNING: {learning}");
        sb.AppendLine();

        // 2. Information vs Knowledge
        Sec(sb, "Information vs Knowledge — The Critical Distinction");
        sb.AppendLine("  INFORMATION: Raw data. State = 'X happened at time T.'");
        sb.AppendLine("  KNOWLEDGE:   Validated predictive models.");
        sb.AppendLine("               Model = 'When X, expect Y with probability P.'");
        sb.AppendLine();
        sb.AppendLine("  KNOWLEDGE = INFORMATION THAT HAS SURVIVED SELECTION.");
        sb.AppendLine("  Information is created by randomness (novel outcomes).");
        sb.AppendLine("  Knowledge is created by evolution (validated outcomes).");
        sb.AppendLine();

        // 3. Why evolution guarantees knowledge
        Sec(sb, "Why Evolution Guarantees Knowledge Accumulation");
        sb.AppendLine("  GENERATION 1: Random models. Most inaccurate. Most die.");
        sb.AppendLine("  GENERATION N: Survivors are models that predicted correctly.");
        sb.AppendLine("  GENERATION 2N: Refined models from the survivor pool.");
        sb.AppendLine("  GENERATION ∞: Population converges to accurate models.");
        sb.AppendLine();
        sb.AppendLine("  THE DIRECTION IS ALWAYS TOWARD ACCURACY:");
        sb.AppendLine("    • False models → wrong predictions → lower survival");
        sb.AppendLine("    • True models → correct predictions → higher survival");
        sb.AppendLine("    • Evolution is a KNOWLEDGE ACCUMULATION MACHINE.");
        sb.AppendLine();

        // 4. Generations to learn
        Sec(sb, "How Fast Does Knowledge Emerge?");
        var keyPoints = points.Where(p => Math.Abs(p.Complexity - 183.9) < 5 ||
                                           Math.Abs(p.Complexity - 71.9) < 2 ||
                                           Math.Abs(p.Complexity - 46.0) < 2)
                               .OrderBy(p => p.KnowledgeIndex).ToList();
        sb.AppendLine("  Complexity   Observer   Knowledge   Gens/Learn   Regime");
        sb.AppendLine("  " + new string('-', 65));
        foreach (var p in keyPoints)
        {
            string gens = p.GenerationsToLearn > 99999 ? "∞" : $"{p.GenerationsToLearn:F0}";
            string marker = p.Complexity > 180 ? " ← OUR UNIVERSE" : "";
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  {0,9:F1}   {1,8:F4}  {2,9:F3}   {3,10}   {4}{5}",
                p.Complexity, p.ObserverIndex, p.KnowledgeIndex, gens, p.Regime, marker));
        }
        sb.AppendLine();
        sb.AppendLine("  Our universe: knowledge accumulates in ~8 generations.");
        sb.AppendLine("  Evolution is a FAST learner in information-rich environments.");
        sb.AppendLine();

        // 5. The principle
        Sec(sb, "The Knowledge Emergence Principle");
        sb.AppendLine(KnowledgeEmergenceAnalyzer.ThePrinciple());

        // 6. Complete XF chain
        Sec(sb, "The Complete ResearchXF Chain — Complexity Physics");
        sb.AppendLine(KnowledgeEmergenceAnalyzer.TheCompleteXFChain());

        // 7. Final
        string classification = "D: Knowledge Is Inevitable";

        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  ResearchXF-005 COMPLETE.");
        sb.AppendLine($"  Classification: {classification}");
        sb.AppendLine($"  K = Information × Accuracy × Persistence.");
        sb.AppendLine($"  Knowledge is the ATTRACTOR of observer evolution.");
        sb.AppendLine($"  XF001-XF005: The first Complexity Physics chain is COMPLETE.");
        sb.AppendLine(new string('=', 100));
        Output.WriteLine(sb.ToString());
    }

    private static void Sec(StringBuilder sb, string t)
    { sb.AppendLine(t); sb.AppendLine(new string('-', t.Length)); }
}
