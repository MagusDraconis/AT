namespace TQM.Core.Research;

/// <summary>
/// Simulates operator populations evolving through meta-operator dynamics.
/// TQM-X025: First L6 Simulation
/// </summary>
public static class OperatorEcology
{
    /// <summary>
    /// Simulate meta-operator evolution for N generations.
    /// Each generation: apply meta-operator to existing operators,
    /// creating new operator families. Track whether innovation saturates.
    /// </summary>
    public static List<L6Metrics.L6Snapshot> Simulate(
        int generations, double mutationRate = 0.3, int? seed = null)
    {
        var rng = seed.HasValue ? new Random(seed.Value) : new Random(42);
        var history = new List<L6Metrics.L6Snapshot>();

        // Start with base operators.
        var operators = new HashSet<string> { "L_Q" };
        int speciesBase = 20;

        for (int gen = 0; gen <= generations; gen++)
        {
            int familyCount = operators.Count;
            int carrierEstimate = familyCount * 6; // each family ~6 carrier types
            int speciesEstimate = familyCount * speciesBase;

            // Check saturation: compare recent growth rate vs overall.
            double innovationRate = 0;
            bool saturating = false;
            if (history.Count >= 10)
            {
                var recent = history.Skip(Math.Max(0, history.Count - 5)).ToList();
                var older = history.Take(Math.Max(1, history.Count - 5)).ToList();
                double recentRate = recent.Count > 1
                    ? (double)(recent.Last().OperatorFamilies - recent.First().OperatorFamilies)
                      / Math.Max(recent.Last().Generation - recent.First().Generation, 1) : 0;
                double olderRate = older.Count > 1
                    ? (double)(older.Last().OperatorFamilies - older.First().OperatorFamilies)
                      / Math.Max(older.Last().Generation - older.First().Generation, 1) : 1;

                innovationRate = recentRate;
                saturating = olderRate > 1e-10 && recentRate < olderRate * 0.3;
            }

            history.Add(new L6Metrics.L6Snapshot(
                gen, familyCount, carrierEstimate, speciesEstimate,
                innovationRate, saturating));

            // Meta-operator evolution: each existing operator has a chance
            // to spawn a new operator family. Cap total families.
            if (operators.Count >= 200) break; // hard cap
            var newOps = new HashSet<string>();
            int toProcess = Math.Min(operators.Count, 5);
            foreach (string op in operators.Take(toProcess).ToList())
            {
                if (rng.NextDouble() < mutationRate)
                {
                    string newOp = $"Op{operators.Count + 1}";
                    newOps.Add(newOp);
                }
            }
            foreach (string nop in newOps)
                operators.Add(nop);

            // Slight decay in mutation rate.
            mutationRate *= 0.99;
            if (mutationRate < 0.05) mutationRate = 0.05;
        }

        return history;
    }
}
