using AT.Core.Temporal;

namespace AT.Core.Resonance.Kuramoto;

/// <summary>
/// Analyzes the relationship between local oscillator density and
/// resonance condensation. Estimates the critical density threshold ρc
/// above which stable condensates form.
/// </summary>
public static class LocalDensityThresholdAnalyzer
{
    /// <summary>
    /// Records density statistics for a single condensate birth event.
    /// </summary>
    public sealed record CondensateBirthRecord(
        double LocalDensity,
        double LocalR,
        int BirthIteration,
        int Lifetime,
        int CondensateSize,
        double K,
        int N,
        string Placement);

    /// <summary>
    /// Runs a threshold analysis simulation and returns birth records
    /// for all condensates that formed.
    /// </summary>
    public static List<CondensateBirthRecord> Analyze(
        TemporalNetwork network,
        TemporalSimulation sim,
        LocalDensityField densityField,
        ResonanceCondensationAnalyzer condAnalyzer,
        int n,
        double k,
        string placement,
        int totalIterations,
        int checkpointInterval = 500)
    {
        var records = new List<CondensateBirthRecord>();
        var seenCondensateIds = new HashSet<int>();

        for (int iter = 0; iter < totalIterations; iter++)
        {
            sim.Step();

            if ((iter + 1) % checkpointInterval == 0 || iter == totalIterations - 1)
            {
                densityField.Compute(network, neighborhoodCells: 1);
                var condensates = condAnalyzer.DetectAndTrack(densityField, iter + 1);

                // Check for new condensates (not seen before).
                foreach (var c in condensates)
                {
                    if (!seenCondensateIds.Contains(c.Id) && c.BirthIteration == iter + 1)
                    {
                        seenCondensateIds.Add(c.Id);

                        // Compute average density in the condensate's cells.
                        double avgDensity = 0;
                        int cellCount = 0;
                        foreach (var (gx, gy) in c.Cells)
                        {
                            avgDensity += densityField.GetLocalDensity(gx, gy);
                            cellCount++;
                        }

                        if (cellCount > 0)
                            avgDensity /= cellCount;

                        records.Add(new CondensateBirthRecord(
                            avgDensity,
                            c.MeanLocalR,
                            c.BirthIteration,
                            c.Lifetime,
                            c.CellCount,
                            k,
                            n,
                            placement));
                    }
                }
            }
        }

        return records;
    }
}
