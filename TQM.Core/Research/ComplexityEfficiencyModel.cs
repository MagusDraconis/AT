namespace TQM.Core.Research;

/// <summary>
/// Tests whether Quantum Reality (Rev∩SC) is the optimal
/// finite-complexity architecture or can be surpassed.
/// TQM-X030: Quantum Optimality Principle
/// </summary>
public static class ComplexityEfficiencyModel
{
    public static List<QuantumOptimalityMetrics.OptimalityTest> TestArchitectures()
    {
        return new List<QuantumOptimalityMetrics.OptimalityTest>
        {
            // QUANTUM REALITY BASELINE.
            new("Quantum Reality (Rev∩SC)",
                1.0, 1.0, 7, 7.0,
                false, "BASELINE: naturally achieves 7 classes via Rev∩SC"),

            // NEAR-QUANTUM: slightly perturbed.
            new("Near-Quantum (R=0.9, S=1.0)",
                0.9, 1.0, 5, 4.5,
                false, "Reduced R → loss of 2 carrier classes due to decoherence"),

            new("Near-Quantum (R=1.0, S=0.9)",
                1.0, 0.9, 4, 3.6,
                false, "Reduced S → loss of 3 classes due to structural instability"),

            // HYBRID ARCHITECTURES.
            new("Hybrid (Linear + NLS)",
                0.8, 0.8, 7, 5.6,
                false, "Same classes as Quantum but lower R,S → lower efficiency"),

            new("Universal Hybrid (All Classes)",
                0.5, 0.5, 16, 8.0,
                true, "MORE classes than Quantum (16 vs 7) but lower R,S → fragile persistence"),

            // ENGINEERED: push above Quantum.
            new("Engineered Optimal (16 classes, R=0.9, S=0.9)",
                0.9, 0.9, 16, 14.4,
                true, "THEORETICALLY beats Quantum on raw complexity density. But requires engineering all 16 classes."),

            // EXTREME: maximum diversity, minimal stability.
            new("Max Diversity (16 classes, R=0.3, S=0.3)",
                0.3, 0.3, 16, 4.8,
                false, "High class count but low R,S → information decays rapidly → ineffective"),
        };
    }
}
