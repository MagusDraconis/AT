namespace TQM.Core.Resonance.Theory;

/// <summary>
/// Defines resource budgets and per-species resource consumption profiles
/// for information species in the Theta field.
///
/// TQM-135: Information Selection Under Resource Constraints
/// </summary>
public static class ResourceConstraintModel
{
    // ══════════════════════════════════════════════════════════════════
    // Default resource budgets.
    // ══════════════════════════════════════════════════════════════════

    public static List<InformationFitnessProfile.ResourceBudget> CreateDefaultBudgets(
        double capacityScale = 1.0)
    {
        return new List<InformationFitnessProfile.ResourceBudget>
        {
            new("Amplitude",   100.0 * capacityScale, 0, 0.05 * capacityScale, true),
            new("Memory",      50.0  * capacityScale, 0, 0.02 * capacityScale, true),
            new("Coherence",   80.0  * capacityScale, 0, 0.03 * capacityScale, true),
            new("Lifetime",    200.0 * capacityScale, 0, 0.10 * capacityScale, true),
            new("Spatial",     60.0  * capacityScale, 0, 0.02 * capacityScale, true),
            new("Bandwidth",   40.0  * capacityScale, 0, 0.01 * capacityScale, true),
        };
    }

    /// <summary>
    /// Create resource budgets scaled for a specific total capacity.
    /// </summary>
    public static List<InformationFitnessProfile.ResourceBudget> CreateBudgets(
        double totalCapacity, double regenerationFraction = 0.05)
    {
        return new List<InformationFitnessProfile.ResourceBudget>
        {
            new("Amplitude",   totalCapacity * 0.30, 0, totalCapacity * regenerationFraction, true),
            new("Memory",      totalCapacity * 0.15, 0, totalCapacity * regenerationFraction * 0.5, true),
            new("Coherence",   totalCapacity * 0.25, 0, totalCapacity * regenerationFraction * 0.7, true),
            new("Lifetime",    totalCapacity * 0.20, 0, totalCapacity * regenerationFraction * 0.3, true),
            new("Spatial",     totalCapacity * 0.07, 0, totalCapacity * regenerationFraction * 0.2, true),
            new("Bandwidth",   totalCapacity * 0.03, 0, totalCapacity * regenerationFraction * 0.1, true),
        };
    }

    // ══════════════════════════════════════════════════════════════════
    // Per-species resource consumption profiles.
    // ══════════════════════════════════════════════════════════════════
    // These are based on the intrinsic properties of each species:
    // - A (Uniform): simple, low energy → low consumption
    // - B (Standing Wave): moderate complexity → moderate consumption
    // - C (Anti-Phase): domain structure → moderate-high consumption
    // - D (Composite): multi-mode, high complexity → high consumption
    // ══════════════════════════════════════════════════════════════════

    private static readonly Dictionary<string, (double amp, double mem, double coh, double life, double spat, double bw)> RawConsumption = new()
    {
        ["A"] = (1.0, 0.5, 1.0, 2.0, 1.0, 0.3),
        ["B"] = (2.0, 1.5, 1.5, 3.0, 1.5, 0.8),
        ["C"] = (2.5, 2.0, 2.0, 2.5, 2.0, 1.0),
        ["D"] = (4.0, 3.0, 3.0, 4.0, 3.0, 2.0),
    };

    public static List<InformationFitnessProfile.ResourceConsumption> GetConsumptionProfiles()
    {
        var profiles = new List<InformationFitnessProfile.ResourceConsumption>();
        foreach (var (sp, (amp, mem, coh, life, spat, bw)) in RawConsumption)
        {
            profiles.Add(new InformationFitnessProfile.ResourceConsumption(
                sp, amp, mem, coh, life, spat, bw));
        }
        return profiles;
    }

    public static InformationFitnessProfile.ResourceConsumption GetConsumption(string species)
        => RawConsumption.TryGetValue(species, out var c)
            ? new InformationFitnessProfile.ResourceConsumption(species, c.amp, c.mem, c.coh, c.life, c.spat, c.bw)
            : new InformationFitnessProfile.ResourceConsumption(species, 1, 1, 1, 1, 1, 1);

    // ══════════════════════════════════════════════════════════════════
    // Total resource consumption for a population.
    // ══════════════════════════════════════════════════════════════════

    public static double ComputeTotalConsumption(
        Dictionary<string, int> populations,
        ResourceConstraintType constraintType)
    {
        double total = 0;
        foreach (var (sp, count) in populations)
        {
            var c = GetConsumption(sp);
            total += count * constraintType switch
            {
                ResourceConstraintType.Amplitude => c.AmplitudeConsumption,
                ResourceConstraintType.Memory => c.MemoryConsumption,
                ResourceConstraintType.Coherence => c.CoherenceConsumption,
                ResourceConstraintType.Lifetime => c.LifetimeConsumption,
                ResourceConstraintType.Spatial => c.SpatialConsumption,
                ResourceConstraintType.Bandwidth => c.BandwidthConsumption,
                _ => c.AmplitudeConsumption + c.MemoryConsumption + c.CoherenceConsumption,
            };
        }
        return total;
    }

    /// <summary>
    /// Compute aggregate resource pressure across all constraint types.
    /// </summary>
    public static double ComputeAggregatePressure(
        Dictionary<string, int> populations,
        List<InformationFitnessProfile.ResourceBudget> budgets)
    {
        if (budgets.Count == 0) return 0;

        var consumptions = GetConsumptionProfiles();
        double totalPressure = 0;
        int count = 0;

        foreach (var budget in budgets)
        {
            double usage = 0;
            foreach (var (sp, pop) in populations)
            {
                var c = consumptions.FirstOrDefault(x => x.SpeciesName == sp);
                if (c == null) continue;
                usage += pop * budget.Name switch
                {
                    "Amplitude" => c.AmplitudeConsumption,
                    "Memory" => c.MemoryConsumption,
                    "Coherence" => c.CoherenceConsumption,
                    "Lifetime" => c.LifetimeConsumption,
                    "Spatial" => c.SpatialConsumption,
                    "Bandwidth" => c.BandwidthConsumption,
                    _ => c.AmplitudeConsumption,
                };
            }
            if (budget.TotalCapacity > 0)
            {
                totalPressure += usage / budget.TotalCapacity;
                count++;
            }
        }

        return count > 0 ? totalPressure / count : 0;
    }

    /// <summary>
    /// Determine the limiting resource for a population.
    /// </summary>
    public static string FindLimitingResource(
        Dictionary<string, int> populations,
        List<InformationFitnessProfile.ResourceBudget> budgets)
    {
        string worst = "None";
        double maxPressure = 0;

        var consumptions = GetConsumptionProfiles();
        foreach (var budget in budgets)
        {
            double usage = 0;
            foreach (var (sp, pop) in populations)
            {
                var c = consumptions.FirstOrDefault(x => x.SpeciesName == sp);
                if (c == null) continue;
                usage += pop * budget.Name switch
                {
                    "Amplitude" => c.AmplitudeConsumption,
                    "Memory" => c.MemoryConsumption,
                    "Coherence" => c.CoherenceConsumption,
                    "Lifetime" => c.LifetimeConsumption,
                    "Spatial" => c.SpatialConsumption,
                    "Bandwidth" => c.BandwidthConsumption,
                    _ => c.AmplitudeConsumption,
                };
            }
            double pressure = budget.TotalCapacity > 0 ? usage / budget.TotalCapacity : 0;
            if (pressure > maxPressure) { maxPressure = pressure; worst = budget.Name; }
        }

        return worst;
    }

    // ══════════════════════════════════════════════════════════════════
    // Constraint type enum for convenience.
    // ══════════════════════════════════════════════════════════════════

    public enum ResourceConstraintType
    {
        Amplitude,
        Memory,
        Coherence,
        Lifetime,
        Spatial,
        Bandwidth,
        Aggregate
    }
}
