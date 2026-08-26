namespace AT.Core.Resonance.Kuramoto;

/// <summary>
/// Stores the complete high-resolution timeline of a single condensate birth,
/// with state recorded at every iteration.
/// </summary>
public sealed class CondensationBirthProfile
{
    public int CondensateId { get; }
    public int BirthIteration { get; }

    /// <summary>
    /// Iteration-by-iteration state: [iteration, localR, localDensity, clusterSize, phaseVariance].
    /// </summary>
    public List<(int Iteration, double LocalR, double LocalDensity, int ClusterSize, double PhaseVariance)> Timeline { get; } = new();

    public int PreBirthCount { get; set; }
    public int PostBirthCount { get; set; }

    public CondensationBirthProfile(int condensateId, int birthIteration)
    {
        CondensateId = condensateId;
        BirthIteration = birthIteration;
    }

    /// <summary>
    /// Analyzes the birth profile and classifies the condensation mechanism.
    /// </summary>
    public (string Mechanism, double PeakDRDT, int TransitionIter, double PrecursorR, int PrecursorIter) Analyze()
    {
        if (Timeline.Count < 10)
            return ("Unknown", 0, -1, 0, -1);

        // Find peak dR/dt.
        double peakDRDT = 0;
        int peakIdx = 0;
        for (int i = 1; i < Timeline.Count; i++)
        {
            double drdt = Timeline[i].LocalR - Timeline[i - 1].LocalR;
            if (drdt > peakDRDT) { peakDRDT = drdt; peakIdx = i; }
        }

        // Find when R first exceeds 0.3 (precursor).
        int precursorIdx = -1;
        double precursorR = 0;
        for (int i = 0; i < Timeline.Count; i++)
        {
            if (Timeline[i].LocalR > 0.3)
            {
                precursorIdx = i;
                precursorR = Timeline[i].LocalR;
                break;
            }
        }

        // Classify.
        string mechanism;
        if (peakDRDT > 0.3)
            mechanism = "Critical Transition (discontinuous)";
        else if (peakDRDT > 0.1)
            mechanism = "Accelerated Growth";
        else
            mechanism = "Continuous Growth";

        return (mechanism, peakDRDT, Timeline[peakIdx].Iteration, precursorR,
            precursorIdx >= 0 ? Timeline[precursorIdx].Iteration : -1);
    }
}
