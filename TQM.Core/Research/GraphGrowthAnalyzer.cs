namespace TQM.Core.Research;

/// <summary>
/// Analyzes graph growth to determine whether expanding networks
/// enable open-ended innovation beyond fixed-size TQM.
///
/// TQM-X004: Graph Growth Physics
/// </summary>
public static class GraphGrowthAnalyzer
{
    public static string GrowthTheory()
    {
        return @"
GRAPH GROWTH PHYSICS

1. THE INSIGHT:

   Fixed N → finite spectrum (N eigenvalues) → bounded innovation.
   If N grows → spectrum expands → potentially OPEN-ENDED.

2. THE TEST:

   Start with N₀ nodes. Add 1 node every interval.
   Track species count S(t) = N(t).
   If S(t) grows without bound → open-ended innovation.

3. THIS IS TRIVIALLY TRUE for 1D chains: each new node adds
   a new eigenmode → a new species. The question is whether
   this mathematical fact constitutes 'open-ended innovation'
   or merely trivial expansion of the Hilbert space.

4. NULL HYPOTHESIS: Graph growth = trivial spectrum expansion.
   Species count growth is just counting new eigenvalues.
";
    }

    public static GraphGrowthMetrics.GrowthReport Analyze(int? seed = null)
    {
        int initNodes = 10;
        int addInterval = 50;
        int totalGens = 2000;
        var history = DynamicNodeModel.SimulateGrowth(initNodes, addInterval, totalGens, seed);

        int initSpecies = history.First().SpeciesCount;
        int finalSpecies = history.Last().SpeciesCount;
        int initNodesCount = history.First().NodeCount;
        int finalNodesCount = history.Last().NodeCount;

        bool speciesGrows = finalSpecies > initSpecies;
        bool openEnded = speciesGrows; // if it grows, it's technically open-ended

        string classification = openEnded ? "C: Expanding Spectral Universe" : "A: Fixed-Size Physics Dominates";

        string verdict = openEnded
            ? $"GRAPH GROWTH PRODUCES EXPANDING SPECTRUM. Nodes: {initNodesCount}→{finalNodesCount}. "
              + $"Species: {initSpecies}→{finalSpecies}. Growth rate: 1 species/{addInterval} gens. "
              + $"Each new node adds 1 eigenvalue → 1 new species. "
              + $"This IS 'open-ended' in the mathematical sense: species count grows without bound. "
              + $"But is it PHYSICALLY meaningful? Each new species is just another sinusoidal mode "
              + $"of a longer chain — not a qualitatively new type of species. "
              + $"Graph growth = trivial spectrum expansion, not genuine innovation."
            : "No growth detected.";

        return new GraphGrowthMetrics.GrowthReport(
            history, initNodesCount, finalNodesCount,
            initSpecies, finalSpecies,
            speciesGrows, openEnded, classification, verdict);
    }

    public static string HostileReview(GraphGrowthMetrics.GrowthReport report)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("HOSTILE REVIEW: Is graph growth 'open-ended innovation'?");
        sb.AppendLine();
        sb.AppendLine($"  Nodes: {report.InitialNodes} → {report.FinalNodes}");
        sb.AppendLine($"  Species: {report.InitialSpecies} → {report.FinalSpecies}");
        sb.AppendLine();
        sb.AppendLine("  YES — species count grows without bound. But:");
        sb.AppendLine("  - Each new species is just sin(πk/(N+1)) for larger N");
        sb.AppendLine("  - Same Fourier mode family, just more of them");
        sb.AppendLine("  - No qualitatively NEW type of species emerges");
        sb.AppendLine("  - This is 'trivial innovation' — counting eigenvalues");
        sb.AppendLine();
        sb.AppendLine("  Genuine open-ended innovation would require:");
        sb.AppendLine("  - NEW mode families (not just more of the same)");
        sb.AppendLine("  - Qualitatively different eigenmode structures");
        sb.AppendLine("  - Innovation in mode TYPE, not just mode COUNT");
        sb.AppendLine();
        return sb.ToString();
    }
}
