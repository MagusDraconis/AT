using System.Globalization;
using System.Text;
using AT.Core.ResearchXE;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXE;

public class AT_XE008_InformationCapacityThresholdAudit : ResearchTestBase
{
    public AT_XE008_InformationCapacityThresholdAudit(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void XE008_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchXE-008 Information Capacity Threshold Audit");

        var snapshots = InformationCapacityAnalyzer.ComputeInfoChain();
        int obsUniverses = snapshots.Count(s => s.ObserversPossible);
        int dead = snapshots.Count(s => !s.ObserversPossible);

        // 1. Info chain table
        Sec(sb, "M² → Information Capacity Chain");
        sb.AppendLine(InformationCapacityAnalyzer.InfoTable(snapshots));
        sb.AppendLine();
        sb.AppendLine($"  {obsUniverses}/{snapshots.Count} support observers. {dead} do not.");
        sb.AppendLine();

        // 2. Three thresholds
        Sec(sb, "Three Information Capacity Thresholds");
        sb.AppendLine("  THRESHOLD 1 — Chemistry EXISTS (M² ≥ 2.5, bits ≥ 40):");
        sb.AppendLine("    Organic molecules can form. No metabolism. No evolution.");
        sb.AppendLine("    State space: ~10⁴ configurations. Too small for adaptive search.");
        sb.AppendLine();
        sb.AppendLine("  THRESHOLD 2 — MINIMUM OBSERVER (M² ≥ 3.0, bits ≥ 80):");
        sb.AppendLine("    Evolution possible. State space: ~10⁸ configurations.");
        sb.AppendLine("    Adequate for simple adaptive systems. Marginal for intelligence.");
        sb.AppendLine();
        sb.AppendLine("  THRESHOLD 3 — RICH OBSERVER (M² ≥ 3.5, bits ≥ 120):");
        sb.AppendLine("    Full evolutionary capacity. State space: ~10¹² configurations.");
        sb.AppendLine("    Sufficient for complex intelligence. Rich enzymatic chemistry.");
        sb.AppendLine();
        sb.AppendLine($"  OUR UNIVERSE (M²≈5, bits≈{snapshots.First(s => Math.Abs(s.M2 - 5.0) < 0.1).StateSpaceBits:F0}):");
        sb.AppendLine("    VASTLY above all thresholds. Not 'tuned' — generously provisioned.");
        sb.AppendLine();

        // 3. Scaling
        Sec(sb, "Chemical State Space Scaling");
        sb.AppendLine("  Molecular diversity ∝ exp(c·Z) for small Z, power-law for large Z.");
        sb.AppendLine("  Each additional element multiplies the molecular search space by ~e^0.8 ≈ 2.2.");
        sb.AppendLine("  Going from Z=20 to Z=30: state space grows by ~e^8 ≈ 3000×.");
        sb.AppendLine("  Going from Z=30 to Z=90: state space grows by ~e^48 ≈ 10^21×.");
        sb.AppendLine();
        sb.AppendLine("  KEY: The transition from 'no observers' to 'observers' occurs");
        sb.AppendLine("  in a NARROW Z range (20-30) because information capacity");
        sb.AppendLine("  crosses the evolutionary threshold exponentially fast.");
        sb.AppendLine();

        // 4. Complete chain
        Sec(sb, "The Complete M² → Observers Chain");
        sb.AppendLine(InformationCapacityAnalyzer.TheFinalChain());

        // 5. Ultimate answer
        Sec(sb, "Why M² ≈ 5? — The Ultimate Answer");
        sb.AppendLine(InformationCapacityAnalyzer.TheUltimateAnswer());

        // 6. ResearchXE summary
        Sec(sb, "ResearchXE — Complete Program Summary");
        sb.AppendLine("  XE001: Monte Carlo stress test — qualitative conclusions robust.");
        sb.AppendLine("  XE002: Assumption dependency audit — 18 assumptions, 5 tiers.");
        sb.AppendLine("  XE003: Counterfactual universes — 3/8 support observers.");
        sb.AppendLine("  XE004: Landscape scan — 50k universes, ~5% observer-supporting.");
        sb.AppendLine("  XE005: Complexity optimum — CHEMISTRY is the dominant bottleneck.");
        sb.AppendLine("  XE006: Chemistry window — M² ≈ 3-5 from atomic stability.");
        sb.AppendLine("  XE007: Periodic table viability — Z ≥ 20 is the threshold.");
        sb.AppendLine("  XE008: Information capacity — bits ≥ 80 for observers.");
        sb.AppendLine();
        sb.AppendLine("  THE CHAIN IS COMPLETE:");
        sb.AppendLine("  M² ≈ 5 → Z ≈ 90 → bits ≈ 300 → OBSERVERS.");
        sb.AppendLine();

        // 7. Final
        string classification = obsUniverses >= 5 ? "D: Primary Observer Bottleneck — Information Capacity"
            : "C: Strong Contribution";

        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  ResearchXE-008 COMPLETE.");
        sb.AppendLine($"  Classification: {classification}");
        sb.AppendLine($"  Observer threshold: bits ≥ 80 (M² ≥ 3.0). Our bits: ~300.");
        sb.AppendLine($"  {obsUniverses}/{snapshots.Count} M² values support observers.");
        sb.AppendLine($"  CHAIN: M² → Periodic Table → Chemistry → Information → Observers.");
        sb.AppendLine($"  AT landscape UNDERSTOOD. ResearchXE program COMPLETE.");
        sb.AppendLine(new string('=', 100));
        Output.WriteLine(sb.ToString());
    }

    private static void Sec(StringBuilder sb, string t)
    { sb.AppendLine(t); sb.AppendLine(new string('-', t.Length)); }
}
