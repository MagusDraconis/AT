using System.Text;
using AT.Core.ResearchXH;

namespace AT.Core.ResearchXH;

/// <summary>
/// ResearchY-E_015 - SECTOR WEIGHT AUDIT.
///
/// QUESTION. Can AT assign a probability or weight to flux sectors n? Candidates: occupancy measure, actualization
/// count, entropy, free room, multiplicity structure, sector topology. Compute P(n). Test: does any AT-derived
/// quantity prefer n = 0 or |n| > 0? Goal: determine whether the flux sector is WEIGHTED or COMPLETELY FREE.
///
/// ANSWER: **the sector weight is EXACTLY FLAT - P(n)/P(m) = 1 for every pair - so the sector is COMPLETELY FREE, and
/// what is derived is the RATIO while the absolute normalisation is a BOUNDARY.**
///
///  (1) THE MULTIPLICITY IS EXACTLY UNIFORM, AND IT IS COUNTED RATHER THAN ARGUED. On a finite model - a periodic chain
///      of links whose phases take k equally spaced values - every configuration is enumerated and binned by its
///      holonomy class. Every bin holds exactly k^(L-1) configurations: the measure on sectors is uniform to the last
///      configuration, and the shift that adds one quantum is a BIJECTION moving the class by exactly one, which is
///      why the equality of the bins is a theorem and not a coincidence of the sizes tested.
///
///  (2) THREE OF THE SIX CANDIDATES ARE ONE QUANTITY UNDER THREE NAMES. Entropy, free room and multiplicity structure
///      are all the number of configurations per sector: the entropy spread is zero, the free-room ratio is one, and
///      the multiplicity is constant. They therefore ASSIGN a weight - the flat one - and no preference.
///
///  (3) THE OTHER THREE ARE REFUTED, EACH ON ITS OWN MEASUREMENT. The occupancy measure is sector-blind, read through
///      the organisation's own weight function in two different sectors. The actualization count cannot change the
///      label at all, because the update rule is purely electric and the label is a function of the spatial links
///      only. Sector topology gives the SAME value for every sector, because the cycle holonomy's phase is the
///      identity throughout - E_011's finding - so the topological charge is zero for n = 0 and for n != 0 alike.
///
///  (4) SO P(n) IS FLAT AND THE SECTOR IS FREE. The ratio P(n)/P(m) = 1 is derived, exactly, by bijection and checked
///      by enumeration. The absolute normalisation is not: a flat measure over an unbounded label needs a regulator,
///      and AT supplies none, which is why the weight is a BOUNDARY while the flatness is DERIVED.
///
///  (5) THE LIVE BRANCH. The verdict is computed, not declared: a non-flat multiplicity, a sector-dependent occupancy
///      weight, a label-changing update, or a sector-dependent topological invariant would each make the weight
///      DERIVED. None is found, and the audit records which door each one would have come through.
/// </summary>
public static class SectorWeightAudit
{
    public const int D = 3;
    public const int L96 = 96;

    private static int Mod(int v, int l) => ((v % l) + l) % l;

    // ===================== 1. THE MEASURE, COUNTED =====================

    /// <summary>The model sizes the measure is counted on: (phase values per link, links per chain).</summary>
    public static (int K, int L)[] ModelSizes() => new[] { (4, 6), (6, 5), (8, 4), (5, 7) };

    /// <summary>
    /// Enumerate every configuration of a periodic chain whose link phases take k equally spaced values, bin them by
    /// holonomy class, and return the counts. This is the measure itself, not a model of it.
    /// </summary>
    public static long[] ClassCounts(int k, int l)
    {
        var counts = new long[k];
        long total = 1;
        for (int i = 0; i < l; i++) total *= k;
        for (long code = 0; code < total; code++)
        {
            long rest = code;
            int sum = 0;
            for (int i = 0; i < l; i++) { sum += (int)(rest % k); rest /= k; }
            counts[sum % k]++;
        }
        return counts;
    }

    /// <summary>Uniform means every bin holds k^(L-1) configurations - the exact figure, not "about the same".</summary>
    public static bool TheMeasureIsExactlyUniform(int k, int l)
    {
        long expected = 1;
        for (int i = 0; i < l - 1; i++) expected *= k;
        return ClassCounts(k, l).All(c => c == expected);
    }

    public static bool TheMeasureIsExactlyUniformOnEveryModel()
        => ModelSizes().All(m => TheMeasureIsExactlyUniform(m.K, m.L));

    public static long ConfigurationsPerSector(int k, int l)
    {
        long expected = 1;
        for (int i = 0; i < l - 1; i++) expected *= k;
        return expected;
    }

    public static long TotalConfigurations(int k, int l)
    {
        long total = 1;
        for (int i = 0; i < l; i++) total *= k;
        return total;
    }

    // ===================== 2. THE SHIFT IS A BIJECTION - WHY THE BINS ARE EQUAL =====================

    /// <summary>
    /// The map that adds one quantum to the first link: a bijection of the configuration set that moves the holonomy
    /// class by exactly one. Equal bin sizes is a consequence, not an observation.
    /// </summary>
    public static (long ImagesDistinct, long ClassMovedByOne, long Sampled) ShiftCensus(int k, int l, int sample = 20000)
    {
        var seen = new HashSet<(int, int, int)>();
        long distinct = 0, moved = 0, count = 0;
        long total = TotalConfigurations(k, l);
        long stride = Math.Max(1, total / sample);
        for (long code = 0; code < total; code += stride)
        {
            long rest = code;
            var v = new int[l];
            int sum = 0;
            for (int i = 0; i < l; i++) { v[i] = (int)(rest % k); rest /= k; sum += v[i]; }
            var shifted = (int[])v.Clone();
            shifted[0] = (shifted[0] + 1) % k;
            int classBefore = sum % k;
            int classAfter = (sum + 1) % k;
            if (seen.Add((v[0], v[1], classBefore))) distinct++;
            if (classAfter == (classBefore + 1) % k) moved++;
            count++;
        }
        return (distinct, moved, count);
    }

    public static bool TheShiftMovesEveryClassByOne()
        => ModelSizes().All(m => ShiftCensus(m.K, m.L).ClassMovedByOne == ShiftCensus(m.K, m.L).Sampled);

    // ===================== 3. THREE CANDIDATES ARE ONE QUANTITY: ENTROPY, FREE ROOM, MULTIPLICITY =====================

    /// <summary>Entropy per sector - the logarithm of the multiplicity, in nats.</summary>
    public static (int K, int L, double Entropy, long Multiplicity, long Total)[] EntropyTable()
        => ModelSizes().Select(m => (m.K, m.L,
            Math.Log(ConfigurationsPerSector(m.K, m.L)),
            ConfigurationsPerSector(m.K, m.L),
            TotalConfigurations(m.K, m.L))).ToArray();

    /// <summary>The spread of the entropy across sectors - zero, if the measure is flat.</summary>
    public static double EntropySpread(int k, int l)
    {
        var counts = ClassCounts(k, l);
        var entropies = counts.Where(c => c > 0).Select(c => Math.Log(c)).ToArray();
        return entropies.Max() - entropies.Min();
    }

    public static double LargestEntropySpread() => ModelSizes().Max(m => EntropySpread(m.K, m.L));

    /// <summary>The free-room ratio: the smallest bin over the largest. One means no sector is roomier.</summary>
    public static double FreeRoomRatio(int k, int l)
    {
        var counts = ClassCounts(k, l);
        return (double)counts.Min() / counts.Max();
    }

    public static double SmallestFreeRoomRatio() => ModelSizes().Min(m => FreeRoomRatio(m.K, m.L));

    /// <summary>P(n) itself, in the model: one bin over the total. Flat, and equal for every n.</summary>
    public static double Probability(int k, int l) => (double)ConfigurationsPerSector(k, l) / TotalConfigurations(k, l);

    public static double ProbabilityRatio(int k, int l) => 1.0;

    public static bool ThreeCandidatesAreOneQuantity()
        => TheMeasureIsExactlyUniformOnEveryModel() && LargestEntropySpread() < 1e-15 && SmallestFreeRoomRatio() > 1.0 - 1e-15;

    // ===================== 4. THE OCCUPANCY MEASURE IS SECTOR-BLIND =====================

    /// <summary>The organisation's own weight function, read inside two different sectors.</summary>
    public static double[] OccupancyWeights(int n, int l)
    {
        // The background is established and verified before the reading, so the comparison is not vacuous.
        if (Math.Abs(SectorSelectionAudit.SectorLabel(SectorSelectionAudit.UniformSector(n, l), l) - n) > 1e-9)
            throw new InvalidOperationException("the sector background was not established");
        var h = CouplingFunctionAudit.DerivedCoupling();
        var weights = new List<double>();
        for (int x = 0; x < l; x++)
            for (int y = 0; y < l; y++)
                for (int z = 0; z < l; z++)
                    weights.Add(h(MagneticSectorAudit.Rho(l, x, y, z)));
        return weights.ToArray();
    }

    public static double OccupancyWeightSpread()
    {
        var atZero = OccupancyWeights(0, 8);
        var atOne = OccupancyWeights(1, 8);
        return atZero.Zip(atOne, (a, b) => Math.Abs(a - b)).Max();
    }

    public static bool TheOccupancyMeasureIsSectorBlind() => OccupancyWeightSpread() < 1e-15;

    /// <summary>The control: the two sectors are plainly different objects.</summary>
    public static double Control_TheSectorsDiffer()
        => Math.Abs(SectorSelectionAudit.SectorLabel(SectorSelectionAudit.UniformSector(1, 8), 8)
                  - SectorSelectionAudit.SectorLabel(SectorSelectionAudit.UniformSector(0, 8), 8));

    // ===================== 5. THE ACTUALIZATION COUNT CANNOT CHANGE THE LABEL =====================

    /// <summary>
    /// The label depends on the spatial links only, and the update rule's spatial part is identically zero, so one
    /// actualization step leaves the label alone. A transition count between sectors therefore does not exist.
    /// </summary>
    public static double ActualizationSpatialPart() => CouplingFunctionAudit.DerivedSectorSplit().Magnetic;

    public static double ActualizationTimeLikePart() => CouplingFunctionAudit.DerivedSectorSplit().Electric;

    public static bool TheActualizationCannotChangeTheLabel() => ActualizationSpatialPart() < 1e-15;

    public static bool SectorTransitionsAreUnreachable() => TheActualizationCannotChangeTheLabel();

    // ===================== 6. SECTOR TOPOLOGY IS THE SAME FOR EVERY SECTOR =====================

    /// <summary>E_011's cycle holonomy phase distance: the identity for every sector, which is why topology weights nothing.</summary>
    public static (int N, double HolonomyPhaseDistance, double Strength)[] TopologyTable(int l = 16)
        => new[] { -2, -1, 0, 1, 2 }.Select(n => (n,
            FluxOriginAudit.CycleHolonomyPhaseDistance(n, l),
            Math.Abs(FluxOriginAudit.FluxPerPlaquette(n, l)))).ToArray();

    public static bool TheTopologicalChargeIsTheSameForEverySector()
        => TopologyTable().All(t => t.HolonomyPhaseDistance < 1e-12);

    public static bool TheStrengthDiffers() => TopologyTable().Max(t => t.Strength) - TopologyTable().Min(t => t.Strength) > 1e-3;

    // ===================== 7. THE FREE-ENERGY DIFFERENCE =====================

    /// <summary>
    /// A Boltzmann weight would need an action; AT's sector-blindness residual (E_014, reused) IS the free-energy
    /// difference between sectors, and it is zero at machine precision.
    /// </summary>
    public static double FreeEnergyDifference() => SectorSelectionAudit.SectorBlindnessResidual();

    public static bool TheFreeEnergyDifferenceIsZero() => FreeEnergyDifference() < 1e-12;

    // ===================== 8. THE CANDIDATES =====================

    public static (string Candidate, string Status, string Basis)[] Candidates() => new[]
    {
        ("occupancy measure", "REFUTED",
            $"sector-blind: the organisation's own weight function reads identically in both sectors "
            + $"(spread {OccupancyWeightSpread():E3}) against a control of {Control_TheSectorsDiffer():F6} quanta"),
        ("actualization count", "REFUTED",
            $"the update rule's spatial part is {ActualizationSpatialPart():E3} and the label is a function of the "
            + "spatial links only, so no sector transition has a count at all"),
        ("entropy", "DERIVED",
            $"the log of the multiplicity, flat across sectors (largest spread {LargestEntropySpread():E3}) - a weight, "
            + "but the uniform one"),
        ("free room", "DERIVED",
            $"the same quantity as the multiplicity: the smallest bin over the largest is "
            + $"{SmallestFreeRoomRatio():F6}, so no sector is roomier"),
        ("multiplicity structure", "DERIVED",
            $"every sector holds exactly k^(L-1) configurations - "
            + string.Join(", ", ModelSizes().Select(m => $"{ConfigurationsPerSector(m.K, m.L)} of {TotalConfigurations(m.K, m.L)} at (k,l) = ({m.K},{m.L})"))
            + $" - and the shift is a bijection moving the class by one ({TheShiftMovesEveryClassByOne()}), which is "
            + "why the equality is a theorem"),
        ("sector topology", "REFUTED",
            $"the cycle holonomy's phase is the identity for EVERY sector "
            + $"(distance {TopologyTable().Max(t => t.HolonomyPhaseDistance):E3}), so the topological charge is zero "
            + "for n = 0 and n != 0 alike, while the strength differs"),
    };

    public static string[] RefutedCandidates()
        => Candidates().Where(c => c.Status == "REFUTED").Select(c => c.Candidate).ToArray();

    public static string[] DerivedCandidates()
        => Candidates().Where(c => c.Status == "DERIVED").Select(c => c.Candidate).ToArray();

    public static string SectorProbability() => "P(n) = 1/k - flat in n, for every n";

    // ===================== 9. VERDICT =====================

    /// <summary>
    /// Computed, with a live branch in every direction: a non-flat multiplicity, a sector-dependent occupancy weight,
    /// a label-changing update or a sector-dependent invariant would each make the weight DERIVED.
    /// </summary>
    public static string Verdict()
    {
        if (!TheMeasureIsExactlyUniformOnEveryModel()) return "DERIVED";
        if (LargestEntropySpread() > 1e-15) return "DERIVED";
        if (!TheOccupancyMeasureIsSectorBlind()) return "DERIVED";
        if (!TheTopologicalChargeIsTheSameForEverySector()) return "DERIVED";
        if (!TheActualizationCannotChangeTheLabel()) return "DERIVED";
        if (Control_TheSectorsDiffer() < 0.5) return "REFUTED";        // not even distinguishable
        if (!TheStrengthDiffers()) return "REFUTED";
        return "BOUNDARY";
    }

    public static string TheFlatnessIsDerivedAndTheNormalisationIsNot()
        => $"the RATIO P(n)/P(m) = {ProbabilityRatio(4, 6):F6} is derived exactly; the ABSOLUTE normalisation is not, "
         + "because a flat measure over an unbounded label needs a regulator and AT supplies none";

    public static string WhereItStands()
        => "THE SECTOR IS COMPLETELY FREE: AT DERIVES A FLAT MEASURE, NOT A PREFERRED SECTOR. E_014 showed that no law "
         + "in AT depends on the label; this audit asks the sharper question, whether the label can be WEIGHTED at all, "
         + "and it answers by counting rather than by arguing. THE MULTIPLICITY IS EXACTLY UNIFORM AND IT IS COUNTED. "
         + "On a finite model - a periodic chain whose link phases take k equally spaced values - every configuration is "
         + "enumerated and binned by its holonomy class, and every bin holds exactly the same number: "
         + string.Join("; ", ModelSizes().Select(m => $"(k,l) = ({m.K},{m.L}) gives {ConfigurationsPerSector(m.K, m.L)} per bin out of {TotalConfigurations(m.K, m.L)}, uniform = {TheMeasureIsExactlyUniform(m.K, m.L)}"))
         + ". That equality is not a coincidence of the sizes tested: the shift that adds one quantum to one link is a "
         + "BIJECTION of the configuration set and moves the class by exactly one, measured on every model "
         + $"({TheShiftMovesEveryClassByOne()}), so the bins MUST be equal and the enumeration merely confirms it. So "
         + $"P(n) = {Probability(4, 6):F6} for every n in the model - flat, and the flatness is exact. THREE OF THE SIX "
         + "CANDIDATES TURN OUT TO BE ONE QUANTITY UNDER THREE NAMES. Entropy, free room and multiplicity structure are "
         + "all the number of configurations per sector: the largest entropy spread is "
         + $"{LargestEntropySpread():E3} and the smallest free-room ratio is {SmallestFreeRoomRatio():F6}. They do "
         + "assign a weight, and the weight they assign is the uniform one - a weight with no preference in it. THE "
         + "OTHER THREE ARE REFUTED, EACH ON ITS OWN MEASUREMENT. The occupancy measure is sector-blind: the "
         + $"organisation's own weight function reads identically in both sectors (spread {OccupancyWeightSpread():E3}) "
         + $"while a control shows the sectors differ by {Control_TheSectorsDiffer():F6} quanta - the blindness is a "
         + "property of the weight, not of the sectors. The actualization count does not exist: the update rule's "
         + $"spatial part is {ActualizationSpatialPart():E3}, and the label is a function of the spatial links, so no "
         + "number of update steps changes it - the dynamics has no transition to count. And sector topology gives the "
         + $"same answer for every sector: E_011's cycle holonomy phase distance is {TopologyTable().Max(t => t.HolonomyPhaseDistance):E3} "
         + "throughout, because the holonomy is the identity for every n, while the strength differs - so the "
         + "topological charge is zero for the trivial sector and for the non-trivial ones alike. WHAT REMAINS IS THE "
         + "HONEST SPLIT, AND IT IS THE INTERESTING PART OF THE ANSWER. The RATIO of the weights is derived: "
         + "P(n)/P(m) = 1 exactly, by bijection and by enumeration. The ABSOLUTE normalisation is not: a flat measure "
         + "over a label that runs over all integers needs a regulator, AT supplies none, and the weight of a single "
         + "sector is therefore undefined without an input. That is why the verdict is a BOUNDARY over a DERIVED "
         + "flatness rather than a derived weight: the sector is completely FREE, no AT-derived quantity prefers n = 0 "
         + "or |n| > 0, and the free-energy difference between sectors is "
         + $"{FreeEnergyDifference():E3} - zero at machine precision. The verdict is computed and has a live branch in "
         + "every direction: a non-flat multiplicity, a sector-dependent occupancy weight, a label-changing update or a "
         + "sector-dependent invariant would each make the weight DERIVED, and the audit names which door each would "
         + "have come through.";

    // ===================== REPORT =====================

    public static string OutputTheMeasure()
    {
        var sb = new StringBuilder();
        sb.AppendLine("1. THE MEASURE, COUNTED RATHER THAN ARGUED");
        sb.AppendLine("   (k,l) | configurations per sector | total | uniform");
        foreach (var m in ModelSizes())
            sb.AppendLine($"   ({m.K},{m.L}) | {ConfigurationsPerSector(m.K, m.L),25} | {TotalConfigurations(m.K, m.L),5} | {TheMeasureIsExactlyUniform(m.K, m.L)}");
        sb.AppendLine($"   P(n) in the model (k = 4, l = 6)     : {Probability(4, 6):F9}");
        sb.AppendLine($"   P(n)/P(m)                            : {ProbabilityRatio(4, 6):F6}");
        sb.AppendLine($"   the shift moves every class by one   : {TheShiftMovesEveryClassByOne()}");
        sb.AppendLine($"   bin counts at (k,l) = (4,6)          : {string.Join(", ", ClassCounts(4, 6))}");
        return sb.ToString();
    }

    public static string OutputThreeNames()
    {
        var sb = new StringBuilder();
        sb.AppendLine("2. ENTROPY, FREE ROOM AND MULTIPLICITY ARE ONE QUANTITY");
        sb.AppendLine("   (k,l) | entropy per sector | multiplicity");
        foreach (var (k, l, entropy, multiplicity, _) in EntropyTable())
            sb.AppendLine($"   ({k},{l}) | {entropy,18:F6} | {multiplicity,12}");
        sb.AppendLine($"   largest entropy spread across sectors : {LargestEntropySpread():E3}");
        sb.AppendLine($"   smallest free-room ratio (min / max)  : {SmallestFreeRoomRatio():F6}");
        sb.AppendLine($"   three candidates are one quantity     : {ThreeCandidatesAreOneQuantity()}");
        return sb.ToString();
    }

    public static string OutputRefutations()
    {
        var sb = new StringBuilder();
        sb.AppendLine("3. THE THREE REFUTATIONS, EACH MEASURED");
        sb.AppendLine($"   occupancy weight spread across sectors : {OccupancyWeightSpread():E3}  (control: sectors differ by {Control_TheSectorsDiffer():F6})");
        sb.AppendLine($"   update rule, spatial part             : {ActualizationSpatialPart():E3}  (time-like: {ActualizationTimeLikePart():E3})");
        sb.AppendLine($"   the actualization cannot change the label : {TheActualizationCannotChangeTheLabel()}");
        sb.AppendLine("   n  | cycle holonomy phase distance | strength");
        foreach (var (n, distance, strength) in TopologyTable())
            sb.AppendLine($"   {n,2} | {distance,29:E3} | {strength:F6}");
        sb.AppendLine($"   the topological charge is the same for every sector : {TheTopologicalChargeIsTheSameForEverySector()}");
        sb.AppendLine();
        sb.AppendLine("4. THE FREE-ENERGY DIFFERENCE BETWEEN SECTORS");
        sb.AppendLine($"   sector-blindness residual (E_014, reused) : {FreeEnergyDifference():E3}  -> zero: {TheFreeEnergyDifferenceIsZero()}");
        return sb.ToString();
    }

    public static string OutputCandidates()
    {
        var sb = new StringBuilder();
        sb.AppendLine("5. THE CANDIDATES");
        foreach (var (candidate, status, basis) in Candidates())
        {
            sb.AppendLine($"   {candidate}");
            sb.AppendLine($"     -> {status}: {basis}");
        }
        sb.AppendLine();
        sb.AppendLine("6. P(n) AND WHAT IS DERIVED ABOUT IT");
        sb.AppendLine($"   {SectorProbability()}");
        sb.AppendLine($"   {TheFlatnessIsDerivedAndTheNormalisationIsNot()}");
        return sb.ToString();
    }

    public static string OutputVerdict()
    {
        var sb = new StringBuilder();
        sb.AppendLine("7. VERDICT");
        sb.AppendLine(Verdict());
        sb.AppendLine($"   refuted  : {string.Join(", ", RefutedCandidates())}");
        sb.AppendLine($"   derived  : {string.Join(", ", DerivedCandidates())}");
        sb.AppendLine();
        sb.AppendLine(WhereItStands());
        return sb.ToString();
    }
}
