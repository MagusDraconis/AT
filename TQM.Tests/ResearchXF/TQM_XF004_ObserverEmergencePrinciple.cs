using System.Globalization;
using System.Text;
using TQM.Core.ResearchXF;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXF;

public class TQM_XF004_ObserverEmergencePrinciple : ResearchTestBase
{
    public TQM_XF004_ObserverEmergencePrinciple(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void XF004_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchXF-004 Observer Emergence Principle");

        var points = ObserverEmergenceAnalyzer.ScanObserverSpace();
        int predictive = points.Count(p => p.Regime >= ObserverEmergenceAnalyzer.ObserverRegime.Predictive);
        int selfAware = points.Count(p => p.Regime == ObserverEmergenceAnalyzer.ObserverRegime.SelfAware);

        // 1. Observer table
        Sec(sb, "Complexity × Evolution → Observer Index");
        sb.AppendLine(ObserverEmergenceAnalyzer.ObserverTable(points));
        sb.AppendLine();
        sb.AppendLine($"  PREDICTIVE (observers): {predictive}  SELF-AWARE: {selfAware}");
        sb.AppendLine();

        // 2. Observer definition
        Sec(sb, "What Is an Observer? — Minimal Definition");
        sb.AppendLine("  OBSERVER = SYSTEM WITH:");
        sb.AppendLine("    1. MEMORY: stores information about past states.");
        sb.AppendLine("    2. PREDICTION: anticipates future states from patterns.");
        sb.AppendLine("    3. ADAPTATION: modifies behavior based on predictions.");
        sb.AppendLine("    4. (Self-Awareness): models itself as part of the environment.");
        sb.AppendLine();
        sb.AppendLine("  O = Memory × Prediction × Adaptation");
        sb.AppendLine();

        // 3. The four thresholds
        Sec(sb, "Observer Emergence Thresholds");
        sb.AppendLine(ObserverEmergenceAnalyzer.TheThresholds());

        // 4. Where thresholds are crossed
        Sec(sb, "Where Observers Emerge in the (Q,R) Plane");
        sb.AppendLine("  SENSING (R>0, Q>0.1): Stimulus-response. No memory.");
        sb.AppendLine("  REACTIVE (Q>0.3, R>0.1): Simple memory. Reflexes.");
        sb.AppendLine("  PREDICTIVE (Q>0.5, R≈0.3-0.7): Internal models. OBSERVERS.");
        sb.AppendLine("  SELF-AWARE (Q>0.7, R≈0.3-0.5): Recursive self-modeling.");
        sb.AppendLine();
        sb.AppendLine("  TRANSLATING TO COMPLEXITY:");
        sb.AppendLine("    Sensing:     C ≈ 1");
        sb.AppendLine("    Reactive:    C ≈ 10");
        sb.AppendLine("    Predictive:  C ≈ 50   ← OBSERVER THRESHOLD");
        sb.AppendLine("    Self-Aware:  C ≈ 150");
        sb.AppendLine("    Our universe: C ≈ 184  ← FAR ABOVE ALL THRESHOLDS");
        sb.AppendLine();

        // 5. The complete chain
        Sec(sb, "The Complete XF Chain — Primitives → Observers");
        sb.AppendLine(ObserverEmergenceAnalyzer.TheChain());

        // 6. Final
        string classification = predictive >= 9 ? "D: Observers Are Inevitable"
            : "C: Strong Emergence";

        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  ResearchXF-004 COMPLETE.");
        sb.AppendLine($"  Classification: {classification}");
        sb.AppendLine($"  O = Memory × Prediction × Adaptation.");
        sb.AppendLine($"  Observer threshold: C ≈ 50. Our universe: C ≈ 184.");
        sb.AppendLine($"  OBSERVERS ARE THE EXPECTED OUTCOME OF EVOLUTION.");
        sb.AppendLine(new string('=', 100));
        Output.WriteLine(sb.ToString());
    }

    private static void Sec(StringBuilder sb, string t)
    { sb.AppendLine(t); sb.AppendLine(new string('-', t.Length)); }
}
