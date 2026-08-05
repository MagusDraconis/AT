namespace TQM.Core.Resonance.Theory;

/// <summary>
/// Statistical analysis of nucleation bias from stored Θ memory.
/// Computes probability ratios, spatial correlations, and mutual
/// information between memory patterns and nucleation events.
///
/// TQM-131: Information Back-Reaction on Proto-Matter Genesis
/// </summary>
public static class ChargeCreationBias
{
    // ══════════════════════════════════════════════════════════════════
    // Estimate nucleation bias from memory pattern.
    // ══════════════════════════════════════════════════════════════════

    public static GenesisMemoryProfile.NucleationBias EstimateBias(
        double[] memoryPattern,         // Θ(x) stored pattern
        double[] nucleationSites,       // x-coordinates where Q nucleated
        double[] controlSites,          // nucleation sites without memory
        int nBins = 20)
    {
        if (memoryPattern.Length == 0 || nucleationSites.Length == 0)
            return new GenesisMemoryProfile.NucleationBias(
                "None", 1.0, 0, 0, 0, false, "None");

        // Correlation: do nucleations prefer regions of high |Θ|?
        double corr = 0;
        int nCorr = Math.Min(memoryPattern.Length, nucleationSites.Length);
        double mpMean = memoryPattern.Average();
        double nsMean = nucleationSites.Average();
        double mpVar = memoryPattern.Average(x => (x - mpMean) * (x - mpMean));
        double nsVar = nucleationSites.Average(x => (x - nsMean) * (x - nsMean));

        if (mpVar > 1e-10 && nsVar > 1e-10)
        {
            double cov = 0;
            for (int i = 0; i < nCorr; i++)
                cov += (memoryPattern[i] - mpMean) * (nucleationSites[i] - nsMean);
            corr = cov / nCorr / Math.Sqrt(mpVar * nsVar);
        }

        // Bias factor: ratio of nucleation probability with memory vs control.
        double probWithMem = nucleationSites.Length > 0
            ? (double)nucleationSites.Count(x => x > 0) / nucleationSites.Length : 0;
        double probControl = controlSites.Length > 0
            ? (double)controlSites.Count(x => x > 0) / controlSites.Length : 1e-6;
        double bias = probWithMem / Math.Max(probControl, 1e-10);

        // Mutual information approximation.
        double mi = Math.Abs(corr) > 0.1
            ? -0.5 * Math.Log(1.0 - Math.Min(corr * corr, 0.99)) : 0;

        bool significant = Math.Abs(bias - 1.0) > 0.2 && Math.Abs(corr) > 0.15;
        string direction = bias > 1.1 ? "Enhance"
                         : bias < 0.9 ? "Suppress" : "None";

        return new GenesisMemoryProfile.NucleationBias(
            "Memory", bias, corr, 0, mi, significant, direction);
    }

    // ══════════════════════════════════════════════════════════════════
    // Simulate nucleation sites biased by memory pattern.
    // ══════════════════════════════════════════════════════════════════

    public static (double[] sitesWithMem, double[] sitesControl)
        SimulateNucleationWithMemory(
        double[] memoryPattern, int nSites, double biasStrength = 0.3)
    {
        var rng = new Random(42);
        int nBins = memoryPattern.Length;
        var withMem = new double[nSites];
        var control = new double[nSites];

        for (int i = 0; i < nSites; i++)
        {
            // Control: uniform random.
            control[i] = rng.NextDouble();

            // With memory: biased toward high |Θ| regions.
            int memIdx = i % nBins;
            double memVal = Math.Abs(memoryPattern[memIdx]);
            double memBias = 0.5 + biasStrength * (memVal - 0.5);
            withMem[i] = Math.Clamp(memBias + NextGaussian(rng) * 0.1, 0, 1);
        }

        return (withMem, control);
    }

    // ══════════════════════════════════════════════════════════════════
    // Modified nucleation condition with memory term.
    // ══════════════════════════════════════════════════════════════════

    public static string DeriveModifiedNucleationCondition(double biasFactor)
    {
        double beta = Math.Clamp(biasFactor - 1.0, -0.5, 0.5);
        string sign = beta > 0 ? "+" : "";

        return
            "MODIFIED NUCLEATION CONDITION:\n\n" +
            "  Original: c₀·M₀ > D_R/w²\n" +
            $"  Modified: c₀·M₀·(1 {sign} {beta:F3}·|Θ|²) > D_R/w²\n\n" +
            "The |Θ|² term represents the local coherence field intensity.\n" +
            "In regions of strong stored memory (high |Θ|), nucleation is:\n" +
            $"  {(beta > 0 ? "ENHANCED" : "SUPPRESSED")} by factor (1 {sign} {beta:F3}·|Θ|²).\n\n" +
            "Physical mechanism: stored phase patterns create local\n" +
            "coherence gradients that modify the effective reaction rate.\n" +
            "Higher |Θ| means oscillators are already partially aligned,\n" +
            "reducing the nucleation barrier.";
    }

    // ══════════════════════════════════════════════════════════════════
    // Test memory survival through re-nucleation.
    // ══════════════════════════════════════════════════════════════════

    public static double MemorySurvivalProbability(
        double density, double nucleationRate, double damping = 0.1)
    {
        // Memory survives if re-nucleation doesn't completely disrupt Θ.
        // Survival ∝ exp(−nucleation_rate · disruption_per_event).
        double disruption = 0.1 / Math.Max(density, 0.1);
        return Math.Exp(-nucleationRate * disruption / Math.Max(damping, 1e-10));
    }

    private static double NextGaussian(Random rng)
    {
        double u1 = 1.0 - rng.NextDouble();
        double u2 = 1.0 - rng.NextDouble();
        return Math.Sqrt(-2.0 * Math.Log(Math.Max(u1, 1e-10))) *
               Math.Cos(2.0 * Math.PI * u2);
    }
}
