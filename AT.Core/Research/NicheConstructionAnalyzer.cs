namespace AT.Core.Research;

/// <summary>
/// Determines whether niche construction (species→graph feedback)
/// can enable Open-Ended Evolution (L6).
/// AT-X020: Niche Construction Principle
/// </summary>
public static class NicheConstructionAnalyzer
{
    public static string NicheTheory()
    {
        return @"
NICHE CONSTRUCTION PRINCIPLE

1. THE HYPOTHESIS:

   X019: L6 requires new CARRIER CLASSES.
   Can species→graph feedback (niche construction) create them?

   Current: Graph → Species (one-way).
   Niche construction: Graph ↔ Species (feedback loop).

2. THE BOTTLENECK:

   Graph modification changes the graph LAPLACIAN.
   But the graph Laplacian always has SINUSOIDAL eigenmodes.
   Changing the graph changes WHICH sinusoids, not the TYPE.

   To create NEW CARRIER CLASSES, you need to change the
   OPERATOR TYPE, not just the operator's parameters.

3. WHAT NICHE CONSTRUCTION CAN DO:

   ✓ Increase species diversity (more eigenmodes)
   ✓ Create new spectral gaps (topological changes)
   ✓ Fragment into sub-ecologies (disconnected components)
   ✗ Create new carrier CLASSES (still graph Laplacian)
   ✗ Non-saturating innovation (finite spectrum for finite graph)

4. WHAT WOULD ACTUALLY CREATE NEW CARRIER CLASSES:

   • Changing graph DIMENSION (1D→2D→3D)
   • Changing operator TYPE (Laplacian→magnetic→nonlinear)
   • Creating topological DEFECTS (vortices, domain walls)

   But these require species to modify the FUNDAMENTAL OPERATOR —
   a capability no known physical system possesses.

5. HONEST VERDICT:

   Niche construction (graph modification alone) does NOT
   enable L6. It increases SPECIES diversity but NOT
   CARRIER CLASS diversity. The bottleneck remains:
   you cannot create new carrier classes by modifying
   a graph within the same operator family.

   L6 requires OPERATOR EVOLUTION — not just graph evolution.
";
    }

    public static NicheConstructionMetrics.NicheConstructionReport Analyze()
    {
        var results = SpeciesGraphFeedback.EvaluateMechanisms();
        int newClasses = results.Count(r => r.NewCarrierClasses);
        int l6Capable = results.Count(r => r.NonSaturating);

        bool closesLoop = newClasses >= 1; // topological defects are new classes
        bool enablesL6 = l6Capable >= 1;

        string classification = enablesL6 ? "B: Increased Diversity Only"
                              : closesLoop ? "C: New Carrier Classes" 
                              : "A: Niche Construction Fails";

        string verdict = closesLoop
            ? $"NICHE CONSTRUCTION PARTIALLY WORKS. {newClasses} mechanisms can create new carrier classes. "
              + $"Topological defect creation is the most promising: species activity can nucleate "
              + $"vortices and domain walls, which ARE qualitatively new carrier types. "
              + $"However: the number of distinct topological sectors is FINITE for any given graph — "
              + $"saturation is inevitable. "
              + $"{(enablesL6 ? "Dimensional expansion or operator-type changes COULD enable L6." : "")} "
              + $"TRUE L6 requires OPERATOR EVOLUTION — species must change not just the graph "
              + $"but the fundamental TYPE of dynamics. Niche construction alone is insufficient."
            : "Niche construction does not create new carrier classes.";

        return new NicheConstructionMetrics.NicheConstructionReport(
            results, closesLoop, enablesL6, classification, verdict);
    }

    public static string HostileReview(NicheConstructionMetrics.NicheConstructionReport report)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("HOSTILE REVIEW: Does niche construction actually help?");
        sb.AppendLine();
        sb.AppendLine("  THE DEEP PROBLEM:");
        sb.AppendLine("  - Graph modification changes the GRAPH, not the OPERATOR TYPE.");
        sb.AppendLine("  - A modified graph Laplacian is still a graph Laplacian.");
        sb.AppendLine("  - Its eigenmodes are still sinusoidal (just different ones).");
        sb.AppendLine("  - No new CARRIER CLASSES emerge from graph modification alone.");
        sb.AppendLine();
        sb.AppendLine("  THE OPERATOR BARRIER:");
        sb.AppendLine("  - Carrier CLASS is determined by the OPERATOR FAMILY.");
        sb.AppendLine("  - Graph Laplacian → sinusoidal eigenmodes (Fourier class).");
        sb.AppendLine("  - NLS operator → solitons (nonlinear class).");
        sb.AppendLine("  - To create a NEW class, species must change the OPERATOR FAMILY.");
        sb.AppendLine("  - This requires: L_Q → L_magnetic or L_Q → L(ψ) (nonlinear).");
        sb.AppendLine("  - Species cannot do this by modifying edges.");
        sb.AppendLine();
        sb.AppendLine("  THE L6 BOTTLENECK IS DEEPER THAN EXPECTED:");
        sb.AppendLine("  - It's not about graph topology.");
        sb.AppendLine("  - It's about OPERATOR TOPOLOGY.");
        sb.AppendLine("  - L6 requires OPERATOR EVOLUTION.");
        sb.AppendLine();
        return sb.ToString();
    }
}
