using System.Globalization;
using System.Text;
using TQM.Core.Resonance.Theory;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.Research;

public class TQM_138_OpenEndedInnovation : ResearchTestBase
{
    public TQM_138_OpenEndedInnovation(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void TQM_138_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("TQM-138 Open-Ended Information Innovation");

        // ── Section 0: Assumptions ──
        Sec(sb, "0. Assumptions");
        sb.AppendLine("  1. Theta supports Darwinian evolution (TQM-134/135/136/137).");
        sb.AppendLine("  2. 4 known species exist: A, B, C, D (TQM-133).");
        sb.AppendLine("  3. Mutation can modify pattern vectors over time.");
        sb.AppendLine("  4. Assume the species catalog is FIXED until novel species are shown.");
        sb.AppendLine("  5. Novelty requires: pattern similarity < 0.4 to ALL known species.");
        sb.AppendLine("  6. Persistence requires: novel species must survive > 100 generations.");
        sb.AppendLine();

        // ── Section 1: Background Recap ──
        Sec(sb, "1. Background — The Complete Theta Hierarchy");
        sb.AppendLine("  TQM-133: 4 stable information species discovered (A, B, C, D).");
        sb.AppendLine("  TQM-134: Reproduction + inheritance + mutation.");
        sb.AppendLine("  TQM-135: Selection under resource constraints.");
        sb.AppendLine("  TQM-136: Fitness law w = r/c.");
        sb.AppendLine("  TQM-137: Evolution is universal.");
        sb.AppendLine();
        sb.AppendLine("  CRITICAL QUESTION: Can evolution create NEW species?");
        sb.AppendLine("  Or is it confined to the 4-species catalog?");
        sb.AppendLine();

        // ── Section 2: Innovation Theory ──
        Sec(sb, "2. Innovation Theory");
        sb.AppendLine(InformationInnovationAnalyzer.InnovationTheory());
        sb.AppendLine();

        // ── Section 3: Innovation Experiments ──
        Sec(sb, "3. Innovation Experiments");

        var sw = System.Diagnostics.Stopwatch.StartNew();
        var report = InformationInnovationAnalyzer.Analyze(seed: 42);
        sw.Stop();

        sb.AppendLine($"  Analysis completed in {sw.Elapsed.TotalMilliseconds:F0}ms.");
        sb.AppendLine($"  Population sizes tested: 100, 500, 1000, 5000");
        sb.AppendLine($"  Time scales: 10,000 and 50,000 generations");
        sb.AppendLine($"  Resource capacities: 500, 1000, 2000");
        sb.AppendLine($"  Mutation strengths: 0.03, 0.05, 0.10");
        sb.AppendLine($"  Independent seeds: 3 per configuration");
        sb.AppendLine($"  Total runs: 4 × 2 × 3 × 3 × 3 = 216");
        sb.AppendLine();

        // ── Section 4: Novel Species Discovered ──
        Sec(sb, "4. Novel Species Discovered");
        sb.AppendLine($"  Total novel species: {report.NovelSpecies.Count}");
        sb.AppendLine($"  Persistent (>100 gens): {report.Metrics.PersistentNovelSpecies}");
        sb.AppendLine($"  Mean novelty score: {report.Metrics.MeanNoveltyScore:F3}");
        sb.AppendLine($"  Innovation rate: {report.Metrics.InnovationRate:F2} per 1000 generations");
        sb.AppendLine();

        if (report.NovelSpecies.Count > 0)
        {
            sb.AppendLine("  Novel species details:");
            sb.AppendLine("  Name │ Discovered │ Parent │ Novelty │ Persistence │ Complexity │ Persistent?");
            sb.AppendLine("  " + new string('─', 80));
            foreach (var n in report.NovelSpecies.Take(12))
                sb.AppendLine($"  {n.Name,-4} │ {n.DiscoveryTime,10} │ {n.ParentSpecies,-6} │ {n.NoveltyScore,7:F3} │ {n.PersistenceGenerations,11} │ {n.MeanComplexity,10:F1} │ {(n.IsPersistent ? "YES" : "no")}");
            sb.AppendLine();
        }
        else
        {
            sb.AppendLine("  ⚠ NO novel species detected in any run.");
            sb.AppendLine("  The species catalog appears FIXED at 4.");
            sb.AppendLine();
        }

        // ── Section 5: Innovation Metrics ──
        Sec(sb, "5. Innovation Metrics");
        var m = report.Metrics;
        sb.AppendLine($"  Total novel species:        {m.TotalNovelSpeciesDiscovered}");
        sb.AppendLine($"  Persistent novel species:   {m.PersistentNovelSpecies}");
        sb.AppendLine($"  Innovation rate:            {m.InnovationRate:F2} / 1000 gens");
        sb.AppendLine($"  Species saturation index:   {m.SpeciesSaturationIndex:F2}");
        sb.AppendLine($"  Initial mean complexity:    {m.MeanComplexityInitial:F2}");
        sb.AppendLine($"  Final mean complexity:      {m.MeanComplexityFinal:F2}");
        sb.AppendLine($"  Complexity growth rate:     {m.ComplexityGrowthRate:F4}");
        sb.AppendLine($"  Max lineage depth:          {m.MaxLineageDepth}");
        sb.AppendLine($"  Discovery curve shape:      {m.DiscoveryCurveShape}");
        sb.AppendLine();
        sb.AppendLine($"  Innovation detected:        {(m.InnovationDetected ? "YES" : "NO")}");
        sb.AppendLine($"  Saturation observed:        {(m.SaturationObserved ? "YES" : "NO")}");
        sb.AppendLine($"  Complexity increased:       {(m.ComplexityIncreased ? "YES" : "NO")}");
        sb.AppendLine();

        // ── Section 6: Diversity History ──
        Sec(sb, "6. Diversity Over Time");
        sb.AppendLine("  Gen   │ Known │ Novel │ Total │ Complexity │ Novelty Score │ Dominant Novel");
        sb.AppendLine("  " + new string('─', 80));
        foreach (var snap in report.DiversityHistory.Where((_, i) => i % 3 == 0))
            sb.AppendLine($"  {snap.TimeStep,5} │ {snap.KnownSpeciesCount,5} │ {snap.NovelSpeciesCount,5} │ {snap.TotalAliveSpecies,5} │ {snap.MeanComplexity,10:F2} │ {snap.MeanNoveltyScore,13:F3} │ {snap.DominantNovelSpecies}");
        sb.AppendLine();

        // ── Section 7: Discovery Curve Analysis ──
        Sec(sb, "7. Discovery Curve Analysis");
        sb.AppendLine($"  Curve shape: {m.DiscoveryCurveShape}");
        sb.AppendLine($"  Saturation index: {m.SpeciesSaturationIndex:F2}");
        sb.AppendLine();
        sb.AppendLine(m.SaturationObserved
            ? "  → The discovery curve has PLATEAUED. Species innovation is BOUNDED."
            : "  → The discovery curve is STILL GROWING. Innovation continues.");
        sb.AppendLine();

        sb.AppendLine(m.ComplexityIncreased
            ? "  → Complexity is INCREASING. Evolution explores more complex patterns."
            : "  → Complexity is STABLE. Evolution stays within bounded complexity.");
        sb.AppendLine();

        // ── Section 8: Hostile Review ──
        Sec(sb, "8. Hostile Review");
        sb.AppendLine(InformationInnovationAnalyzer.HostileReview(report));
        sb.AppendLine();

        // ── Section 9: Research Questions ──
        Sec(sb, "9. Research Questions");
        sb.AppendLine(InformationInnovationAnalyzer.ResearchQuestions(report));
        sb.AppendLine();

        // ── Section 10: Classification ──
        Sec(sb, "10. Classification");
        sb.AppendLine($"  {report.Classification}");
        sb.AppendLine();
        sb.AppendLine($"  {report.Verdict}");
        sb.AppendLine();

        // ── Final ──
        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  Experiment TQM-138 completed.");
        sb.AppendLine($"  Classification: {report.Classification}");
        sb.AppendLine($"  Open-ended evolution: {(report.OpenEndedEvolution ? "DETECTED" : "NOT DETECTED")}");
        sb.AppendLine($"  Novel species: {report.NovelSpecies.Count}");
        sb.AppendLine($"  Species catalog: {(report.NovelSpecies.Count > 0 ? "EXPANDED beyond A/B/C/D" : "FIXED at 4")}");
        sb.AppendLine(new string('=', 100));

        Output.WriteLine(sb.ToString());
    }

    private static void Sec(StringBuilder sb, string t)
    { sb.AppendLine(t); sb.AppendLine(new string('-', t.Length)); }
}
