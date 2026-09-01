using System.Globalization;
using System.Text;
using AT.Core.Resonance.Theory;
using AT.Tests.Shared;

namespace AT.Tests.Research;

public class AT_139_InformationLandscapeTopology : ResearchTestBase
{
    public AT_139_InformationLandscapeTopology(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void AT_139_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("AT-139 Information Attractor Landscape Topology");

        // ── Section 0: Assumptions ──
        Sec(sb, "0. Assumptions");
        sb.AppendLine("  1. Theta has an attractor landscape with ~19 species (AT-138).");
        sb.AppendLine("  2. Attractors are minima of an effective information potential V(p).");
        sb.AppendLine("  3. V(p) = -α·self_consistency - β·fitness + γ·roughness.");
        sb.AppendLine("  4. Gradient descent on V(p) maps the basin structure.");
        sb.AppendLine("  5. Assume NO landscape structure until topology is demonstrated.");
        sb.AppendLine();

        // ── Section 1: AT-138 Recap ──
        Sec(sb, "1. AT-138 Recap — Bounded Innovation");
        sb.AppendLine("  AT-138: 66 novel species, 15 unique, saturation index 0.82.");
        sb.AppendLine("  The attractor landscape contains ~19 stable species.");
        sb.AppendLine("  Innovation is bounded — the landscape is finite.");
        sb.AppendLine();
        sb.AppendLine("  AT-139 asks: WHY ~19 species? What is the landscape topology?");
        sb.AppendLine();

        // ── Section 2: Landscape Theory ──
        Sec(sb, "2. Landscape Theory");
        sb.AppendLine(InformationLandscapeAnalyzer.LandscapeTheory());
        sb.AppendLine();

        // ── Section 3: Landscape Mapping ──
        Sec(sb, "3. Landscape Mapping — Gradient Descent Experiments");

        var sw = System.Diagnostics.Stopwatch.StartNew();
        var report = InformationLandscapeAnalyzer.Analyze(seed: 42);
        sw.Stop();

        sb.AppendLine($"  Analysis completed in {sw.Elapsed.TotalMilliseconds:F0}ms.");
        sb.AppendLine($"  Initial conditions generated: {report.TotalICsGenerated}");
        sb.AppendLine($"  Converged: {report.ConvergedICs} ({report.ConvergenceRate:P0})");
        sb.AppendLine($"  Attractors discovered: {report.Graph.TotalAttractors}");
        sb.AppendLine($"  Transitions mapped: {report.Graph.TotalTransitions}");
        sb.AppendLine();

        // ── Section 4: Basin Structure ──
        Sec(sb, "4. Attractor Basin Structure");
        sb.AppendLine(InformationLandscapeAnalyzer.BasinSummary(report.Basins));
        sb.AppendLine();

        sb.AppendLine($"  Mean basin volume: {report.MeanBasinVolume:P2}");
        sb.AppendLine($"  Basin volume entropy: {report.BasinVolumeEntropy:F3}");
        sb.AppendLine(report.BasinVolumeEntropy > 1.5
            ? "  → High entropy — diverse basin sizes suggest real structure."
            : "  → Low entropy — uniform basin sizes suggest noise.");
        sb.AppendLine();

        // Highlight the largest basins.
        var top3 = report.Basins.OrderByDescending(b => b.BasinVolume).Take(3).ToList();
        sb.AppendLine("  Top 3 basins by volume:");
        foreach (var b in top3)
            sb.AppendLine($"    {b.Name}: {b.BasinVolume:P1} volume, fitness={b.Fitness:F3}, complexity={b.Complexity:F1}, symmetry={b.SymmetryClass}");
        sb.AppendLine();

        // ── Section 5: Transition Topology ──
        Sec(sb, "5. Transition Graph Topology");
        var g = report.Graph;
        sb.AppendLine($"  Total attractors:     {g.TotalAttractors}");
        sb.AppendLine($"  Total transitions:    {g.TotalTransitions}");
        sb.AppendLine($"  Mean connectivity:    {g.MeanConnectivity:F1} edges/node");
        sb.AppendLine($"  Graph density:        {g.GraphDensity:F3}");
        sb.AppendLine($"  Connected components: {g.ConnectedComponents}");
        sb.AppendLine($"  Fully connected:      {(g.IsFullyConnected ? "YES" : "NO")}");
        sb.AppendLine($"  Diameter:             {g.Diameter} steps");
        sb.AppendLine($"  Clustering coeff:     {g.ClusteringCoefficient:F3}");
        sb.AppendLine($"  Topology type:        {g.Topology}");
        sb.AppendLine($"  Hub attractors:       {g.CentralHubAttractorCount}");
        sb.AppendLine($"  Bottleneck attractors: {g.BottleneckAttractors.Count}");
        sb.AppendLine();

        if (g.BottleneckAttractors.Count > 0)
        {
            sb.AppendLine($"  Bottleneck species: {string.Join(", ", g.BottleneckAttractors.Take(5))}");
            sb.AppendLine("  → These species are CRITICAL — their loss fragments the landscape.");
        }
        sb.AppendLine();

        // ── Section 6: Potential Landscape ──
        Sec(sb, "6. Effective Information Potential");
        sb.AppendLine("  V(p) = -3.0·self_consistency - 2.0·fitness + 1.5·roughness");
        sb.AppendLine();
        sb.AppendLine("  1D potential slice (100 points along principal axis):");
        double potMin = report.PotentialLandscape1D.Min();
        double potMax = report.PotentialLandscape1D.Max();
        int minimaCount = 0;
        for (int i = 1; i < report.PotentialLandscape1D.Length - 1; i++)
            if (report.PotentialLandscape1D[i] < report.PotentialLandscape1D[i - 1]
             && report.PotentialLandscape1D[i] < report.PotentialLandscape1D[i + 1])
                minimaCount++;

        sb.AppendLine($"  Potential range: [{potMin:F2}, {potMax:F2}]");
        sb.AppendLine($"  Local minima in 1D slice: {minimaCount}");
        sb.AppendLine($"  Landscape class: {report.LandscapeClass}");
        sb.AppendLine();

        // ── Section 7: AT-138 Consistency ──
        Sec(sb, "7. Consistency with AT-138");
        int expectedSpecies = 19;
        int observedSpecies = g.TotalAttractors;
        sb.AppendLine($"  AT-138 species count:  ~{expectedSpecies}");
        sb.AppendLine($"  AT-139 attractor count: {observedSpecies}");
        sb.AppendLine($"  Match: {(Math.Abs(observedSpecies - expectedSpecies) <= 5 ? "CONSISTENT (±5)" : "DIFFERENT")}");
        sb.AppendLine();
        sb.AppendLine(report.FiniteLandscape
            ? "  → Finite landscape CONFIRMED. Innovation saturates because"
              + " the attractor count is bounded."
            : "  → Landscape may be larger than detected.");
        sb.AppendLine();

        // ── Section 8: Hostile Review ──
        Sec(sb, "8. Hostile Review");
        sb.AppendLine(InformationLandscapeAnalyzer.HostileReview(report));
        sb.AppendLine();

        // ── Section 9: Research Questions ──
        Sec(sb, "9. Research Questions");
        sb.AppendLine(InformationLandscapeAnalyzer.ResearchQuestions(report));
        sb.AppendLine();

        // ── Section 10: Classification ──
        Sec(sb, "10. Classification");
        sb.AppendLine($"  {report.Classification}");
        sb.AppendLine();
        sb.AppendLine($"  {report.Verdict}");
        sb.AppendLine();

        // ── Final ──
        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  Experiment AT-139 completed.");
        sb.AppendLine($"  Classification: {report.Classification}");
        sb.AppendLine($"  Landscape topology: {report.LandscapeClass}");
        sb.AppendLine($"  Attractors mapped: {observedSpecies}");
        sb.AppendLine($"  Finite landscape: {(report.FiniteLandscape ? "CONFIRMED" : "NOT CONFIRMED")}");
        sb.AppendLine(new string('=', 100));

        Output.WriteLine(sb.ToString());
    }

    private static void Sec(StringBuilder sb, string t)
    { sb.AppendLine(t); sb.AppendLine(new string('-', t.Length)); }
}
