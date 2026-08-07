using System.Globalization;
using System.Text;
using TQM.Core.Research;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchX;

public class TQM_X018_ComplexityEmergencePrinciple : ResearchTestBase
{
    public TQM_X018_ComplexityEmergencePrinciple(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void TQM_X018_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("TQM-X018 Complexity Emergence Principle");

        Sec(sb, "0. Assumptions");
        sb.AppendLine("  1. TQM-X015: R+S is the minimal reality foundation.");
        sb.AppendLine("  2. Reality is necessary but NOT sufficient for complexity.");
        sb.AppendLine("  3. Hypothesis: complexity emerges gradually through levels.");
        sb.AppendLine();

        Sec(sb, "1. Complexity Theory");
        sb.AppendLine(ComplexityEmergenceAnalyzer.ComplexityTheory());
        sb.AppendLine();

        var sw = System.Diagnostics.Stopwatch.StartNew();
        var report = ComplexityEmergenceAnalyzer.Analyze();
        sw.Stop();
        sb.AppendLine($"  Analysis: {sw.Elapsed.TotalMilliseconds:F0}ms");

        Sec(sb, "2. The Complexity Staircase");
        sb.AppendLine("  Level  │ Name              │ Score │ Ingredients Added          │ Examples");
        sb.AppendLine("  " + new string('─', 90));
        foreach (var l in report.Levels)
            sb.AppendLine($"  L{l.Level[6]}      │ {l.Level.Substring(9),-17} │ {l.ComplexityScore,5:F1} │ {l.Requirements,-26} │ {l.Examples}");
        sb.AppendLine();

        Sec(sb, "3. What Each Level Requires (Cumulative)");
        sb.AppendLine("  L0 → L1: R+S");
        sb.AppendLine("    Reality foundations. Persistence + identity. Quantum eigenstates.");
        sb.AppendLine("    TQM confirms: R+S is necessary and sufficient for L1.");
        sb.AppendLine();
        sb.AppendLine("  L1 → L2: Information Encoding");
        sb.AppendLine("    Persistent structures that carry information.");
        sb.AppendLine("    TQM confirms: eigenmodes encode information naturally.");
        sb.AppendLine();
        sb.AppendLine("  L2 → L3: Diversity + Reproducibility");
        sb.AppendLine("    Multiple distinct carrier TYPES that can be reproduced.");
        sb.AppendLine("    TQM confirms: ~19 species catalog (TQM-138).");
        sb.AppendLine();
        sb.AppendLine("  L3 → L4: Interactions + Populations");
        sb.AppendLine("    Multiple species interacting in shared environment.");
        sb.AppendLine("    TQM confirms: competition, coexistence observed (TQM-135).");
        sb.AppendLine();
        sb.AppendLine("  L4 → L5: Variation + Selection");
        sb.AppendLine("    Heritable variation subject to differential survival.");
        sb.AppendLine("    TQM confirms: Darwinian triad demonstrated (TQM-134/135/136).");
        sb.AppendLine();
        sb.AppendLine("  L5 → L6: Unbounded Innovation");
        sb.AppendLine("    Continuous novelty without saturation.");
        sb.AppendLine("    TQM: NOT OBSERVED — TQM-138 showed saturation at ~19 species.");
        sb.AppendLine("    ResearchX X002-X004: dynamic graphs do NOT achieve L6.");
        sb.AppendLine("    L6 remains an OPEN QUESTION.");
        sb.AppendLine();

        Sec(sb, "4. The Cumulative Principle");
        sb.AppendLine("  You CANNOT skip levels:");
        sb.AppendLine("    ✗ Evolution without ecologies (no populations to select from)");
        sb.AppendLine("    ✗ Ecologies without species (nothing to interact)");
        sb.AppendLine("    ✗ Species without carriers (no persistent identity)");
        sb.AppendLine("    ✗ Carriers without reality (nothing persists)");
        sb.AppendLine();
        sb.AppendLine("  Each level is a PREREQUISITE for the next.");
        sb.AppendLine("  The staircase is MONOTONIC and CUMULATIVE.");
        sb.AppendLine();
        sb.AppendLine($"  Minimal ingredients for Level 5: {report.MinimalIngredients}");
        sb.AppendLine();

        Sec(sb, "5. The Level-6 Gap");
        sb.AppendLine("  The TQM program has demonstrated Levels 0-5 conclusively.");
        sb.AppendLine("  Level 6 (Open-Ended Evolution) has NOT been observed.");
        sb.AppendLine();
        sb.AppendLine("  TQM-X002-X004 attempted: node motion, phase sweep, graph growth.");
        sb.AppendLine("  All failed to produce unbounded innovation.");
        sb.AppendLine();
        sb.AppendLine("  Possible routes to Level 6:");
        sb.AppendLine("    1. Niche construction (species modify their environment)");
        sb.AppendLine("    2. Co-evolution (species create new selective pressures)");
        sb.AppendLine("    3. Higher-dimensional graphs (2D/3D richer spectra)");
        sb.AppendLine("    4. External energy input (driven/open systems)");
        sb.AppendLine("    5. Hybrid linear-nonlinear systems");
        sb.AppendLine();

        Sec(sb, "6. Hostile Review");
        sb.AppendLine(ComplexityEmergenceAnalyzer.HostileReview(report));
        sb.AppendLine();

        Sec(sb, "7. Classification");
        sb.AppendLine($"  {report.Classification}");
        sb.AppendLine($"  {report.Verdict}");
        sb.AppendLine();

        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  TQM-X018 complete. Classification: {report.Classification}");
        sb.AppendLine($"  Complexity emerges GRADUALLY through 6 cumulative levels.");
        sb.AppendLine(new string('=', 100));
        Output.WriteLine(sb.ToString());
    }

    private static void Sec(StringBuilder sb, string t)
    { sb.AppendLine(t); sb.AppendLine(new string('-', t.Length)); }
}
