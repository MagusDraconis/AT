namespace AT.Core.Resonance.Theory;

/// <summary>
/// Information interaction analysis: computes overlaps, transfer entropy,
/// merger/cancellation detection, and entropy evolution for co-existing
/// Θ-field information patterns.
///
/// AT-132: Information Dynamics in the Θ Field
/// </summary>
public static class InformationInteractionProfile
{
    // ══════════════════════════════════════════════════════════════════
    // Compute pattern overlap.
    // ══════════════════════════════════════════════════════════════════

    public static double PatternOverlap(double[] a, double[] b)
    {
        if (a.Length == 0 || b.Length == 0) return 0;
        int n = Math.Min(a.Length, b.Length);
        double dot = 0, normA = 0, normB = 0;
        for (int i = 0; i < n; i++)
        {
            dot += a[i] * b[i];
            normA += a[i] * a[i];
            normB += b[i] * b[i];
        }
        double denom = Math.Sqrt(normA * normB);
        return denom > 1e-10 ? dot / denom : 0;
    }

    // ══════════════════════════════════════════════════════════════════
    // Simulate interaction between two patterns.
    // ══════════════════════════════════════════════════════════════════

    public static ThetaInformationPattern.PatternInteraction SimulateInteraction(
        ThetaInformationPattern.InfoPattern a,
        ThetaInformationPattern.InfoPattern b,
        double density, double damping = 0.1)
    {
        double initOverlap = PatternOverlap(a.Pattern, b.Pattern);

        // Co-evolution: patterns interact through the damped wave field.
        // Interaction strength depends on overlap and density.
        double interactionStrength = initOverlap * density * 0.5;

        // Simulated outcomes based on overlap sign and magnitude.
        double finalOverlap;
        string type;
        double te_AB = 0, te_BA = 0;
        double deltaHA = 0, deltaHB = 0;
        bool transformed;

        if (initOverlap > 0.8)
        {
            // Highly overlapping: reinforce each other.
            finalOverlap = Math.Min(initOverlap + interactionStrength * 0.1, 1.0);
            type = "Reinforce";
            te_AB = interactionStrength * 0.3;
            te_BA = interactionStrength * 0.3;
            deltaHA = -0.1; // entropy decreases (more ordered)
            deltaHB = -0.1;
            transformed = false;
        }
        else if (initOverlap < -0.5)
        {
            // Anti-correlated: cancel each other.
            finalOverlap = initOverlap + interactionStrength * 0.3;
            type = "Cancel";
            te_AB = interactionStrength * 0.5;
            te_BA = interactionStrength * 0.5;
            deltaHA = 0.3; // entropy increases (disruption)
            deltaHB = 0.3;
            transformed = true;
        }
        else if (Math.Abs(initOverlap) < 0.2 && density > 0.5)
        {
            // Orthogonal + high density: merge into composite.
            finalOverlap = interactionStrength * 0.5;
            type = "Merge";
            te_AB = interactionStrength * 0.4;
            te_BA = interactionStrength * 0.4;
            deltaHA = -0.05;
            deltaHB = -0.05;
            transformed = true;
        }
        else
        {
            // Low overlap: independent evolution.
            finalOverlap = initOverlap;
            type = "Independent";
            te_AB = interactionStrength * 0.05;
            te_BA = interactionStrength * 0.05;
            deltaHA = 0;
            deltaHB = 0;
            transformed = false;
        }

        double mi = Math.Abs(finalOverlap) > 0.2
            ? -0.5 * Math.Log(1.0 - Math.Min(finalOverlap * finalOverlap, 0.99)) : 0;

        return new ThetaInformationPattern.PatternInteraction(
            a.Name, b.Name, initOverlap, finalOverlap,
            mi, te_AB, te_BA, deltaHA, deltaHB,
            type, transformed,
            type switch
            {
                "Reinforce" => "Patterns amplify each other — constructive interference.",
                "Cancel" => "Anti-correlated patterns partially cancel.",
                "Merge" => "Patterns merge into composite state.",
                _ => "Patterns evolve independently."
            });
    }

    // ══════════════════════════════════════════════════════════════════
    // Compute information entropy of a pattern.
    // ══════════════════════════════════════════════════════════════════

    public static ThetaInformationPattern.InfoEntropyProfile ComputeEntropy(
        double[] pattern, string label, double density)
    {
        if (pattern.Length == 0)
            return new ThetaInformationPattern.InfoEntropyProfile(
                label, 0, 0, 0, 0, 0);

        // Shannon entropy from histogram of pattern values.
        int nBins = 10;
        var hist = new int[nBins];
        double min = pattern.Min(), max = pattern.Max();
        double range = max - min;
        if (range < 1e-10) range = 1.0;

        foreach (double v in pattern)
        {
            int bin = (int)((v - min) / range * nBins);
            if (bin >= nBins) bin = nBins - 1;
            if (bin < 0) bin = 0;
            hist[bin]++;
        }

        double entropy = 0;
        int total = pattern.Length;
        for (int b = 0; b < nBins; b++)
        {
            if (hist[b] > 0)
            {
                double p = (double)hist[b] / total;
                entropy -= p * Math.Log(p);
            }
        }

        // Production rate: −γ·I (damped) + density·|interaction|.
        double prodRate = -0.1 * entropy + density * 0.05;

        // Complexity: number of significant bins.
        int complexity = hist.Count(h => (double)h / total > 0.05);

        return new ThetaInformationPattern.InfoEntropyProfile(
            label, entropy, 0, 0, prodRate, complexity);
    }

    // ══════════════════════════════════════════════════════════════════
    // Generate test patterns.
    // ══════════════════════════════════════════════════════════════════

    public static ThetaInformationPattern.InfoPattern[] GenerateTestPatterns(int nPoints = 20)
    {
        return new[]
        {
            new ThetaInformationPattern.InfoPattern("A: In-Phase", "PhasePulse",
                Enumerable.Range(0, nPoints).Select(i => Math.Sin(2 * Math.PI * i / nPoints)).ToArray(),
                1.0, 1.0, 1),
            new ThetaInformationPattern.InfoPattern("B: Standing Wave", "StandingWave",
                Enumerable.Range(0, nPoints).Select(i => Math.Sin(4 * Math.PI * i / nPoints)).ToArray(),
                0.8, 2.0, 2),
            new ThetaInformationPattern.InfoPattern("C: Anti-Phase", "AntiPhase",
                Enumerable.Range(0, nPoints).Select(i => i < nPoints / 2 ? 1.0 : -1.0).ToArray(),
                1.0, 1.0, 1),
            new ThetaInformationPattern.InfoPattern("D: Gaussian Pulse", "Pulse",
                Enumerable.Range(0, nPoints).Select(i => Math.Exp(-Math.Pow((i - nPoints / 2.0) / (nPoints / 6.0), 2))).ToArray(),
                0.7, 0.5, 1),
        };
    }
}
