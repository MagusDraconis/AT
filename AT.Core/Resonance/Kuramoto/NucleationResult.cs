namespace AT.Core.Resonance.Kuramoto;

/// <summary>
/// Result of a single nucleation experiment.
/// </summary>
public sealed class NucleationResult
{
    public int NucleusSize { get; }
    public bool Survived { get; }       // nucleus maintained R > 0.8
    public bool Grew { get; }           // final cluster size > nucleus size
    public int FinalClusterSize { get; }
    public double FinalR { get; }
    public int Lifetime { get; }        // iterations until decay, or total if survived
    public double GrowthRate { get; }   // (final - initial size) / iterations

    public NucleationResult(int nucleusSize, bool survived, bool grew,
        int finalClusterSize, double finalR, int lifetime, double growthRate)
    {
        NucleusSize = nucleusSize;
        Survived = survived;
        Grew = grew;
        FinalClusterSize = finalClusterSize;
        FinalR = finalR;
        Lifetime = lifetime;
        GrowthRate = growthRate;
    }

    public override string ToString() =>
        $"Nc={NucleusSize}: {(Survived ? "SURVIVED" : "DECAYED")} {(Grew ? "GREW" : "stable")} " +
        $"final_size={FinalClusterSize} R={FinalR:F3}";
}
