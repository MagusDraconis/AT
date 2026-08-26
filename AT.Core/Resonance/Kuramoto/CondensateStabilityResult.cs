namespace AT.Core.Resonance.Kuramoto;

/// <summary>
/// Result of a single condensate perturbation experiment.
/// </summary>
public sealed class CondensateStabilityResult
{
    public string PerturbationType { get; }
    public double PerturbationLevel { get; }
    public bool Survived { get; }
    public int CondensatesBefore { get; }
    public int CondensatesAfter { get; }
    public double LocalRBefore { get; }
    public double LocalRAfter { get; }
    public int RecoveryIterations { get; } // iterations until first condensate reforms, -1 if never
    public double LifetimeReduction { get; } // fraction of original lifetime lost
    public bool Fragmented { get; } // did one condensate split into multiple?
    public bool Merged { get; } // did multiple condensates merge?

    public CondensateStabilityResult(
        string perturbationType,
        double perturbationLevel,
        bool survived,
        int condensatesBefore,
        int condensatesAfter,
        double localRBefore,
        double localRAfter,
        int recoveryIterations,
        double lifetimeReduction,
        bool fragmented,
        bool merged)
    {
        PerturbationType = perturbationType;
        PerturbationLevel = perturbationLevel;
        Survived = survived;
        CondensatesBefore = condensatesBefore;
        CondensatesAfter = condensatesAfter;
        LocalRBefore = localRBefore;
        LocalRAfter = localRAfter;
        RecoveryIterations = recoveryIterations;
        LifetimeReduction = lifetimeReduction;
        Fragmented = fragmented;
        Merged = merged;
    }

    public override string ToString() =>
        $"{PerturbationType} {PerturbationLevel:P0}: " +
        $"{(Survived ? "SURVIVED" : "DESTROYED")} " +
        $"({CondensatesBefore}→{CondensatesAfter} condensates, " +
        $"R_local {LocalRBefore:F3}→{LocalRAfter:F3}, recovery {RecoveryIterations} iter)";
}
