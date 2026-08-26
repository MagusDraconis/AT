using AT.Core.Temporal;

namespace AT.Core.Resonance.Kuramoto;

/// <summary>
/// Stages in a condensate's lifecycle.
/// </summary>
public enum LifecycleStage { Birth, Growth, Stable, Merger, Split, Decay, Death }

/// <summary>
/// Tracks a single condensate's evolution from birth to death.
/// </summary>
public sealed class LifecycleProfile
{
    public int CondensateId { get; }
    public LifecycleStage FinalStage { get; set; } = LifecycleStage.Stable;
    public string LifecycleClass { get; set; } = "Unknown"; // Growing, Stable, Shrinking, etc.

    public record Snapshot(int Iteration, int Size, double Density, double LocalR, bool Merged, bool Split);

    public List<Snapshot> History { get; } = new();
    public int BirthIteration => History.Count > 0 ? History[0].Iteration : -1;
    public int DeathIteration => History.Count > 0 ? History[^1].Iteration : -1;
    public int Lifetime => DeathIteration - BirthIteration;

    public LifecycleProfile(int id) => CondensateId = id;

    public void Classify()
    {
        if (History.Count < 3) { LifecycleClass = "Transient"; return; }

        int startSize = History[0].Size;
        int endSize = History[^1].Size;
        double growthRate = (endSize - startSize) / (double)Lifetime;

        if (History.Any(s => s.Merged)) LifecycleClass = "Merger";
        else if (History.Any(s => s.Split)) LifecycleClass = "Split";
        else if (growthRate > 0.5) LifecycleClass = "Growing";
        else if (growthRate < -0.5) LifecycleClass = "Shrinking";
        else if (endSize < 5) LifecycleClass = "Dying";
        else LifecycleClass = "Stable";
    }
}

/// <summary>
/// Analyzes the full lifecycle of all condensates in a simulation.
/// </summary>
public static class CondensateLifecycleAnalyzer
{
    public static List<LifecycleProfile> Analyze(
        int n, double k, double lambda, Random rng, int iterations = 5000, int checkpointInterval = 200)
    {
        var network = new TemporalNetwork(n);
        for (int i = 0; i < n; i++)
        {
            double phase = rng.NextDouble() * 2.0 * Math.PI;
            double freq = 0.5 + rng.NextDouble() * 1.5;
            var node = new TemporalNode(i, phase, freq);
            var cc = new[] { (0.1, 0.1), (0.9, 0.1), (0.5, 0.5), (0.1, 0.9), (0.9, 0.9) };
            var (cx, cy) = cc[i % 5];
            node.X = Math.Clamp(cx + NextGaussian(rng) * 0.02, 0, 1);
            node.Y = Math.Clamp(cy + NextGaussian(rng) * 0.02, 0, 1);
            network.AddNode(node);
        }

        network.Matrix.FillSpatialCoupling(network.Nodes, k, lambda, normalize: false);
        var sim = new TemporalSimulation(network) { TimeStep = 0.01, CouplingStrength = n };
        var densityField = new LocalDensityField(20);
        var condAnalyzer = new ResonanceCondensationAnalyzer
            { CondensationThreshold = 0.80, MinCondensateCells = 2, OverlapThreshold = 0.3 };

        var profiles = new Dictionary<int, LifecycleProfile>();
        var activeIds = new HashSet<int>();
        int totalChecks = iterations / checkpointInterval;

        for (int iter = 0; iter < iterations; iter++)
        {
            sim.Step();

            if ((iter + 1) % checkpointInterval == 0 || iter == iterations - 1)
            {
                densityField.Compute(network, neighborhoodCells: 1);
                var condensates = condAnalyzer.DetectAndTrack(densityField, iter + 1);
                var currentIds = new HashSet<int>(condensates.Select(c => c.Id));

                foreach (var c in condensates)
                {
                    if (!profiles.ContainsKey(c.Id))
                        profiles[c.Id] = new LifecycleProfile(c.Id);

                    double avgDens = 0;
                    foreach (var (gx, gy) in c.Cells)
                        avgDens += densityField.GetLocalDensity(
                            Math.Clamp(gx, 0, 19), Math.Clamp(gy, 0, 19));
                    avgDens /= Math.Max(1, c.Cells.Count);

                    // Check for mergers: was this created from multiple previous condensates?
                    bool merged = false;
                    if (activeIds.Count > 0 && c.Id > activeIds.Max())
                    {
                        // If the new ID is larger than any previous, likely a merger.
                        // Multiple old condensates disappeared when this appeared.
                        merged = currentIds.Count < activeIds.Count;
                    }

                    profiles[c.Id].History.Add(new LifecycleProfile.Snapshot(
                        iter + 1, c.CellCount, avgDens, c.MeanLocalR, merged, false));
                }

                // Detect deaths: active IDs that disappeared.
                foreach (int deadId in activeIds.Except(currentIds))
                {
                    if (profiles.ContainsKey(deadId))
                        profiles[deadId].FinalStage = LifecycleStage.Death;
                }

                activeIds = currentIds;
            }
        }

        // Classify all profiles.
        foreach (var profile in profiles.Values)
            profile.Classify();

        return profiles.Values.ToList();
    }

    private static double NextGaussian(Random rng)
    {
        double u1 = 1.0 - rng.NextDouble(), u2 = 1.0 - rng.NextDouble();
        return Math.Sqrt(-2.0 * Math.Log(Math.Max(u1, 1e-15))) * Math.Cos(2.0 * Math.PI * u2);
    }
}
