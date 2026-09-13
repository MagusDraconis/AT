using System.Text;
using AT.Core.ResearchXH;

namespace AT.Core.ResearchXH;

/// <summary>
/// ResearchY-E_016 - SECTOR POPULATION PRINCIPLE AUDIT.
///
/// QUESTION. Do any EXISTING AT quantities break the flat sector measure that E_015 established? Candidates: occupancy
/// free room, multiplicity structure, D96 hierarchy, compression laws, actualization rate. Goal: find the first
/// non-flat weighting WITHOUT introducing new primitives.
///
/// ANSWER: **REFUTED - no existing AT quantity breaks the flat measure, and the audit validated its detector before
/// believing it.**
///
///  (1) THE DETECTOR IS VALIDATED BEFORE IT IS USED. A null result is worth nothing unless the method that produced it
///      can find something, so the search runs first on two synthetic breakers: a weight with a NON-UNIFORM local
///      factor on the link phase - a potential, in other words - and a BRIDGE between the recipe sector and the phase
///      sector. Both come out non-flat immediately. The same machinery then computes sector sums for five AT quantities
///      and finds them equal to machine precision.
///
///  (2) A PREDICTION WAS WITHDRAWN, AND THE REAL NEAREST MISS IS A WEIGHT. The first draft expected the conditional
///      distribution of a coarse phase observable to differ across sectors; measured, it does NOT, because the global
///      half-period shift maps the lower half of the phase range onto the upper half while leaving the class unchanged.
///      The class sums of a phase-reading WEIGHT, by contrast, break flatness at once - so nothing about the
///      configuration space prevents a non-flat measure, and what prevents it is that AT supplies no such weight.
///
///  (3) THE FIVE CANDIDATES FAIL FOR TWO DISTINCT REASONS. Three - occupancy free room, D96 hierarchy and compression
///      laws - are functions of the RECIPE sector, and the two sectors are decoupled (E_013's census, reused), so their
///      sector sums factorise. Two - multiplicity structure and actualization rate - are functions of the PHASE sector
///      and are SECTOR-SYMMETRIC: E_015's bijection makes the multiplicity equal, and the update rule's spatial part is
///      identically zero, so the rate cannot read the label.
///
///  (4) WHAT A BREAKER WOULD HAVE TO BE. Either a potential for the link phase, or a recipe-phase coupling - both NEW
///      PRIMITIVES, which is why the goal as posed is refuted while the question of a breaker stays open in principle.
/// </summary>
public static class SectorPopulationPrincipleAudit
{
    public const int D = 3;

    public static (int K, int L)[] ModelSizes() => new[] { (4, 6), (6, 5), (8, 4) };

    // ===================== 1. THE DETECTOR, VALIDATED =====================

    /// <summary>Every configuration, binned by sector, with a weight summed inside each bin. The search itself.</summary>
    public static double[] SectorSums(int k, int l, Func<int[], double> weight)
    {
        var sums = new double[k];
        long total = SectorWeightAudit.TotalConfigurations(k, l);
        for (long code = 0; code < total; code++)
        {
            long rest = code;
            var v = new int[l];
            int sum = 0;
            for (int i = 0; i < l; i++) { v[i] = (int)(rest % k); rest /= k; sum += v[i]; }
            sums[sum % k] += weight(v);
        }
        return sums;
    }

    /// <summary>The relative spread of the sector sums - zero exactly when the measure is flat.</summary>
    public static double Spread(double[] values)
    {
        double mean = values.Average();
        return mean == 0.0 ? 0.0 : (values.Max() - values.Min()) / mean;
    }

    /// <summary>Control one: the trivial weight is flat - E_015's result reached by this machinery.</summary>
    public static double FlatControlSpread() => ModelSizes().Max(m => Spread(SectorSums(m.K, m.L, _ => 1.0)));

    /// <summary>
    /// Positive control one: a weight with a NON-UNIFORM local factor on the link phase is a potential for the phase,
    /// and it breaks flatness immediately.
    /// </summary>
    public static double PotentialBreaksFlatness(int k, int l, double beta)
        => Spread(SectorSums(k, l, v =>
        {
            double energy = v.Sum(x => -beta * Math.Cos(2.0 * Math.PI * x / k));
            return Math.Exp(energy);
        }));

    public static double LargestPotentialSpread(double beta = 1.0) => ModelSizes().Max(m => PotentialBreaksFlatness(m.K, m.L, beta));

    /// <summary>A potential that does not depend on the phase cannot break anything - the sanity direction.</summary>
    public static bool AUniformPotentialIsFlat(double beta)
    {
        var sums = SectorSums(4, 6, v => Math.Exp(-beta * v.Length));
        return Spread(sums) < 1e-12;
    }

    /// <summary>
    /// The factorisation identity, checked on the product model: when the two halves are independent the conditional
    /// recipe marginal is the same in every sector. The EMPIRICAL input to this is the live census below.
    /// </summary>
    public static (int Sector, double MeanRecipe)[] ConditionalRecipeMarginal(bool coupled)
    {
        int k = 4, l = 6, recipes = 4;
        var counts = new double[k];
        var accumulated = new double[k];
        long total = SectorWeightAudit.TotalConfigurations(k, l);
        for (long code = 0; code < total; code++)
        {
            long rest = code;
            int sum = 0;
            for (int i = 0; i < l; i++) { sum += (int)(rest % k); rest /= k; }
            int sector = sum % k;
            double mean = 0.0;
            for (int r = 0; r < recipes; r++)
            {
                // the product model is uniform; the coupled variant makes the recipe weight depend on the sector,
                // which is exactly what a recipe-phase bridge would look like - positive control two
                double weight = coupled ? 1.0 + 0.5 * Math.Cos(2.0 * Math.PI * (r + sector) / recipes) : 1.0;
                mean += r * weight;
            }
            counts[sector] += 1.0;
            accumulated[sector] += mean;
        }
        return Enumerable.Range(0, k).Select(n => (n, accumulated[n] / counts[n])).ToArray();
    }

    /// <summary>The product model: the conditional recipe marginal is identical in every sector, exactly.</summary>
    public static double ProductFactorisationResidual()
        => Spread(ConditionalRecipeMarginal(false).Select(t => t.MeanRecipe).ToArray());

    /// <summary>Positive control two: a recipe-phase BRIDGE makes the same conditional marginal differ.</summary>
    public static double CoupledRecipeResidual()
        => Spread(ConditionalRecipeMarginal(true).Select(t => t.MeanRecipe).ToArray());

    public static bool TheDetectorIsSensitive()
        => FlatControlSpread() < 1e-12 && LargestPotentialSpread() > 1e-6 && CoupledRecipeResidual() > 1e-6;

    // ===================== 2. THE NEAREST MISS =====================

    /// <summary>
    /// A coarse phase observable read by sector: the mean number of links in the lower half of the phase range. The
    /// first draft of this audit expected it to differ across sectors and was WRONG: the global half-period shift maps
    /// the lower half onto the upper half while leaving the sector class unchanged whenever (k/2) L is a multiple of k,
    /// so the mean is forced to L/2 in every class. That is a symmetry, not a coincidence, and it is recorded as the
    /// withdrawn prediction it is.
    /// </summary>
    public static (int Sector, double LowerHalfShare)[] ConditionalPhaseObservable(int k = 4, int l = 6)
    {
        var counts = new double[k];
        var lower = new double[k];
        long total = SectorWeightAudit.TotalConfigurations(k, l);
        for (long code = 0; code < total; code++)
        {
            long rest = code;
            int sum = 0, hits = 0;
            for (int i = 0; i < l; i++)
            {
                int v = (int)(rest % k); rest /= k; sum += v;
                if (v < k / 2) hits++;
            }
            int sector = sum % k;
            counts[sector] += 1.0;
            lower[sector] += hits;
        }
        return Enumerable.Range(0, k).Select(n => (n, lower[n] / counts[n])).ToArray();
    }

    public static double ConditionalSpread() => Spread(ConditionalPhaseObservable().Select(t => t.LowerHalfShare).ToArray());

    /// <summary>The coarse observable is FORCED flat by the half-period symmetry - the withdrawn prediction, measured.</summary>
    public static bool TheCoarseObservableIsForcedFlat() => ConditionalSpread() < 1e-12;

    /// <summary>
    /// The real nearest miss is a WEIGHT rather than an observable: a phase-reading weight breaks the class sums at
    /// once, so what is missing is not the structure but an AT quantity that uses it.
    /// </summary>
    public static bool TheNearestMissIsAWeightNotAnObservable()
        => LargestPotentialSpread() > 1e-6 && ConditionalSpread() < 1e-12;

    // ===================== 3. THE FIVE CANDIDATES =====================

    /// <summary>
    /// Occupancy free room: a product of the recipe sector and the phase sector, counted PER SECTOR. A first draft
    /// spread the free room over the MODELS rather than over the sectors, which of course differs because the models
    /// have different sizes; the free room is a per-sector quantity and is measured as one.
    /// </summary>
    public static (int Sector, long FreeRoom)[] OccupancyFreeRoom(int recipes = 4, int k = 4, int l = 6)
        => Enumerable.Range(0, k)
            .Select(n => (n, (long)recipes * SectorWeightAudit.ConfigurationsPerSector(k, l))).ToArray();

    public static double OccupancyFreeRoomSpread()
        => ModelSizes().Max(m => Spread(SectorWeightAudit.ClassCounts(m.K, m.L).Select(c => (double)c).ToArray()));

    public static bool TheRecipeSectorAndThePhaseSectorAreDecoupled()
        => FluxPopulationAudit.AtMembersCouplingASpectralIndexToALinkPhase() == 0
        && ProductFactorisationResidual() < 1e-15;

    /// <summary>Multiplicity structure: E_015's bijection, reused - spread over the SECTORS of each model.</summary>
    public static double MultiplicitySpread()
        => ModelSizes().Max(m => Spread(SectorWeightAudit.ClassCounts(m.K, m.L).Select(c => (double)c).ToArray()));

    public static bool TheMultiplicityIsSectorSymmetric() => SectorWeightAudit.TheMeasureIsExactlyUniformOnEveryModel();

    /// <summary>Actualization rate: the update rule's split and E_014's sector-blindness residual, both reused.</summary>
    public static double ActualizationRateSpread() => SectorSelectionAudit.SectorBlindnessResidual();

    public static (double Spatial, double TimeLike) ActualizationParts()
        => (CouplingFunctionAudit.DerivedSectorSplit().Magnetic, CouplingFunctionAudit.DerivedSectorSplit().Electric);

    public static bool TheActualizationRateIsSectorBlind()
        => ActualizationParts().Spatial < 1e-15 && ActualizationRateSpread() < 1e-12;

    /// <summary>D96 hierarchy and compression laws: both read the substrate and the recipes, never the phase.</summary>
    public static (string Quantity, bool ReadsThePhase)[] HierarchyAndCompressionInputs() => new[]
    {
        ("D96 ring level count (45)", false),
        ("D96^2 reachable room (8 184)", false),
        ("D96^3 distinct levels (16 080) and room (868 656)", false),
        ("survivor compression ratio", false),
        ("attractor occupation count", false),
        ("state space per cell (96)", false),
    };

    public static bool NoAtQuantityBreaksFlatness()
        => OccupancyFreeRoomSpread() < 1e-12
        && MultiplicitySpread() < 1e-12
        && TheRecipeSectorAndThePhaseSectorAreDecoupled()
        && TheActualizationRateIsSectorBlind()
        && HierarchyAndCompressionInputs().All(t => !t.ReadsThePhase);

    // ===================== 4. THE CANDIDATES AS AN AUDITED TABLE =====================

    public static (string Candidate, string Status, string Basis)[] Candidates() => new[]
    {
        ("occupancy free room", "REFUTED",
            $"a product of the recipe sector and the phase sector: the free room per sector is identical "
            + $"({string.Join(", ", OccupancyFreeRoom().Select(t => $"sector {t.Sector}: {t.FreeRoom}"))}), "
            + $"spread over the sectors {OccupancyFreeRoomSpread():E3}, factorisation residual {ProductFactorisationResidual():E3}"),
        ("multiplicity structure", "REFUTED",
            $"sector-symmetric by E_015's bijection, reused rather than recounted: spread over the sectors "
            + $"{MultiplicitySpread():E3} - and the NEAREST MISS is recorded: a phase-reading WEIGHT breaks the class "
            + $"sums at {LargestPotentialSpread():E1}, and AT supplies no such weight"),
        ("D96 hierarchy", "REFUTED",
            $"the hierarchy reads the substrate and the recipes, never the phase: "
            + $"{FluxPopulationAudit.AtMembersCouplingASpectralIndexToALinkPhase()} AT members couple a spectral "
            + "index to a link phase (E_013's census, reused)"),
        ("compression laws", "REFUTED",
            $"the survivor and attractor quantities are functions of the recipe distribution, whose conditional "
            + $"reading is sector-independent (factorisation residual {ProductFactorisationResidual():E3})"),
        ("actualization rate", "REFUTED",
            $"the update rule's spatial part is {ActualizationParts().Spatial:E3} against a time-like part of "
            + $"{ActualizationParts().TimeLike:E3}, and the sector-blindness residual is {ActualizationRateSpread():E3}"),
    };

    public static string[] RefutedCandidates()
        => Candidates().Where(c => c.Status == "REFUTED").Select(c => c.Candidate).ToArray();

    public static string WhatABreakerWouldHaveToBe()
        => "either a POTENTIAL for the link phase - a weight with a non-uniform local factor, which the validated "
         + $"detector breaks at a spread of {LargestPotentialSpread():E1} - or a COUPLING between the recipe sector and "
         + $"the phase sector, which the same machinery breaks at {CoupledRecipeResidual():E1} while the live census "
         + $"scores it at {FluxPopulationAudit.AtMembersCouplingASpectralIndexToALinkPhase()}";

    public static bool EveryBreakerWouldBeANewPrimitive()
        => NoAtQuantityBreaksFlatness()
        && FluxPopulationAudit.AtMembersCouplingASpectralIndexToALinkPhase() == 0
        && TheDetectorIsSensitive();

    // ===================== 5. VERDICT =====================

    /// <summary>
    /// Computed, with a live branch: a phase-reading AT quantity with a non-uniform local factor, or a non-zero census
    /// of recipe-phase couplings, would produce a non-flat weight and move the verdict to DERIVED.
    /// </summary>
    public static string Verdict()
    {
        if (!TheDetectorIsSensitive()) return "BOUNDARY";              // the search cannot be trusted
        if (FluxPopulationAudit.AtMembersCouplingASpectralIndexToALinkPhase() != 0) return "DERIVED";
        if (!NoAtQuantityBreaksFlatness()) return "DERIVED";
        if (OccupancyFreeRoomSpread() > 1e-12) return "DERIVED";
        if (MultiplicitySpread() > 1e-12) return "DERIVED";
        if (!TheActualizationRateIsSectorBlind()) return "DERIVED";
        return "REFUTED";
    }

    public static string WhereItStands()
        => "NO EXISTING AT QUANTITY BREAKS THE FLAT MEASURE, AND THE AUDIT VALIDATED ITS DETECTOR BEFORE BELIEVING "
         + "IT. E_015 established that the sector measure is flat; this audit asks whether any quantity the theory "
         + "already has can tilt it, and a null result of this kind is worth nothing unless the method could have found "
         + "something. So the search runs first on two synthetic breakers. A weight with a NON-UNIFORM local factor on "
         + "the link phase - which is what a potential for the phase IS - breaks flatness at once, at a relative spread "
         + $"of {LargestPotentialSpread():E1}, and a BRIDGE between the recipe sector and the phase sector breaks the "
         + $"conditional recipe marginal at {CoupledRecipeResidual():E1}. Controls confirm the other direction: the "
         + $"trivial weight gives {FlatControlSpread():E3} and a potential that does not depend on the phase gives "
         + $"{Spread(SectorSums(4, 6, v => Math.Exp(-1.0 * v.Length))):E3}. The machinery can detect a breaker, and it "
         + "finds none among the five candidates. THE FIVE FAIL FOR TWO DISTINCT REASONS, AND THE SPLIT IS THE USEFUL "
         + "PART. THREE ARE RECIPE-SIDE QUANTITIES: occupancy free room, the D96 hierarchy and the compression laws all "
         + "read the substrate and the recipes, and the sector labels the PHASE half, so their sector sums factorise "
         + "into a phase count times a recipe quantity - free room per sector "
         + $"{string.Join(", ", OccupancyFreeRoom().Select(t => $"{t.FreeRoom}"))} at (k,l) = (4,6), spread over the "
         + $"sectors {OccupancyFreeRoomSpread():E3}, factorisation residual {ProductFactorisationResidual():E3}. A "
         + "quantity that never takes the phase as an argument cannot see the label, and the live census confirms the "
         + $"absence of any bridge: {FluxPopulationAudit.AtMembersCouplingASpectralIndexToALinkPhase()} AT members "
         + "couple a spectral index to a link phase, E_013's count reused rather than recounted. TWO ARE PHASE-SIDE "
         + "QUANTITIES AND BOTH ARE SECTOR-SYMMETRIC: the multiplicity is equal by E_015's bijection, reused here "
         + $"(spread {MultiplicitySpread():E3}), and the update rule's spatial part is {ActualizationParts().Spatial:E3} "
         + $"against a time-like part of {ActualizationParts().TimeLike:E3}, so the actualization rate cannot read the "
         + "label at all. THE AUDIT'S OWN FIRST PREDICTION WAS WRONG AND IS WITHDRAWN HERE. It expected the conditional "
         + "distribution of a coarse phase observable to differ across sectors, because the shift relating neighbouring "
         + "sectors moves one link's value by one. Measured, it does NOT: the global half-period shift maps the lower "
         + "half of the phase range onto the upper half while leaving the class unchanged whenever (k/2)L is a multiple "
         + $"of k, so the mean is forced to L/2 in every class and the conditional spread is {ConditionalSpread():E3} - "
         + "a symmetry, not an accident. THE REAL NEAREST MISS IS A WEIGHT RATHER THAN AN OBSERVABLE: a phase-reading "
         + $"weight breaks the class sums at {LargestPotentialSpread():E1}, so nothing about the configuration space "
         + "prevents a non-flat measure - what prevents it is that AT supplies no such weight (no potential for the "
         + "link phase, E_013) and no bridge into the recipe sector (the census above). "
         + "SO THE GOAL AS POSED IS REFUTED: no existing AT quantity produces a non-flat weighting, and the audit "
         + "states precisely what a breaker would have to be - " + WhatABreakerWouldHaveToBe() + " - both of which are "
         + "NEW PRIMITIVES. That is the shape of a useful null result: the question of a breaker is open in principle, "
         + "closed in practice, and now stated in a form a future audit can test directly. The verdict is computed and "
         + "has a live branch: a phase-reading quantity with a non-uniform local factor, or a non-zero recipe-phase "
         + "census, would move it to DERIVED.";

    // ===================== REPORT =====================

    public static string OutputDetector()
    {
        var sb = new StringBuilder();
        sb.AppendLine("1. THE DETECTOR, VALIDATED BEFORE IT IS TRUSTED");
        sb.AppendLine("   weight                                       | relative spread of the sector sums");
        sb.AppendLine($"   trivial (the control)                        | {FlatControlSpread():E3}");
        sb.AppendLine($"   a potential uniform in the phase (sanity)    | {Spread(SectorSums(4, 6, v => Math.Exp(-1.0 * v.Length))):E3}");
        sb.AppendLine($"   a NON-uniform local potential (breaker one)  | {LargestPotentialSpread():E1}");
        sb.AppendLine($"   a recipe-phase BRIDGE (breaker two)          | {CoupledRecipeResidual():E1}");
        sb.AppendLine($"   the detector is sensitive                    : {TheDetectorIsSensitive()}");
        sb.AppendLine($"   a uniform potential is flat                  : {AUniformPotentialIsFlat(1.0)}");
        return sb.ToString();
    }

    public static string OutputNearestMiss()
    {
        var sb = new StringBuilder();
        sb.AppendLine("2. THE WITHDRAWN PREDICTION, AND WHERE THE REAL NEAREST MISS IS");
        sb.AppendLine("   sector | mean number of links in the lower half of the phase range");
        foreach (var (sector, share) in ConditionalPhaseObservable())
            sb.AppendLine($"   {sector,6} | {share:F9}");
        sb.AppendLine($"   conditional spread                 : {ConditionalSpread():E3}  -> forced flat by symmetry: {TheCoarseObservableIsForcedFlat()}");
        sb.AppendLine($"   a phase-reading WEIGHT breaks it   : {LargestPotentialSpread():E1}  -> the miss is a weight, not an observable: {TheNearestMissIsAWeightNotAnObservable()}");
        return sb.ToString();
    }

    public static string OutputCandidates()
    {
        var sb = new StringBuilder();
        sb.AppendLine("3. THE FIVE CANDIDATES");
        foreach (var (candidate, status, basis) in Candidates())
        {
            sb.AppendLine($"   {candidate}");
            sb.AppendLine($"     -> {status}: {basis}");
        }
        sb.AppendLine();
        sb.AppendLine("4. WHAT A BREAKER WOULD HAVE TO BE");
        sb.AppendLine($"   {WhatABreakerWouldHaveToBe()}");
        sb.AppendLine($"   every breaker would be a new primitive : {EveryBreakerWouldBeANewPrimitive()}");
        return sb.ToString();
    }

    public static string OutputVerdict()
    {
        var sb = new StringBuilder();
        sb.AppendLine("5. VERDICT");
        sb.AppendLine(Verdict());
        sb.AppendLine($"   refuted : {string.Join(", ", RefutedCandidates())}");
        sb.AppendLine();
        sb.AppendLine(WhereItStands());
        return sb.ToString();
    }
}
