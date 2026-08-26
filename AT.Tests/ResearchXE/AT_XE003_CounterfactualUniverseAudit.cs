using System.Globalization;
using System.Text;
using AT.Core.ResearchXE;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXE;

public class AT_XE003_CounterfactualUniverseAudit : ResearchTestBase
{
    public AT_XE003_CounterfactualUniverseAudit(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void XE003_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchXE-003 Counterfactual Universe Audit");

        var universes = CounterfactualUniverseAnalyzer.BuildUniverses();
        var necessities = CounterfactualUniverseAnalyzer.BuildNecessities(universes);

        int viableUniverses = universes.Count(u => u.ObserversPossible);
        int impossibleUniverses = universes.Count(u => !u.ObserversPossible);

        // 1. Universe table
        Sec(sb, "Counterfactual Universe Viability");
        sb.AppendLine(CounterfactualUniverseAnalyzer.UniverseTable(universes));
        sb.AppendLine();
        sb.AppendLine($"  {viableUniverses}/{universes.Count} universes support observers.");
        sb.AppendLine($"  {impossibleUniverses}/{universes.Count} are dead (no observers possible).");
        sb.AppendLine();

        // 2. Universe details
        Sec(sb, "Universe-by-Universe Analysis");
        foreach (var u in universes.OrderByDescending(u => u.ViabilityScore))
        {
            sb.AppendLine($"  [{u.Id}] {u.Name}");
            sb.AppendLine($"  Modification: {u.Modification}");
            sb.AppendLine($"  Viability: {u.ViabilityScore:F2}");
            sb.AppendLine($"  Verdict: {u.Verdict}");
            sb.AppendLine();
        }

        // 3. Necessity ranking
        Sec(sb, "Assumption Necessity Ranking");
        sb.AppendLine(CounterfactualUniverseAnalyzer.NecessityRanking(necessities));

        // 4. What's indispensable
        Sec(sb, "Indispensable Assumptions (Cannot Be Changed)");
        foreach (var n in necessities.Where(n => n.Class == CounterfactualUniverseAnalyzer.NecessityClass.Indispensable))
        {
            sb.AppendLine($"  {n.Assumption}: {n.Reasoning}");
            sb.AppendLine();
        }

        // 5. What's flexible
        Sec(sb, "Contingent Assumptions (Could Be Different)");
        foreach (var n in necessities.Where(n => n.Class == CounterfactualUniverseAnalyzer.NecessityClass.Contingent))
        {
            sb.AppendLine($"  {n.Assumption}: {n.Reasoning}");
            sb.AppendLine();
        }

        // 6. Viable window
        Sec(sb, "The Viable Window");
        sb.AppendLine(CounterfactualUniverseAnalyzer.ViableWindow());

        // 7. The anthropic landscape
        Sec(sb, "AT's Anthropic Landscape");
        sb.AppendLine("  ┌─────────────────────────────────────────────┐");
        sb.AppendLine("  │  AT PREDICTS A LANDSCAPE, NOT A POINT       │");
        sb.AppendLine("  │                                               │");
        sb.AppendLine("  │  UNIQUE (no choice):                          │");
        sb.AppendLine("  │    • Q must exist                            │");
        sb.AppendLine("  │    • Randomness must exist                    │");
        sb.AppendLine("  │    • Identity + Abundance both needed         │");
        sb.AppendLine("  │                                               │");
        sb.AppendLine("  │  NARROW WINDOW (few choices):                 │");
        sb.AppendLine("  │    • d = 3+1 (only viable dimensionality)     │");
        sb.AppendLine("  │    • M² ≈ 2–8 (nonlinearity window)           │");
        sb.AppendLine("  │                                               │");
        sb.AppendLine("  │  CONTINGENT (many choices):                   │");
        sb.AppendLine("  │    • Generations: 2–4 all viable               │");
        sb.AppendLine("  │    • Specific M² within window                │");
        sb.AppendLine("  │    • Specific α within stability window       │");
        sb.AppendLine("  └─────────────────────────────────────────────┘");
        sb.AppendLine();

        // 8. Final
        int indispensable = necessities.Count(n => n.Class == CounterfactualUniverseAnalyzer.NecessityClass.Indispensable);
        string classification = indispensable <= 2 && viableUniverses <= 3
            ? "D: Narrow Viable Universe Class"
            : viableUniverses <= 5 ? "C: Strong Selection Pressure"
            : "B: Moderate Dependence";

        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  ResearchXE-003 COMPLETE.");
        sb.AppendLine($"  Classification: {classification}");
        sb.AppendLine($"  {viableUniverses}/{universes.Count} universes support observers.");
        sb.AppendLine($"  {indispensable} assumptions are INDISPENSABLE (Q, Abundance).");
        sb.AppendLine($"  3+1D and M²≈2-8 are STRONGLY REQUIRED.");
        sb.AppendLine($"  AT predicts a LANDSCAPE, not a unique universe.");
        sb.AppendLine(new string('=', 100));
        Output.WriteLine(sb.ToString());
    }

    private static void Sec(StringBuilder sb, string t)
    { sb.AppendLine(t); sb.AppendLine(new string('-', t.Length)); }
}
