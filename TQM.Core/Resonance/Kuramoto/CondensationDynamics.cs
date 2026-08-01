namespace TQM.Core.Resonance.Kuramoto;

/// <summary>
/// Stages of condensate formation.
/// </summary>
public enum CondensationStage
{
    PreCondensation,   // no coherent structure
    SeedFormation,     // first local coherence detected
    Growth,            // cluster expanding
    CriticalTransition,// sharp increase in coherence
    Maturation,        // stable condensate formed
    Decay              // condensate dissolving (if applicable)
}

/// <summary>
/// Records the state of a condensate at a single timestep during formation.
/// </summary>
public sealed record CondensationSnapshot(
    int Iteration,
    double LocalDensity,
    double LocalR,
    int ClusterSize,
    double PhaseVariance,
    double GrowthRate);

/// <summary>
/// Full timeline of a single condensate's formation.
/// </summary>
public sealed class CondensationTimeline
{
    public int CondensateId { get; }
    public int BirthIteration { get; }
    public List<CondensationSnapshot> Snapshots { get; } = new();
    public CondensationStage FinalStage { get; set; }
    public int PrecursorIteration { get; set; } = -1; // when precursor signal first detected
    public string BirthMechanism { get; set; } = "Unknown"; // Gradual, Critical, Cascade, Merger

    public CondensationTimeline(int condensateId, int birthIteration)
    {
        CondensateId = condensateId;
        BirthIteration = birthIteration;
    }

    /// <summary>
    /// Classifies the birth mechanism based on the timeline.
    /// </summary>
    public void Classify()
    {
        if (Snapshots.Count < 3) return;

        // Compute growth rate of local R.
        var rValues = Snapshots.Select(s => s.LocalR).ToList();
        double maxJump = 0;
        int jumpIdx = 0;
        for (int i = 1; i < rValues.Count; i++)
        {
            double jump = rValues[i] - rValues[i - 1];
            if (jump > maxJump) { maxJump = jump; jumpIdx = i; }
        }

        double midR = Snapshots[Snapshots.Count / 2].LocalR;
        double startR = Snapshots[0].LocalR;
        double endR = Snapshots[^1].LocalR;

        if (maxJump > 0.5)
            BirthMechanism = "Critical Transition";
        else if (endR - startR > 0.6 && maxJump < 0.3)
            BirthMechanism = "Gradual Accumulation";
        else if (Snapshots.Any(s => s.LocalR > 0.8 && s.ClusterSize > 10))
            BirthMechanism = "Cascade Synchronization";
        else
            BirthMechanism = "Gradual Accumulation";

        // Find precursor: when local R first exceeds 0.3.
        for (int i = 0; i < Snapshots.Count; i++)
        {
            if (Snapshots[i].LocalR > 0.3)
            {
                PrecursorIteration = Snapshots[i].Iteration;
                break;
            }
        }

        // Determine final stage.
        if (endR > 0.9)
            FinalStage = CondensationStage.Maturation;
        else if (endR > 0.7)
            FinalStage = CondensationStage.Growth;
        else if (endR > startR)
            FinalStage = CondensationStage.SeedFormation;
        else
            FinalStage = CondensationStage.Decay;
    }
}
