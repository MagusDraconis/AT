namespace TQM.Core.Resonance.Kuramoto;

/// <summary>
/// Result of a single condensate interaction experiment between two condensates.
/// </summary>
public sealed class CondensateInteractionResult
{
    public double InitialSeparation { get; }
    public double InitialPhaseOffset { get; }
    public double CouplingK { get; }

    public double FinalSeparation { get; }
    public double FinalPhaseDifference { get; }
    public double FinalFrequencyDifference { get; }

    public string InteractionType { get; } // Attractive, Repulsive, Merging, Neutral, Oscillatory

    public bool DidMerge { get; }
    public bool DidFragmentation { get; }
    public int MergeIteration { get; } // iteration where merge happened, -1 if never
    public double CoherenceTransfer { get; } // change in local R difference between clusters
    public double SeparationChange { get; } // final - initial separation (negative = attract)

    public CondensateInteractionResult(
        double initialSeparation,
        double initialPhaseOffset,
        double couplingK,
        double finalSeparation,
        double finalPhaseDifference,
        double finalFrequencyDifference,
        string interactionType,
        bool didMerge,
        bool didFragmentation,
        int mergeIteration,
        double coherenceTransfer,
        double separationChange)
    {
        InitialSeparation = initialSeparation;
        InitialPhaseOffset = initialPhaseOffset;
        CouplingK = couplingK;
        FinalSeparation = finalSeparation;
        FinalPhaseDifference = finalPhaseDifference;
        FinalFrequencyDifference = finalFrequencyDifference;
        InteractionType = interactionType;
        DidMerge = didMerge;
        DidFragmentation = didFragmentation;
        MergeIteration = mergeIteration;
        CoherenceTransfer = coherenceTransfer;
        SeparationChange = separationChange;
    }

    public override string ToString() =>
        $"{InteractionType}: sep {InitialSeparation:F3}→{FinalSeparation:F3} " +
        $"Δsep={SeparationChange:+0.000;-0.000} merge={(DidMerge ? "YES" : "no")}";
}
