using System.Globalization;
using System.Text;
using AT.Core.Research;
using AT.Tests.Shared;

namespace AT.Tests.ResearchX;

public class AT_X060h_GravityEmergenceAudit : ResearchTestBase
{
    public AT_X060h_GravityEmergenceAudit(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void AT_X060h_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("AT-X060h Gravity Emergence Audit");

        var nodes = GravityEmergenceAuditAnalyzer.AuditDependencies();
        var paths = GravityEmergenceAuditAnalyzer.AuditPaths();

        int beforeGravity = nodes.Count(n => n.ExistsBeforeGravity);
        int afterGravity = nodes.Count(n => !n.ExistsBeforeGravity);
        int survivingPaths = paths.Count(p => p.Survives);

        // 1. Dependency audit
        Sec(sb, "What Exists Before Gravity?");
        sb.AppendLine("  Concept                          Before Gravity?  Requires");
        sb.AppendLine("  " + new string('─', 65));
        foreach (var n in nodes)
        {
            string bg = n.ExistsBeforeGravity ? "✓ YES" : "✗ NO (after)";
            sb.AppendLine($"  {n.Concept,-33} {bg,-15} {string.Join(", ", n.Requires)}");
        }
        sb.AppendLine();
        sb.AppendLine($"  {beforeGravity}/{nodes.Count} structures exist BEFORE gravity.");
        sb.AppendLine($"  Only {afterGravity} emerge AFTER: G, Λ (both require gravity first).");
        sb.AppendLine();

        // 2. The hierarchy
        Sec(sb, "Gravity in the AT Hierarchy");
        sb.AppendLine(GravityEmergenceAuditAnalyzer.TheHierarchy());

        // 3. Reconstruction paths
        Sec(sb, "Gravity Reconstruction Paths");
        sb.AppendLine("  Path                                      Recovers GR?  Survives?");
        sb.AppendLine("  " + new string('─', 65));
        foreach (var p in paths)
        {
            string gr = p.RecoversGR ? "✓ YES" : "✗ NO";
            string s = p.Survives ? "✓" : "✗";
            sb.AppendLine($"  {p.Name,-40}  {gr,-12} {s}");
        }
        sb.AppendLine();
        sb.AppendLine($"  Only Path A (causal set) recovers GR — but with external BDG dependency.");
        sb.AppendLine("  Paths B-D recover Newtonian gravity but NOT full GR.");
        sb.AppendLine();

        // 4. The GR gap
        Sec(sb, "The GR Gap — AT's Weakest Link");
        sb.AppendLine("  AT PROVIDES:");
        sb.AppendLine("    ✓  Causal order (partial order of Q-events)");
        sb.AppendLine("    ✓  Metric geometry (from correlations)");
        sb.AppendLine("    ✓  Event density → volume element √(-g) d⁴x");
        sb.AppendLine("    ✓  Defect density → stress-energy source T_μν");
        sb.AppendLine();
        sb.AppendLine("  AT DOES NOT PROVIDE (external dependency):");
        sb.AppendLine("    ✗  BDG action → Einstein-Hilbert action → G_μν = 8πG T_μν");
        sb.AppendLine("    ✗  The specific functional form of curvature from discreteness");
        sb.AppendLine("    ✗  β (dimensionless BDG coefficient)");
        sb.AppendLine();
        sb.AppendLine("  CLOSING THE GR GAP would require:");
        sb.AppendLine("    1. Deriving the Einstein equations directly from Q-event");
        sb.AppendLine("       causal structure, WITHOUT using the BDG action.");
        sb.AppendLine("    OR");
        sb.AppendLine("    2. Deriving the BDG action itself from Q-event dynamics.");
        sb.AppendLine("  This is the capstone open problem of AT gravity.");
        sb.AppendLine();

        // 5. Standard vs AT ordering
        Sec(sb, "Standard Physics vs AT — Reversed Ordering");
        sb.AppendLine("  STANDARD PHYSICS:");
        sb.AppendLine("    Spacetime → Matter → Quantum Fields");
        sb.AppendLine("    (GR is the background on which everything else lives)");
        sb.AppendLine();
        sb.AppendLine("  AT:");
        sb.AppendLine("    Q → Randomness → Time → Correlations → Geometry");
        sb.AppendLine("    → Defects → Gauge Theory → Matter → GRAVITY");
        sb.AppendLine("    (Gravity is the LAST structure to emerge)");
        sb.AppendLine();
        sb.AppendLine("  This REVERSAL is one of AT's deepest claims:");
        sb.AppendLine("  Gravity is not the stage — it's the final act.");
        sb.AppendLine();

        // 6. Verdict
        Sec(sb, "Verdict");
        sb.AppendLine(GravityEmergenceAuditAnalyzer.TheVerdict());

        // 7. Final
        string classification = survivingPaths == 1 && beforeGravity >= 10
            ? "C: Strong Emergence (with external GR gap)"
            : "B: Weak Emergence";

        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  AT-X060h COMPLETE.");
        sb.AppendLine($"  Classification: {classification}");
        sb.AppendLine($"  Gravity is LAYER 4 — the LAST structure to emerge.");
        sb.AppendLine($"  {beforeGravity}/{nodes.Count} structures exist BEFORE gravity.");
        sb.AppendLine($"  Causal set → GR is the weakest link (external dependency).");
        sb.AppendLine(new string('=', 100));
        Output.WriteLine(sb.ToString());
    }

    private static void Sec(StringBuilder sb, string t)
    { sb.AppendLine(t); sb.AppendLine(new string('-', t.Length)); }
}
