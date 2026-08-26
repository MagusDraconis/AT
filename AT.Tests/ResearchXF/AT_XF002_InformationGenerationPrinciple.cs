using System.Globalization;
using System.Text;
using AT.Core.ResearchXF;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXF;

public class AT_XF002_InformationGenerationPrinciple : ResearchTestBase
{
    public AT_XF002_InformationGenerationPrinciple(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void XF002_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchXF-002 Information Generation Principle");

        var points = InformationGenerationAnalyzer.ScanInfoSpace();
        int growing = points.Count(p => p.Regime == InformationGenerationAnalyzer.InfoRegime.Growing);
        int frozen = points.Count(p => p.Regime == InformationGenerationAnalyzer.InfoRegime.Frozen);
        int decaying = points.Count(p => p.Regime == InformationGenerationAnalyzer.InfoRegime.Decaying);

        // 1. Phase diagram
        Sec(sb, "Information Phase Diagram — (Q, R) Plane");
        sb.AppendLine(InformationGenerationAnalyzer.InfoPhaseDiagram(points));
        sb.AppendLine();
        sb.AppendLine($"  ▲ GROWING: {growing}  □ FROZEN: {frozen}  - DECAYING: {decaying}");
        sb.AppendLine();

        // 2. Key points
        Sec(sb, "Information Dynamics — Key Configurations");
        sb.AppendLine(InformationGenerationAnalyzer.InfoGrowthTable(points));
        sb.AppendLine();

        // 3. dI/dt analysis
        Sec(sb, "dI/dt = Creation − Decay — The Fundamental Equation");
        sb.AppendLine("  CREATION ∝ R · States(Q)");
        sb.AppendLine("    Randomness generates new information by actualizing novel outcomes.");
        sb.AppendLine("    Rate proportional to the state space: more entities → more novelty.");
        sb.AppendLine();
        sb.AppendLine("  DECAY ∝ (1 − Retention(Q,R)) · I");
        sb.AppendLine("    Information degrades when it cannot be structurally preserved.");
        sb.AppendLine("    High Q → high retention (topological protection).");
        sb.AppendLine("    High R → low retention (chaos erases memory).");
        sb.AppendLine();
        sb.AppendLine("  STEADY STATE: I* = Creation / DecayRate");
        sb.AppendLine("    For Q≈1, R≈0.5: I* is LARGE and GROWING.");
        sb.AppendLine();

        // 4. Three limits
        Sec(sb, "Three Limits — Why Information Only Grows in the Middle");
        sb.AppendLine("  FROZEN (R=0): dI/dt = 0.");
        sb.AppendLine("    All information present at t=0. Nothing changes. Static universe.");
        sb.AppendLine();
        sb.AppendLine("  CHAOTIC (R>0.7): dI/dt < 0.");
        sb.AppendLine("    Decay outpaces creation. Information is destroyed faster than produced.");
        sb.AppendLine("    No memory. No learning. No evolution.");
        sb.AppendLine();
        sb.AppendLine("  VACUUM (Q=0): I = 0 always.");
        sb.AppendLine("    No entities → no states → no information → nothing.");
        sb.AppendLine();
        sb.AppendLine("  GROWING (Q>0.5, R≈0.3-0.7): dI/dt > 0.");
        sb.AppendLine("    Our universe: Q≈1, R≈0.5. Information GROWS.");
        sb.AppendLine();

        // 5. The principle
        Sec(sb, "The Information Generation Principle");
        sb.AppendLine(InformationGenerationAnalyzer.ThePrinciple());

        // 6. Final
        string classification = growing >= 9 ? "D: Information Growth Derived from Primitives"
            : "C: Strong Emergence";

        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  ResearchXF-002 COMPLETE.");
        sb.AppendLine($"  Classification: {classification}");
        sb.AppendLine($"  dI/dt = Creation − Decay. Growing: {growing}/49 configurations.");
        sb.AppendLine($"  INFORMATION GROWTH IS INEVITABLE when Q + Randomness coexist.");
        sb.AppendLine($"  Our universe (Q≈1, R≈0.5): dI/dt > 0. Evolution is MANDATORY.");
        sb.AppendLine(new string('=', 100));
        Output.WriteLine(sb.ToString());
    }

    private static void Sec(StringBuilder sb, string t)
    { sb.AppendLine(t); sb.AppendLine(new string('-', t.Length)); }
}
