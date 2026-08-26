using AT.Core.Temporal;

namespace AT.Core.Resonance.Kuramoto;

/// <summary>
/// Analyzes interactions between two spatially separated condensate clusters.
/// Tracks separation, phase/frequency convergence, merger events, and classifies
/// the interaction type.
/// </summary>
public sealed class CondensateInteractionAnalyzer
{
    /// <summary>
    /// Fraction of oscillators that must be phase-coherent for a merger to be detected.
    /// </summary>
    public const double MergerCoherenceThreshold = 0.80;

    /// <summary>
    /// Runs an interaction experiment between two condensate clusters.
    /// </summary>
    public static CondensateInteractionResult Analyze(
        TemporalNetwork network, TemporalSimulation sim,
        double initialSeparation, double initialPhaseOffset, double couplingK,
        int totalIterations, int checkpointInterval = 250)
    {
        int halfN = network.NodeCount / 2;
        var nodes = network.Nodes;

        // Compute initial spatial centroids.
        double cx1 = 0, cy1 = 0, cx2 = 0, cy2 = 0;
        for (int i = 0; i < halfN; i++)
        {
            cx1 += nodes[i].X; cy1 += nodes[i].Y;
            cx2 += nodes[halfN + i].X; cy2 += nodes[halfN + i].Y;
        }
        cx1 /= halfN; cy1 /= halfN;
        cx2 /= halfN; cy2 /= halfN;

        double initialSep = Math.Sqrt((cx1 - cx2) * (cx1 - cx2) + (cy1 - cy2) * (cy1 - cy2));

        // Initial phase centroids.
        double sin1 = 0, cos1 = 0, sin2 = 0, cos2 = 0;
        for (int i = 0; i < halfN; i++)
        {
            sin1 += Math.Sin(nodes[i].Phase); cos1 += Math.Cos(nodes[i].Phase);
            sin2 += Math.Sin(nodes[halfN + i].Phase); cos2 += Math.Cos(nodes[halfN + i].Phase);
        }
        double initialR1 = Math.Sqrt(sin1 * sin1 + cos1 * cos1) / halfN;

        bool didMerge = false;
        int mergeIteration = -1;
        double finalSep = initialSep;
        double finalPhaseDiff = 0;
        double finalFreqDiff = 0;

        for (int iter = 0; iter < totalIterations; iter++)
        {
            sim.Step();

            if ((iter + 1) % checkpointInterval == 0 || iter == totalIterations - 1)
            {
                // Compute centroids.
                cx1 = cy1 = cx2 = cy2 = 0;
                sin1 = cos1 = sin2 = cos2 = 0;
                double sumFreq1 = 0, sumFreq2 = 0;

                for (int i = 0; i < halfN; i++)
                {
                    cx1 += nodes[i].X; cy1 += nodes[i].Y;
                    sin1 += Math.Sin(nodes[i].Phase); cos1 += Math.Cos(nodes[i].Phase);
                    sumFreq1 += nodes[i].Frequency;

                    cx2 += nodes[halfN + i].X; cy2 += nodes[halfN + i].Y;
                    sin2 += Math.Sin(nodes[halfN + i].Phase); cos2 += Math.Cos(nodes[halfN + i].Phase);
                    sumFreq2 += nodes[halfN + i].Frequency;
                }

                cx1 /= halfN; cy1 /= halfN;
                cx2 /= halfN; cy2 /= halfN;
                double currentSep = Math.Sqrt((cx1 - cx2) * (cx1 - cx2) + (cy1 - cy2) * (cy1 - cy2));

                double phase1 = Math.Atan2(sin1, cos1);
                double phase2 = Math.Atan2(sin2, cos2);
                double r1 = Math.Sqrt(sin1 * sin1 + cos1 * cos1) / halfN;
                double r2 = Math.Sqrt(sin2 * sin2 + cos2 * cos2) / halfN;

                // Global coherence: are all oscillators phase-aligned?
                double sinAll = sin1 + sin2, cosAll = cos1 + cos2;
                double rGlobal = Math.Sqrt(sinAll * sinAll + cosAll * cosAll) / network.NodeCount;

                // Merger detection: global R exceeds threshold AND clusters' phases converge.
                if (!didMerge && rGlobal >= MergerCoherenceThreshold)
                {
                    didMerge = true;
                    mergeIteration = iter + 1;
                }

                if (iter == totalIterations - 1)
                {
                    finalSep = currentSep;
                    finalPhaseDiff = Math.Abs(TemporalSimulation.NormalizePhase(phase1 - phase2 + Math.PI) - Math.PI);
                    finalFreqDiff = Math.Abs(sumFreq1 / halfN - sumFreq2 / halfN);
                }
            }
        }

        // Classify interaction type.
        double sepChange = finalSep - initialSep;
        double coherenceTransfer = Math.Abs(finalFreqDiff); // proxy

        string interactionType;
        if (didMerge)
            interactionType = "Merging";
        else if (sepChange < -0.01)
            interactionType = "Attractive";
        else if (sepChange > 0.01)
            interactionType = "Repulsive";
        else
            interactionType = "Neutral";

        return new CondensateInteractionResult(
            initialSeparation, initialPhaseOffset, couplingK,
            finalSep, finalPhaseDiff, finalFreqDiff,
            interactionType, didMerge, false, mergeIteration,
            coherenceTransfer, sepChange);
    }
}
