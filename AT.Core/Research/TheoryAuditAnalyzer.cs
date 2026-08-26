namespace AT.Core.Research;

/// <summary>
/// Performs a complete audit of the AT framework:
/// identifies hidden assumptions, evaluates alternatives,
/// and ranks future research directions.
///
/// AT-X001: Alternative Foundations Audit
/// </summary>
public static class TheoryAuditAnalyzer
{
    public static AssumptionRegistry.AuditReport PerformAudit()
    {
        var assumptions = RegistryAssumptions();
        var alternatives = AlternativeFoundation.EvaluateAlternatives();

        var critical = assumptions
            .Where(a => a.ImportanceScore >= 8 && a.TestCoverage <= 3)
            .Select(a => a.Name).ToArray();

        var promising = assumptions
            .OrderByDescending(a => a.NoveltyPotential)
            .Take(5).Select(a => a.Name).ToArray();

        var pathDeps = new[]
        {
            "Pairwise interactions → graph Laplacian (no 3-body terms tested)",
            "1D chain → sinusoidal eigenmodes → species (higher-D richer)",
            "Static graph → L_Q constant (dynamic graphs unexplored)",
            "Linear operator → superposition → Hilbert space (nonlinear unexplored)",
            "Graph Laplacian → tight-binding identity (other operators different physics)",
        };

        bool biased = critical.Length >= 3;

        string verdict = biased
            ? $"AT FRAMEWORK IS BIASED. {critical.Length} critical assumptions with low test coverage. "
              + $"Most critical: [{string.Join(", ", critical)}]. "
              + $"Most promising unexplored directions: [{string.Join(", ", promising)}]. "
              + $"The framework reflects early choices (pairwise, linear, static, 1D) "
              + $"that were never systematically challenged."
            : "Framework appears well-audited.";

        return new AssumptionRegistry.AuditReport(
            assumptions, alternatives, critical, promising, pathDeps, biased, verdict);
    }

    private static List<AssumptionRegistry.TrackedAssumption> RegistryAssumptions()
    {
        return new List<AssumptionRegistry.TrackedAssumption>
        {
            new("Pairwise interactions", "Q charges interact only in pairs", "All AT",
                false, "Not tested", 9, 9, 1, 7,
                "CRITICAL: Test 3-body+ interactions via hypergraph Laplacian"),

            new("Local interactions", "J_ij = exp(-|x_i-x_j|/r_c)", "AT-142, 143",
                true, "Tested across 10 topologies (AT-143)", 8, 7, 6, 5,
                "Well-tested: long-range breaks discrete species"),

            new("Static graph", "Q positions fixed → L_Q constant", "All AT",
                false, "Not tested", 10, 10, 1, 9,
                "HIGHEST PRIORITY: Dynamic graphs → L_Q(t) → open-ended innovation?"),

            new("Graph Laplacian L_Q", "L_Q = D - A as fundamental operator", "AT-142+",
                false, "Alternatives not tested", 10, 10, 2, 8,
                "Test alternatives: normalized, magnetic, fractional, nonlinear"),

            new("Linearity", "L(ψ₁+ψ₂)=L(ψ₁)+L(ψ₂)", "AT-140, 149",
                false, "Not tested", 9, 9, 1, 8,
                "Nonlinear L(ψ) could enable solitons, new physics"),

            new("Reversibility", "d/dt ||ψ||²=0 (norm conserved)", "AT-149-152",
                true, "Equivalent to unitarity (AT-152)", 10, 10, 8, 3,
                "Well-understood: equivalent to unitarity"),

            new("Euclidean distance", "Coupling depends on Euclidean |x_i-x_j|", "AT-142",
                false, "Not tested", 5, 3, 1, 6,
                "Test graph distance instead of Euclidean"),

            new("Symmetric coupling", "J_ij = J_ji", "All AT",
                false, "Not tested", 8, 8, 1, 7,
                "Directed graphs with J_ij≠J_ji unexplored"),

            new("1D primary focus", "Most AT tested on 1D chains", "AT-117-154",
                true, "2D/3D partially tested (AT-143)", 6, 7, 4, 6,
                "Higher dimensions have richer spectra"),

            new("Theta field", "Θ as intermediary between Q and information", "AT-128-133",
                false, "Optionality not tested", 4, 5, 3, 4,
                "Q→L_Q may suffice without Θ"),

            new("Spectral decomposition", "Species = eigenmodes of L_Q", "AT-133, 140",
                false, "Built into definition", 6, 7, 3, 5,
                "Circular: framework defines species as eigenmodes"),
        };
    }
}
