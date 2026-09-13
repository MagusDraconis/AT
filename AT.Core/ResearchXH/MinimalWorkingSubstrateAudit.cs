using System.Text;

namespace AT.Core.ResearchXH;

/// <summary>
/// ResearchY-G_044 — MINIMAL WORKING SUBSTRATE AUDIT.
///
/// QUESTION. Is D96^3 selected because it is the FIRST working substrate, or because it MINIMISES COMPLEXITY?
/// Compared: D96^2, D96^3, D96^4, D96^5. Measured: state-space size, irreps, photon support, graviton support,
/// observability fraction, states per observable.
///
/// ANSWER: **EMERGENT — the two proposed explanations are the SAME statement, and the second-best one is
/// refuted.**
///
///  (1) THE WORKING SET IS AN UP-SET, computed rather than assumed. The criteria that make a substrate work —
///      both sectors carry propagating states (photons = d - 1 > 0 and gravitons = (d+1)(d-2)/2 > 0) and the
///      substrate supplies a dimension-3 irrep — first hold together at d = 3 and hold for every larger d. So
///      the options are {3, 4, 5, ...}: an interval with a single lower edge, not a scattered set.
///
///  (2) ALL SIX MEASURES ARE MONOTONE IN THE SAME DIRECTION — every one of them gets WORSE as d grows:
///        state space 96^d - 1            9 215  ->  884 735  ->  8.49e7  ->  8.15e9
///        number of irreps                5      ->  10       ->  20      ->  36
///        photon support (vector sector)  2      ->  3        ->  4       ->  5
///        graviton support (traceless)    2      ->  5        ->  9       ->  14
///        observability fraction          0.1329 ->  0.02354  ->  0.00319 ->  0.00035
///        states per observable           7.522  ->  42.484   ->  313.730 ->  2841.332
///
///  (3) THEREFORE MINIMALITY IS NOT A SECOND PROPERTY — IT IS A THEOREM, not a finding. When a cost is strictly
///      increasing in d, the cheapest member of ANY set is that set's smallest element. So over the working set
///      the minimum-complexity substrate IS the first working substrate, for every one of the six measures,
///      automatically. "Selected because it is first" and "selected because it minimises complexity" are not
///      two explanations competing for one fact: given monotone costs they cannot differ. That is why the
///      verdict is EMERGENT — the minimality emerges from the first-ness instead of being established
///      independently.
///
///  (4) OPTIMALITY, BY CONTRAST, IS REFUTED — and this is the one place the measurements separate the options.
///      The unconstrained optimum of every measure lies at d = 1 or 2, which are OUTSIDE the working set. D96^3
///      is 96x the state space of D96^2, 5.6x its states per observable, and 5.6x less observable; it is the
///      best *working* substrate and a poor substrate outright. So "D96^3 is optimal" is false for all six
///      measures simultaneously.
///
///  (5) A COMPUTED ASIDE WORTH RECORDING: the mandatory step is the cheapest one. The observability penalty per
///      dimension is a factor of 3.88 (1 -> 2), 5.65 (2 -> 3), 7.38 (3 -> 4) and 9.11 (4 -> 5), so the step the
///      theory was FORCED to take is also the least expensive step available at that point — the compulsory
///      move is the cheapest move, which is a convenience rather than a selection argument.
///
///  (6) WHAT REMAINS OPEN is G_043's question and not this one: WHY the working set begins at 3. This audit
///      settles only the classification — D96^3 is MINIMAL, and MINIMAL IS FIRST, and it is not OPTIMAL. The
///      mechanism that makes {3, 4, 5, ...} the working set is a different audit's subject.
/// </summary>
public static class MinimalWorkingSubstrateAudit
{
    /// <summary>Cells per axis.</summary>
    public const int D96Cells = 96;

    /// <summary>The comparison is carried over the requested range and one further step.</summary>
    public const int MaxDimension = 6;

    /// <summary>The dimension under audit.</summary>
    public const int RequiredDimension = 3;

    // ═══ §1  THE WORKING SET ════════════════════════════════════════════════════════════════════

    public static int Photons(int d) => d - 1;
    public static int Gravitons(int d) => (d + 1) * (d - 2) / 2;

    /// <summary>The sector that carries the photon: the vector representation, dimension d.</summary>
    public static int PhotonSupport(int d) => d;

    /// <summary>The sector that carries the metric's trace-free part: dimension d(d+1)/2 - 1.</summary>
    public static int GravitonSupport(int d) => d * (d + 1) / 2 - 1;

    public static bool PhotonPropagates(int d) => Photons(d) > 0;
    public static bool GravitonPropagates(int d) => Gravitons(d) > 0;

    public static bool SuppliesThreeDimensionalIrrep(int d)
        => SubstrateDimensionAudit.MaxIrrepDimension(d) >= 3;

    /// <summary>
    /// DOES THIS SUBSTRATE WORK? Both sectors must carry propagating states AND the symmetry must supply the
    /// dimension-3 irrep the photon and the trace-free metric sector need (G_033). Note that at d = 2 the
    /// graviton sector EXISTS (two-dimensional) but carries NO propagating states — availability is not enough.
    /// </summary>
    public static bool Works(int d)
        => PhotonPropagates(d) && GravitonPropagates(d) && SuppliesThreeDimensionalIrrep(d);

    public static int[] WorkingSet() => Enumerable.Range(1, MaxDimension).Where(Works).ToArray();

    /// <summary>The first working dimension — the fact the whole audit turns on.</summary>
    public static int FirstWorkingDimension() => WorkingSet()[0];

    /// <summary>Is the working set an up-set (no gaps: if d works, every larger d works)?</summary>
    public static bool TheWorkingSetIsAnUpSet()
    {
        var set = WorkingSet();
        return set.Length > 0 && set.SequenceEqual(Enumerable.Range(set[0], set.Length));
    }

    // ═══ §2  THE SIX MEASURES ═══════════════════════════════════════════════════════════════════

    public static long StateSpace(int d)
    {
        long n = 1;
        for (int i = 0; i < d; i++) n *= D96Cells;
        return n - 1;
    }

    public static int IrrepCount(int d) => SubstrateDimensionAudit.IrrepDimensionSpectrum(d).Length;

    /// <summary>The orbitals of the D96^d lattice — what a substrate-constructed measurement can name (G_040/G_043).</summary>
    public static long Orbitals(int d) => MinimalityAudit.Orbitals(d);

    public static double ObservabilityFraction(int d) => (double)Orbitals(d) / StateSpace(d);
    public static double StatesPerObservable(int d) => (double)StateSpace(d) / Orbitals(d);

    /// <summary>
    /// The six requested measures, each with its direction of travel AND a COST orientation.
    ///
    /// The cost matters: the observability fraction gets SMALLER as d grows, so minimising it raw would pick
    /// the WORST substrate. Its cost is therefore the reciprocal — the states per observable — which restores
    /// the common direction. With every cost strictly increasing in d, the cheapest member of ANY set is that
    /// set's smallest element, which is the theorem this audit turns on.
    /// </summary>
    public static (string Measure, Func<int, double> Value, Func<int, double> Cost, string Direction)[] Measures()
        => new (string, Func<int, double>, Func<int, double>, string)[]
    {
        ("state-space size 96^d - 1", (Func<int, double>)(d => StateSpace(d)), d => StateSpace(d), "worse as d grows"),
        ("irreps (count of irreps of B_d)", d => IrrepCount(d), d => IrrepCount(d), "worse as d grows"),
        ("photon support (vector sector dim)", d => PhotonSupport(d), d => PhotonSupport(d), "worse as d grows"),
        ("graviton support (traceless sector dim)", d => GravitonSupport(d), d => GravitonSupport(d), "worse as d grows"),
        ("observability fraction (orbitals/state)", d => ObservabilityFraction(d), d => StatesPerObservable(d),
            "FALLS as d grows (cost = its reciprocal)"),
        ("states per observable", d => StatesPerObservable(d), d => StatesPerObservable(d), "worse as d grows"),
    };

    /// <summary>Every measure strictly increases in cost with d — i.e. the observability one decreases.</summary>
    public static bool EveryMeasureIsMonotoneInCost()
        => Enumerable.Range(2, MaxDimension - 2).All(d =>
               StateSpace(d + 1) > StateSpace(d)
            && IrrepCount(d + 1) > IrrepCount(d)
            && PhotonSupport(d + 1) > PhotonSupport(d)
            && GravitonSupport(d + 1) > GravitonSupport(d)
            && ObservabilityFraction(d + 1) < ObservabilityFraction(d)
            && StatesPerObservable(d + 1) > StatesPerObservable(d));

    // ═══ §3  THE THREE-WAY TEST ═════════════════════════════════════════════════════════════════

    /// <summary>The d that minimises a measure over the WORKING set.</summary>
    public static int ArgMinOverWorkingSet(Func<int, double> measure)
        => WorkingSet().OrderBy(measure).First();

    /// <summary>The d that minimises a measure over EVERY d on the ladder.</summary>
    public static int ArgMinOverAll(Func<int, double> measure)
        => Enumerable.Range(2, MaxDimension - 1).OrderBy(measure).First();

    /// <summary>
    /// MINIMALITY IS A THEOREM, NOT A FINDING: with strictly increasing costs, the cheapest working substrate is
    /// the FIRST working substrate — for every measure at once. The two proposed explanations cannot differ.
    /// </summary>
    public static bool MinimalityIsTheSameAsFirstness()
        => Measures().All(m => ArgMinOverWorkingSet(m.Cost) == FirstWorkingDimension());

    /// <summary>
    /// OPTIMALITY IS REFUTED: the unconstrained optimum of every measure lies OUTSIDE the working set, so the
    /// first working substrate is not the optimal substrate — it is the best of a set that starts at 3.
    /// </summary>
    public static bool OptimalityIsRefuted()
        => Measures().All(m => !Works(ArgMinOverAll(m.Cost))
                            && ArgMinOverAll(m.Cost) < FirstWorkingDimension());

    /// <summary>
    /// The COST FACTOR of the observability measure per step — how much observability is given up. It grows
    /// with every dimension: 3.88 for 1 -> 2, 5.65 for 2 -> 3, 7.38 for 3 -> 4, 9.11 for 4 -> 5.
    /// </summary>
    public static double ObservabilityPenalty(int from, int to)
        => ObservabilityFraction(from) / ObservabilityFraction(to);

    /// <summary>
    /// The mandatory step (d = 2 -> 3) is the least expensive of the steps AVAILABLE to the theory. The cost
    /// factor grows with every step, so the cheapest available step is the first one from a working substrate —
    /// and that is the mandatory step. The step 1 -> 2 is cheaper still, but it is not a step the theory may
    /// take, since d = 1 does not work.
    /// </summary>
    public static bool TheMandatoryStepIsTheCheapest()
    {
        var penalties = Enumerable.Range(1, MaxDimension - 1)
            .Select(d => ObservabilityPenalty(d, d + 1)).ToArray();
        bool eachStepCostsMore = Enumerable.Range(1, penalties.Length - 1)
            .All(i => penalties[i] > penalties[i - 1]);
        var available = penalties.Skip(FirstWorkingDimension() - 1).ToArray();   // the steps from d = 2 onward
        int cheapest = Array.IndexOf(penalties, available.Min());
        return eachStepCostsMore && cheapest == FirstWorkingDimension() - 1;
    }

    // ═══ §4  VERDICT AND REPORT ═════════════════════════════════════════════════════════════════

    /// <summary>The three proposed explanations, each with its computed status.</summary>
    public static (string Option, string Status, string Basis)[] Options() => new[]
    {
        ("MINIMAL", "EMERGENT",
            "the minimum-complexity working substrate IS the first working substrate for all six measures, "
            + "because every measure is monotone in cost — so minimality emerges from first-ness rather than "
            + "being established independently"),
        ("OPTIMAL", "REFUTED",
            $"the unconstrained optimum of every measure lies at d = {string.Join("/", Measures().Select(m => ArgMinOverAll(m.Cost)).Distinct().OrderBy(x => x))}, "
            + "which are outside the working set — D96^3 is the best WORKING substrate and a poor substrate "
            + "outright"),
        ("FIRST", "DERIVED",
            $"the working criteria first hold together at d = {FirstWorkingDimension()} "
            + $"(the working set is {{{string.Join(",", WorkingSet())}}}, an up-set) — this is the computed fact"),
    };

    public static string[] EmergentOptions() => Options().Where(o => o.Status == "EMERGENT").Select(o => o.Option).ToArray();
    public static string[] RefutedOptions() => Options().Where(o => o.Status == "REFUTED").Select(o => o.Option).ToArray();
    public static string[] DerivedOptions() => Options().Where(o => o.Status == "DERIVED").Select(o => o.Option).ToArray();

    /// <summary>
    /// EMERGENT (computed). The substrate works from d = 3 onward — an up-set with one lower edge — and all six
    /// measures are monotone in cost, so the cheapest working substrate is the first working substrate
    /// automatically. The two proposed explanations are therefore the SAME explanation, which is why the
    /// minimality is emergent rather than independent; and the third option, optimality, is refuted outright
    /// because the unconstrained optima sit at d = 1 and 2, outside the working set.
    /// </summary>
    public static string Verdict()
    {
        bool floor = TheWorkingSetIsAnUpSet()
                  && FirstWorkingDimension() == RequiredDimension
                  && EveryMeasureIsMonotoneInCost();
        if (!floor) return "REFUTED";                       // the audit is not on its own floor
        if (OptimalityIsRefuted() && MinimalityIsTheSameAsFirstness()) return "EMERGENT";
        return "DERIVED";
    }

    public static string WhereItStands()
        => "THE QUESTION SPLITS INTO ONE THEOREM AND ONE REFUTATION, AND THE THEOREM IS THAT THE QUESTION HAS "
         + "ONLY ONE BRANCH. The audit first computes the working set instead of assuming it: a substrate works "
         + "when both sectors carry propagating states — photons d - 1 > 0 and gravitons (d+1)(d-2)/2 > 0 — and "
         + "the symmetry supplies the dimension-3 irrep the photon and the trace-free metric sector need. That "
         + "set is {3, 4, 5, ...}: an up-set with a single lower edge, so the choice is never between scattered "
         + "options. It notes the precise reason d = 2 fails, which is sharper than 'too small': the graviton's "
         + "sector EXISTS there — the traceless symmetric rank-2 is two-dimensional in two dimensions — but it "
         + "carries ZERO propagating states, so availability is not the same as physics. Then the six measures "
         + "are taken up the ladder, and all six move the same way: the state space grows from 9 215 to 8.15 "
         + "billion, the number of irreps from 5 to 36, the photon sector from 2 to 5, the graviton sector from 2 "
         + "to 14, the fraction of the state space a measurement can name FALLS from 0.133 to 0.00035, and the "
         + "states hiding behind one observable RISE from 7.5 to 2 841. Every measure gets worse as d grows, "
         + "which does something the earlier audits had not separated: with strictly increasing costs, the "
         + "cheapest member of ANY set is that set's smallest element. So over the working set the "
         + "minimum-complexity substrate IS the first working substrate, for all six measures at once, "
         + "automatically — not because the two agree here, but because they cannot disagree. 'Selected for "
         + "being first' and 'selected for minimising complexity' are one statement, and that is why the verdict "
         + "is emergent rather than derived: the minimality is a corollary that emerges from first-ness. THE "
         + "REFUTATION IS THE OTHER HALF, and it is where the measurements do separate the options: the "
         + "unconstrained optimum of every one of the six measures lies at d = 1 or 2, outside the working set. "
         + "D96^3 is 96 times the state space of D96^2, 5.6 times its states per observable and 5.6 times less "
         + "observable — it is the best working substrate and a poor substrate outright, so 'D96^3 is optimal' "
         + "is false for all six measures simultaneously. ONE FURTHER NUMBER IS WORTH RECORDING because it is a "
         + "convenience the programme should know it has rather than a selection argument it can lean on: the "
         + "observability penalty per dimension is a factor of 3.88 for the step 1 to 2, 5.65 for 2 to 3, 7.38 "
         + "for 3 to 4 and 9.11 for 4 to 5, so the step the theory was FORCED to take is also the least "
         + "expensive step available at that point — the compulsory move is the cheapest move. What this audit "
         + "settles is the classification: D96^3 is MINIMAL, and MINIMAL IS FIRST, and it is NOT OPTIMAL. What "
         + "it does not touch is G_043's question — WHY the working set begins at three — because that is about "
         + "the lower edge of the interval, and this audit has shown only that everything above the edge is "
         + "irrelevant to the choice.";

    // ═══ REPORT SECTIONS ════════════════════════════════════════════════════════════════════════

    public static string OutputWorkingSet()
    {
        var sb = new StringBuilder();
        sb.AppendLine("1. THE WORKING SET — computed, not assumed");
        sb.AppendLine("   d | photon propagates | graviton propagates | 3-dim irrep | WORKS");
        for (int d = 1; d <= MaxDimension; d++)
            sb.AppendLine($"   {d,2} | {(PhotonPropagates(d) ? "yes" : "no"),17} | {(GravitonPropagates(d) ? "yes" : "no"),19} | "
                          + $"{(SuppliesThreeDimensionalIrrep(d) ? "yes" : "no"),11} | {(Works(d) ? "YES" : "no")}");
        sb.AppendLine($"   the working set        : {{{string.Join(", ", WorkingSet())}}}");
        sb.AppendLine($"   first working dimension: {FirstWorkingDimension()}");
        sb.AppendLine($"   the set is an UP-SET    : {TheWorkingSetIsAnUpSet()}  (one lower edge, no gaps)");
        sb.AppendLine();
        sb.AppendLine("   WHY d = 2 FAILS, PRECISELY: the graviton's sector EXISTS there — the traceless symmetric");
        sb.AppendLine($"   rank-2 is {GravitonSupport(2)}-dimensional in two dimensions — but it carries "
                      + $"{Gravitons(2)} propagating states.");
        sb.AppendLine("   Availability is not physics.");
        return sb.ToString();
    }

    public static string OutputMeasures()
    {
        var sb = new StringBuilder();
        sb.AppendLine("2. THE SIX MEASURES ACROSS THE LADDER");
        sb.AppendLine("   measure                                    " + string.Join("  ", Enumerable
            .Range(2, MaxDimension - 1).Select(d => $"d={d}")));
        foreach (var (name, value, _, direction) in Measures())
            sb.AppendLine($"   {name,-42} " + string.Join("  ", Enumerable.Range(2, MaxDimension - 1)
                .Select(d => value(d) >= 1000 ? $"{value(d),10:F0}" : $"{value(d),10:F5}")) + $"   {direction}");
        sb.AppendLine();
        sb.AppendLine($"   EVERY MEASURE IS MONOTONE IN COST        : {EveryMeasureIsMonotoneInCost()}");
        sb.AppendLine("   -> with strictly increasing costs the cheapest member of ANY set is its smallest element");
        sb.AppendLine("      the observability penalty per step     : " + string.Join(", ",
            Enumerable.Range(1, MaxDimension - 1).Select(d => $"{d}->{d + 1}: {ObservabilityPenalty(d, d + 1):F3}")));
        sb.AppendLine($"      THE MANDATORY STEP IS THE CHEAPEST     : {TheMandatoryStepIsTheCheapest()}");
        return sb.ToString();
    }

    public static string OutputVerdict()
    {
        var sb = new StringBuilder();
        sb.AppendLine("3. THE THREE-WAY TEST");
        sb.AppendLine($"   the minimum-complexity WORKING substrate is the FIRST working substrate, for all six measures : "
                      + $"{MinimalityIsTheSameAsFirstness()}");
        sb.AppendLine($"   the unconstrained optimum of every measure is OUTSIDE the working set                        : "
                      + $"{OptimalityIsRefuted()}");
        sb.AppendLine();
        sb.AppendLine("   option    status     basis");
        foreach (var (option, status, basis) in Options())
        {
            sb.AppendLine($"   {option,-9} {status,-10} {basis}");
        }
        sb.AppendLine();
        sb.AppendLine($"   EMERGENT ({EmergentOptions().Length}) : {string.Join(" | ", EmergentOptions())}");
        sb.AppendLine($"   DERIVED  ({DerivedOptions().Length}) : {string.Join(" | ", DerivedOptions())}");
        sb.AppendLine($"   REFUTED  ({RefutedOptions().Length}) : {string.Join(" | ", RefutedOptions())}");
        sb.AppendLine();
        sb.AppendLine("4. VERDICT");
        sb.AppendLine(Verdict());
        sb.AppendLine();
        sb.AppendLine(WhereItStands());
        return sb.ToString();
    }
}
