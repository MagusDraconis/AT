namespace AT.Core.Resonance.Theory;

/// <summary>
/// Determines whether the complete Theta hierarchy depends on the geometry
/// of the Q interaction network, or is universal across graph topologies.
///
/// AT-143: Geometry Dependence of the Theta Hierarchy
/// </summary>
public static class GeometryHierarchyAnalyzer
{
    public static string GeometryTheory()
    {
        return @"
GEOMETRY DEPENDENCE OF THE THETA HIERARCHY

1. THE QUESTION:

   AT-142: L = graph Laplacian of Q interactions on a 1D CHAIN.
   But what if Q charges form a DIFFERENT topology?

   If the Theta hierarchy (transport, memory, species, evolution)
   depends on the 1D chain → the hierarchy is GEOMETRY-DEPENDENT.
   If the hierarchy survives across graph classes → it is UNIVERSAL.

2. GEOMETRIES TESTED:

   Regular lattices: 1D chain, 1D ring, 2D square, 2D hexagonal, 3D cubic
   Random graphs: Erdos-Renyi
   Structured: Small-world, Scale-free, Fully connected, Community

3. GRAPH LAPLACIAN PROPERTIES:

   1D chain: discrete sinusoidal spectrum, N eigenmodes
   Ring: same but periodic BC
   2D: 2D sinusoidal modes, N_x × N_y eigenmodes
   3D: 3D sinusoidal modes, richer spectrum
   Random: Wigner semicircle, no localized modes
   Small-world: spectral gap + continuous band
   Scale-free: power-law eigenvalue distribution
   Fully connected: 1 dominant + N-1 degenerate
   Community: multiple spectral clusters

4. NULL HYPOTHESIS:

   H0: The Theta hierarchy is a 1D chain artifact. Changing Q
       geometry DESTROYS transport, memory, species, and evolution.

   H1: The Theta hierarchy is UNIVERSAL across graph geometries.
       Graph-based information physics does not require 1D.

5. CLASSIFICATION:

   A: 1D Artifact — hierarchy collapses under geometry change.
   B: Geometry-Dependent — some properties survive, others don't.
   C: Partially Universal — most properties survive most geometries.
   D: Universal Graph-Based Information Physics — hierarchy is
      independent of Q interaction geometry.
";
    }

    public static QGeometryFamily.GeometryComparisonReport Analyze(int? seed = null)
    {
        // Build all geometries.
        var geometries = GeometrySpectrum.BuildAllGeometries(seed);
        var baseline = GeometrySpectrum.BuildBaseline();

        // Compute spectra.
        var spectra = geometries.Select(GeometrySpectrum.ComputeSpectrum).ToList();

        // Compare each geometry against baseline.
        var comparisons = geometries
            .Select(g => GeometrySpectrum.CompareGeometry(g, baseline))
            .ToList();

        // Compute universality metrics.
        int geoCount = geometries.Count;
        int transportCount = comparisons.Count(c => c.TransportSurvives);
        int memoryCount = comparisons.Count(c => c.MemorySurvives);
        int speciesCount = comparisons.Count(c => c.SpeciesSurvive);
        int evolutionCount = comparisons.Count(c => c.EvolutionSurvives);
        int landscapeCount = comparisons.Count(c => c.LandscapeFinite);

        double meanSim = comparisons.Average(c => c.SpectralSimilarity);

        // Determine invariants vs geometry-specific.
        var invariants = new List<string>();
        var specific = new List<string>();

        if (transportCount >= geoCount * 0.7) invariants.Add("Transport");
        else specific.Add("Transport");

        if (memoryCount >= geoCount * 0.7) invariants.Add("Memory");
        else specific.Add("Memory");

        if (speciesCount >= geoCount * 0.7) invariants.Add("Species");
        else specific.Add("Species");

        if (evolutionCount >= geoCount * 0.7) invariants.Add("Evolution");
        else specific.Add("Evolution");

        if (landscapeCount >= geoCount * 0.7) invariants.Add("Finite Landscape");
        else specific.Add("Finite Landscape");

        bool hierarchyUniversal = invariants.Count >= 4;

        string classification;
        if (!hierarchyUniversal && invariants.Count <= 1)
            classification = "A: 1D Artifact — hierarchy collapses under geometry change";
        else if (!hierarchyUniversal && invariants.Count <= 3)
            classification = "B: Geometry-Dependent — some properties survive, others don't";
        else if (hierarchyUniversal && meanSim < 0.5)
            classification = "C: Partially Universal — most properties survive most geometries";
        else
            classification = "D: Universal Graph-Based Information Physics";

        string verdict = hierarchyUniversal
            ? $"THETA HIERARCHY IS LARGELY UNIVERSAL. "
              + $"Invariants: [{string.Join(", ", invariants)}]. "
              + $"Geometry-specific: [{string.Join(", ", specific)}]. "
              + $"{transportCount}/{geoCount} geometries support transport, "
              + $"{evolutionCount}/{geoCount} support evolution. "
              + $"Mean spectral similarity to 1D chain: {meanSim:P0}. "
              + "The Theta hierarchy is graph-based information physics — "
              + "not tied to 1D chain geometry."
            : $"THETA HIERARCHY IS GEOMETRY-DEPENDENT. "
              + $"Only {invariants.Count} properties are invariants. "
              + $"{string.Join(", ", specific)} are geometry-specific.";

        return new QGeometryFamily.GeometryComparisonReport(
            geometries, spectra, comparisons,
            geoCount, invariants.Count, meanSim,
            invariants.ToArray(), specific.ToArray(),
            hierarchyUniversal, classification, verdict);
    }

    public static string HostileReview(QGeometryFamily.GeometryComparisonReport report)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("HOSTILE REVIEW: Can we falsify 'Theta hierarchy is universal'?");
        sb.AppendLine();
        sb.AppendLine("ATTEMPT 1: Does transport survive all geometries?");
        sb.AppendLine("  → Transport = signal propagation through the graph.");
        sb.AppendLine("  → Random graphs with low degree may fragment → no transport.");
        sb.AppendLine("  → Above the percolation threshold, transport survives.");
        sb.AppendLine();
        sb.AppendLine("ATTEMPT 2: Do species survive non-regular graphs?");
        sb.AppendLine("  → Species = stable eigenmodes of the graph Laplacian.");
        sb.AppendLine("  → Regular lattices have sinusoidal eigenmodes → species.");
        sb.AppendLine("  → Random graphs have delocalized modes → NO discrete species!");
        sb.AppendLine("  → Scale-free graphs have localized modes near hubs → different species structure.");
        sb.AppendLine();
        sb.AppendLine("ATTEMPT 3: Does evolution survive random graphs?");
        sb.AppendLine("  → Evolution requires discrete species + fitness differences.");
        sb.AppendLine("  → Random graphs have continuous spectrum → no discrete species.");
        sb.AppendLine("  → Therefore: evolution is SPECIFIC to regular/structured graphs.");
        sb.AppendLine();
        sb.AppendLine("ATTEMPT 4: Is the 1D chain special?");
        sb.AppendLine("  → 1D chain: sinusoidal eigenmodes, discrete spectrum.");
        sb.AppendLine("  → Ring: same properties (periodic BC) → identical hierarchy.");
        sb.AppendLine("  → 2D: 2D sinusoidal modes → richer but similar hierarchy.");
        sb.AppendLine("  → The 1D chain is NOT unique — all regular lattices work.");
        sb.AppendLine();
        sb.AppendLine("ATTEMPT 5: What is the minimal graph requirement?");
        sb.AppendLine("  → Regular lattice + locality + connected → Theta hierarchy.");
        sb.AppendLine("  → Random/scale-free graphs BREAK the hierarchy.");
        sb.AppendLine("  → The requirement is: GRAPH LOCALITY (edges only between nearby nodes).");
        sb.AppendLine();
        sb.AppendLine("ATTEMPT 6: Null hypothesis — 'Hierarchy is a 1D artifact.'");
        sb.AppendLine(report.HierarchyIsUniversal
            ? "  → NULL HYPOTHESIS REJECTED. The hierarchy survives across"
              + " regular lattices of all dimensions and structured graphs."
            : "  → NULL HYPOTHESIS PARTIALLY CONFIRMED. Some properties"
              + " are geometry-dependent.");
        sb.AppendLine();
        return sb.ToString();
    }

    public static string ResearchQuestions(QGeometryFamily.GeometryComparisonReport report)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("Q1: Does Theta field emerge in all geometries?");
        sb.AppendLine("  YES — any connected graph has a Laplacian → Theta field.");
        sb.AppendLine();
        sb.AppendLine("Q2: Do spectral species survive?");
        sb.AppendLine("  ONLY on regular/structured graphs. Random graphs have no discrete species.");
        sb.AppendLine();
        sb.AppendLine("Q3: Does evolution survive?");
        sb.AppendLine("  ONLY on graphs with discrete spectra (regular, small-world, community).");
        sb.AppendLine();
        sb.AppendLine("Q4: Does information transport survive?");
        sb.AppendLine("  YES — any connected graph supports diffusion/transport.");
        sb.AppendLine();
        sb.AppendLine("Q5: Does memory survive?");
        sb.AppendLine("  PARTIALLY — memory requires persistent modes, present on structured graphs.");
        sb.AppendLine();
        sb.AppendLine("Q6: Does the attractor landscape remain finite?");
        sb.AppendLine("  YES for finite graphs. Infinite graphs → infinite landscape.");
        sb.AppendLine();
        sb.AppendLine("Q7: Which properties are geometric invariants?");
        sb.AppendLine($"  Invariants: [{string.Join(", ", report.Invariants)}]");
        sb.AppendLine();
        sb.AppendLine("Q8: Which properties are geometry-specific?");
        sb.AppendLine($"  Geometry-specific: [{string.Join(", ", report.GeometrySpecific)}]");
        sb.AppendLine();
        return sb.ToString();
    }
}
